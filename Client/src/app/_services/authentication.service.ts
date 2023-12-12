import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private userSubject: BehaviorSubject<User | null>;
    public user: Observable<User | null>;

    constructor(
        private router: Router,
        private http: HttpClient,
        private apiService:ApiService
    ) {
        this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
        this.user = this.userSubject.asObservable();
    }

    public get userValue() {
        return this.userSubject.value;
    }

    login(username: string, password: string) {
        debugger;
        //return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, { username, password })
        return this.http.post<any>(`${environment.apiUrl}/authentication/login`, {email: username,password: password }, {observe:'response',withCredentials:true})
        //.subscribe(user=>{
            .pipe(map((httpResponse) => {
                //store user details and jwt token in local storage to keep user logged in between page refreshes
                var response = httpResponse.body;
                if(response.isSuccess && response.data)
                {
                    localStorage.setItem('user', JSON.stringify(response.data.user));
                    localStorage.setItem('token', JSON.stringify(response.data.token));
                    this.apiService.setToken(response.data.token);
                    this.userSubject.next(response.data.user);
                }
                return response;
            }));
    }

    login2Factor(email: string, code: string) {
        //return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, { username, password })
        return this.http.post<any>(`${environment.apiUrl}/authentication/login-2fa?code=${code}`, {}, {observe:'response',withCredentials:true})
        .pipe(map(httpResponse => {
            var response = httpResponse.body;
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            if(response.isSuccess && response.data)
            {
                localStorage.setItem('user', JSON.stringify(response.data.user));
                localStorage.setItem('token', JSON.stringify(response.data.token));
                this.apiService.setToken(response.data.token);
                this.userSubject.next(response.data.user);
            }
            return response;
        }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('user');
        this.userSubject.next(null);
        this.router.navigate(['/login']);
    }
}
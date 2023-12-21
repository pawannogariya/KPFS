import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { Role, User, UserWithToken } from '@app/_models';
import { ApiService } from './api.service';
import { IResponse } from './dto/response.dto';

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

    loginOffline()
    {
        var  response={
            isSuccess: true,
            message: null,
            data: {
                token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYmFkaTRuZXRAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiYmFkaTRuZXRAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiI2NjhmOTZkZi04ZTAzLTExZWUtYjUwNy1lODZhNjRiNDdhYWUiLCJqdGkiOiJmMDhlZGRkMy0zNmNhLTQ5OTMtOTQ3Ny0xOTY3MTU2NDQ4ODIiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTcwMjgxMzcyMCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMjYifQ.uhlemKiFkrN_UONkHi5aJrCoZ1SXWxNuVIpcdPzys3k",
                expiration: "2023-12-17T11:48:40Z",
                user: {
                    "id":1,
                    "username": "badi4net@gmail.com",
                    "firstName": "Super",
                    "lastName": "Admin",
                    "role": Role.Admin,
                }
            }
        }

        localStorage.setItem('user', JSON.stringify(response.data.user));
        localStorage.setItem('token', JSON.stringify(response.data.token));
        this.apiService.setToken(response.data.token);
        this.userSubject.next(response.data.user);
        return response;
    }

    login(username: string, password: string) {
        //return this.http.post<IResponse<UserWithToken>>(`${environment.apiUrl}/users/authenticate`, { username, password })
        return this.http.post<IResponse<UserWithToken>>(`${environment.apiUrl}/authentication/login`, {email: username,password: password }, {observe:'response',withCredentials:true})
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
        //return this.http.post<IResponse<UserWithToken>>(`${environment.apiUrl}/users/authenticate`, { username, password })
        return this.http.post<IResponse<UserWithToken>>(`${environment.apiUrl}/authentication/login-2fa?code=${code}`, {}, {observe:'response',withCredentials:true})
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
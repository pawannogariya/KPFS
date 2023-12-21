import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { IAddUserDto, IRegisterUserDto } from './dto/registration.dto';
import { IResponse } from './dto/response.dto';

@Injectable({ providedIn: 'root' })
export class RegistrationService {
    constructor(private http: HttpClient) {
    }

    registerUser(dto:IRegisterUserDto) {
        return this.http.post<IResponse<boolean>>(`${environment.apiUrl}/authentication/register`, dto)
        //.subscribe(user=>{
            .pipe(map((response) => {
                return response;
            }));
    }
    
    confirmEmail(email:string,token:string) {
        return this.http.get<IResponse<boolean>>(`${environment.apiUrl}/authentication/confirm-email?token=${token}&email=${email}`)
        //.subscribe(user=>{
            .pipe(map((response) => {
                return response;
            }));
    }

    addUser(dto:IAddUserDto) {
        return this.http.post<IResponse<boolean>>(`${environment.apiUrl}/admin/user/add`, dto)
        //.subscribe(user=>{
            .pipe(map((response) => {
                return response;
            }));
    }
}
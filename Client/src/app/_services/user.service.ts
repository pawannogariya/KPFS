import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { ApiService } from './api.service';
import { IResponse } from './dto/response.dto';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private readonly apiService: ApiService,
        private http: HttpClient) { }

    getAll(): Promise<IResponse<User[]>> {
        return this.apiService.get<IResponse<User[]>>("/users");
    }

    getById(id: number) {
        return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
    }
}
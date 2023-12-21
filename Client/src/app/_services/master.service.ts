import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { IResponse } from './dto/response.dto';
import { ApiService } from './api.service';
import { IUsers, User } from '@app/_models/user';

@Injectable({ providedIn: 'root' })
export class MasterService {
    constructor(private readonly apiService: ApiService,
        private http: HttpClient) { }

    // getAllUsers() {
    //     return this.http.get<User[]>(`${environment.apiUrl}/users`);
    // }

    public async getAllUsers(): Promise<IResponse<IUsers[]>> {
        return await this.apiService.get<IResponse<IUsers[]>>("/admin/user/list");
    }

    getById(id: number) {
        return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
    }
}
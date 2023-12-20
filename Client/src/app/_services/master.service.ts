import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { IResponse } from './dto/response.dto';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class MasterService {
    constructor(private readonly apiService: ApiService,
        private http: HttpClient) { }

    // getAllUsers() {
    //     return this.http.get<User[]>(`${environment.apiUrl}/users`);
    // }

    public async getAllUsers(): Promise<IResponse<any>> {
        return await this.apiService.get<IResponse<any>>("/admin/user/list");
    }

    getById(id: number) {
        return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
    }
}
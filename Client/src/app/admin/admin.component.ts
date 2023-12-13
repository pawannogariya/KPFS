﻿import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService } from '@app/_services';

@Component({
    selector: 'app-admin',
    templateUrl: 'admin.component.html',
    styleUrls: ['admin.component.scss']
  })
export class AdminComponent implements OnInit {
    loading = false;
    users: User[] = [];

    constructor(private userService: UserService) { }

    ngOnInit() {
      
    }
}
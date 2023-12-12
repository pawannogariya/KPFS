﻿import { Component, OnInit } from '@angular/core';
import { User } from '@app/_models';
import { UserService } from '@app/_services';

@Component({ templateUrl: 'multiple-entry.component.html' })
export class MultipleEntryComponent implements OnInit {
    loading = false;
    users: User[] = [];

    constructor(private userService: UserService) { }

    ngOnInit() {
     
    }
}
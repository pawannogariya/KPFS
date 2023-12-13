import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService } from '@app/_services';

@Component({
    selector: 'app-master',
    templateUrl: 'master.component.html',
    styleUrls: ['master.component.scss']
  })
export class MasterComponent implements OnInit {
    loading = false;
    users: User[] = [];

    constructor(private userService: UserService) { }

    ngOnInit() {
    }

    addUserDialog() {
        
    }
}
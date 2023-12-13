import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService } from '@app/_services';

@Component({
    selector: 'app-single-entry',
    templateUrl: 'single-entry.component.html',
    styleUrls: ['single-entry.component.scss']
  })
export class SingleEntryComponent implements OnInit {
    loading = false;
    users: User[] = [];

    constructor(private userService: UserService) { }

    ngOnInit() {
        //this.loading = true;
        // this.userService.getAll().pipe(first()).subscribe(users => {
        //     this.loading = false;
        //     this.users = users;
        // });
    }
}
import { Component, OnInit } from '@angular/core';
import { User } from '@app/_models';
import { UserService } from '@app/_services';

@Component({
    selector: 'app-multiple-entry',
    templateUrl: 'multiple-entry.component.html',
    styleUrls: ['multiple-entry.component.scss']
  })
export class MultipleEntryComponent implements OnInit {
    loading = false;
    users: User[] = [];

    constructor(private userService: UserService) { }

    ngOnInit() {
     
    }
}
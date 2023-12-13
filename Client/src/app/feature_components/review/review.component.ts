import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService } from '@app/_services';

@Component({
    selector: 'app-review',
    templateUrl: 'review.component.html',
    styleUrls: ['review.component.scss']
  })
export class ReviewComponent implements OnInit {
    loading = false;
    users: User[] = [];

    constructor(private userService: UserService) { }

    ngOnInit() {
       
    }
}
import { Component, HostListener, OnInit } from '@angular/core';

import { AuthenticationService } from './_services';
import { User, Role } from './_models';

// @Component({ selector: 'app-root', templateUrl: 'app.component.html' })
@Component({
    selector: 'app-root',
    templateUrl: 'app.component.html',
    styleUrls: ['app.component.scss']
})
export class AppComponent implements OnInit {
    user?: User | null;

    constructor(public authenticationService: AuthenticationService) {
        this.authenticationService.user.subscribe(x => this.user = x);
    }

    get isAdmin() {
        return this.user?.role === Role.Admin;
    }

    logout() {
        this.authenticationService.logout();
    }

    ngOnInit(): void {
        this.getScreenSize();
    }

    @HostListener('window:resize', ['$event'])
    getScreenSize(event?) {
        setTimeout(() => {
            const screenHeight = window.innerHeight;
            const header = document.getElementById('main-header')?.clientHeight;
            const footer = document.getElementById('footer')?.clientHeight;
            const finalHeight = screenHeight - (header + footer);
            const mainContainer = document.getElementById('main-container');
            if (mainContainer && !isNaN(finalHeight)) {                
                mainContainer.style.height = finalHeight + "px";
            }
        }, 500);
    }
}
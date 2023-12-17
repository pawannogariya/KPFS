import { Component, HostListener, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '@app/_services';

@Component({
    selector: 'app-login',
    templateUrl: 'login.component.html',
    styleUrls: ['login.component.scss']
  })
export class LoginComponent implements OnInit {
    loginForm!: FormGroup;
    codeForm!: FormGroup;
    loading = false;
    submitted = false;
    isLogin=false;
    error = '';
    passwordField = true;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.userValue) {
            this.router.navigate(['/home']);
        }
    }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.email]],
            password: ['', Validators.required]
        });
        this.codeForm = this.formBuilder.group({
            username: ['', Validators.required],
            code: ['', Validators.required]
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.loginForm.controls; }

    // convenience getter for easy access to form fields
    get c() { return this.codeForm.controls; }

    onSubmit() {
        // stop here if form is invalid
        if (this.loginForm.valid) {
            var response:any={};// this.authenticationService.loginOffline();
            response.isSuccess=false;
            debugger;
            if(response.isSuccess && response.data)
            {
                localStorage.setItem('user', JSON.stringify(response.data.user));
                localStorage.setItem('token', JSON.stringify(response.data.token));
                // get return url from query parameters or default to home page
                const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';
                this.router.navigateByUrl(returnUrl);
            }
            else
            {
                this.submitted = true;
                this.loading = true;
                this.authenticationService.login(this.f.username.value, this.f.password.value)
                //.pipe(first())
                .subscribe({
                    next: (response) => {
                        debugger;
                        this.submitted = false;
                        this.loading = false;
                        if(response.isSuccess){
                            this.isLogin=true;
                            this.c.username.setValue(this.f.username.value);
                            if(response.data && response.data.user && response.data.token)
                            {
                                localStorage.setItem('user', JSON.stringify(response.data.user));
                                localStorage.setItem('token', JSON.stringify(response.data.token));
                                // get return url from query parameters or default to home page
                                const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';
                                this.router.navigateByUrl(returnUrl);
                            }
                        }
                        else
                        {
                            //Need to display message
                            this.error=response.message;
                        }
                    },
                    error: error => {
                        this.error = error;
                        this.loading = false;
                    }
                });
            }
        }
    }

    onCodeSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.codeForm.invalid) {
            return;
        }

        this.loading = true;
        this.authenticationService.login2Factor(this.c.username.value, this.c.code.value)
            .pipe(first())
            .subscribe({
                next: (response) => {
                    this.isLogin=true;
                        this.submitted = false;
                        this.loading = false;
                    if(response.isSuccess)
                    {
                        localStorage.setItem('user', JSON.stringify(response.data.user));
                        localStorage.setItem('token', JSON.stringify(response.data.token));
                        // get return url from query parameters or default to home page
                        const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';
                        this.router.navigateByUrl(returnUrl);
                    }
                    else
                    {
                        //Need to display message
                        alert(response.message);
                    }
                },
                error: error => {
                    this.error = error;
                    this.loading = false;
                }
            });
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

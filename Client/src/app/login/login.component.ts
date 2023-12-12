import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '@app/_services';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
    loginForm!: FormGroup;
    codeForm!: FormGroup;
    loading = false;
    submitted = false;
    isLogin=false;
    error = '';

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.userValue) {
            this.router.navigate(['/']);
        }
    }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
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
        this.submitted = true;

        // stop here if form is invalid
        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        debugger;
        this.authenticationService.login(this.f.username.value, this.f.password.value)
            .pipe(first())
            .subscribe({
                next: (response) => {
                    debugger;
                    this.isLogin=true;
                    this.submitted = false;
                    this.loading = false;
                    this.c.username.setValue(this.f.username.value);
                    if(response.isSuccess && response.data)
                    {
                        localStorage.setItem('user', JSON.stringify(response.data.user));
                        localStorage.setItem('token', JSON.stringify(response.data.token));
                        // get return url from query parameters or default to home page
                        const returnUrl = this.route.snapshot.queryParams['returnUrl'] || 'home';
                        this.router.navigateByUrl(returnUrl);
                    }
                    else
                    {
                        //Need to display message
                    }
                },
                error: error => {
                    this.error = error;
                    this.loading = false;
                }
            });
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
                        const returnUrl = this.route.snapshot.queryParams['returnUrl'] || 'home';
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
}

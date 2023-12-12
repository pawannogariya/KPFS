import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RegistrationService } from '@app/_services/registration.service';
import { first } from 'rxjs';

@Component({ templateUrl: 'confirmation-email.component.html' })
export class ConfirmationEmailComponent implements OnInit {
  token: string;
  email:string;
  constructor(
      private route: ActivatedRoute,
        private router: Router,
        private registrationService: RegistrationService
    ) { }
  
    ngOnInit() {
      this.token = this.route.snapshot.paramMap.get('token');
      this.email = this.route.snapshot.paramMap.get('email');
      this.confirmEmail();
    }
  
    confirmEmail() {
      debugger
      //this.submitted = true;
      if (this.token && this.email) {
        //this.loading=true;
        this.registrationService.confirmEmail(this.email,this.token)
            .pipe(first())
            .subscribe({
                next: (response) => {
                    debugger;
                    if(response.isSuccess){
                      this.router.navigate(['/login']);
                      alert("Your email has been confirmed.");
                    }
                    else{
                      alert(response.message);
                    }
                },
                error: error => {
                  error=error.message;
                }
            });
      }
    }
  }

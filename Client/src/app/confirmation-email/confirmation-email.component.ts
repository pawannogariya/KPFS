import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertsService } from '@app/_services/alerts.service';
import { RegistrationService } from '@app/_services/registration.service';
import { first } from 'rxjs';

@Component({
  selector: 'app-confirmation-email',
  templateUrl: 'confirmation-email.component.html',
  styleUrls: ['confirmation-email.component.scss']
})
export class ConfirmationEmailComponent implements OnInit {
  token: string;
  email:string;
  constructor(
      private route: ActivatedRoute,
        private router: Router,
        private alertsService:AlertsService,
        private registrationService: RegistrationService
    ) { }
  
    ngOnInit() {
      this.token = this.route.snapshot.paramMap.get('token');
      this.email = this.route.snapshot.paramMap.get('email');
      this.confirmEmail();
    }
  
    confirmEmail() {
      // this.alertsService.showInfo("Congratulations! Your email has been confirmed successfully, please login to continue!");
      // this.router.navigate(['/login']);

      //this.submitted = true;
      if (this.token && this.email) {
        //this.loading=true;
        this.registrationService.confirmEmail(this.email,this.token)
            .pipe(first())
            .subscribe({
                next: (response) => {
                    if(response.isSuccess){
                      this.alertsService.showInfo("Congratulations! Your email has been confirmed successfully, please login to continue!");
                      this.router.navigate(['/login']);
                    }
                    else{
                      this.alertsService.showInfo(response.message);
                    }
                },
                error: error => {
                  this.alertsService.showInfo(error,'Error');
                }
            });
      }
    }
  }

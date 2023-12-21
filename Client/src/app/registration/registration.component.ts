import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertsService } from '@app/_services/alerts.service';
import { CustomvalidationService } from '@app/_services/custom-validation.service';
import { IRegisterUserDto } from '@app/_services/dto/registration.dto';
import { RegistrationService } from '@app/_services/registration.service';
import { first } from 'rxjs';

@Component({
  selector: 'app-registration',
  templateUrl: 'registration.component.html',
  styleUrls: ['registration.component.scss']
})
export class RegistrationComponent implements OnInit {

    registerForm!: FormGroup;
    registerUserDto:IRegisterUserDto=new IRegisterUserDto();
    submitted = false;
    loading = false;
    error = '';

    constructor(
      private fb: FormBuilder,
      private alertsService:AlertsService,
      private customValidator: CustomvalidationService,
      private route: ActivatedRoute,
        private router: Router,
        private registrationService: RegistrationService
    ) { }
  
    ngOnInit() {
      this.registerForm = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.compose([Validators.required, this.customValidator.patternValidator()])],
        confirmPassword: ['', [Validators.required]],
      },
        {
          validator: this.customValidator.MatchPassword('password', 'confirmPassword'),
        }
      );
    }
  
    get registerFormControl() {
      return this.registerForm.controls;
    }
  
    onSubmit() {
      //this.submitted = true;
      if (this.registerForm.valid) {
        //this.loading=true;
        this.registerUserDto = new IRegisterUserDto();
        this.registerUserDto.firstName=this.registerFormControl.firstName.value;
        this.registerUserDto.lastName=this.registerFormControl.lastName.value;
        this.registerUserDto.email=this.registerFormControl.email.value;
        this.registerUserDto.password=this.registerFormControl.password.value;

        this.registrationService.registerUser(this.registerUserDto)
            .pipe(first())
            .subscribe({
                next: (response) => {
                    this.submitted = false;
                    this.loading=false;
                    if(response.isSuccess)
                    this.alertsService.showInfo("Registration completed and confirmation link has been sent.");
                    else
                    this.alertsService.showInfo(response.message);
                },
                error: error => {
                  this.loading=false;
                  error=error.message;
                }
            });
        console.table(this.registerForm.value);
      }
    }
  }

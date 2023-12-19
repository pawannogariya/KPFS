import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomvalidationService } from '@app/_services/custom-validation.service';
import { IAddUserDto } from '@app/_services/dto/registration.dto';
import { RegistrationService } from '@app/_services/registration.service';
import { first } from 'rxjs';

@Component({
  selector: 'app-add-user',
  templateUrl: 'add-user.component.html',
  styleUrls: ['add-user.component.scss']
})
export class AddUserComponent implements OnInit {

  addUserForm!: FormGroup;
  addUserDto:IAddUserDto=new IAddUserDto();
  submitted = false;
  loading = false;
  error = '';
  hidePassword = true;
  
    constructor(
      public dialogRef: MatDialogRef<AddUserComponent>,
      @Inject(MAT_DIALOG_DATA) public data: any,
      private fb: FormBuilder,
      private customValidator: CustomvalidationService,
      private registrationService: RegistrationService
    ) { }
  
    ngOnInit() {
      this.addUserForm = this.fb.group({
        role: ['User', Validators.required],
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
      return this.addUserForm.controls;
    }
  
    onSubmit() {
      //this.submitted = true;
      if (this.addUserForm.valid) {
        //this.loading=true;
        this.addUserDto = new IAddUserDto();
        this.addUserDto.firstName=this.registerFormControl.firstName.value;
        this.addUserDto.lastName=this.registerFormControl.lastName.value;
        this.addUserDto.email=this.registerFormControl.email.value;
        this.addUserDto.password=this.registerFormControl.password.value;
        this.addUserDto.role=this.registerFormControl.role.value;

        // "email": "user@example.com",
        // "password": "string",
        // "firstName": "string",
        // "lastName": "string",
        // "role": "string"

        this.registrationService.addUser(this.addUserDto)
            .pipe(first())
            .subscribe({
                next: (response) => {
                    this.submitted = false;
                    this.loading=false;
                    if(response.isSuccess){
                      alert("User added and confirmation link has been sent.");
                      this.dialogRef.close(true);
                    }
                    else
                      alert(response.message);
                },
                error: error => {
                  this.loading=false;
                  error=error.message;
                }
            });
        console.table(this.addUserForm.value);
      }
    }
    onNoClick(): void {
      this.dialogRef.close(false);
      }
  }

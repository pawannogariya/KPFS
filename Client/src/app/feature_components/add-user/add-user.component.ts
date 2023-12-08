import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomvalidationService } from '@app/_services/custom-validation.service';

@Component({ templateUrl: 'add-user.component.html' })
export class AddUserComponent implements OnInit {

    addUserForm!: FormGroup;
    submitted = false;
  
    constructor(
      private fb: FormBuilder,
      private customValidator: CustomvalidationService
    ) { }
  
    ngOnInit() {
      this.addUserForm = this.fb.group({
        name: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        username: ['', [Validators.required], this.customValidator.userNameValidator.bind(this.customValidator)],
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
      this.submitted = true;
      if (this.addUserForm.valid) {
        alert('Form Submitted succesfully!!!\n Check the values in browser console.');
        console.table(this.addUserForm.value);
      }
    }
  }

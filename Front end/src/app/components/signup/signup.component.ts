import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserRole } from '../../enums/user-role.enum';
import { UserService } from '../../services/user.service';
import { UserCitizenDto } from '../../dtos/user-citizen.dto'; 
import { Observable } from 'rxjs';
import { Gender } from '../../enums/gender.enum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  signupForm: FormGroup;
  dto: UserCitizenDto;

  userRoles: string[] = []; 
  genders: string[] = []; 
  backendErrors: string = ''; // To store backend validation errors
  infoMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    // Initialize the DTO
    this.dto = new UserCitizenDto();
    this.userRoles = [UserRole.Citizen];
    this.genders = Object.values(Gender);

    this.signupForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      occupation: [''],
      country: ['', Validators.required],
      city: ['', Validators.required],
      address: [''],
      postalCode: [''],
      userRole: [UserRole.Citizen],
      gender: [''],
      birthDate: [null],
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  get firstName() {
    return this.signupForm.get('firstName')!;
  }
  get lastName() {
    return this.signupForm.get('lastName')!;
  }
  get email() {
    return this.signupForm.get('email')!;
  }
  get phoneNumber() {
    return this.signupForm.get('phoneNumber')!;
  }
  get occupation(){
    return this.signupForm.get('occupation')!;
  }
  get country() {
    return this.signupForm.get('country')!;
  }
  get city() {
    return this.signupForm.get('city')!;
  }
  get address() {
    return this.signupForm.get('address')!;
  }
  get postalCode() {
    return this.signupForm.get('postalCode')!;
  }
  get userRole() {
    return this.signupForm.get('userRole')!;
  }  
  get gender() {
    return this.signupForm.get('gender')!;
  }  
  get birthDate() {
    return this.signupForm.get('birthDate')!;
  }  
  get username() {
    return this.signupForm.get('username')!;
  }  
  get password() {
    return this.signupForm.get('password')!;
  }


  onSubmit() {

    if (this.signupForm.invalid) {
      return;
    }
    this.backendErrors = '';
    this.infoMessage = '';

    this.dto = this.signupForm.value;
    console.log(this.dto);
    this.userService.registerCitizen(this.dto).subscribe(
      {
        next: (response: Observable<UserCitizenDto>) => {
          this.infoMessage = 'Registration successful!';
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        error: (error: string) => {
          // Display the backend validation errors
          this.backendErrors = error;
        }
    });
  }

}

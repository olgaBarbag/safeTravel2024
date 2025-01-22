import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserRole } from '../../enums/user-role.enum';
import { UserService } from '../../services/user.service';
import { Observable } from 'rxjs';
import { UserAgentDto } from '../../dtos/user-agent.dto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup-agent',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './signup-agent.component.html',
  styleUrl: './signup-agent.component.css'
})
export class SignupAgentComponent {
signupForm: FormGroup;
  dto: UserAgentDto;

  userRoles: string[] = [];
  backendErrors: string = ''; // To store backend validation errors
  infoMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    // Initialize the DTO
    this.dto = new UserAgentDto();
    this.userRoles = [UserRole.Agent];

    this.signupForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      companyName: ['', [Validators.required]],
      vatNumber: ['', [Validators.required]],
      country: ['', Validators.required],
      city: ['', Validators.required],
      address: ['', Validators.required],
      postalCode: ['', Validators.required],
      userRole: [UserRole.Agent],
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
  get companyName(){
    return this.signupForm.get('companyName')!;
  }
  get vatNumber(){
    return this.signupForm.get('vatNumber')!;
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
    this.userService.registerAgent(this.dto).subscribe(
      {
        next: (response: Observable<UserAgentDto>) => {
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

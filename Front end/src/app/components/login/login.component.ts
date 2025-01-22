import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../services/user.service';
import { Observable } from 'rxjs';
import { UserCitizenDto } from '../../dtos/user-citizen.dto';
import { UserRole } from '../../enums/user-role.enum';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  backendErrors: string = ''; // To store backend validation errors
  infoMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    // Initialize the form with validators
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', Validators.required]
    });
  }

  // Getter for easier access to form controls in the template
  get username() {
    return this.loginForm.get('username')!;
  }

  get password() {
    return this.loginForm.get('password')!;
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      return;
    }
    this.backendErrors = '';

    this.userService.login(this.loginForm.value).subscribe({
      next: (response: Observable<any>) => {
        let loggedInUser: UserCitizenDto | null = this.userService.getLoggedInUserDto();
        if (loggedInUser == null) {
          this.router.navigate(['/login']);
          return;
        }
        if (UserRole.Citizen == loggedInUser.userRole) {
          this.router.navigate(['/home']);
          return;
        }
        if (UserRole.Agent == loggedInUser.userRole) {
          this.router.navigate(['/dashboard']);
          return;
        }
      },
      error: (error: string) => {
        // Display the backend validation errors
        this.backendErrors = error;
      }
    });
    // let jwt: string = this.userService.loginTest(this.loginForm.value);
    // localStorage.setItem('jwtToken', jwt);
    // this.router.navigate(['/dashboard']);
  }
}

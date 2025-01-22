import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private userService: UserService
  ) {}

  canActivate(): boolean {
    if (this.userService.isJwtTokenPresent()) {
      // Allow access to the route
      return true;
    } else {
      // Redirect to login if no token is found
      this.router.navigate(['/login']);
      return false;
    }
  }
}
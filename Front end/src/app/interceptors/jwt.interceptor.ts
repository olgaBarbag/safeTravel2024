import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { UserService } from '../services/user.service';

export const JwtInterceptor: HttpInterceptorFn = (req, next) => {
    const userService = inject(UserService); // Inject AuthService
    const token = userService.getJwtToken();

  if (token) {
    // Clone the request and add the Authorization header
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(req); // Pass the modified request to the next handler
}

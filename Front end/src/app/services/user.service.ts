import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError, catchError, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { UserCitizenDto } from '../dtos/user-citizen.dto';
import { jwtDecode } from 'jwt-decode';
import { UserAgentDto } from '../dtos/user-agent.dto';
import { UserRole } from '../enums/user-role.enum';
import { JwtPayload } from '../dtos/jwtpayload.dto';
@Injectable({
  providedIn: 'root'
})

export class UserService {

  // private baseUrl = environment.apiBaseUrl;
  private apiUrl = "https://localhost:5000/api/users"

  // This BehaviorSubject holds the current login state and broadcasts changes to all subscribers
  private isLoggedInSubject = new BehaviorSubject<boolean>(this.isJwtTokenPresent());
  // Observable that components can subscribe to
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  private isAgentLoggedInSubject = new BehaviorSubject<boolean>(this.isJwtTokenPresent());
  isAgentLoggedIn$ = this.isLoggedInSubject.asObservable();

  private isCitizenLoggedInSubject = new BehaviorSubject<boolean>(this.isJwtTokenPresent());
  isCitizenLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(private http: HttpClient) { }

  login(credentials: { username: string, password: string }): Observable<any> {

    return this.http.post(`${this.apiUrl}/login`, credentials).pipe(
      tap({
        next: (response: any) => {
          localStorage.setItem('jwtToken', response.token);
          this.isLoggedInSubject.next(true);
          let loggedInUserDto = this.getLoggedInUserDto();
          if (UserRole.Agent == loggedInUserDto?.userRole) {
            this.isAgentLoggedInSubject.next(true);
            this.isCitizenLoggedInSubject.next(false);
            return;
          }
          if (UserRole.Citizen == loggedInUserDto?.userRole) {
            this.isAgentLoggedInSubject.next(false);
            this.isCitizenLoggedInSubject.next(true);
            return;
          }
        }
      }),
      catchError((error) => {
        this.isLoggedInSubject.next(false);
        this.isAgentLoggedInSubject.next(false);
        this.isCitizenLoggedInSubject.next(false);
        return this.handleError(error); // Ensure the error is rethrown
      })
    );
  }

  loginTest(credentials: { username: string, password: string }): string {
    this.isLoggedInSubject.next(true);
    return "jwttokenresponse";
  }

  getLoggedInUserDto(): UserCitizenDto | null {
    const token = this.getJwtToken();
    if (!token) {
      return null;
    }
    let decodedToken:any;
    try {
      decodedToken = jwtDecode(token);
    } catch (error) {
      console.error('Invalid token', error);
      return null;
    }

    let userDto = new UserCitizenDto();
    userDto.username = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    userDto.id = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    userDto.email = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
    userDto.userRole = UserRole[decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] as keyof typeof UserRole];
    return userDto
  }

  isJwtTokenPresent(): boolean {
    return !!this.getJwtToken();
  }

  getJwtToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  logout(): void {
    localStorage.removeItem('jwtToken');
    this.isLoggedInSubject.next(false);
  }

  registerCitizen(dto: UserCitizenDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/citizen/registration`, dto).pipe(
      catchError(this.handleError)
    );
  }

  registerAgent(dto: UserAgentDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/agent/registration`, dto).pipe(
      catchError(this.handleError)
    );
  }

  fetchAgentProfile(): Observable<any> {
    return this.http.get("https://localhost:5000/api/agents/profile").pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      if (error.status === 400) {
          if (error.error && error.error.errors) {
          Object.keys(error.error.errors).forEach((key) => {
            errorMessage += `${key}: ${error.error.errors[key].join(', ')}<br>`;
          });
        }
      } else {
        errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
      }
    }
    return throwError(() => new Error(errorMessage));
  }

  private extractValidationErrors(errorObj: any): string {
    let errors = '';
    if (errorObj && errorObj.errors) {
      Object.keys(errorObj.errors).forEach((key) => {
        errors += `${key}: ${errorObj.errors[key].join(', ')}\n`;
      });
    }
    return errors;
  }
}

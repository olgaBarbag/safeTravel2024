import { ChangeDetectorRef, Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from './services/user.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterModule, NgbCollapseModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'safe-travel-app';
  isMenuCollapsed = true;

  isLoggedIn: boolean = false;
  isAgentLoggedIn: boolean = false;
  isCitizenLoggedIn: boolean = false;

  constructor(private userService: UserService, private changeDetector: ChangeDetectorRef) {}

  ngOnInit(): void {
    // Subscribe to the login state observable
    this.userService.isLoggedIn$.subscribe(status => {
      this.isLoggedIn = status; // Update the navbar UI based on login state
      this.changeDetector.markForCheck();
    });
    this.userService.isAgentLoggedIn$.subscribe(status => {
      this.isAgentLoggedIn = status; // Update the navbar UI based on login state
      this.changeDetector.markForCheck();
    });
    this.userService.isCitizenLoggedIn$.subscribe(status => {
      this.isCitizenLoggedIn = status; // Update the navbar UI based on login state
      this.changeDetector.markForCheck();
    });
  }

  logout() {
    this.userService.logout();
    this.isLoggedIn = false; 
    this.isAgentLoggedIn = false; 
    this.isCitizenLoggedIn = false;
  }
}

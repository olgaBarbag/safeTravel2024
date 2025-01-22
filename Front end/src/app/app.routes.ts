import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { SignupComponent } from './components/signup/signup.component';
import { RecommendationsComponent } from './components/recommendations/recommendations.component';
import { RecommendationComponent } from './components/recommendation/recommendation.component';
import { SignupAgentComponent } from './components/signup-agent/signup-agent.component';
import { HomeComponent } from './components/home/home.component';
import { ProfileAgentComponent } from './components/profile-agent/profile-agent.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'signup', component: SignupComponent },
    { path: 'signup-agent', component: SignupAgentComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'account', component: ProfileAgentComponent, canActivate: [AuthGuard] },
    { path: 'my-recommendations', component: RecommendationsComponent, canActivate: [AuthGuard] },    
    { path: 'add-recommendation', component: RecommendationComponent, canActivate: [AuthGuard] },    
    { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: '**', component: PageNotFoundComponent },  // Wildcard route for a 404 page
];

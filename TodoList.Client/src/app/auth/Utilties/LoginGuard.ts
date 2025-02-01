import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { SessionService } from '../services/SessionService';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  constructor(private sessionService: SessionService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    if (this.sessionService.getUser()) {
      // If logged in, prevent access to auth pages and redirect to /todo
      this.router.navigate(['/todo']);
      return false;
    }
    return true;
  }
}

import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { SessionService } from '../services/SessionService';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private sessionService: SessionService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const user = this.sessionService.getUser();
    const isAuthRoute = route.routeConfig?.path?.startsWith('auth');

    if (!user && !isAuthRoute) {
      this.router.navigate(['/auth/login']);
      return false;
    }

    if (user && isAuthRoute) {
      this.router.navigate(['/todo']);
      return false;
    }

    return true;
  }
}

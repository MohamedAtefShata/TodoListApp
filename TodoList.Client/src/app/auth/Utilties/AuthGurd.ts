import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import {SessionService} from '../services/SessionService';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
  private sessionService: SessionService,
  private router: Router
  ) {}

  canActivate(): boolean {
    if (!this.sessionService.getUser()) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}

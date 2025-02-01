import { Injectable } from '@angular/core';
import { UserDto } from '../models/UserDto';
import { ISessionService } from './ISessionService';

@Injectable({
  providedIn: 'root',
  useClass: SessionService
})
export class SessionService implements ISessionService {
  private userKey = 'user';
  private tokenKey = 'token';

  setUser(user: UserDto): void {
    localStorage.setItem(this.userKey, JSON.stringify(user));
  }

  getUser(): UserDto | null {
    const user = localStorage.getItem(this.userKey);
    return user ? JSON.parse(user) : null;
  }

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  clearSession(): void {
    localStorage.removeItem(this.userKey);
    localStorage.removeItem(this.tokenKey);
  }
}

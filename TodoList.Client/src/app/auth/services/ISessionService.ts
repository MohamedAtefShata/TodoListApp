import { UserDto } from '../models/UserDto';

export interface ISessionService {
  setUser(user: UserDto): void;
  getUser(): UserDto | null;
  setToken(token: string): void;
  getToken(): string | null;
  clearSession(): void;
}

import { AuthResponseDto } from '../models/AuthResponseDto';
import { LoginRequestDto } from '../models/LoginRequestDto';
import { RegisterRequestDto } from '../models/RegisterRequestDto';
import {Observable} from 'rxjs';

export interface IAuthService {
  login(request: LoginRequestDto): Observable<AuthResponseDto>;
  register(request: RegisterRequestDto): Observable<AuthResponseDto>;
}

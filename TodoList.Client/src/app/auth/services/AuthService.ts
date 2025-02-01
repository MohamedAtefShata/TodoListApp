import { Injectable } from '@angular/core';
import { IAuthService } from './IAuthService';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthResponseDto } from '../models/AuthResponseDto';
import { LoginRequestDto } from '../models/LoginRequestDto';
import { RegisterRequestDto } from '../models/RegisterRequestDto';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root',
  useClass: AuthService
})
export class AuthService implements IAuthService {
  private apiUrl = environment.API_URL+'/Auth';

  constructor(private http: HttpClient) {

  }

  login(request: LoginRequestDto): Observable<AuthResponseDto> {
    const headers = new HttpHeaders({
      'accept': 'text/plain',
      'Content-Type': 'application/json'
    });
    return this.http.post<AuthResponseDto>(`${this.apiUrl}/login`,JSON.stringify(request), { headers });
  }

  register(request: RegisterRequestDto): Observable<AuthResponseDto> {
    return this.http.post<AuthResponseDto>(`${this.apiUrl}/register`, request);
  }
}

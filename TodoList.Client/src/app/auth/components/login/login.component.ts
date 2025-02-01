import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthModule } from '../../auth.module';
import { AuthService } from '../../services/AuthService';
import { LoginRequestDto } from '../../models/LoginRequestDto';
import {finalize} from 'rxjs';
import { SessionService } from '../../services/SessionService';
import {NgIf} from '@angular/common';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  imports: [AuthModule, RouterLink,NgIf],
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  hidePassword = true;
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar,
    private authService: AuthService,
    private sessionService: SessionService
  ) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.isLoading = true;

      const loginRequest: LoginRequestDto = {
        userName: this.loginForm.value.username,
        password: this.loginForm.value.password
      };

      this.authService.login(loginRequest)
        .pipe(
          finalize(() => this.isLoading = false)
        )
        .subscribe({
          next: (response) => {
            // Store the token and user data
            this.sessionService.setToken(response.token);
            this.sessionService.setUser(response.user);

            this.snackBar.open('Login successful!', 'Close', {
              duration: 3000,
              horizontalPosition: 'end',
              verticalPosition: 'top'
            });

            this.router.navigate(['todo']);
          },
          error: (error) => {
            let errorMessage = 'Login failed';

            if (error.status === 401) {
              errorMessage = 'Invalid email or password';
            } else if (error.error?.message) {
              errorMessage = error.error.message;
            }

            this.snackBar.open(errorMessage, 'Close', {
              duration: 3000,
              horizontalPosition: 'end',
              verticalPosition: 'top'
            });
          }
        });
    } else {
      this.markFormGroupTouched(this.loginForm);
    }
  }

  private markFormGroupTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();
      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }

  getErrorMessage(controlName: string): string {
    const control = this.loginForm.get(controlName);
    if (control?.hasError('required')) {
      return `${controlName.charAt(0).toUpperCase() + controlName.slice(1)} is required`;
    }
    if (control?.hasError('username')) {
      return 'Please enter a valid username address';
    }
    if (control?.hasError('minlength')) {
      return 'Password must be at least 6 characters long';
    }
    return '';
  }
}

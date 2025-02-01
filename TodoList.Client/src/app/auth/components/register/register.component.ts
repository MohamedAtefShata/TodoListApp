import { Component, OnInit } from '@angular/core';
import { FormGroup, NonNullableFormBuilder, Validators} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthModule } from '../../auth.module';
import { AuthService } from '../../services/AuthService';
import { RegisterRequestDto } from '../../models/RegisterRequestDto';
import {finalize} from 'rxjs';
import { SessionService } from '../../services/SessionService';
import {NgIf} from '@angular/common';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  imports: [AuthModule,RouterLink,NgIf],
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  hidePassword = true;
  isLoading = false;

  constructor(
    private fb: NonNullableFormBuilder,
    private router: Router,
    private snackBar: MatSnackBar,
    private authService: AuthService,
    private sessionService: SessionService
  ) {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required]],
      firstname: ['', [Validators.required]],
      lastname: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    console.log(this.registerForm);
    if (this.registerForm.valid) {
      this.isLoading = true;

      const registerRequest: RegisterRequestDto = {
        userName: this.registerForm.value.username,
        firstName: this.registerForm.value.firstname,
        lastName: this.registerForm.value.lastname,
        password: this.registerForm.value.password
      };

      this.authService.register(registerRequest)
        .pipe(
          finalize(() => this.isLoading = false)
        )
        .subscribe({
          next: (response) => {
            // Store the token and user data
            this.sessionService.setToken(response.token);
            this.sessionService.setUser(response.user);

            this.snackBar.open('register successful!', 'Close', {
              duration: 3000,
              horizontalPosition: 'end',
              verticalPosition: 'top'
            });

            this.router.navigate(['/todo']);
          },
          error: (error) => {
            let errorMessage = 'register failed';

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
      this.markFormGroupTouched(this.registerForm);
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
    const control = this.registerForm.get(controlName);
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

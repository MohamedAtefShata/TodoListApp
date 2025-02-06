import { Routes } from '@angular/router';
import { LoginComponent } from "./auth/components/login/login.component";
import { RegisterComponent } from "./auth/components/register/register.component";
import { TodoListComponent } from './todo/components/todo-lists/todo-lists.component';
import { AuthGuard } from './auth/Utilties/AuthGuard';
import { LoginGuard } from './auth/Utilties/LoginGuard';

export const routes: Routes = [
    { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
    { path:'auth/login', component : LoginComponent, canActivate: [LoginGuard] },
    { path:'auth/register', component : RegisterComponent, canActivate: [LoginGuard] },
    { path:'todo', component : TodoListComponent , canActivate: [AuthGuard] },
    { path:'**', redirectTo: 'auth/login' }
];

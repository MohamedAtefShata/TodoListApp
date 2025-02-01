import { Routes } from '@angular/router';
import { LoginComponent } from "./auth/components/login/login.component";
import { RegisterComponent } from "./auth/components/register/register.component";
import { TodoListComponent } from './todo/components/todo-lists/todo-lists.component';
import { AuthGuard } from './auth/Utilties/AuthGurd';

export const routes: Routes = [
    { path:'auth/login', component : LoginComponent },
    { path:'auth/register', component : RegisterComponent },
    { path:'todo', component : TodoListComponent , canActivate: [AuthGuard] },
    { path:'**', redirectTo: 'auth/login' }
];

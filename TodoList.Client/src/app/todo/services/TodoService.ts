import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITodoService } from './ITodoService';
import { environment } from '../../environment';
import {CreateTodoItemDto} from '../models/CreateTodoItemDto';
import {TodoItemDto} from '../models/TodoItemDto';
import {TodoListDto} from '../models/TodoListDto';

@Injectable({
  providedIn: 'root',
  useClass: TodoService
})
export class TodoService implements ITodoService {
  private readonly baseUrl = `${environment.API_URL}/Todo`;

  constructor(private http: HttpClient) {}

  createTodo(dto: CreateTodoItemDto): Observable<TodoItemDto> {
    if (!dto.title?.trim()) {
      throw new Error('Title is required');
    }
    const headers = new HttpHeaders({
      'accept': 'text/plain',
      'Content-Type': 'application/json'
    });
    return this.http.post<TodoItemDto>(this.baseUrl, JSON.stringify(dto),{ headers });
  }

  deleteTodo(todoId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${todoId}`);
  }

  getUserTodoList(): Observable<TodoListDto> {
    return this.http.get<TodoListDto>(this.baseUrl);
  }

  toggleTodoCompletion(todoId: number): Observable<TodoItemDto> {
    const headers = new HttpHeaders({
      'accept': 'text/plain',
      'Content-Type': 'application/json'
    });
    return this.http.put<TodoItemDto>(`${this.baseUrl}/${todoId}`, {headers});
  }
}

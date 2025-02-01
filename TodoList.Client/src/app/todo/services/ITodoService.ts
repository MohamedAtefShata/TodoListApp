import {CreateTodoItemDto} from '../models/CreateTodoItemDto';
import {Observable} from 'rxjs';
import {TodoItemDto} from '../models/TodoItemDto';
import {TodoListDto} from '../models/TodoListDto';

export interface ITodoService {
  createTodo(dto: CreateTodoItemDto): Observable<TodoItemDto>;
  deleteTodo(todoId: number): Observable<void>;
  getUserTodoList(): Observable<TodoListDto>;
  toggleTodoCompletion(todoId: number): Observable<TodoItemDto>;
}

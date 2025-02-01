import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/TodoService';
import { NonNullableFormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { TodoListDto } from '../../models/TodoListDto';
import { CreateTodoItemDto } from '../../models/CreateTodoItemDto';
import { TodoItemDto } from '../../models/TodoItemDto';
import {TodoModule} from '../../todo.module';
import {SessionService} from '../../../auth/services/SessionService';
import {Router} from '@angular/router';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-lists.component.html',
  styleUrls: ['./todo-lists.component.scss'],
  imports: [TodoModule,ReactiveFormsModule]
})
export class TodoListComponent implements OnInit {
  todoForm: FormGroup;
  todoList: TodoListDto = { completedTodos: [], incompleteTodos: [] };
  isLoading = false;
  errorMessage = '';
  selectedTabIndex = 0;


  constructor(
    private router: Router,
    private todoService:TodoService,
    private sessionService:SessionService,
    private fb : NonNullableFormBuilder,
  ) {
    this.todoForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(1)]]
    });
  }

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.isLoading = true;
    this.todoService.getUserTodoList()
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: (todos: TodoListDto) => {
          this.todoList = todos;
          this.errorMessage = '';
        },
        error: (error) => {
          this.errorMessage = 'Failed to load todos. Please try again.';
          console.error('Error loading todos:', error);
        }
      });
  }

  onSubmit(): void {
    if (this.todoForm.invalid) return;

    const newTodo: CreateTodoItemDto = {
      title: this.todoForm.get('title')?.value.trim()
    };

    this.isLoading = true;
    this.todoService.createTodo(newTodo)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: (todo) => {
          this.todoList.incompleteTodos.push(todo);
          this.todoForm.reset();
        },
        error: (error) => {
          this.errorMessage = 'Failed to create todo. Please try again.';
          console.error('Error creating todo:', error);
        }
      });
  }

  toggleTodo(todo: TodoItemDto): void {
    this.isLoading = true;
    this.todoService.toggleTodoCompletion(todo.id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadTodos();
          this.errorMessage = '';
        },
        error: (error) => {
          this.errorMessage = 'Failed to update todo. Please try again.';
          console.error('Error updating todo:', error);
        }
      });
  }

  deleteTodo(todo: TodoItemDto): void {
    if (!confirm('Are you sure you want to delete this todo?')) return;

    this.isLoading = true;
    this.todoService.deleteTodo(todo.id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadTodos();
          this.errorMessage = '';
        },
        error: (error) => {
          this.errorMessage = 'Failed to delete todo. Please try again.';
          console.error('Error deleting todo:', error);
        }
      });
  }
  logout(): void {
    if (!confirm('Are you sure you want to logout ?'))
      return;
    this.sessionService.clearSession();
    this.router.navigate(['/auth/login']);
  }
}

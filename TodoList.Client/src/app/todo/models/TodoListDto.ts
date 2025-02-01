import {TodoItemDto} from './TodoItemDto';

export interface TodoListDto {
  completedTodos: TodoItemDto[];
  incompleteTodos: TodoItemDto[];
}

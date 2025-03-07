﻿namespace TodoList.Domain;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public ICollection<TodoItem> TodoItems { get; set; }
}

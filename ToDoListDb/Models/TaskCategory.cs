﻿using System;
using System.Collections.Generic;

namespace ToDoListDb.Models;

public partial class TaskCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<ToDoList> ToDoLists { get; } = new List<ToDoList>();
}
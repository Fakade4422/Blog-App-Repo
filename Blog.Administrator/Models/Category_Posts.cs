﻿using Blog.Administrator.Models;

namespace Blog.Administrator.Models
{
    public class Category_Posts
    {
        public Posts? Posts { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public partial class Student
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GroupId { get; set; }

        public virtual StudentGroup? Group { get; set; }
    }
}

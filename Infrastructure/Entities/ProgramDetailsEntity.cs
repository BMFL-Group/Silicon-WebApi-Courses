﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class ProgramDetailsEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Course")]
        public string CourseId { get; set; } = null!;
        public virtual CourseEntity Course { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
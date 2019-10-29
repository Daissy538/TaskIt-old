using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TaskItApi.Entities
{
    /// <summary>
    /// Task of a group that an user can be holder of.
    /// </summary>
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        [Required]
        public DateTime Until { get; set; }
        [Required]
        public TaskStatus Status { get; set; }
        [Required]
        public int GroupID { get; set; }
        public Group Group { get; set; }

        public ICollection<TaskHolder> TaskHolders { get; set; }
    }

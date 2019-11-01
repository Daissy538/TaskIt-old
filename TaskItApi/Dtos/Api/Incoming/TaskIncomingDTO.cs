using System;
using System.ComponentModel.DataAnnotations;

namespace TaskItApi.Dtos.Api.Incoming
{
    /// <summary>
    /// Incoming task data
    /// </summary>
    public class TaskIncomingDTO
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime Until { get; set; }
        [Required]
        public int GroupID { get; set; }
    }
}

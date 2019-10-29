using System;
using System.Collections.Generic;
using TaskItApi.Dtos.Api.Outgoing;

namespace TaskItApi.Dtos.Api
{
    /// <summary>
    /// Outgoing task data
    /// </summary>
    public class TaskOutgoingDTO
    {
        /// <summary>
        /// Task ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Task title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Task description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The current state of the task
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// The deathline for a task
        /// </summary>
        public DateTime Until { get; set; }
        /// <summary>
        /// The starting time for a task
        /// </summary>
        public DateTime From { get; set; }
        /// <summary>
        /// The group where the task is part of
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// The color that the group has
        /// </summary>
        public string GroupColor { get; set; }  
        /// <summary>
        /// The holders of the task
        /// </summary>
        public IEnumerable<TaskHolderOutgoingDTO> TaskHolders { get; set; }
    }
}

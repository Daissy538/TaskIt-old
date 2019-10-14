using System.ComponentModel.DataAnnotations;

namespace TaskItApi.Dtos
{
    /// <summary>
    /// The incoming data of a group
    /// </summary>
    public class GroupIncomingDTO
    {
        /// <summary>
        /// The name of the group
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The description of the group
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The id of the icon
        /// </summary>
        [Required]
        public int IconID { get; set; }
        /// <summary>
        /// The id of the color
        /// </summary>
        [Required]
        public int ColorID { get; set; }
    }
}

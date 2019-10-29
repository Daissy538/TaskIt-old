using TaskItApi.Attributes;

namespace TaskItApi.Enums
{
    /// <summary>
    /// The status of a task
    /// String value is used to seed the LookUp table of the database
    /// see <see cref="DbSeeder"/>
    /// </summary>
    public enum TaskStatuses
    {
        [StringValue("Finished")]
        Finished = 1,
        [StringValue("Canceled")]
        Canceled = 2,
        [StringValue("Open")]
        Open = 3
    }
}

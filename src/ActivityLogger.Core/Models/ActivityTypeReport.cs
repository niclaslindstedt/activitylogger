namespace AL.Core.Models
{
    public class ActivityTypeReport
    {
        public string ActivityType { get; set; }
        public float ActivityHourGoal { get; set; }
        public bool UserIsIdle { get; set; }
        public bool UserIsActive { get; set; }
    }
}

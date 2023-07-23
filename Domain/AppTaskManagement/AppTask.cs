using Domain.AppTaskManagement.Enums;
using Domain.Shared;

namespace Domain.AppTaskManagement
{
    public class AppTask : BaseEntity<Guid>
    {
        public AppTask()
        {
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Priority = string.Empty;
        }

        public AppTask(string title, string description, string priority)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Title cannot be null or empty");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("Description cannot be null or empty");
            }

            if (string.IsNullOrEmpty(priority))
            {
                throw new ArgumentNullException("Priority cannot be null or empty");
            }
            
            this.Title = title;
            this.Description = description;
            this.Priority = priority;
            this.Status = Status.Pending;
        }

        public void ChangeStatus(Status status)
        {
            this.Status = status;
        }

        public override Guid Id { get; set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string Priority { get; private set; }

        public Status Status { get; private set; }
    }
}

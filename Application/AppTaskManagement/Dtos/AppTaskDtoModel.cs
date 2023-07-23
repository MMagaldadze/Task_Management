using Domain.AppTaskManagement.Enums;

namespace Application.AppTaskManagement.Dtos
{
    public class AppTaskDtoModel
    {
        public AppTaskDtoModel(
            Guid id,
            string title,
            string description,
            string priority,
            Status status)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Priority = priority;
            this.Status = status;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; } 
        public string Priority { get; private set; } 
        public Status Status { get; private set; }
    }
}

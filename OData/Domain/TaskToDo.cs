using System;

namespace OData.Domain
{
    public class TaskToDo : IdentityEntity
    {
        public string Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime DeadLine { get; set; }

        public bool Status { get; set; }

        public int UserId { get; set; }

        // EntityFramework Navigation Properties
        public virtual Developer User { get; set; }
    }
}

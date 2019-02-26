using System;

namespace TestProject.Data.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}

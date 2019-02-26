using System;

namespace TestProject.Data.Entities
{
    public class GarageEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual AreaEntity Area { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}

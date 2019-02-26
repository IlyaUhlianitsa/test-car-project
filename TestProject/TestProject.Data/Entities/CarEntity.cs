﻿namespace TestProject.Data.Entities
{
    public class CarEntity : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public long GarageId { get; set; }

        public virtual GarageEntity Garage { get; set; }

        public long CategoryId { get; set; }

        public virtual CarCategoryEntity Category { get; set; }
    }
}

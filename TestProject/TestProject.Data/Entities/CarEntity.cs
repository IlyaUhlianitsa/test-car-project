using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestProject.Data.Entities
{
    public class CarEntity : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public long GarageId { get; set; }

        public virtual GarageEntity Garage { get; set; }

        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }

        public virtual CarCategoryEntity Category { get; set; }
    }
}

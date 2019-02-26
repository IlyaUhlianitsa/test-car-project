namespace TestProject.Data.Entities
{
    public class GarageEntity : BaseEntity
    {
        public string Name { get; set; }

        public long AreaId { get; set; }

        public virtual AreaEntity Area { get; set; }
    }
}

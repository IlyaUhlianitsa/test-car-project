namespace TestProject.Data.Entities
{
    public class GarageEntity : BaseEntity
    {
        public string Name { get; set; }

        public virtual AreaEntity Area { get; set; }
    }
}

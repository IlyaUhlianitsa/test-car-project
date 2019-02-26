namespace TestProject.Services.Models
{
    public class Car
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long GarageId { get; set; }

        public string GarageName { get; set; }

        public long AreaId { get; set; }

        public string AreaName { get; set; }

        public long CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}

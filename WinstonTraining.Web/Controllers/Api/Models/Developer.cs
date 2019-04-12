namespace WinstonTraining.Web.Controllers.Api.Models
{
    public class Developer
    {
        public Developer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
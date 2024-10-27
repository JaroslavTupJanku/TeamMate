namespace TeamMateServer.Data.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;
        public List<PlayerEntity> Players { get; set; } = new List<PlayerEntity>();
    }
}

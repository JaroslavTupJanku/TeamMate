namespace TeamMateServer.Data.Entities
{
    public class PlayerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Details { get; set; } = string.Empty;

        public int TeamId { get; set; }
    }
}

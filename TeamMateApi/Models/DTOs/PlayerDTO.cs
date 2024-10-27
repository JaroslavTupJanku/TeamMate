namespace TeamMateApi.Models.DTOs
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public int Age { get; set; }

        public int TeamId { get; set; }
    }
}

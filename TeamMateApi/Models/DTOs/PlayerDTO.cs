using TeamMateApi.Core.Enums;

namespace TeamMateApi.Models.DTOs
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public Position Position { get; set; }
        public int Age { get; set; }
        public string Details { get; set; } = string.Empty;

        public int TeamId { get; set; }
    }
}

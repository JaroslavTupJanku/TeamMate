namespace TeamMateApi.Models.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;
        public List<PlayerDTO> Players { get; set; } = new List<PlayerDTO>();
    }
}

namespace API_VariasBDs.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }

        public string Nome { get; set; } = null!;

        public DateTime Data { get; set; }

        public string? Token { get; set; }
    }
}

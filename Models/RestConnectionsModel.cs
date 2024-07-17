namespace REF_XML_REQUEST.Models
{
    public class RestConnectionsModel
    {
        public int Id { get; set; }
        public string? RestName { get; set; }
        public string? UserLogin { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Port { get; set; }
    }
}

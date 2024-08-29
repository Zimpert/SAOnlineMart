namespace SAOnlineMart.Models
{
    public class User
    {
        public int UserID { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }                           
        public required string Password { get; set; }    
        public required string Role { get; set; }
    }
}

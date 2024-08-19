namespace CalculatorAPI.Models
{
    public class Registration
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
        public int IsActive { get; set; }

    }
}

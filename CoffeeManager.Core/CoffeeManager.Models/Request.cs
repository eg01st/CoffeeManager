namespace CoffeeManager.Models
{
    public class Request
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string ObjectJson { get; set; }

        public string ErrorMessage { get; set; }
    }
}

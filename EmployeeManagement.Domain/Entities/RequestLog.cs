namespace EmployeeManagement.Domain.Entities
{
    public class RequestLog
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

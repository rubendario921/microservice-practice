namespace multritrabajos_withdrawal.DTOs
{
    public class NotificationRequest
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public required decimal Ammount { get; set; }
        public int IdCustomer { get; set; }
        public int IdAccount { get; set; }
    }
}

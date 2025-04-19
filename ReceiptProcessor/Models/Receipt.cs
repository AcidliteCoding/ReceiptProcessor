namespace ReceiptProcessor.Models
{
    // Maps to Receipt values
    public class Receipt
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Retailer { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchaseTime { get; set; }
        public List<ReceiptItem> Items { get; set; } = new List<ReceiptItem>();
        public decimal Total {  get; set; }
    }
}

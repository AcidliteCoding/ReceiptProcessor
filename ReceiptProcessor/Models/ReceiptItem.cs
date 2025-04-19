namespace ReceiptProcessor.Models
{
    // Maps to items array within Json
    public class ReceiptItem
    {
        public string ShortDescription { get; set; } 
        public decimal Price { get; set; }  
    }
}

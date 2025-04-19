using ReceiptProcessor.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReceiptProcessor.Services
{
    public class ReceiptService
    {
        // In-Memory Storage
        public readonly static ConcurrentDictionary<Guid, int> _receiptPoints = new ConcurrentDictionary<Guid, int>();

        public Guid ProcessReceipt(Receipt receipt)
        {
            int points = CalculatePoints(receipt);
            _receiptPoints[receipt.Id] = points;

            return receipt.Id;
        }

        public int GetPoints(Guid id) {

            if (_receiptPoints.TryGetValue(id, out int points)) {
                return points;

            }

            throw new KeyNotFoundException("Receipt not found.");
        }


        private int CalculatePoints(Receipt receipt) 
        {
            int points = 0;

            // Rule 1: Points for alphanumeric characters in retailer name.
            points += Regex.Matches(receipt.Retailer, @"[a-zA-Z0-9]").Count;

            // Rule 2: 50 points if the total is a round dollar amount.
            if (receipt.Total % 1 == 0) {
                points += 50;
            }

            // Rule 3: 25 points if the total is a multiple of 0.25.
            if (receipt.Total % 0.25m == 0) { 
                points += 25;
            }

            // Rule 4: 5 points for every two items.
            points += (receipt.Items.Count / 2) * 5;

            // Rule 5: Points for item description length multiple of 3
            foreach (var item in receipt.Items)
            {
                var desc = item.ShortDescription?.Trim();
                if (!string.IsNullOrWhiteSpace(desc) && desc.Length % 3 == 0)
                    points += (int)Math.Ceiling(item.Price * 0.2m);
            }

            // Rule 6: 6 points if purchase day is odd.
            if (DateTime.TryParse(receipt.PurchaseDate.ToString(), out var date) && date.Day % 2 != 0)
                points += 6;

            // Rule 7: 10 points for purchases between 2:00pm and 4:00pm.
            var time = TimeSpan.Parse(receipt.PurchaseTime);
            if (time >= new TimeSpan(14, 0, 0) && time < new TimeSpan(16, 0, 0)) points += 10;

            return points;
        }
    }
}

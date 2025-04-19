using Microsoft.AspNetCore.Mvc;
using ReceiptProcessor.Models;
using ReceiptProcessor.Services;
using System;

namespace ReceiptProcessor.Controllers
{
    [ApiController]
    [Route("receipts")]
    public class ReceiptsController : Controller
    {
        private readonly ReceiptService _receiptService;

        public ReceiptsController(ReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        // POST: /receipts/process
        [HttpPost("process")]
        public IActionResult ProcessReceipt([FromBody] Receipt receipt) { 
            var receiptId = _receiptService.ProcessReceipt(receipt);
            return Ok(new {id = receiptId});
        }

        // GET: /receipts/{id}/points
        [HttpGet("{id}/points")]
        public IActionResult GetPoints(string id) {
            if (Guid.TryParse(id, out var receiptId)) { 
                try
                {
                    var points = _receiptService.GetPoints(receiptId);
                    return Ok(new { points });
                }catch (KeyNotFoundException ex) { 
                    return NotFound(ex.Message);
                }
            }

            return BadRequest("Invalid receipt ID format");
        }
    }
}

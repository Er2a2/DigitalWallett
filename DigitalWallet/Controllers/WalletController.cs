using DigitalWallet.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost("deposit")]
        public IActionResult Deposit(int walletId, decimal amount)
        {
            _walletService.Deposit(walletId, amount);
            return Ok(new { Message = "واریز با موفقیت انجام شد" });
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw(int walletId, decimal amount)
        {
            _walletService.Withdraw(walletId, amount);
            return Ok(new { Message = "برداشت با موفقیت انجام شد" });
        }

        [HttpPost("transfer")]
        public IActionResult Transfer(int fromWalletId, int toWalletId, decimal amount)
        {
            _walletService.Transfer(fromWalletId, toWalletId, amount);
            return Ok(new { Message = "انتقال وجه با موفقیت انجام شد" });
        }

        [HttpGet("transactions/{walletId}")]
        public IActionResult GetTransactions(int walletId)
        {
            var transactions = _walletService.GetTransactions(walletId);
            return Ok(transactions);
        }
    }
}

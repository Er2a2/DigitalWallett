using DigitalWallet.Application.Commands;
using DigitalWallet.Application.CQRS.Command;
using DigitalWallet.Application.CQRS.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DigitalWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletCQRSController : ControllerBase
    { 
     private readonly IMediator _mediator;

    public WalletCQRSController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("deposit")]
        [SwaggerOperation(
    Summary = "انتقال وجه",
    Description = "شماره حساب را وارد کنید",
    OperationId = "Wallets.Deposit",
    Tags = new[] { "WalletCQRSController" })]
    public async Task<IActionResult> Deposit(int walletId, decimal amount)
    {
        await _mediator.Send(new DepositCommand(walletId, amount));
        return Ok(new { Message = "واریز با موفقیت انجام شد" });
    }

    [HttpPost("withdraw")]
        [SwaggerOperation(
    Summary = "برداشت وجه",
    Description = "شماره حساب را وارد کنید",
    OperationId = "Wallets.Withdraw",
    Tags = new[] { "WalletCQRSController" })]
        public async Task<IActionResult> Withdraw(int walletId, decimal amount)
    {
        await _mediator.Send(new WithdrawCommand(walletId, amount));
        return Ok(new { Message = "برداشت با موفقیت انجام شد" });
    }

    [HttpPost("transfer")]
        [SwaggerOperation(
    Summary = "انتقال وجه",
    Description = "شماره حساب را وارد کنید",
    OperationId = "Wallets.Transfer",
    Tags = new[] { "WalletCQRSController" })]
        public async Task<IActionResult> Transfer(int fromWalletId, int toWalletId, decimal amount)
    {
        await _mediator.Send(new TransferCommand(fromWalletId, toWalletId, amount));
        return Ok(new { Message = "انتقال وجه با موفقیت انجام شد" });
    }

    [HttpGet("transactions/{walletId}")]
        [SwaggerOperation(
  Summary = "سابقه تراکنش ها",
  Description = "شماره حساب را وارد کنید",
  OperationId = "Wallets.GetTransactions",
  Tags = new[] { "WalletCQRSController" })]
        public async Task<IActionResult> GetTransactions(int walletId)
    {
        var transactions = await _mediator.Send(new GetTransactionsQuery(walletId));
        return Ok(transactions);
    }
}
}

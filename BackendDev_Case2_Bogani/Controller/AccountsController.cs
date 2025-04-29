using Microsoft.AspNetCore.Mvc;
using BackendDev_Case2_Bogani.DTOs;
using BackendDev_Case2_Bogani.Services;

namespace BackendDev_Case2_Bogani.Controller
{
   [ApiController]
   [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;

    public AccountsController(IAccountService service) => _service = service;

    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPost] public async Task<IActionResult> Create(AccountDTO dto) => Ok(await _service.CreateAsync(dto));
    [HttpPut("{id}")] public async Task<IActionResult> Update(int id, AccountDTO dto) => Ok(await _service.UpdateAsync(id, dto));
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) => Ok(await _service.DeleteAsync(id));
    [HttpGet("total-balance")] public async Task<IActionResult> TotalBalance() => Ok(await _service.GetTotalBalanceAsync());
    [HttpGet("below/{amount}")] public async Task<IActionResult> Below(decimal amount) => Ok(await _service.GetAccountsBelowThresholdAsync(amount));
    [HttpGet("top/{count}")] public async Task<IActionResult> Top(int count) => Ok(await _service.GetTopAccountsAsync(count));
       
    }
}
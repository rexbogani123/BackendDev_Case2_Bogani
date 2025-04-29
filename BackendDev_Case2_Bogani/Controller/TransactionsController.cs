using Microsoft.AspNetCore.Mvc;
using BackendDev_Case2_Bogani.Services;
using BackendDev_Case2_Bogani.DTOs;

namespace BackendDev_Case2_Bogani.Controller
{
 [ApiController]
[Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service) => _service = service;

    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _service.GetByIdAsync(id));
    [HttpPost] public async Task<IActionResult> Create(TransactionCreateDTO dto) => Ok(await _service.CreateAsync(dto));
    [HttpPut("{id}")] public async Task<IActionResult> Update(int id, TransactionDTO dto) => Ok(await _service.UpdateAsync(id, dto));
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) => Ok(await _service.DeleteAsync(id));
    [HttpGet("account/{accountId}")] public async Task<IActionResult> GetByAccount(int accountId) => Ok(await _service.GetTransactionsByAccountIdAsync(accountId));

    }
}
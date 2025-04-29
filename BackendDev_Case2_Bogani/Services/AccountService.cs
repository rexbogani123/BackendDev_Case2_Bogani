using BackendDev_Case2_Bogani.DTOs;
using BackendDev_Case2_Bogani.Model;
using Microsoft.EntityFrameworkCore;
using BackendDev_Case2_Bogani.Data;

namespace BackendDev_Case2_Bogani.Services
{
      public class AccountService : IAccountService
{
    private readonly FinancialDbContext _context;

    public AccountService(FinancialDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAsync() =>
        await _context.Accounts.Select(a => new AccountDTO
        {
            Id = a.Id,
            AccountNumber = a.AccountNumber,
            AccountHolder = a.AccountHolder,
            Balance = a.Balance
        }).ToListAsync();

    public async Task<AccountDTO?> GetByIdAsync(int id)
    {
        var acc = await _context.Accounts.FindAsync(id);
    return acc == null
        ? null
        : new AccountDTO
        {
            Id = acc.Id,
            AccountNumber = acc.AccountNumber,
            AccountHolder = acc.AccountHolder,
            Balance = acc.Balance
        };
    }

    public async Task<AccountDTO> CreateAsync(AccountDTO dto)
    {
        var acc = new Account { AccountNumber = dto.AccountNumber, AccountHolder = dto.AccountHolder, Balance = dto.Balance };
        _context.Accounts.Add(acc);
        await _context.SaveChangesAsync();
        dto.Id = acc.Id;
        return dto;
    }

    public async Task<AccountDTO?> UpdateAsync(int id, AccountDTO dto)
    {
        var acc = await _context.Accounts.FindAsync(id);
        if (acc == null) return null;
        acc.AccountNumber = dto.AccountNumber;
        acc.AccountHolder = dto.AccountHolder;
        acc.Balance = dto.Balance;
        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var acc = await _context.Accounts.FindAsync(id);
        if (acc == null) return false;
        _context.Accounts.Remove(acc);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetTotalBalanceAsync() => await _context.Accounts.SumAsync(a => a.Balance);
    public async Task<IEnumerable<AccountDTO>> GetAccountsBelowThresholdAsync(decimal threshold) =>
        await _context.Accounts.Where(a => a.Balance < threshold).Select(a => new AccountDTO { Id = a.Id, AccountNumber = a.AccountNumber, AccountHolder = a.AccountHolder, Balance = a.Balance }).ToListAsync();
    public async Task<IEnumerable<AccountDTO>> GetTopAccountsAsync(int top) =>
        await _context.Accounts.OrderByDescending(a => a.Balance).Take(top).Select(a => new AccountDTO { Id = a.Id, AccountNumber = a.AccountNumber, AccountHolder = a.AccountHolder, Balance = a.Balance }).ToListAsync();
}
}

using BackendDev_Case2_Bogani.DTOs;
using BackendDev_Case2_Bogani.Model;
using Microsoft.EntityFrameworkCore;
using BackendDev_Case2_Bogani.Data;


namespace BackendDev_Case2_Bogani.Services
{
   public class TransactionService : ITransactionService
{
    private readonly FinancialDbContext _context;

    public TransactionService(FinancialDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransactionDTO>> GetAllAsync() =>
        await _context.Transactions.Select(t => new TransactionDTO
        {
            Id = t.Id,
            AccountId = t.AccountId,
            TransactionType = t.TransactionType,
            Amount = t.Amount,
            TransactionDate = t.TransactionDate
        }).ToListAsync();

    public async Task<TransactionDTO?> GetByIdAsync(int id)
    {
        var t = await _context.Transactions.FindAsync(id);
        return t == null ? null : new TransactionDTO
        {
            Id = t.Id,
            AccountId = t.AccountId,
            TransactionType = t.TransactionType,
            Amount = t.Amount,
            TransactionDate = t.TransactionDate
        };
    }

    public async Task<TransactionDTO?> CreateAsync(TransactionCreateDTO dto)
    {
        var acc = await _context.Accounts.FindAsync(dto.AccountId);
        if (acc == null) return null;

        if (dto.TransactionType == "Withdrawal" && acc.Balance < dto.Amount)
            return null; // or throw custom error

        if (dto.TransactionType == "Deposit")
            acc.Balance += dto.Amount;
        else
            acc.Balance -= dto.Amount;

        var t = new Transaction
        {
            AccountId = dto.AccountId,
            TransactionType = dto.TransactionType,
            Amount = dto.Amount,
            TransactionDate = DateTime.UtcNow
        };

        _context.Transactions.Add(t);
        await _context.SaveChangesAsync();

        return new TransactionDTO
        {
            Id = t.Id,
            AccountId = t.AccountId,
            TransactionType = t.TransactionType,
            Amount = t.Amount,
            TransactionDate = t.TransactionDate
        };
    }

    public async Task<TransactionDTO?> UpdateAsync(int id, TransactionDTO dto)
    {
        var t = await _context.Transactions.FindAsync(id);
        if (t == null) return null;
        t.TransactionType = dto.TransactionType;
        t.Amount = dto.Amount;
        t.TransactionDate = dto.TransactionDate;
        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var t = await _context.Transactions.FindAsync(id);
        if (t == null) return false;
        _context.Transactions.Remove(t);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TransactionDTO>> GetTransactionsByAccountIdAsync(int accountId) =>
        await _context.Transactions.Where(t => t.AccountId == accountId)
            .Select(t => new TransactionDTO
            {
                Id = t.Id,
                AccountId = t.AccountId,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate
            }).ToListAsync();
}
}

using BackendDev_Case2_Bogani.DTOs;

namespace BackendDev_Case2_Bogani.Services
{
    public interface  IAccountService
    {
    Task<IEnumerable<AccountDTO>> GetAllAsync();
    Task<AccountDTO> GetByIdAsync(int id);
    Task<AccountDTO> CreateAsync(AccountDTO dto);
    Task<AccountDTO> UpdateAsync(int id, AccountDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<decimal> GetTotalBalanceAsync();
    Task<IEnumerable<AccountDTO>> GetAccountsBelowThresholdAsync(decimal threshold);
    Task<IEnumerable<AccountDTO>> GetTopAccountsAsync(int top);
    }
}
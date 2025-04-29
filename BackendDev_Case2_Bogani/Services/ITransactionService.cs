using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendDev_Case2_Bogani.DTOs;
using BackendDev_Case2_Bogani.Model;
using Microsoft.EntityFrameworkCore;
using BackendDev_Case2_Bogani.Data;

namespace BackendDev_Case2_Bogani.Services
{
    public interface  ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllAsync();
        Task<TransactionDTO> GetByIdAsync(int id);
        Task<TransactionDTO> CreateAsync(TransactionCreateDTO dto);
        Task<TransactionDTO> UpdateAsync(int id, TransactionDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByAccountIdAsync(int accountId);
    }
}
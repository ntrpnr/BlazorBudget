using BlazorBudget.Wasm.Abstractions;

namespace BlazorBudget.Wasm.Services;

public interface IBudgetService
{
    Task<List<Budget>> GetAllBudgetsAsync();
    Task<bool> CreateOrUpdateBudgetAsync(Budget budget);

    Task<bool> DeleteBudgetAsync(Guid budgetId);

    Task<List<Budget>> GetBudgetsByUserIdAsync(int userId);

    Task<Budget> GetBudgetByIdAsync(Guid budgetId);

    Task<List<Transaction>> GetTransactionsByCategoryBudgetIdAsync(int categoryBudgetId);
}

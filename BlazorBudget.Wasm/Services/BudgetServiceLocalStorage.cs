using BlazorBudget.Wasm.Abstractions;
using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorBudget.Wasm.Services;

public class BudgetServiceLocalStorage : IBudgetService
{
    private readonly ILocalStorageService _localStorage;
    private readonly ICategoryService _categoryService;

    private const string BudgetKey = "budget";

    public BudgetServiceLocalStorage(ILocalStorageService localStorage, ICategoryService categoryService)
    {
        _localStorage = localStorage;
        _categoryService = categoryService;
    }

    public async Task<bool> CreateOrUpdateBudgetAsync(Budget budget)
    {
        var budgets = await GetBudgetsByUserIdAsync(budget.UserId);
        var existingBudget = budgets.FirstOrDefault(b => b.Id == budget.Id);
        if (existingBudget == null)
        {
            budgets.Add(budget);
        }
        else
        {
            var index = budgets.IndexOf(existingBudget);
            budgets[index] = budget;
        }

        await _localStorage.SetItemAsync(BudgetKey, budgets);
        return true;
    }

    public async Task<bool> DeleteBudgetAsync(Guid budgetId)
    {
        var budgets = await GetAllBudgetsAsync();
        var budget = budgets.FirstOrDefault(b => b.Id == budgetId);
        if (budget == null)
            return false;

        budgets.Remove(budget);
        await _localStorage.SetItemAsync(BudgetKey, budgets);
        return true;
    }
    public async Task<List<Budget>> GetAllBudgetsAsync()
    {
        return await _localStorage.GetItemAsync<List<Budget>>(BudgetKey) ?? new List<Budget>();
    }

    public async Task<List<Budget>> GetBudgetsByUserIdAsync(int userId)
    {
        var budgets = await _localStorage.GetItemAsync<List<Budget>>(BudgetKey);
        return budgets?.Where(b => b.UserId == userId).ToList() ?? new List<Budget>();
    }

    public async Task<Budget> GetBudgetByIdAsync(Guid budgetId)
    {
        var budgets = await GetAllBudgetsAsync();
        return budgets.FirstOrDefault(b => b.Id == budgetId);
    }

    public async Task<List<Transaction>> GetTransactionsByCategoryBudgetIdAsync(Guid categoryBudgetId)
    {
        var budgets = await GetAllBudgetsAsync();
        return budgets.SelectMany(b => b.Transactions).Where(t => t.CategoryId == categoryBudgetId).ToList();
    }
    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _categoryService.GetCategoriesAsync();
    }
}
using Blazored.LocalStorage;
using System.Transactions;

namespace BlazorBudget.Wasm.Abstractions;

public record Category
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public CategoryType Type { get; set; }
}

public record Budget
{
    public Guid BudgetId { get; set; } = Guid.NewGuid();
    public int UserId { get; set; }
    public decimal IncomeAmount => Transactions.Count(t => t.Type == TransactionType.Income);
    public decimal SpentAmount => Transactions.Count(t => t.Type == TransactionType.Expense);
    public DateTime? Month { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}

public class Transaction
{
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
    public TransactionType Type { get; set; }
    public int CategoryId { get; set; }
}

public enum TransactionType
{
    Income,
    Expense
}

public enum CategoryType
{
    Fixed,  // For fixed income/expenses
    Variable  // For variable income/expenses
}

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<Budget> Budgets { get; set; }
}
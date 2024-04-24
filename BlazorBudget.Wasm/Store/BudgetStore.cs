using BlazorBudget.Wasm.Abstractions;
using BlazorBudget.Wasm.Services;
using Fluxor;

namespace BlazorBudget.Wasm.Store;

// Budget Feature
public class BudgetFeature : Feature<BudgetState>
{
    public override string GetName() => "Budgets";

    protected override BudgetState GetInitialState() =>
        new BudgetState(isLoading: false, budgets: new List<Budget>(), error: null);
}

// Budget State
public record BudgetState
{
    public bool IsLoading { get; init; } = true;
    public List<Budget> Budgets { get; init; }
    public string Error { get; init; }

    public BudgetState(bool isLoading, List<Budget> budgets, string error) =>
        (IsLoading, Budgets, Error) = (isLoading, budgets, error);
}

// Actions
public record FetchBudgetsAction { public int UserId { get; init; } }
public record FetchBudgetsSuccessAction { public List<Budget> Budgets { get; init; } }
public record FetchBudgetsFailureAction { public string Error { get; init; } }
public record AddOrUpdateBudgetAction { public Budget Budget { get; init; } }
public record DeleteBudgetAction { public int UserId { get; init; } public Guid BudgetId { get; init; } }

// Reducers
public static class BudgetReducers
{
    [ReducerMethod]
    public static BudgetState ReduceFetchBudgetsAction(BudgetState state, FetchBudgetsAction action) =>
        state with { IsLoading = true };

    [ReducerMethod]
    public static BudgetState ReduceFetchBudgetsSuccessAction(BudgetState state, FetchBudgetsSuccessAction action) =>
        state with { IsLoading = false, Budgets = action.Budgets };

    [ReducerMethod]
    public static BudgetState ReduceFetchBudgetsFailureAction(BudgetState state, FetchBudgetsFailureAction action) =>
        state with { IsLoading = false, Error = action.Error };
}

// Effects
public class BudgetEffects
{
    private readonly IBudgetService _budgetService;

    public BudgetEffects(IBudgetService budgetService) =>
        _budgetService = budgetService;

    [EffectMethod]
    public async Task HandleFetchBudgets(FetchBudgetsAction action, IDispatcher dispatcher)
    {
        try
        {
            var budgets = await _budgetService.GetBudgetsByUserIdAsync(action.UserId);
            dispatcher.Dispatch(new FetchBudgetsSuccessAction { Budgets = budgets });
        }
        catch (System.Exception e)
        {
            dispatcher.Dispatch(new FetchBudgetsFailureAction { Error = e.Message });
        }
    }

    [EffectMethod]
    public async Task HandleAddOrUpdateBudget(AddOrUpdateBudgetAction action, IDispatcher dispatcher)
    {
        var result = await _budgetService.CreateOrUpdateBudgetAsync(action.Budget);

        if (result)
            dispatcher.Dispatch(new FetchBudgetsAction { UserId = action.Budget.UserId });
    }

    [EffectMethod]
    public async Task HandleDeleteBudget(DeleteBudgetAction action, IDispatcher dispatcher)
    {
        var result = await _budgetService.DeleteBudgetAsync(action.BudgetId);
        if (result)
            dispatcher.Dispatch(new FetchBudgetsAction { UserId = action.UserId });
    }
}
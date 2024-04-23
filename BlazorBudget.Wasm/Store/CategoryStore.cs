using BlazorBudget.Wasm.Abstractions;
using BlazorBudget.Wasm.Services;
using Fluxor;

namespace BlazorBudget.Wasm.Store;


// Category Feature
public class CategoryFeature : Feature<CategoryState>
{
    public override string GetName() => "Categories";

    protected override CategoryState GetInitialState() =>
        new CategoryState(isLoading: false, categories: new List<Category>(), error: null);
}

// Category State

public record CategoryState
{
    public bool IsLoading { get; init; }
    public List<Category> Categories { get; init; }
    public string Error { get; init; }

    public CategoryState(bool isLoading, List<Category> categories, string error) =>
        (IsLoading, Categories, Error) = (isLoading, categories, error);
}

// Actions

public record FetchCategoriesAction { }
public record FetchCategoriesSuccessAction { public List<Category> Categories { get; init; } }
public record FetchCategoriesFailureAction { public string Error { get; init; } }

public record AddOrUpdateCategoryAction { public Category Category { get; init; } }
public record DeleteCategoryAction { public int CategoryId { get; init; } }

// Reducers

public static class CategoryReducers
{
    [ReducerMethod]
    public static CategoryState ReduceFetchCategoriesAction(CategoryState state, FetchCategoriesAction action) =>
        state with { IsLoading = true };

    [ReducerMethod]
    public static CategoryState ReduceFetchCategoriesSuccessAction(CategoryState state, FetchCategoriesSuccessAction action) =>
        state with { IsLoading = false, Categories = action.Categories };

    [ReducerMethod]
    public static CategoryState ReduceFetchCategoriesFailureAction(CategoryState state, FetchCategoriesFailureAction action) =>
        state with { IsLoading = false, Error = action.Error };
}

// Effects

public class CategoryEffects
{
    private readonly ICategoryService _categoryService;

    public CategoryEffects(ICategoryService categoryService) =>
        _categoryService = categoryService;

    [EffectMethod]
    public async Task HandleFetchCategories(FetchCategoriesAction action, IDispatcher dispatcher)
    {
        try
        {
            var categories = await _categoryService.GetCategoriesAsync();
            dispatcher.Dispatch(new FetchCategoriesSuccessAction { Categories = categories });
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new FetchCategoriesFailureAction { Error = ex.Message });
        }
    }
}



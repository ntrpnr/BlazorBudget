using BlazorBudget.Wasm.Abstractions;

namespace BlazorBudget.Wasm.Services;

public interface ICategoryService
{
    Task<List<Category>> GetCategoriesAsync();

    Task<Category> GetCategoryByIdAsync(int categoryId);

    Task CreateCategoryAsync(Category category);

    Task UpdateCategoryAsync(Category category);

    Task DeleteCategoryAsync(int categoryId);

    Task<List<Category>> GetCategoriesByTypeAsync(CategoryType type);
}

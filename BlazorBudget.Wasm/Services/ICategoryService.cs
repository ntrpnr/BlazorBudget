using BlazorBudget.Wasm.Abstractions;
using System.Runtime.CompilerServices;

namespace BlazorBudget.Wasm.Services;

public interface ICategoryService
{
    Task<List<Category>> GetCategoriesAsync();

    Task<Category> GetCategoryByIdAsync(Guid categoryId);

    Task<string> GetNewCategoryColorAsync();

    Task CreateOrUpdateCategoryAsync(Category category);

    Task DeleteCategoryAsync(Guid categoryId);

    Task<List<Category>> GetCategoriesByTypeAsync(CategoryType type);

    Task SeedCategories();
}

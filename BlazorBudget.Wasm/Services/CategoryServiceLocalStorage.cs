using BlazorBudget.Wasm.Abstractions;
using Blazored.LocalStorage;

namespace BlazorBudget.Wasm.Services;

public class CategoryServiceLocalStorage : ICategoryService
{
    private readonly ILocalStorageService _localStorage;
    private const string CategoryKey = "categories";

    public CategoryServiceLocalStorage(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        var categories = await _localStorage.GetItemAsync<List<Category>>(CategoryKey);
        return categories ?? new List<Category>();
    }

    public async Task<Category> GetCategoryByIdAsync(int categoryId)
    {
        var categories = await GetCategoriesAsync();
        return categories.FirstOrDefault(c => c.CategoryId == categoryId);
    }

    public async Task CreateCategoryAsync(Category category)
    {
        var categories = await GetCategoriesAsync();
        categories.Add(category);
        await _localStorage.SetItemAsync(CategoryKey, categories);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        var categories = await GetCategoriesAsync();
        var index = categories.FindIndex(c => c.CategoryId == category.CategoryId);
        if (index == -1)
            return;

        categories[index] = category;
        await _localStorage.SetItemAsync(CategoryKey, categories);
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var categories = await GetCategoriesAsync();
        var category = categories.FirstOrDefault(c => c.CategoryId == categoryId);
        if (category == null)
            return;

        categories.Remove(category);
        await _localStorage.SetItemAsync(CategoryKey, categories);
    }

    public async Task<List<Category>> GetCategoriesByTypeAsync(CategoryType type)
    {
        var categories = await GetCategoriesAsync();
        return categories.Where(c => c.Type == type).ToList();
    }
}

﻿using BlazorBudget.Wasm.Abstractions;
using Blazored.LocalStorage;

namespace BlazorBudget.Wasm.Services;

public class CategoryServiceLocalStorage : ICategoryService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IColorService _colorService;
    private const string CategoryKey = "categories";

    public CategoryServiceLocalStorage(ILocalStorageService localStorage, IColorService colorService)
    {
        _localStorage = localStorage;
        _colorService = colorService;
    }

    public async Task<string> GetNewCategoryColorAsync()
    {
        var categories = await GetCategoriesAsync();
        var existingColors = categories.Select(c => c.Color).ToList();
        var color = _colorService.GetNewColor(existingColors);
        return color;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        var categories = await _localStorage.GetItemAsync<List<Category>>(CategoryKey);
        return categories ?? new List<Category>();
    }

    public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
    {
        var categories = await GetCategoriesAsync();
        return categories.FirstOrDefault(c => c.Id == categoryId);
    }

    public async Task CreateOrUpdateCategoryAsync(Category category)
    {
        var categories = await GetCategoriesAsync();
        var index = categories.FindIndex(c => c.Id == category.Id);
        if (index == -1)
        {
            categories.Add(category);
        }
        else
        {
            categories[index] = category;
        }

        await _localStorage.SetItemAsync(CategoryKey, categories);
    }

    public async Task DeleteCategoryAsync(Guid categoryId)
    {
        var categories = await GetCategoriesAsync();
        var category = categories.FirstOrDefault(c => c.Id == categoryId);
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

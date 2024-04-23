using BlazorBudget.Wasm.Abstractions;
using BlazorBudget.Wasm.Services;
using Blazored.LocalStorage;
using Moq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace BlazorBudget.UnitTest;

public class BudgetServiceLocalStorageTests
{
    private readonly Mock<ILocalStorageService> _mockLocalStorage;
    private readonly BudgetServiceLocalStorage _budgetService;
    private readonly CategoryServiceLocalStorage _categoryService;
    private readonly List<Budget> _budgets;
    private readonly Guid _budgetIdOne;

    public BudgetServiceLocalStorageTests()
    {
        _mockLocalStorage = new Mock<ILocalStorageService>();
        _categoryService = new CategoryServiceLocalStorage(_mockLocalStorage.Object);
        _budgetService = new BudgetServiceLocalStorage(_mockLocalStorage.Object, _categoryService);
        _budgetIdOne = Guid.NewGuid();
        _budgets = new List<Budget>
        {
            new Budget { BudgetId = _budgetIdOne, Month = new DateTime(2022, 1, 1), UserId = 1 },
            new Budget { Month = new DateTime(2022, 2, 1), UserId = 1 }
        };
    }

    [Fact]
    public async Task GetBudgetsByUserIdAsync_ReturnsBudgetsForUser()
    {
        // Arrange
        _mockLocalStorage.Setup(x => x.GetItemAsync<string>("budgets", CancellationToken.None))
            .ReturnsAsync(JsonSerializer.Serialize(_budgets));

        // Act
        var result = await _budgetService.GetBudgetsByUserIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _mockLocalStorage.Verify(ls => ls.GetItemAsync<string>("budgets", CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task CreateBudgetAsync_AddsNewBudget()
    {
        // Arrange
        var newBudget = new Budget { Month = new DateTime(2022, 3, 1), UserId = 1 };
        _mockLocalStorage.Setup(x => x.GetItemAsync<string>("budgets", CancellationToken.None))
            .ReturnsAsync(JsonSerializer.Serialize(_budgets));
        _mockLocalStorage.Setup(x => x.SetItemAsync("budgets", It.IsAny<string>(), CancellationToken.None))
            .Returns(new ValueTask());

        // Act
        var success = await _budgetService.CreateBudgetAsync(newBudget);

        // Assert
        Assert.True(success);
        _mockLocalStorage.Verify(ls => ls.SetItemAsync("budgets", It.IsAny<string>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task UpdateBudgetAsync_UpdatesExistingBudget()
    {
        // Arrange
        var updatedBudget = new Budget { BudgetId = _budgetIdOne, Month = new DateTime(2022, 3, 1), UserId = 1 };
        _mockLocalStorage.Setup(x => x.GetItemAsync<string>("budgets", CancellationToken.None))
            .ReturnsAsync(JsonSerializer.Serialize(_budgets));
        _mockLocalStorage.Setup(x => x.SetItemAsync("budgets", It.IsAny<string>(), CancellationToken.None))
            .Returns(new ValueTask());

        // Act
        var success = await _budgetService.UpdateBudgetAsync(updatedBudget);

        // Assert
        Assert.True(success);
        _mockLocalStorage.Verify(ls => ls.SetItemAsync("budgets", It.IsAny<string>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteBudgetAsync_DeletesBudget()
    {
        // Arrange
        _mockLocalStorage.Setup(x => x.GetItemAsync<string>("budgets", CancellationToken.None))
            .ReturnsAsync(JsonSerializer.Serialize(_budgets));
        _mockLocalStorage.Setup(x => x.SetItemAsync("budgets", It.IsAny<string>(), CancellationToken.None))
            .Returns(new ValueTask());

        // Act
        var success = await _budgetService.DeleteBudgetAsync(_budgetIdOne);

        // Assert
        Assert.True(success);
        _mockLocalStorage.Verify(ls => ls.SetItemAsync("budgets", It.IsAny<string>(), CancellationToken.None), Times.Once);
    }
}

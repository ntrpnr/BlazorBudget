# BlazorBudget

A simple budgeting app built with Blazor WebAssembly.
The purpose of this project is to have a simple front-end application that can be ran without a backend. The data is stored in the browser's local storage.


## GitHub Copilot Lab

### First steps
1. Make sure you have a newer version of Visual Studio 2022 installed.
1. Install or update the GitHub Copilot extension from the Visual Studio Marketplace. (Extensions -> Manage Extensions -> Search for "GitHub Copilot"))
1. Clone the repository and open it in Visual Studio 2022.				
1. Try to run `BlazorBudget.Wasm` project to make sure everything is working.	
1. Familyarize yourself with the application. Try to add a new budget and a new transaction.

### Copilot assignments

#### Unit tests
Open the `BudgetServiceTests.cs` file and make sure that all the tests are passing.

1. Add a new test called `GetBudgetsByUserIdAsync_LocalStorageReturnsNull_ReturnsEmptyList()` and see if Copilot can help you write the full test.
1. Come up with a new test scenario and see if Copilot can help you write the test.

#### Components
1. Open the Home.razor file
1. Add this comment on top of the page:
`
@* This page has some nice overview of the budgets and transactions. It has MudCards with elevation 2 where you can see the income and spending for each month. If there are no budgets let the user know. Inherit from FluxorComponent. *@
`
1. Press enter a few times and see if Copilot can help you write the full page. You might need to add some using statements and references to the MudBlazor components.
1. There might be missing content at the bottom of the page. Put the cursor there and press enter to generate more code.  

#### Funcionality
1. See if you can use Copilot to implement remove transation functionality in the Transactions.razor page. The user should be able to remove a transaction by clicking on the trash icon. The transaction should be removed from the list and the budget balance should be updated. Removal functionality is already implemented for budgets, so use that as reference.
1. Optional: Try to implement the remove category functionality.
1. Try to implement the Edit transaction functionality. Most of the code is already in place. 					
1. Find some other functionality that you would like to implement and see if Copilot can help you with that.

#### Visuals
1. See if you can use Copilot to improve the visuals of the application. Add containers and other MudBlazor components to make the application look better.
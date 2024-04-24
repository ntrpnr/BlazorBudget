namespace BlazorBudget.Wasm.Abstractions
{
    public record Category
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public CategoryType Type { get; set; }
        public string? Color { get; set; }
    }
}

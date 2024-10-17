public record Event()
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Logo { get; set; }
}

public class SearchCriteriaViewModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<int>? ProductIds { get; set; }
    public string? OrderStatusStartsWith { get; set; }
    public string? OrderStatusEndsWith { get; set; }
}
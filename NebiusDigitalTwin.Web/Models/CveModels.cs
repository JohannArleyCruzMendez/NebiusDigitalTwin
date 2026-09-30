public class CveQueryRequest
{
    public string Query { get; set; }
}

public class CveResponseDto
{
    public string CveCode { get; set; }
    public string Description { get; set; }
    public string Source { get; set; } // "NVD-Live" o "Local-Fallback"
}
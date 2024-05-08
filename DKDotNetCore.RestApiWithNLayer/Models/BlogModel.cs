namespace DKDotNetCore.RestApiWithNLayer.Models;

[Table("Table_Blog")]
public class BlogModel
{
    [Key]
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }

}

//public record BlogEntity(int BlogId, String BlogTitle, String BlogAuthor, String BlogContent);

//Ctrl + k + c
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKDotNetCore.ConsoleApp;

[Table("Table_Blog")]
public class BlogDto
{
    [Key]
    public int BlogId { get; set; }
    public String BlogTitle { get; set; }
    public String BlogAuthor { get; set; }
    public String BlogContent { get; set; }

}

//public record BlogEntity(int BlogId, String BlogTitle, String BlogAuthor, String BlogContent);

//Ctrl + k + c
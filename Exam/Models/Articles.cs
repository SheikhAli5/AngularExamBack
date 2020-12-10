using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam.Models
{
  public class Articles
  {
    public int ArticleId { get; set; }
    public string ArticleName { get; set; }
    public string MainText { get; set; }
    public int AuthorId { get; set; }
    public string Photo { get; set; }
  }
}

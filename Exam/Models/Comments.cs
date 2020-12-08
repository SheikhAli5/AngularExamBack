using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam.Models
{
  public class Comments
  {
    public int CommentId { get; set; }
    public int ArticleId { get; set; }
    public string MainText { get; set; }
    public string CommentName { get; set; }
  }
}

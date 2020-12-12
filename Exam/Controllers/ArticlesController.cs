using Exam.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Exam.Controllers
{
    public class ArticlesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                select ArticleId, ArticleName, MainText, AuthorId, Photo from dbo.Articles
            ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["ArticlesDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(Articles articles)
        {
            try
            {
                string query = @"insert into dbo.Articles values (
                '" + articles.ArticleName + @"',
                '" + articles.MainText + @"',
                '" + articles.AuthorId + @"',
                '" + articles.Photo + @"'
                )";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["ArticlesDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully";
            }
            catch (Exception)
            {

                return "Failed to Add";
            }
        }
        public string Put(Articles articles)
        {
            try
            {
                string query = @"
                update dbo.Articles set
                ArticleName= '" + articles.ArticleName + @"'
                ,MainText= '" + articles.MainText + @"'
                ,AuthorId= '" + articles.AuthorId + @"'
                ,Photo= '" + articles.Photo + @"'
                where ArticleId = " + articles.ArticleId + @"
      
            ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["ArticlesDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully";
            }
            catch (Exception)
            {

                return "Failed to Update";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @"
                delete from dbo.Articles 
                where ArticleId = " + id + @"
      
            ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["ArticlesDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully";
            }
            catch (Exception)
            {

                return "Failed to Delete";
            }
        }
        [Route("api/Articles/AllAuthors")]
        [HttpGet]
        public HttpResponseMessage AllAuthors()
        {
            string query = @"
                select AuthorName, AuthorId from dbo.Authors";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["ArticlesDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);

        }
        [Route("api/Articles/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                postedFile.SaveAs(physicalPath);
                return filename;

            }
            catch (Exception)
            {
                return "something.png";
            }
        }
    }
}

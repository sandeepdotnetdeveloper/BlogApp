using BlogApp.Models;
using System.Text.Json;

namespace BlogApp.Services
{
    public class BlogService
    {
        private const string FilePath = "blogs.json";
        public List<Blog> GetAllBlogs()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Blog>();
            }

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Blog>>(json) ?? new List<Blog>();
        }

        public void AddBlog(Blog blog)
        {
            var blogs = GetAllBlogs();
            blog.Id = blogs.Any() ? blogs.Max(b => b.Id) + 1 : 1;
            blog.CreatedAt = DateTime.UtcNow;
            blogs.Add(blog);
            SaveBlogsToFile(blogs);
        }

        private void SaveBlogsToFile(List<Blog> blogs)
        {
            var json = JsonSerializer.Serialize(blogs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}

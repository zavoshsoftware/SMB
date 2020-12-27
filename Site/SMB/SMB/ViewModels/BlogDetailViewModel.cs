using System.Collections.Generic;
using Models;

namespace ViewModels
{
    public class BlogDetailViewModel : _BaseViewModel
    {
        public Blog Blog { get; set; }
        public List<Blog> RelatedBlogs { get; set; }
        public List<BlogComment> BlogComments { get; set; }
    }
}
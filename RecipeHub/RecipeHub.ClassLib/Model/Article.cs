using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.ClassLib.Model
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }
}

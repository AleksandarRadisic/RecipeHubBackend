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
        public double Rating { get; set; }

        public void CalculateRating()
        {
            if (Comments == null || Comments.Count() == 0)
            {
                Rating = 0;
            }

            double rating = 0;
            int commentCount = 0;
            foreach (var comment in Comments)
            {
                if (comment.Report == null || !comment.Report.BlockApproved)
                {
                    commentCount++;
                    rating += comment.Rating;
                }
            }

            if (commentCount == 0) Rating = 0;
            Rating = rating / commentCount;
        }
    }
}

using PetShopProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace PetShopProject.DAL
{
    public class CommentRepository : IRepository<Comment>
    {
        private AnimalContext context;
        public CommentRepository(AnimalContext context)
        {
            this.context = context;
        }


        public Comment? GetByID(int id)
        {
            Comment? comment = GetAll().FirstOrDefault(x => x.Id == id);

            return comment;

        }

        public IEnumerable<Comment> GetAll() => context.Comments.ToList();

        public void Delete(int id)
        {
            Comment? comment = GetByID(id);
            if (comment != null)
            {
                context.Comments.Remove(comment);
                Save();
            }

        }

        public void Insert(Comment comment)
        {
            context.Comments.Add(comment);
            Save();
        }

        public void Update(Comment comment)
        {
            var commentInDb = GetByID(comment.Id);
            if (commentInDb != null)
            {
                commentInDb.UserName = comment.UserName;
                commentInDb.Text = comment.Text;
                commentInDb.UserIcon = comment.UserIcon;
                context.Comments.Update(commentInDb);
                Save();
            }
        }

        public void Save() => context.SaveChanges();


    }
}

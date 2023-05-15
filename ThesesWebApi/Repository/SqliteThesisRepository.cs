using Microsoft.EntityFrameworkCore;
using ThesesWebApi.Data;
using ThesesWebApi.Dto;
using ThesesWebApi.Interfaces;
using ThesesWebApi.Models;

namespace ThesesWebApi.Repository
{
    public class SqliteThesisRepository : IThesisRepository
    {
        private readonly DataContext _context;

        public SqliteThesisRepository(DataContext context)
        {
            _context = context;
        }
        public bool ThesisExists(int id)
        {
            return _context.Theses.Any(t => t.Id == id);
        }
        public bool Add(Thesis newThesis)
        {
            _context.Add(newThesis);
            return Save();
        }
        public bool Delete(Thesis thesis)
        {
            _context.Remove(thesis);
            return Save();
        }
        public ICollection<Thesis> GetAllTheses()
        {
            return _context.Theses.Include(t => t.MainAuthor).ToList();
        }
        public Thesis GetThesis(int id)
        {
            return _context.Theses.Include(t => t.MainAuthor).
                                   Include(t => t.OtherAuthors).Where(t => t.Id == id).FirstOrDefault();
        }
        public bool Update(int Id, ThesisDto newThesis)
        {
            var updatedThesis = GetThesis(Id);
            updatedThesis.MainAuthor = newThesis.MainAuthor;
            updatedThesis.OtherAuthors = newThesis.OtherAuthor;
            updatedThesis.Topic = newThesis.Topic;
            updatedThesis.ContactEmail = newThesis.ContactEmail;
            updatedThesis.Content = newThesis.Content;
            updatedThesis.Updated = DateTime.Now;

            _context.Update(updatedThesis);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}

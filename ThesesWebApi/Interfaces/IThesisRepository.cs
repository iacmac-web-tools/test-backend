using ThesesWebApi.Dto;
using ThesesWebApi.Models;

namespace ThesesWebApi.Interfaces
{
    public interface IThesisRepository
    {
        ICollection<Thesis> Search(string searchTerm);
        ICollection<Thesis> GetAllTheses();
        Thesis GetThesis(int id);
        bool Update(int Id, ThesisDto newThesis);
        bool Add(Thesis newThesis);
        bool Delete(Thesis thesis);
        public bool ThesisExists(int id);
    }
}

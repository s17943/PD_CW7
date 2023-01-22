using EF_1.Models;
using EF_1.Models.DTO;

namespace EF_1.Services
{
    public interface IDbService 
    {
        public List<Trip> GetTripList();
        public void DeleteClient(DeleteRequest request);
        public InsertRequest InsertClient(InsertRequest request);
    }
}

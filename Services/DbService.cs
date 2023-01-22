using EF_1.Models;
using EF_1.Models.DTO;
using EF_1.Services;


namespace EF_1.Services
{
    public class DbService : IDbService
    {
        private S17943Context _context;
        public DbService(S17943Context context)
        {
            _context = context;
        }
        public List<Trip> GetTripList()
        {
            List<Trip> TripList = _context.Trips.ToList();
            return TripList;
        }
        public void DeleteClient(DeleteRequest request)
        {
            var client = _context.Clients.Find(request.id);
            if (client == null)
            {
                throw new InvalidOperationException();
            }
            _context.Remove(client);
            _context.SaveChanges();
        }
        public InsertRequest InsertClient(InsertRequest request)
        {
            var trip = _context.Trips.FirstOrDefault(t => t.IdTrip == request.IdTrip);
            if (trip == null) { throw new InvalidOperationException(); ; }
            var pesel = _context.Clients.FirstOrDefault(c => c.Pesel == request.Pesel);
            if (pesel == null)
            {
                var Client = new Client()
                {
                    IdClient = _context.Clients.Max(cl => cl.IdClient) + 1,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Telephone = request.Telephone,
                    Pesel = request.Pesel
                };
                _context.Clients.Add(Client);
            };
            var client = _context.Clients.FirstOrDefault(c => c.Pesel == request.Pesel);
            if (_context.ClientTrips.FirstOrDefault(c => c.IdClient == client.IdClient && c.IdTrip == request.IdTrip) != null)
            {
                throw new InvalidOperationException();
            }
            var clientTrip = _context.ClientTrips.Where(clt => clt.IdTrip == trip.IdTrip).FirstOrDefault();
            if (clientTrip == null)
            {
                clientTrip = new ClientTrip()
                {
                    IdTrip = trip.IdTrip,
                    PaymentDate = DateTime.Now
                };
                _context.ClientTrips.Add(clientTrip);
            }

            return request;


        }
    }
}

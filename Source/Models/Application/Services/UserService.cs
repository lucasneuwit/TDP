using TDP.Models.Persistence;

namespace TDP.Models.Application.Services
{
    public class UserService
    {
        private readonly TdpDbContext _context;
        public UserService(TdpDbContext context) {

            context = context;
        }

        public async Task<Domain.User> GetUser(string username)
        {
            var user = _context.Set<Domain.User>().Where(usr => usr.Username.Equals(username)).FirstOrDefault();
            return user;
        }


    }
}

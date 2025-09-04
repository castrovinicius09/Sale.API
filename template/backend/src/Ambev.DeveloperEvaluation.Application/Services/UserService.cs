using Ambev.DeveloperEvaluation.Application.Services.Abstractions;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class UserService : IUserService
    {
        public void ValidateUser(Guid userId)
        {
            if (false)
                throw new NotImplementedException();
        }
    }
}

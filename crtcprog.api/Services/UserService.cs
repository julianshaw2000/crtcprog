using AutoMapper;
using crtcprog.api.DTO;
using ctrcprog.api.Data;
using ctrcprog.api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace crtcprog.api.Services
{
    public interface IUserService
    { 
        Task<User> RegisterAsync(UserDto userModel);
        Task<bool> ResetPasswordAsync(string email);
        Task<bool> CheckEmailExistsAsync(string email);

        Task<User> GetUserByEmailAsync(string email);

    }




    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        // Consider injecting other services like email service, encryption service, etc.

        public UserService(IRepository<User> userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userRepository.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }


         



        public async Task<User> RegisterAsync(UserDto userModel)
        {
            // Implement registration logic. For example, check if the user already exists,
            // hash the password, and then save the user to the database.
            var userExists = await _userRepository.AnyAsync(u => u.Email.ToLower() == userModel.Email.ToLower());

            if (userExists)
                return null;

            var newUser = new User();

            newUser = _mapper.Map<User>(userModel);

            await _userRepository.InsertAsync(newUser);
            await _userRepository.SaveAsync();

            return newUser;
        }

        public async Task<bool> ResetPasswordAsync(string email)
        {
            // Implement password reset logic. For example, verify the email,
            // generate a reset token, send an email with the token, etc.
            var user = await _userRepository.FindAsync(u => u.Email == email);

            if (user == null)
                return false;

            // Generate a reset token and email it to the user

            return true;
        }
         

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.", nameof(email));
            }

            var user = await _userRepository.FindAsync(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            return user.FirstOrDefault();
        }
         

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crtcprog.api.Services
{
    public class AuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        //public async Task<bool> AuthenticateAsync(string email, string plainTextPassword)
        //{
        //    var user = await _userService.GetUserByEmailAsync(email);
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return _userService.VerifyUserPassword(plainTextPassword, user.Password);
        //}
    }
}
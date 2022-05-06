using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Persistence.Repositories;
using TeContrato.API.Domain.Services;
using TeContrato.API.Domain.Services.Communications;
using TeContrato.API.Settings;
using BCryptNet = BCrypt.Net;


namespace TeContrato.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AccountService(IOptions<AppSettings> appSettings, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            AuthenticationResponse response;
            var users = _userRepository.ListAsync();
            var user = users.Result.SingleOrDefault(x => x.Temail == request.Email);
            if (user ==null)
            {
                return new AuthenticationResponse("this email doesn't correspond to any user");
            }
            
            if (!BCryptNet.BCrypt.Verify(request.Password, user.Cpassword))
            {
                return new AuthenticationResponse("Invalid password for this user");
            }
            
            response = _mapper.Map<User, AuthenticationResponse>(user);
            response.Token = GenerateJwtToken(response.Cuser.ToString());
            return response;
        }
        
        private string GenerateJwtToken(string value)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, value)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
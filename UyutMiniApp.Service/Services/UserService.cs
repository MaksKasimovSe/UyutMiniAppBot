using Mapster;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Users;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class UserService(IGenericRepository<User> genericRepository, IGenericRepository<Order> orderRepository, IGenericRepository<Basket> basketRepository, IConfiguration configuration) : IUserService
    {
        public async Task AddAsync(CreateUserDto dto)
        {
            var existUser = await genericRepository.GetAsync(u => u.TelegramUserId == dto.TelegramUserId || u.PhoneNumber == dto.PhoneNumber);

            if (existUser != null)
                throw new HttpStatusCodeException(400, "Phone number or telegram account already registered");
            
            var newUser = await genericRepository.CreateAsync(dto.Adapt<User>());

            var basket = new Basket()
            {
                UserId = newUser.Id
            };

            await basketRepository.CreateAsync(basket);
            await genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var isDeleted = await genericRepository.DeleteAsync(u => u.Id == id);
            if (!isDeleted)
                throw new HttpStatusCodeException(404, "User not found");
            await genericRepository.SaveChangesAsync();
        }

        public async Task<string> GenerateToken(long telegramUserId)
        {
            var existUser =
                await genericRepository.GetAsync(u => u.TelegramUserId == telegramUserId);
            if (existUser is null)
                throw new HttpStatusCodeException(404, "User not found");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] tokenKey = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", existUser.Id.ToString()),
                    new Claim("TelegramUserId", existUser.TelegramUserId.ToString()),
                    new Claim(ClaimTypes.Role, Enum.GetName(existUser.Role))
                }),
                Expires = DateTime.UtcNow.AddMonths(int.Parse(configuration["Jwt:lifetime"])),
                Issuer = configuration["Jwt:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public async Task<ViewUserDto> GetAsync(long telegramUserId)
        {
            var user = await genericRepository.GetAsync(u => u.TelegramUserId == telegramUserId, includes: ["Basket"]);
            if (user is null)
                throw new HttpStatusCodeException(404, "User not found");
            int count = orderRepository.GetAll(false,o => o.UserId == user.Id).Count();
            var dto = user.Adapt<ViewUserDto>();
            if (count == 0)
                dto.HasOrders = false;
            else
                dto.HasOrders = true;
            
            return dto;
        }
    }
}

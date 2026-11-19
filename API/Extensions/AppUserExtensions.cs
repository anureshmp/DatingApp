using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class AppUserExtensions
{
    public static UserDTO ToDTO(this AppUser user, ITokenService tokenService)
    {
            return new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Id = user.Id,
                Token = tokenService.CreateToken(user)
            };
        
    }
}

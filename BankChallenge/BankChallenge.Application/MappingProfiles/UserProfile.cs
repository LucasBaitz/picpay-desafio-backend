using AutoMapper;
using BankChallenge.Application.DTOs.UserDTOs;
using BankChallenge.Domain.Models;

namespace BankChallenge.Application.MappingProfiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<UserCreateAccountDTO, User>()
				.ForMember(user => user.Balance,
					opt => opt.MapFrom(userDto => userDto.InitialBalance));

			CreateMap<UserLoginDTO, User>();
		}
	}
}

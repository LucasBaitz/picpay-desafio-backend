using AutoMapper;
using BankChallenge.Application.DTOs.TransactionDTOs;
using BankChallenge.Domain.Models;

namespace BankChallenge.Application.MappingProfiles
{
	public class AccountTransactionProfile : Profile
	{
		public AccountTransactionProfile()
		{
			CreateMap<MakeTransactionDTO, AccountTransaction>()
				.ForMember(transaction => transaction.SenderId,
					opt => opt.MapFrom(dto => dto.SenderId));

			CreateMap<AccountTransaction, ReadMadeTransactionDTO>()
				.ForMember(dto => dto.Sender,
					opt => opt.MapFrom(transaction => transaction.Sender.FullName))
				.ForMember(dto => dto.Receiver,
					opt => opt.MapFrom(transaction => transaction.Receiver.FullName));
		}
	}
}

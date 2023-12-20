using System.Runtime.Serialization;

namespace BankChallenge.Domain.Enums
{
	public enum AccountType
	{
		[EnumMember(Value = "Individual")]
		Individual = 0,

		[EnumMember(Value = "Bussiness")]
		Bussiness = 1,
	}
}

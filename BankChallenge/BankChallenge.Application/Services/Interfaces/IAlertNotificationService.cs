using BankChallenge.Domain.Models;

namespace BankChallenge.Application.Services.Interfaces
{
	public interface IAlertNotificationService
	{
		Task SendUserNotification(User user, string message);
	}
}

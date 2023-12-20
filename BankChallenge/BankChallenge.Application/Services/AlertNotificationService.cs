using BankChallenge.Application.DTOs.AlertDTOs;
using BankChallenge.Application.Services.Interfaces;
using BankChallenge.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BankChallenge.Application.Services
{
	public class AlertNotificationService : IAlertNotificationService
	{
		private const string UserNotificationApiUrl = "https://run.mocky.io/v3/54dc2cf1-3add-45b5-b5a9-6bf7e7f1f4a6";
		private readonly ILogger<AlertNotificationService> _logger;
		public AlertNotificationService(ILogger<AlertNotificationService> logger)
		{
			_logger = logger;
		}

		public async Task SendUserNotification(User user, string message)
		{
			using (HttpClient client = new HttpClient())
			{
				SendNotificationDTO notificationDto = new(user.Email, message);
				HttpResponseMessage response = await client.PostAsJsonAsync(UserNotificationApiUrl, notificationDto);

				if (!response.IsSuccessStatusCode)
				{
					_logger.LogError($"Notification for {user.FullName} failed.");
					return;
				}

				_logger.LogInformation($"Notification sent to {user.FullName} at {user.Email}");
			}
		}
	}
}

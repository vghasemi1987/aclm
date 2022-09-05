using DomainApplication.Specifications;
using DomainContracts.Commons;
using DomainEntities.EmailAggregate;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DomainApplication.Services.EmailAggregate
{
	public class EmailService : IEmailService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<EmailOutbox> _repository;
		private readonly IEmailSender _emailSender;
		public EmailService(IUnitOfWork unitOfWork,
			IEmailSender emailSender,
			IRepository<EmailOutbox> repository)
		{
			_unitOfWork = unitOfWork;
			_emailSender = emailSender;
			_repository = repository;
		}

		public async Task SaveAsync(EmailOutbox model)
		{
			if (model.Id > 0)
			{
				_repository.Update(model);
			}
			else
			{
				_repository.Add(model);
			}
			await _unitOfWork.SaveAsync();
		}

		public async Task BackgroundEmailSendAsync()
		{
			try
			{
				var filter = new EmailFilterSpecification(false);
				var emailList = _repository.List(filter);
				if (emailList.Any())
				{
					foreach (var email in emailList)
					{

						var fileAddress = string.Empty;
						await _emailSender.SendEmailAsync(email.Email, email.Subject, email.Message, fileAddress);
						email.IsSent = true;
						_repository.Update(email, o => o.IsSent);
					}
					await _unitOfWork.SaveAsync();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public async Task SendEmailConfirmationAsync(string email, string link)
		{
			var emailObject = new EmailOutbox
			{
				Email = email,
				Message =
					$"لطفا با کلیک برروی لینک مقابل ایمیل خود را تایید کنید: <a href='{HtmlEncoder.Default.Encode(link)}'>تایید ایمیل</a>",
				Subject = "تایید ایمیل",
				UserId = 0,
				IsSent = false
			};
			await SaveAsync(emailObject);
		}
		public async Task SendResetPasswordAsync(string email, string link)
		{
			var emailObject = new EmailOutbox
			{
				Email = email,
				Message = $"جهت تغییر کلمه عبور خود لینک مقابل را دنبال نمایید: <a href='{HtmlEncoder.Default.Encode(link)}'>تغییر کلمه عبور</a>",
				Subject = "کلمه عبور خود را فراموش کرده اید؟",
				UserId = 0,
				IsSent = false
			};
			await SaveAsync(emailObject);
		}
	}
}
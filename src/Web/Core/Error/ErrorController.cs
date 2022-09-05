using Infrastructure.Data.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Web.Core.Error
{
	[Route("[controller]/[action]")]
	public class ErrorController : Controller
	{
		private readonly ServerAccessibilityMonitorContext _crmContext;
		public ErrorController(ServerAccessibilityMonitorContext crmContext)
		{
			_crmContext = crmContext;
		}
		[HttpGet("{status}")]
		public IActionResult Code(string status)
		{
			switch (status)
			{
				case "403":
					return View(new Tuple<string, string>("403", "شماه اجازه دسترسی به این بخش را ندارید!"));
				case "404":
					return View(new Tuple<string, string>("404", "صفحه درخواست شده وجود ندارد!"));
			}
			return View(new Tuple<string, string>("400", "خطای غیرمنتظره ای رخ داده است!"));
		}

		public string AddDb()
		{
			_crmContext.Database.Migrate();
			return "OK!";
		}

		public string GetDbScript()
		{
			//var miglist = _crmContext.Database.GetPendingMigrations();
			//return string.Join(", ", miglist) + "\n" + _crmContext.Database.GenerateCreateScript();
			string script = string.Empty;
			var migrations = _crmContext.Database.GetPendingMigrations();
			if (migrations.Any())
			{
				//var scriptor = new MigratorScriptingDecorator(migrator);
				//script = scriptor.ScriptUpdate(null, migrations.Last());
				//if (!String.IsNullOrEmpty(script))
				//{
				//    _crmContext.Database.ExecuteSqlCommand(script);
				//}
			}

			return script;
		}
	}
}
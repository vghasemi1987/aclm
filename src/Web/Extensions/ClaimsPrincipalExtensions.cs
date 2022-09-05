using DomainEntities.ApplicationUserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Web.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string DecryptString(this string cipherString)
		{
			bool useHashing = true;
			byte[] keyArray;
			byte[] toEncryptArray = Convert.FromBase64String(cipherString);
			byte[] resultArray;
			string key = string.Empty;

			//key = ConfigurationManager.AppSettings.Get("SecurityKey"); 
			key = "MAKV2SPBNI99212";


			if (useHashing)
			{
				MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
				keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
				hashmd5.Clear();
			}
			else
			{
				keyArray = UTF8Encoding.UTF8.GetBytes(key);
			}

			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
			tdes.Key = keyArray;
			tdes.Mode = CipherMode.ECB;
			tdes.Padding = PaddingMode.PKCS7;


			ICryptoTransform cTransform = tdes.CreateDecryptor();
			resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);


			tdes.Clear();


			return UTF8Encoding.UTF8.GetString(resultArray);

		}
		public static int GetUserId(this ClaimsPrincipal principal)
		{
			return principal?.FindFirst(ClaimTypes.NameIdentifier) == null ? 0 : int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
		}
		public static string GetUserFullName(this ClaimsPrincipal principal)
		{
			return principal == null ? string.Empty : principal.FindFirst(ClaimTypes.GivenName)?.Value;
		}
		public static string GetUserPicture(this ClaimsPrincipal principal)
		{
			return principal == null ? string.Empty : principal.FindFirst(ClaimTypes.UserData)?.Value;
		}
		public static int? GetDepartmentId(this ClaimsPrincipal principal)
		{
			var departmentId = principal.FindFirst("DepartmentId")?.Value;
			return string.IsNullOrEmpty(departmentId) ? (int?)null : int.Parse(departmentId);
		}
		public static int? GetOrganizationalChart(this ClaimsPrincipal principal)
		{
			var chartType = principal.FindFirst("ChartType")?.Value;
			return string.IsNullOrEmpty(chartType) ? (int?)null : int.Parse(chartType);
		}
		public static bool IsImpersonating(this ClaimsPrincipal principal)
		{
			return principal != null && principal.HasClaim("IsImpersonating", "true");
		}
	}
	public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
	{
		public UserClaimsPrincipalFactory(
			UserManager<ApplicationUser> userManager,
			RoleManager<ApplicationRole> roleManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, roleManager, optionsAccessor)
		{
		}
		protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
		{
			var identity = await base.GenerateClaimsAsync(user);
			identity.AddClaims(new List<Claim>
			{
				new Claim(ClaimTypes.GivenName, user.Name ?? string.Empty),
				new Claim(ClaimTypes.UserData, user.Picture ?? "no_image.png"),
				new Claim("DepartmentId", user.DepartmentId.HasValue? user.DepartmentId.Value.ToString():string.Empty),
				new Claim("PositionId", user.PositionId.HasValue? user.PositionId.Value.ToString():string.Empty)
			});
			return identity;
		}
	}
}
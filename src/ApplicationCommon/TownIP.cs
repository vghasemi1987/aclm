using System;
using System.Net;
using System.Net.Sockets;

namespace ApplicationCommon
{
	public static class TownIP
	{
		public static string TownLogin(string ip)
		{
			switch (ip)
			{
				case "10.20":
					return "خراسان رضوی";
				case "10.21":
					return "اصفهان";
				case "10.22":
					return "آذربایجان شرقی";
				case "10.23":
					return "آذربایجان غربی";
				case "10.24":
					return "هرمزگان";
				case "10.25":
					return "مازندران";
				case "10.26":
					return "یزد";
				case "10.27":
					return "کرمانشاه";
				case "10.28":
					return "مرکزی";
				case "10.29":
					return "سیستان و بلوچستان";
				case "10.30":
					return "قم";
				case "10.31":
					return "همدان";
				case "10.32":
					return "قزوین";
				case "10.33":
					return "زنجان";
				case "10.34":
					return "گیلان";
				case "10.35":
					return "فارس";
				case "10.36":
					return "کرمان";
				case "10.37":
					return "اردبیل";
				case "10.38":
					return "گلستان";
				case "10.39":
					return "سمنان";
				case "10.40":
					return "کردستان";
				case "10.41":
					return "چهارمحال بختیاری";
				case "10.42":
					return "کهگیلویه و بویراحمد";
				case "10.43":
					return "بوشهر";
				case "10.44":
					return "ایلام";
				case "10.45":
					return "لرستان";
				case "10.46":
					return "خوزستان";
				case "10.47":
					return "البرز";
				case "10.48.46":
					return "تهران 3";
				case "10.49":
					return "تهران 1";
				case "10.50":
					return "خراسان جنوبی";
				case "10.51":
					return "خراسان شمالی";
				case "10.54":
					return "تهران 2";
				case "10.81.1.1":
					return "ادارات 1";
				case "10.48.28":
					return "ادارات 2";
				case "::1":
					return "داخلی";
				default:
					return "داخلی";
			}
		}
		public static string ValidateIPv4(string ipString)
		{
			if (ipString.Contains("::")) return "داخلی";
			try
			{
				if (!ipString.StartsWith("10.48.46"))
				{
					string[] str = ipString.Split('.');
					string sendIp = $"{str[0]}.{str[1]}";
					return TownLogin(sendIp);
				}
				else
				{
					string[] str = ipString.Split('.');
					string exc = $"{str[0]}.{str[1]}.{str[2]}";
					return TownLogin(exc);
				}
			}
			catch (Exception ex)
			{
				return "";
			}
		}
		public static string CompareIpAddress(string ipAddress)
		{
			IPAddress ip1 = IPAddress.Parse("123.123.0.1");
			IPAddress ip2 = IPAddress.Parse("123.123.0.2");
			var result = "123.123.0.1".StartsWith("123.123.0.0");
			if (ip1.Equals(ip2))
			{
				return ipAddress;
			}
			return "";
		}
	}
	public class IPAddressRange
	{
		readonly AddressFamily addressFamily;
		readonly byte[] lowerBytes;
		readonly byte[] upperBytes;

		public IPAddressRange(IPAddress lowerInclusive, IPAddress upperInclusive)
		{
			// Assert that lower.AddressFamily == upper.AddressFamily

			this.addressFamily = lowerInclusive.AddressFamily;
			this.lowerBytes = lowerInclusive.GetAddressBytes();
			this.upperBytes = upperInclusive.GetAddressBytes();
		}

		public bool IsInRange(IPAddress address)
		{
			if (address.AddressFamily != addressFamily)
			{
				return false;
			}

			byte[] addressBytes = address.GetAddressBytes();

			bool lowerBoundary = true, upperBoundary = true;

			for (int i = 0; i < this.lowerBytes.Length &&
				(lowerBoundary || upperBoundary); i++)
			{
				if ((lowerBoundary && addressBytes[i] < lowerBytes[i]) ||
					(upperBoundary && addressBytes[i] > upperBytes[i]))
				{
					return false;
				}

				lowerBoundary &= (addressBytes[i] == lowerBytes[i]);
				upperBoundary &= (addressBytes[i] == upperBytes[i]);
			}

			return true;
		}
	}
}
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace Web.Commons
{
	public static class File
	{
		public static void SaveCsvFile(string webRoot, IFormFile file)
		{
			if (file.Length <= 0)
				return;
			var filePath = Path.Combine($@"{webRoot}\uploads\user-excel", file.FileName);

			if (System.IO.File.Exists(filePath))
				System.IO.File.Delete(filePath);

			using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
			{
				file.CopyTo(fileStream);
			}
		}
		public static string SaveFile(string path, IFormFile file)
		{
			if (file.Length <= 0)
				return "";

			var fileName = file.FileName.Split('.');
			path += fileName.First() + Guid.NewGuid() + "." + fileName.Last();

			using (FileStream stream = new FileStream(path, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return path;
		}
	}
}

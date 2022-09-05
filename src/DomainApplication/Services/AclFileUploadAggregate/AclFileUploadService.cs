using DomainContracts.AccessibilityAggregate;
using DomainContracts.AclFilesUploadAggregate;
using DomainContracts.Commons;
using DomainEntities.AclFilesRecordAggregate;
using DomainEntities.AclFilesUploadAggregate;
using DomainEntities.Commons;
using DomainEntities.InvalidFileItemAggregate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DomainApplication.Services.AclFileUploadAggregate
{
	public class AclFileUploadService : IAclFileUploadService
	{
		private readonly IAclFilesUploadRepository _aclFilesUploadRepository;
		private readonly IAccessibilityRepository _accessibilityRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AclFileUploadService(IAclFilesUploadRepository aclFilesUploadRepository,
			IAccessibilityRepository accessibilityRepository, IUnitOfWork unitOfWork)
		{
			_aclFilesUploadRepository = aclFilesUploadRepository;
			_accessibilityRepository = accessibilityRepository;
			_unitOfWork = unitOfWork;
		}
		public async Task UploadAclFile(AclFilesUpload model, IFormFile file)
		{
			string uploadFileContent = GetUploadedFile(file);
			_accessibilityRepository.DeleteByRouterId(model.RouterId.Value);
			_aclFilesUploadRepository.Add(model);
			CreateAclRecordFile(model, uploadFileContent, model.RouterId);
			await _unitOfWork.SaveAsync();
		}


		#region Private Methods
		private void CreateAclRecordFile(AclFilesUpload model, string fileContent, int? routerId)
		{
			var invalidItemsList = new List<InvalidFileItem>();
			var actionList = new List<string> { "permit", "deny" };
			var protocolList = new List<string> { "tcp", "icmp", "ip", "udp" };
			var ipList = new List<string> { "any", "host" };
			var permitList = new List<AclFilesRecordIndexDto>();
			var denyList = new List<AclFilesRecordIndexDto>();
			var uploadFileContent = fileContent.Replace("\r", "").Split('\n');

			for (int j = 0; j < uploadFileContent.Length; j++)
			{
				bool isInvalidItem = false;
				var line = uploadFileContent[j].ToLower();
				try
				{
					if (string.IsNullOrEmpty(line)) continue;

					var lineContent = line.Split(new char[0]);
					if (lineContent.Any(s => string.IsNullOrWhiteSpace(s)))
					{
						lineContent = lineContent.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToArray();
					}
					if (lineContent.Length < 6 || lineContent[0] != "access-list")
					{
						isInvalidItem = true;
						goto InvalidItem;
					}

					int i = 0;
					var accesslist = lineContent[i++];
					var constNum = lineContent[i++];

					#region Action
					string action = string.Empty;
					if (actionList.Any(p => p == lineContent[i]))
						action = lineContent[i++];
					else
					{
						isInvalidItem = true;
						goto InvalidItem;
					}
					#endregion

					#region Protocol
					string protocol = string.Empty;
					if (protocolList.Any(p => p == lineContent[i]))
						protocol = lineContent[i++];
					else
					{
						isInvalidItem = true;
						goto InvalidItem;
					}
					#endregion

					#region SourceIp
					string sourceIp = string.Empty, sourceIp2 = string.Empty;
					if (lineContent.Length > i)
					{
						if (ipList.Any(p => p == lineContent[i]))
						{
							sourceIp = lineContent[i++];
							if (sourceIp == "host")
							{
								if (IsIpValid(lineContent[i]))
									sourceIp2 = lineContent[i++];
								else
								{
									isInvalidItem = true;
									goto InvalidItem;
								}
							}
						}
						else if (IsIpValid(lineContent[i]))
						{
							sourceIp = lineContent[i++];
							if (IsIpValid(lineContent[i]))
								sourceIp2 = lineContent[i++];
						}
						else
						{
							isInvalidItem = true;
							goto InvalidItem;
						}
					}
					#endregion

					#region Source Port
					string sourcePort = string.Empty;
					if (lineContent.Length > i && lineContent[i] == "eq")
					{
						i++;
						sourcePort = lineContent[i++];
					}
					#endregion

					#region DestinationIp
					string destinationIp = string.Empty, destinationIp2 = string.Empty;
					if (lineContent.Length > i)
					{
						if (ipList.Any(p => p == lineContent[i]))
						{
							destinationIp = lineContent[i++];
							if (destinationIp == "host")
							{
								if (IsIpValid(lineContent[i]))
									destinationIp2 = lineContent[i++];
								else
								{
									isInvalidItem = true;
									goto InvalidItem;
								}
							}
						}
						else if (IsIpValid(lineContent[i]))
						{
							destinationIp = lineContent[i++];
							if (IsIpValid(lineContent[i]))
								destinationIp2 = lineContent[i++];
						}
						else
						{
							isInvalidItem = true;
							goto InvalidItem;
						}
					}
					#endregion

					#region Destination Port
					string destinationPort = string.Empty;
					if (lineContent.Length > i && lineContent[i] == "eq")
					{
						i++;
						destinationPort = lineContent[i++];
					}
					#endregion
					if (action == "permit")
					{
						permitList.Add(new AclFilesRecordIndexDto
						{
							RouterNo = constNum,
							Action = action,
							Protocol = protocol,
							SourceIp = sourceIp,
							SourceIp2 = sourceIp2,
							SourcePort = sourcePort,
							DestinationIp = destinationIp,
							DestinationIp2 = destinationIp2,
							DestinationPort = destinationPort,
							RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
							AclFilesUpload = model,
							Index = j,
						});
					}
					else if (action == "deny")
					{
						denyList.Add(new AclFilesRecordIndexDto
						{
							RouterNo = constNum,
							Action = action,
							Protocol = protocol,
							SourceIp = sourceIp,
							SourceIp2 = sourceIp2,
							SourcePort = sourcePort,
							DestinationIp = destinationIp,
							DestinationIp2 = destinationIp2,
							DestinationPort = destinationPort,
							RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
							AclFilesUpload = model,
							Index = j,
						});
					}


				InvalidItem:
					if (isInvalidItem)
					{
						AddInvalidItem(model, j, line);
						continue;
					}
				}
				catch
				{
					AddInvalidItem(model, j, line);
				}
			}
			foreach (var permit in permitList)
			{
				try
				{
					//تست
					/*if (permit.SourceIp=="10.19.128.0" && permit.DestinationIp2== "10.15.0.44")
                    {

                    }*/
					//بر اساس DestinationPort
					if (denyList.Any(d => d.Index < permit.Index && permit.DestinationPort != "" && d.DestinationIp == "" && d.DestinationPort == permit.DestinationPort)) { }
					//بر اساس SourceIp2
					else if (denyList.Any(d => d.Index < permit.Index && d.DestinationIp == "any" && d.SourceIp == "host" && d.SourceIp2 == permit.SourceIp2)) { }
					//بر اساس SourceIp و SourceIp2
					else if (denyList.Any(d => d.Index < permit.Index && d.DestinationIp == "any" && d.SourceIp != "host" && permit.SourceIp == "host" &&
						IsInRange(d.SourceIp, d.SourceIp2.Replace("0.0.0.", d.SourceIp.Replace(".0", ".")), permit.SourceIp2))) { }
					//بر اساس DestinationIp2 و DestinationPort
					else if (denyList.Any(d => d.Index < permit.Index && d.DestinationIp == "host" && d.SourceIp == "any" && d.DestinationIp2 == permit.DestinationIp2 &&
						(d.DestinationPort == permit.DestinationPort || d.DestinationPort == ""))) { }
					//بر اساس DestinationIp و DestinationIp2 و DestinationPort
					else if (denyList.Any(d => d.Index < permit.Index && d.DestinationIp == "host" && d.SourceIp != "any" &&
						d.SourceIp2.Contains("0.0.0.") &&
						IsInRange(d.SourceIp, d.SourceIp2.Replace("0.0.0.", d.SourceIp.Replace(".0", ".")), permit.SourceIp2) &&
						d.DestinationIp2 == permit.DestinationIp2 &&
						(d.DestinationPort == permit.DestinationPort || d.DestinationPort == ""))) { }
					//بر اساس DestinationIp و DestinationIp2 و DestinationPort
					else if (denyList.Any(d => d.Index < permit.Index && d.DestinationIp == "host" && d.SourceIp != "any" &&
						!d.SourceIp2.Contains("0.0.0.") &&
						IsInRange(d.SourceIp, d.SourceIp2.Replace("0.0.", d.SourceIp.Replace(".0.0", ".")), permit.SourceIp2) &&
						d.DestinationIp2 == permit.DestinationIp2 &&
						(d.DestinationPort == permit.DestinationPort || d.DestinationPort == ""))) { }
					//بر اساس DestinationIp و DestinationIp2 
					else if (denyList.Any(d => d.Index < permit.Index && d.DestinationIp2 == "0.0.0.255" &&
					IsInRange(d.DestinationIp, d.DestinationIp2.Replace("0.0.0.", d.DestinationIp.Replace(".0", ".")), permit.DestinationIp2))) { }

					else
					{
						model.AclFilesRecords.Add(new AclFilesRecord
						{
							RouterNo = permit.RouterNo,
							Action = permit.Action,
							Protocol = permit.Protocol,
							SourceIp = permit.SourceIp,
							SourceIp2 = permit.SourceIp2,
							SourcePort = permit.SourcePort,
							DestinationIp = permit.DestinationIp,
							DestinationIp2 = permit.DestinationIp2,
							DestinationPort = permit.DestinationPort,
							RecordStatus = permit.RecordStatus,
							AclFilesUpload = permit.AclFilesUpload,
							RouterId = routerId,
						});
					}
				}
				catch (Exception e)
				{

					throw;
				}

			}
			/*foreach (var deny in denyList)
            {
                    model.AclFilesRecords.Add(new AclFilesRecord
                    {
                        RouterNo = deny.RouterNo,
                        Action = deny.Action,
                        Protocol = deny.Protocol,
                        SourceIp = deny.SourceIp,
                        SourceIp2 = deny.SourceIp2,
                        SourcePort = deny.SourcePort,
                        DestinationIp = deny.DestinationIp,
                        DestinationIp2 = deny.DestinationIp2,
                        DestinationPort = deny.DestinationPort,
                        RecordStatus = deny.RecordStatus,
                        AclFilesUpload = deny.AclFilesUpload,
                    });
            }*/
		}

		public static bool IsInRange(string startIpAddr, string endIpAddr, string address)
		{
			IPAddress a;
			if (IPAddress.TryParse(startIpAddr, out a) && IPAddress.TryParse(endIpAddr, out a) && IPAddress.TryParse(address, out a))
			{
				long ipStart = BitConverter.ToInt32(IPAddress.Parse(startIpAddr).GetAddressBytes().Reverse().ToArray(), 0);
				long ipEnd = BitConverter.ToInt32(IPAddress.Parse(endIpAddr).GetAddressBytes().Reverse().ToArray(), 0);
				long ip = BitConverter.ToInt32(IPAddress.Parse(address).GetAddressBytes().Reverse().ToArray(), 0);
				return ip >= ipStart && ip <= ipEnd; //edited
			}
			else
			{
				return false;
			}

		}
		private void AddInvalidItem(AclFilesUpload model, int lineNumber, string lineContent)
		{
			model.InvalidFileItems.Add(new InvalidFileItem
			{
				LineNumber = lineNumber + 1,
				InvalidItemTitle = lineContent,
				AclFilesUploadId = model.Id,
				RecordStatus = Convert.ToBoolean(RecordStatus.NotDeleted),
				AclFilesUpload = model
			});
		}

		private bool IsIpValid(string ip)
		{
			if (ip.Count(c => c == '.') != 3) return false;
			return IPAddress.TryParse(ip, out IPAddress address);
		}
		private string GetUploadedFile(IFormFile file)
		{
			string result = string.Empty;
			if (file.Length > 0)
			{
				using (var ms = new MemoryStream())
				{
					file.CopyTo(ms);
					var fileBytes = ms.ToArray();
					result = Encoding.UTF8.GetString(fileBytes);
				}
			}
			return result;
		}
		#endregion
	}
}

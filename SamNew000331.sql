USE [SamNew000331]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accessibilities]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accessibilities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[SourceIp] [nvarchar](50) NULL,
	[DestinationIp] [nvarchar](50) NULL,
	[SourcePort] [nvarchar](50) NULL,
	[DestinationPort] [nvarchar](50) NULL,
	[Port] [nvarchar](50) NULL,
	[IsTemp] [bit] NOT NULL,
	[Protocol] [nvarchar](50) NULL,
	[ProtocolsId] [int] NULL,
	[SourceSystemId] [int] NULL,
	[DestinationSystemId] [int] NULL,
	[ServiceId] [int] NULL,
	[DestinationServiceId] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionTypesId] [int] NULL,
	[RouterId] [int] NULL,
	[UserId] [int] NOT NULL,
	[AclFilesUploadId] [int] NULL,
	[VersionDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Accessibilities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccessibilityLevels]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessibilityLevels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AccessibilityLevels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccessibilityRequests]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessibilityRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[LetterNo] [nvarchar](50) NULL,
	[LetterDate] [datetime2](7) NULL,
	[SourceIp] [nvarchar](50) NULL,
	[DestinationIp] [nvarchar](50) NULL,
	[AccessStartDate] [datetime2](7) NULL,
	[AccessEndDate] [datetime2](7) NULL,
	[Description] [nvarchar](250) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[SourceSystemId] [int] NULL,
	[DestinationSystemId] [int] NULL,
	[ServiceId] [int] NULL,
	[DestinationServiceId] [int] NULL,
	[UserDepartmentId] [int] NULL,
	[RequestDepartmentId] [int] NULL,
	[AccessibilityTypeId] [int] NULL,
	[RequestingUserId] [int] NULL,
	[ConfirmUserId] [int] NULL,
	[SourceAccessibilityLevelId] [int] NULL,
	[DestAccessibilityLevelId] [int] NULL,
	[SourceProtocolId] [int] NULL,
	[DestinationProtocolId] [int] NULL,
 CONSTRAINT [PK_AccessibilityRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccessRangeDetails]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessRangeDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[IpFrom] [nvarchar](50) NOT NULL,
	[IpTo] [nvarchar](50) NOT NULL,
	[AccessRangeHeaderId] [int] NOT NULL,
 CONSTRAINT [PK_AccessRangeDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccessRangeHeaders]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessRangeHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AccessRangeHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccessTypes]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_AccessTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AclFilesRecords]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AclFilesRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[SourceIp] [nvarchar](50) NULL,
	[DestinationIp] [nvarchar](50) NULL,
	[Action] [nvarchar](50) NULL,
	[SourceIp2] [nvarchar](50) NULL,
	[DestinationIp2] [nvarchar](50) NULL,
	[SourcePort] [nvarchar](50) NULL,
	[DestinationPort] [nvarchar](50) NULL,
	[Protocol] [nvarchar](50) NULL,
	[RouterNo] [nvarchar](50) NULL,
	[RouterId] [int] NULL,
	[SourceAddressTypeId] [int] NULL,
	[DestinationAddressTypeId] [int] NULL,
	[AclFilesUploadId] [int] NOT NULL,
 CONSTRAINT [PK_AclFilesRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AclFilesUpload]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AclFilesUpload](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[RouterId] [int] NULL,
 CONSTRAINT [PK_AclFilesUpload] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActionTypes]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ActionTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddressTypes]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_AddressTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationRoleAuthorities]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationRoleAuthorities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[ApplicationRoleId] [int] NOT NULL,
	[AuthorityId] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationRoleAuthorities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUser_ActivityLogs]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUser_ActivityLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[ActivityTitle] [nvarchar](200) NULL,
	[ActionName] [nvarchar](150) NULL,
	[ControllerName] [nvarchar](150) NULL,
	[UserId] [int] NOT NULL,
	[EntryDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ApplicationUser_ActivityLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUser_Claims]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUser_Claims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_ApplicationUser_Claims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUser_Logins]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUser_Logins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationUser_Logins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUser_OrganizationalCharts]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUser_OrganizationalCharts](
	[Id] [smallint] NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](100) NULL,
 CONSTRAINT [PK_ApplicationUser_OrganizationalCharts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUser_RoleClaims]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUser_RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_ApplicationUser_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUser_Roles]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUser_Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PanelMenu] [nvarchar](max) NULL,
 CONSTRAINT [PK_ApplicationUser_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUser_Tokens]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUser_Tokens](
	[UserId] [int] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_ApplicationUser_Tokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUser_UserRoles]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUser_UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationUser_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUserAccessRageHeaders]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUserAccessRageHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[ApplicationUserId] [int] NOT NULL,
	[AccessRangeHeaderId] [int] NOT NULL,
 CONSTRAINT [PK_ApplicationUserAccessRageHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUserItems]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUserItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[PersonnelCode] [nvarchar](10) NULL,
	[Picture] [nvarchar](150) NULL,
	[RegDateTime] [datetime2](7) NOT NULL,
	[DepartmentId] [int] NULL,
	[PositionId] [int] NULL,
	[BankBranchId] [int] NULL,
	[DutyPositionId] [int] NULL,
	[ExpertPerformance] [bit] NOT NULL,
	[ExpertPerformanceToken] [nvarchar](100) NULL,
	[Qualifications] [bit] NOT NULL,
	[QualificationsToken] [nvarchar](100) NULL,
 CONSTRAINT [PK_ApplicationUserItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authorities]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authorities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_Authorities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankBranches]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankBranches](
	[Id] [int] NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[BranchHeadId] [int] NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BankBranches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Common_Priorities]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Common_Priorities](
	[Id] [smallint] NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](20) NULL,
 CONSTRAINT [PK_Common_Priorities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Common_TestOptions]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Common_TestOptions](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[TableId] [smallint] NOT NULL,
	[Title] [nvarchar](100) NULL,
 CONSTRAINT [PK_Common_TestOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DutyPositions]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DutyPositions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Code] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DutyPositions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExcelFormatDetails]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExcelFormatDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Column] [nvarchar](50) NULL,
	[MappedColumn] [nvarchar](50) NULL,
	[HeaderId] [int] NOT NULL,
 CONSTRAINT [PK_ExcelFormatDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExcelFormatHeaders]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExcelFormatHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[ExcelHeaders] [nvarchar](500) NULL,
	[FileFormat] [tinyint] NOT NULL,
	[Separator] [nvarchar](5) NULL,
 CONSTRAINT [PK_ExcelFormatHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvalidFileItems]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvalidFileItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[InvalidItemTitle] [nvarchar](max) NOT NULL,
	[LineNumber] [int] NOT NULL,
	[AclFilesUploadId] [int] NOT NULL,
 CONSTRAINT [PK_InvalidFileItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification_Categories]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification_Categories](
	[Id] [smallint] NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](100) NULL,
 CONSTRAINT [PK_Notification_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationItems]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[EntryDateTime] [datetime2](7) NOT NULL,
	[Text] [nvarchar](300) NULL,
	[CreatedByUserId] [int] NOT NULL,
	[ForUserId] [int] NOT NULL,
	[ToDoTaskId] [int] NULL,
	[IsRead] [bit] NOT NULL,
	[IsSent] [bit] NOT NULL,
	[CategoryId] [smallint] NOT NULL,
	[Url] [nvarchar](max) NULL,
 CONSTRAINT [PK_NotificationItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Policies]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Policies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
	[ProtocolId] [int] NULL,
 CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Protocols]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Protocols](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Protocols] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report_Boxes]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report_Boxes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[Icon] [nvarchar](50) NULL,
	[Key] [nvarchar](50) NULL,
	[Link] [nvarchar](150) NULL,
	[AccessRight] [nvarchar](50) NULL,
	[BoxStatus] [nvarchar](100) NULL,
	[SqlCommand] [nvarchar](500) NULL,
 CONSTRAINT [PK_Report_Boxes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report_Charts]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report_Charts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Type] [nvarchar](max) NULL,
	[Command] [nvarchar](max) NULL,
	[SeriesName] [nvarchar](max) NULL,
	[ClassName] [nvarchar](max) NULL,
	[Style] [nvarchar](max) NULL,
	[Color] [nvarchar](max) NULL,
	[ChartOptions] [nvarchar](max) NULL,
	[AccessRight] [nvarchar](max) NULL,
 CONSTRAINT [PK_Report_Charts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Routers]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Routers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[AccessNo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Routers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchLogDetails]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchLogDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[NationalCode] [nvarchar](10) NULL,
	[FatherName] [nvarchar](50) NULL,
	[Address] [nvarchar](250) NULL,
	[LetterIdentifier] [nvarchar](max) NULL,
	[CardNumber] [nvarchar](50) NULL,
	[UserId] [int] NOT NULL,
	[SearchDate] [datetime2](7) NOT NULL,
	[SearchTime] [nvarchar](50) NOT NULL,
	[IsSuccess] [bit] NOT NULL,
	[IsVictim] [bit] NOT NULL,
 CONSTRAINT [PK_SearchLogDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchLogTransactionDetail]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchLogTransactionDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[SearchLogDetailId] [int] NOT NULL,
	[TransactionDetailId] [int] NOT NULL,
	[TopupAccountTransactionDetailId] [int] NULL,
	[TopupChannelTransactionDetailId] [int] NULL,
 CONSTRAINT [PK_SearchLogTransactionDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceGroups]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_ServiceGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceLevels]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceLevels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ServiceLevels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Port] [int] NULL,
	[ProtocolId] [int] NULL,
	[ServiceLevelId] [int] NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Id] [smallint] NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[EmailUsername] [varchar](100) NULL,
	[EmailPassword] [varchar](100) NULL,
	[EmailSmtpServer] [varchar](100) NULL,
	[EmailPortNumber] [smallint] NULL,
	[EnableSsl] [bit] NULL,
	[WelcomeText] [nvarchar](max) NULL,
	[WebSiteTitle] [nvarchar](100) NULL,
	[SmsServiceNumber] [nvarchar](50) NULL,
	[SmsUserName] [nvarchar](max) NULL,
	[SmsPassword] [nvarchar](100) NULL,
	[ThanksMsg] [nvarchar](max) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Systems]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Systems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[SystemName] [nvarchar](50) NOT NULL,
	[IpAddress] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[AccessibilityLevelId] [int] NULL,
	[InformationAccessibilityLevelId] [int] NULL,
	[KindId] [int] NOT NULL,
	[ImportanceFactor] [int] NULL,
	[PersonelCode] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[DepartmentId] [int] NULL,
	[IpFrom] [nvarchar](max) NULL,
	[IpTo] [nvarchar](max) NULL,
 CONSTRAINT [PK_Systems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToDoTask_States]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToDoTask_States](
	[Id] [smallint] NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
 CONSTRAINT [PK_ToDoTask_States] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToDoTask_UsageTypes]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToDoTask_UsageTypes](
	[Id] [smallint] NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
 CONSTRAINT [PK_ToDoTask_UsageTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToDoTasks]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToDoTasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](300) NULL,
	[UsageTypeId] [smallint] NULL,
	[DueDateTime] [datetime2](7) NULL,
	[CompletionDateTime] [datetime2](7) NULL,
	[AssignedToUserId] [int] NULL,
	[CreatorUserId] [int] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[PriorityId] [smallint] NULL,
	[StateId] [smallint] NULL,
	[EntryDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ToDoTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopupAccountExcelFormatDetails]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopupAccountExcelFormatDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Column] [nvarchar](50) NULL,
	[MappedColumn] [nvarchar](50) NULL,
	[HeaderId] [int] NOT NULL,
 CONSTRAINT [PK_TopupAccountExcelFormatDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopupAccountExcelFormatHeaders]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopupAccountExcelFormatHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[ExcelHeaders] [nvarchar](500) NULL,
 CONSTRAINT [PK_TopupAccountExcelFormatHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopupAccountTransactionDetails]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopupAccountTransactionDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[TransactionDate] [nvarchar](20) NULL,
	[TransactionTime] [nvarchar](10) NULL,
	[TransactionType] [nvarchar](10) NULL,
	[TransactionStatus] [nvarchar](10) NULL,
	[TransactionAmountText] [nvarchar](50) NULL,
	[TransactionAmount] [nvarchar](max) NULL,
	[RefrenceCode] [nvarchar](20) NULL,
	[Extra1] [nvarchar](100) NULL,
	[Extra2] [nvarchar](100) NULL,
	[Extra3] [nvarchar](100) NULL,
	[Extra4] [nvarchar](100) NULL,
	[BranchCode] [nvarchar](20) NULL,
	[FollowupCode2] [nvarchar](50) NULL,
	[AccountNumber] [nvarchar](30) NULL,
	[CustomerAccountNumber] [nvarchar](30) NULL,
	[FollowupCode] [nvarchar](50) NULL,
	[HeaderId] [int] NOT NULL,
 CONSTRAINT [PK_TopupAccountTransactionDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopupAccountTransactionHeaders]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopupAccountTransactionHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[UploadDate] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_TopupAccountTransactionHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopupChannelExcelFormatDetails]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopupChannelExcelFormatDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Column] [nvarchar](50) NULL,
	[MappedColumn] [nvarchar](50) NULL,
	[HeaderId] [int] NOT NULL,
 CONSTRAINT [PK_TopupChannelExcelFormatDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopupChannelExcelFormatHeaders]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopupChannelExcelFormatHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[ExcelHeaders] [nvarchar](500) NULL,
 CONSTRAINT [PK_TopupChannelExcelFormatHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopupChannelTransactionDetails]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopupChannelTransactionDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[FollowupCode] [nvarchar](50) NULL,
	[CustomerAccountNumber] [nvarchar](50) NULL,
	[FollowupCode2] [nvarchar](50) NULL,
	[Extra1] [nvarchar](50) NULL,
	[Extra2] [nvarchar](20) NULL,
	[ChannelType] [nvarchar](10) NULL,
	[TransactionDate] [nvarchar](20) NULL,
	[MobileNumber] [nvarchar](50) NULL,
	[AmountText] [nvarchar](50) NULL,
	[TransactionAmount] [nvarchar](max) NULL,
	[TransactionStatus] [nvarchar](10) NULL,
	[OperatorName] [nvarchar](50) NULL,
	[HeaderId] [int] NOT NULL,
 CONSTRAINT [PK_TopupChannelTransactionDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopupChannelTransactionHeaders]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopupChannelTransactionHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[UploadDate] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_TopupChannelTransactionHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionDetails]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[SourcePan] [nvarchar](16) NULL,
	[TargetPan] [nvarchar](16) NULL,
	[SourcePanOrgianl] [nvarchar](30) NULL,
	[TargetPanOrgianl] [nvarchar](30) NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
	[TransactionTime] [nvarchar](8) NULL,
	[Tel] [nvarchar](13) NULL,
	[Amount] [bigint] NOT NULL,
	[IpAddress] [nvarchar](50) NULL,
	[LogDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[RefNumber] [nvarchar](10) NULL,
	[UserAgent] [nvarchar](50) NULL,
	[Application] [nvarchar](50) NULL,
	[HeaderId] [int] NOT NULL,
 CONSTRAINT [PK_TransactionDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionHeaders]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecordStatus] [bit] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[UploadDate] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_TransactionHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accessibilities]  WITH CHECK ADD  CONSTRAINT [FK_Accessibilities_AclFilesUpload_AclFilesUploadId] FOREIGN KEY([AclFilesUploadId])
REFERENCES [dbo].[AclFilesUpload] ([Id])
GO
ALTER TABLE [dbo].[Accessibilities] CHECK CONSTRAINT [FK_Accessibilities_AclFilesUpload_AclFilesUploadId]
GO
ALTER TABLE [dbo].[Accessibilities]  WITH CHECK ADD  CONSTRAINT [FK_Accessibilities_ActionTypes_ActionTypesId] FOREIGN KEY([ActionTypesId])
REFERENCES [dbo].[ActionTypes] ([Id])
GO
ALTER TABLE [dbo].[Accessibilities] CHECK CONSTRAINT [FK_Accessibilities_ActionTypes_ActionTypesId]
GO
ALTER TABLE [dbo].[Accessibilities]  WITH CHECK ADD  CONSTRAINT [FK_Accessibilities_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
GO
ALTER TABLE [dbo].[Accessibilities] CHECK CONSTRAINT [FK_Accessibilities_ApplicationUserItems_UserId]
GO
ALTER TABLE [dbo].[Accessibilities]  WITH CHECK ADD  CONSTRAINT [FK_Accessibilities_Protocols_ProtocolsId] FOREIGN KEY([ProtocolsId])
REFERENCES [dbo].[Protocols] ([Id])
GO
ALTER TABLE [dbo].[Accessibilities] CHECK CONSTRAINT [FK_Accessibilities_Protocols_ProtocolsId]
GO
ALTER TABLE [dbo].[Accessibilities]  WITH CHECK ADD  CONSTRAINT [FK_Accessibilities_Routers_RouterId] FOREIGN KEY([RouterId])
REFERENCES [dbo].[Routers] ([Id])
GO
ALTER TABLE [dbo].[Accessibilities] CHECK CONSTRAINT [FK_Accessibilities_Routers_RouterId]
GO
ALTER TABLE [dbo].[Accessibilities]  WITH CHECK ADD  CONSTRAINT [FK_Accessibilities_Services_DestinationServiceId] FOREIGN KEY([DestinationServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[Accessibilities] CHECK CONSTRAINT [FK_Accessibilities_Services_DestinationServiceId]
GO
ALTER TABLE [dbo].[Accessibilities]  WITH CHECK ADD  CONSTRAINT [FK_Accessibilities_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[Accessibilities] CHECK CONSTRAINT [FK_Accessibilities_Services_ServiceId]
GO
ALTER TABLE [dbo].[Accessibilities]  WITH CHECK ADD  CONSTRAINT [FK_Accessibilities_Systems_DestinationSystemId] FOREIGN KEY([DestinationSystemId])
REFERENCES [dbo].[Systems] ([Id])
GO
ALTER TABLE [dbo].[Accessibilities] CHECK CONSTRAINT [FK_Accessibilities_Systems_DestinationSystemId]
GO
ALTER TABLE [dbo].[Accessibilities]  WITH CHECK ADD  CONSTRAINT [FK_Accessibilities_Systems_SourceSystemId] FOREIGN KEY([SourceSystemId])
REFERENCES [dbo].[Systems] ([Id])
GO
ALTER TABLE [dbo].[Accessibilities] CHECK CONSTRAINT [FK_Accessibilities_Systems_SourceSystemId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_AccessibilityLevels_DestAccessibilityLevelId] FOREIGN KEY([DestAccessibilityLevelId])
REFERENCES [dbo].[AccessibilityLevels] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_AccessibilityLevels_DestAccessibilityLevelId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_AccessibilityLevels_SourceAccessibilityLevelId] FOREIGN KEY([SourceAccessibilityLevelId])
REFERENCES [dbo].[AccessibilityLevels] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_AccessibilityLevels_SourceAccessibilityLevelId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_AccessTypes_AccessibilityTypeId] FOREIGN KEY([AccessibilityTypeId])
REFERENCES [dbo].[AccessTypes] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_AccessTypes_AccessibilityTypeId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_ApplicationUserItems_ConfirmUserId] FOREIGN KEY([ConfirmUserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_ApplicationUserItems_ConfirmUserId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_ApplicationUserItems_RequestingUserId] FOREIGN KEY([RequestingUserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_ApplicationUserItems_RequestingUserId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_Departments_RequestDepartmentId] FOREIGN KEY([RequestDepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_Departments_RequestDepartmentId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_Departments_UserDepartmentId] FOREIGN KEY([UserDepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_Departments_UserDepartmentId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_Protocols_DestinationProtocolId] FOREIGN KEY([DestinationProtocolId])
REFERENCES [dbo].[Protocols] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_Protocols_DestinationProtocolId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_Protocols_SourceProtocolId] FOREIGN KEY([SourceProtocolId])
REFERENCES [dbo].[Protocols] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_Protocols_SourceProtocolId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_Services_DestinationServiceId] FOREIGN KEY([DestinationServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_Services_DestinationServiceId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_Services_ServiceId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_Systems_DestinationSystemId] FOREIGN KEY([DestinationSystemId])
REFERENCES [dbo].[Systems] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_Systems_DestinationSystemId]
GO
ALTER TABLE [dbo].[AccessibilityRequests]  WITH CHECK ADD  CONSTRAINT [FK_AccessibilityRequests_Systems_SourceSystemId] FOREIGN KEY([SourceSystemId])
REFERENCES [dbo].[Systems] ([Id])
GO
ALTER TABLE [dbo].[AccessibilityRequests] CHECK CONSTRAINT [FK_AccessibilityRequests_Systems_SourceSystemId]
GO
ALTER TABLE [dbo].[AccessRangeDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccessRangeDetails_AccessRangeHeaders_AccessRangeHeaderId] FOREIGN KEY([AccessRangeHeaderId])
REFERENCES [dbo].[AccessRangeHeaders] ([Id])
GO
ALTER TABLE [dbo].[AccessRangeDetails] CHECK CONSTRAINT [FK_AccessRangeDetails_AccessRangeHeaders_AccessRangeHeaderId]
GO
ALTER TABLE [dbo].[AclFilesRecords]  WITH CHECK ADD  CONSTRAINT [FK_AclFilesRecords_AclFilesUpload_AclFilesUploadId] FOREIGN KEY([AclFilesUploadId])
REFERENCES [dbo].[AclFilesUpload] ([Id])
GO
ALTER TABLE [dbo].[AclFilesRecords] CHECK CONSTRAINT [FK_AclFilesRecords_AclFilesUpload_AclFilesUploadId]
GO
ALTER TABLE [dbo].[AclFilesRecords]  WITH CHECK ADD  CONSTRAINT [FK_AclFilesRecords_AddressTypes_DestinationAddressTypeId] FOREIGN KEY([DestinationAddressTypeId])
REFERENCES [dbo].[AddressTypes] ([Id])
GO
ALTER TABLE [dbo].[AclFilesRecords] CHECK CONSTRAINT [FK_AclFilesRecords_AddressTypes_DestinationAddressTypeId]
GO
ALTER TABLE [dbo].[AclFilesRecords]  WITH CHECK ADD  CONSTRAINT [FK_AclFilesRecords_AddressTypes_SourceAddressTypeId] FOREIGN KEY([SourceAddressTypeId])
REFERENCES [dbo].[AddressTypes] ([Id])
GO
ALTER TABLE [dbo].[AclFilesRecords] CHECK CONSTRAINT [FK_AclFilesRecords_AddressTypes_SourceAddressTypeId]
GO
ALTER TABLE [dbo].[AclFilesRecords]  WITH CHECK ADD  CONSTRAINT [FK_AclFilesRecords_Routers_RouterId] FOREIGN KEY([RouterId])
REFERENCES [dbo].[Routers] ([Id])
GO
ALTER TABLE [dbo].[AclFilesRecords] CHECK CONSTRAINT [FK_AclFilesRecords_Routers_RouterId]
GO
ALTER TABLE [dbo].[AclFilesUpload]  WITH CHECK ADD  CONSTRAINT [FK_AclFilesUpload_Routers_RouterId] FOREIGN KEY([RouterId])
REFERENCES [dbo].[Routers] ([Id])
GO
ALTER TABLE [dbo].[AclFilesUpload] CHECK CONSTRAINT [FK_AclFilesUpload_Routers_RouterId]
GO
ALTER TABLE [dbo].[ApplicationRoleAuthorities]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationRoleAuthorities_ApplicationUser_Roles_ApplicationRoleId] FOREIGN KEY([ApplicationRoleId])
REFERENCES [dbo].[ApplicationUser_Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationRoleAuthorities] CHECK CONSTRAINT [FK_ApplicationRoleAuthorities_ApplicationUser_Roles_ApplicationRoleId]
GO
ALTER TABLE [dbo].[ApplicationRoleAuthorities]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationRoleAuthorities_Authorities_AuthorityId] FOREIGN KEY([AuthorityId])
REFERENCES [dbo].[Authorities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationRoleAuthorities] CHECK CONSTRAINT [FK_ApplicationRoleAuthorities_Authorities_AuthorityId]
GO
ALTER TABLE [dbo].[ApplicationUser_ActivityLogs]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_ActivityLogs_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUser_ActivityLogs] CHECK CONSTRAINT [FK_ApplicationUser_ActivityLogs_ApplicationUserItems_UserId]
GO
ALTER TABLE [dbo].[ApplicationUser_Claims]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_Claims_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUser_Claims] CHECK CONSTRAINT [FK_ApplicationUser_Claims_ApplicationUserItems_UserId]
GO
ALTER TABLE [dbo].[ApplicationUser_Logins]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_Logins_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUser_Logins] CHECK CONSTRAINT [FK_ApplicationUser_Logins_ApplicationUserItems_UserId]
GO
ALTER TABLE [dbo].[ApplicationUser_RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_RoleClaims_ApplicationUser_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[ApplicationUser_Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUser_RoleClaims] CHECK CONSTRAINT [FK_ApplicationUser_RoleClaims_ApplicationUser_Roles_RoleId]
GO
ALTER TABLE [dbo].[ApplicationUser_Tokens]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_Tokens_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUser_Tokens] CHECK CONSTRAINT [FK_ApplicationUser_Tokens_ApplicationUserItems_UserId]
GO
ALTER TABLE [dbo].[ApplicationUser_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_UserRoles_ApplicationUser_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[ApplicationUser_Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUser_UserRoles] CHECK CONSTRAINT [FK_ApplicationUser_UserRoles_ApplicationUser_Roles_RoleId]
GO
ALTER TABLE [dbo].[ApplicationUser_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_UserRoles_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUser_UserRoles] CHECK CONSTRAINT [FK_ApplicationUser_UserRoles_ApplicationUserItems_UserId]
GO
ALTER TABLE [dbo].[ApplicationUserAccessRageHeaders]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserAccessRageHeaders_AccessRangeHeaders_AccessRangeHeaderId] FOREIGN KEY([AccessRangeHeaderId])
REFERENCES [dbo].[AccessRangeHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUserAccessRageHeaders] CHECK CONSTRAINT [FK_ApplicationUserAccessRageHeaders_AccessRangeHeaders_AccessRangeHeaderId]
GO
ALTER TABLE [dbo].[ApplicationUserAccessRageHeaders]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserAccessRageHeaders_ApplicationUserItems_ApplicationUserId] FOREIGN KEY([ApplicationUserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUserAccessRageHeaders] CHECK CONSTRAINT [FK_ApplicationUserAccessRageHeaders_ApplicationUserItems_ApplicationUserId]
GO
ALTER TABLE [dbo].[ApplicationUserItems]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserItems_BankBranches_BankBranchId] FOREIGN KEY([BankBranchId])
REFERENCES [dbo].[BankBranches] ([Id])
GO
ALTER TABLE [dbo].[ApplicationUserItems] CHECK CONSTRAINT [FK_ApplicationUserItems_BankBranches_BankBranchId]
GO
ALTER TABLE [dbo].[ApplicationUserItems]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserItems_Departments_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[ApplicationUserItems] CHECK CONSTRAINT [FK_ApplicationUserItems_Departments_DepartmentId]
GO
ALTER TABLE [dbo].[ApplicationUserItems]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserItems_DutyPositions_DutyPositionId] FOREIGN KEY([DutyPositionId])
REFERENCES [dbo].[DutyPositions] ([Id])
GO
ALTER TABLE [dbo].[ApplicationUserItems] CHECK CONSTRAINT [FK_ApplicationUserItems_DutyPositions_DutyPositionId]
GO
ALTER TABLE [dbo].[ApplicationUserItems]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserItems_Positions_PositionId] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Positions] ([Id])
GO
ALTER TABLE [dbo].[ApplicationUserItems] CHECK CONSTRAINT [FK_ApplicationUserItems_Positions_PositionId]
GO
ALTER TABLE [dbo].[BankBranches]  WITH CHECK ADD  CONSTRAINT [FK_BankBranches_BankBranches_BranchHeadId] FOREIGN KEY([BranchHeadId])
REFERENCES [dbo].[BankBranches] ([Id])
GO
ALTER TABLE [dbo].[BankBranches] CHECK CONSTRAINT [FK_BankBranches_BankBranches_BranchHeadId]
GO
ALTER TABLE [dbo].[ExcelFormatDetails]  WITH CHECK ADD  CONSTRAINT [FK_ExcelFormatDetails_ExcelFormatHeaders_HeaderId] FOREIGN KEY([HeaderId])
REFERENCES [dbo].[ExcelFormatHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExcelFormatDetails] CHECK CONSTRAINT [FK_ExcelFormatDetails_ExcelFormatHeaders_HeaderId]
GO
ALTER TABLE [dbo].[InvalidFileItems]  WITH CHECK ADD  CONSTRAINT [FK_InvalidFileItems_AclFilesUpload_AclFilesUploadId] FOREIGN KEY([AclFilesUploadId])
REFERENCES [dbo].[AclFilesUpload] ([Id])
GO
ALTER TABLE [dbo].[InvalidFileItems] CHECK CONSTRAINT [FK_InvalidFileItems_AclFilesUpload_AclFilesUploadId]
GO
ALTER TABLE [dbo].[NotificationItems]  WITH CHECK ADD  CONSTRAINT [FK_NotificationItems_ApplicationUserItems_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
GO
ALTER TABLE [dbo].[NotificationItems] CHECK CONSTRAINT [FK_NotificationItems_ApplicationUserItems_CreatedByUserId]
GO
ALTER TABLE [dbo].[NotificationItems]  WITH CHECK ADD  CONSTRAINT [FK_NotificationItems_ApplicationUserItems_ForUserId] FOREIGN KEY([ForUserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
GO
ALTER TABLE [dbo].[NotificationItems] CHECK CONSTRAINT [FK_NotificationItems_ApplicationUserItems_ForUserId]
GO
ALTER TABLE [dbo].[NotificationItems]  WITH CHECK ADD  CONSTRAINT [FK_NotificationItems_Notification_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Notification_Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NotificationItems] CHECK CONSTRAINT [FK_NotificationItems_Notification_Categories_CategoryId]
GO
ALTER TABLE [dbo].[NotificationItems]  WITH CHECK ADD  CONSTRAINT [FK_NotificationItems_ToDoTasks_ToDoTaskId] FOREIGN KEY([ToDoTaskId])
REFERENCES [dbo].[ToDoTasks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NotificationItems] CHECK CONSTRAINT [FK_NotificationItems_ToDoTasks_ToDoTaskId]
GO
ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Protocols_ProtocolId] FOREIGN KEY([ProtocolId])
REFERENCES [dbo].[Protocols] ([Id])
GO
ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Protocols_ProtocolId]
GO
ALTER TABLE [dbo].[SearchLogDetails]  WITH CHECK ADD  CONSTRAINT [FK_SearchLogDetails_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SearchLogDetails] CHECK CONSTRAINT [FK_SearchLogDetails_ApplicationUserItems_UserId]
GO
ALTER TABLE [dbo].[SearchLogTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_SearchLogTransactionDetail_SearchLogDetails_SearchLogDetailId] FOREIGN KEY([SearchLogDetailId])
REFERENCES [dbo].[SearchLogDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SearchLogTransactionDetail] CHECK CONSTRAINT [FK_SearchLogTransactionDetail_SearchLogDetails_SearchLogDetailId]
GO
ALTER TABLE [dbo].[SearchLogTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_SearchLogTransactionDetail_TopupAccountTransactionDetails_TopupAccountTransactionDetailId] FOREIGN KEY([TopupAccountTransactionDetailId])
REFERENCES [dbo].[TopupAccountTransactionDetails] ([Id])
GO
ALTER TABLE [dbo].[SearchLogTransactionDetail] CHECK CONSTRAINT [FK_SearchLogTransactionDetail_TopupAccountTransactionDetails_TopupAccountTransactionDetailId]
GO
ALTER TABLE [dbo].[SearchLogTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_SearchLogTransactionDetail_TopupChannelTransactionDetails_TopupChannelTransactionDetailId] FOREIGN KEY([TopupChannelTransactionDetailId])
REFERENCES [dbo].[TopupChannelTransactionDetails] ([Id])
GO
ALTER TABLE [dbo].[SearchLogTransactionDetail] CHECK CONSTRAINT [FK_SearchLogTransactionDetail_TopupChannelTransactionDetails_TopupChannelTransactionDetailId]
GO
ALTER TABLE [dbo].[SearchLogTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_SearchLogTransactionDetail_TransactionDetails_TransactionDetailId] FOREIGN KEY([TransactionDetailId])
REFERENCES [dbo].[TransactionDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SearchLogTransactionDetail] CHECK CONSTRAINT [FK_SearchLogTransactionDetail_TransactionDetails_TransactionDetailId]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_Protocols_ProtocolId] FOREIGN KEY([ProtocolId])
REFERENCES [dbo].[Protocols] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_Protocols_ProtocolId]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_ServiceLevels_ServiceLevelId] FOREIGN KEY([ServiceLevelId])
REFERENCES [dbo].[ServiceLevels] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_ServiceLevels_ServiceLevelId]
GO
ALTER TABLE [dbo].[Systems]  WITH CHECK ADD  CONSTRAINT [FK_Systems_AccessibilityLevels_AccessibilityLevelId] FOREIGN KEY([AccessibilityLevelId])
REFERENCES [dbo].[AccessibilityLevels] ([Id])
GO
ALTER TABLE [dbo].[Systems] CHECK CONSTRAINT [FK_Systems_AccessibilityLevels_AccessibilityLevelId]
GO
ALTER TABLE [dbo].[Systems]  WITH CHECK ADD  CONSTRAINT [FK_Systems_AccessibilityLevels_InformationAccessibilityLevelId] FOREIGN KEY([InformationAccessibilityLevelId])
REFERENCES [dbo].[AccessibilityLevels] ([Id])
GO
ALTER TABLE [dbo].[Systems] CHECK CONSTRAINT [FK_Systems_AccessibilityLevels_InformationAccessibilityLevelId]
GO
ALTER TABLE [dbo].[Systems]  WITH CHECK ADD  CONSTRAINT [FK_Systems_Departments_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Systems] CHECK CONSTRAINT [FK_Systems_Departments_DepartmentId]
GO
ALTER TABLE [dbo].[ToDoTasks]  WITH CHECK ADD  CONSTRAINT [FK_ToDoTasks_ApplicationUserItems_AssignedToUserId] FOREIGN KEY([AssignedToUserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
GO
ALTER TABLE [dbo].[ToDoTasks] CHECK CONSTRAINT [FK_ToDoTasks_ApplicationUserItems_AssignedToUserId]
GO
ALTER TABLE [dbo].[ToDoTasks]  WITH CHECK ADD  CONSTRAINT [FK_ToDoTasks_ApplicationUserItems_CreatorUserId] FOREIGN KEY([CreatorUserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ToDoTasks] CHECK CONSTRAINT [FK_ToDoTasks_ApplicationUserItems_CreatorUserId]
GO
ALTER TABLE [dbo].[ToDoTasks]  WITH CHECK ADD  CONSTRAINT [FK_ToDoTasks_Common_Priorities_PriorityId] FOREIGN KEY([PriorityId])
REFERENCES [dbo].[Common_Priorities] ([Id])
GO
ALTER TABLE [dbo].[ToDoTasks] CHECK CONSTRAINT [FK_ToDoTasks_Common_Priorities_PriorityId]
GO
ALTER TABLE [dbo].[ToDoTasks]  WITH CHECK ADD  CONSTRAINT [FK_ToDoTasks_ToDoTask_States_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[ToDoTask_States] ([Id])
GO
ALTER TABLE [dbo].[ToDoTasks] CHECK CONSTRAINT [FK_ToDoTasks_ToDoTask_States_StateId]
GO
ALTER TABLE [dbo].[ToDoTasks]  WITH CHECK ADD  CONSTRAINT [FK_ToDoTasks_ToDoTask_UsageTypes_UsageTypeId] FOREIGN KEY([UsageTypeId])
REFERENCES [dbo].[ToDoTask_UsageTypes] ([Id])
GO
ALTER TABLE [dbo].[ToDoTasks] CHECK CONSTRAINT [FK_ToDoTasks_ToDoTask_UsageTypes_UsageTypeId]
GO
ALTER TABLE [dbo].[TopupAccountExcelFormatDetails]  WITH CHECK ADD  CONSTRAINT [FK_TopupAccountExcelFormatDetails_TopupAccountExcelFormatHeaders_HeaderId] FOREIGN KEY([HeaderId])
REFERENCES [dbo].[TopupAccountExcelFormatHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopupAccountExcelFormatDetails] CHECK CONSTRAINT [FK_TopupAccountExcelFormatDetails_TopupAccountExcelFormatHeaders_HeaderId]
GO
ALTER TABLE [dbo].[TopupAccountTransactionDetails]  WITH CHECK ADD  CONSTRAINT [FK_TopupAccountTransactionDetails_TopupAccountTransactionHeaders_HeaderId] FOREIGN KEY([HeaderId])
REFERENCES [dbo].[TopupAccountTransactionHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopupAccountTransactionDetails] CHECK CONSTRAINT [FK_TopupAccountTransactionDetails_TopupAccountTransactionHeaders_HeaderId]
GO
ALTER TABLE [dbo].[TopupAccountTransactionHeaders]  WITH CHECK ADD  CONSTRAINT [FK_TopupAccountTransactionHeaders_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
GO
ALTER TABLE [dbo].[TopupAccountTransactionHeaders] CHECK CONSTRAINT [FK_TopupAccountTransactionHeaders_ApplicationUserItems_UserId]
GO
ALTER TABLE [dbo].[TopupChannelExcelFormatDetails]  WITH CHECK ADD  CONSTRAINT [FK_TopupChannelExcelFormatDetails_TopupChannelExcelFormatHeaders_HeaderId] FOREIGN KEY([HeaderId])
REFERENCES [dbo].[TopupChannelExcelFormatHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopupChannelExcelFormatDetails] CHECK CONSTRAINT [FK_TopupChannelExcelFormatDetails_TopupChannelExcelFormatHeaders_HeaderId]
GO
ALTER TABLE [dbo].[TopupChannelTransactionDetails]  WITH CHECK ADD  CONSTRAINT [FK_TopupChannelTransactionDetails_TopupChannelTransactionHeaders_HeaderId] FOREIGN KEY([HeaderId])
REFERENCES [dbo].[TopupChannelTransactionHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopupChannelTransactionDetails] CHECK CONSTRAINT [FK_TopupChannelTransactionDetails_TopupChannelTransactionHeaders_HeaderId]
GO
ALTER TABLE [dbo].[TopupChannelTransactionHeaders]  WITH CHECK ADD  CONSTRAINT [FK_TopupChannelTransactionHeaders_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
GO
ALTER TABLE [dbo].[TopupChannelTransactionHeaders] CHECK CONSTRAINT [FK_TopupChannelTransactionHeaders_ApplicationUserItems_UserId]
GO
ALTER TABLE [dbo].[TransactionDetails]  WITH CHECK ADD  CONSTRAINT [FK_TransactionDetails_TransactionHeaders_HeaderId] FOREIGN KEY([HeaderId])
REFERENCES [dbo].[TransactionHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TransactionDetails] CHECK CONSTRAINT [FK_TransactionDetails_TransactionHeaders_HeaderId]
GO
ALTER TABLE [dbo].[TransactionHeaders]  WITH CHECK ADD  CONSTRAINT [FK_TransactionHeaders_ApplicationUserItems_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUserItems] ([Id])
GO
ALTER TABLE [dbo].[TransactionHeaders] CHECK CONSTRAINT [FK_TransactionHeaders_ApplicationUserItems_UserId]
GO
/****** Object:  StoredProcedure [dbo].[uspAccessibilityDeleteTemp]    Script Date: 6/22/2021 07:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspAccessibilityDeleteTemp]
    @UserId INT  
AS  
	DELETE  FROM Accessibilities
	WHERE IsTemp = 1 AND UserId = @UserId

GO

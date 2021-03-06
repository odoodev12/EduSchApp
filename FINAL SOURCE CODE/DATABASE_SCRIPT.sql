USE [SchoolApp_Live]
GO
/****** Object:  Table [dbo].[ISPicker]    Script Date: 20/03/2020 1:41:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISPicker](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NULL,
	[ParentID] [int] NULL,
	[StudentID] [int] NULL,
	[Title] [varchar](255) NULL,
	[FirstName] [varchar](255) NULL,
	[LastName] [varchar](255) NULL,
	[Photo] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Phone] [varchar](255) NULL,
	[PickupCodeCunter] [varchar](255) NULL,
	[OneOffPickerFlag] [bit] NULL,
	[EndDate] [datetime] NULL,
	[ActiveStatus] [varchar](255) NULL,
	[ActiveStatusLastUpdatedDate] [datetime] NULL,
	[ActiveStatusLastUpdatedParent] [varchar](255) NULL,
	[ActiveStatusLastUpdatedParentEmail] [varchar](255) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[PickerType] [int] NULL,
	[CreatedByEmail] [nvarchar](500) NULL,
	[EmergencyContact] [bit] NULL,
	[CreatedByName] [nvarchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISPickerAssignment]    Script Date: 20/03/2020 1:41:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISPickerAssignment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PickerId] [int] NULL,
	[StudentID] [int] NULL,
	[PickerCode] [varchar](255) NULL,
	[PickCodeExDate] [datetime] NULL,
	[PickCodeLastUpdateDate] [datetime] NULL,
	[PickCodayExDate] [datetime] NULL,
	[PickTodayLastUpdateDate] [datetime] NULL,
	[PickTodayLastUpdateParent] [varchar](255) NULL,
	[RemoveChildStatus] [int] NULL,
	[RemoveChildLastUpdateDate] [datetime] NULL,
	[RemoveChildLastupdateParent] [varchar](255) NULL,
	[StudentPickAssignFlag] [int] NULL,
	[StudentPickAssignLastUpdateParent] [varchar](255) NULL,
	[StudentPickAssignDate] [datetime] NULL,
	[StudentAssignBy] [nvarchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewNotAssignPicker]    Script Date: 20/03/2020 1:41:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewNotAssignPicker]
AS
SELECT        ID, SchoolID, ParentID, StudentID, Title, FirstName, LastName, Photo, Email, Phone, PickupCodeCunter, OneOffPickerFlag, EndDate, ActiveStatus, ActiveStatusLastUpdatedDate, ActiveStatusLastUpdatedParent, 
                         ActiveStatusLastUpdatedParentEmail, Active, Deleted, CreatedBy, CreatedDateTime, ModifyBy, ModifyDateTime, DeletedBy, DeletedDateTime
FROM            dbo.ISPicker
WHERE        (ID NOT IN
                             (SELECT        PickerId
                               FROM            dbo.ISPickerAssignment
                               WHERE        (RemoveChildStatus = 0)))



GO
/****** Object:  Table [dbo].[ISAttendance]    Script Date: 20/03/2020 1:41:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISAttendance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[StudentID] [int] NULL,
	[TeacherID] [int] NULL,
	[Status] [varchar](255) NULL,
	[Time] [datetime] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[AttendenceComplete] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISStudent]    Script Date: 20/03/2020 1:41:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISStudent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudentNo] [varchar](250) NULL,
	[ClassID] [int] NULL,
	[SchoolID] [int] NULL,
	[StudentName] [varchar](255) NULL,
	[Photo] [varchar](255) NULL,
	[DOB] [date] NULL,
	[ParantName1] [varchar](255) NULL,
	[ParantEmail1] [varchar](50) NULL,
	[ParantPassword1] [varchar](255) NULL,
	[ParantPhone1] [varchar](11) NULL,
	[ParantRelation1] [varchar](255) NULL,
	[ParantPhoto1] [varchar](255) NULL,
	[ParantName2] [varchar](255) NULL,
	[ParantEmail2] [varchar](50) NULL,
	[ParantPassword2] [varchar](255) NULL,
	[ParantPhone2] [varchar](11) NULL,
	[ParantRelation2] [varchar](255) NULL,
	[ParantPhoto2] [varchar](255) NULL,
	[PickupMessageID] [varchar](255) NULL,
	[PickupMessageTime] [time](7) NULL,
	[PickupMessageDate] [date] NULL,
	[PickupMessageLastUpdatedParent] [varchar](50) NULL,
	[AttendanceEmailFlag] [varchar](50) NULL,
	[LastAttendanceEmailDate] [date] NULL,
	[LastAttendanceEmailTime] [time](7) NULL,
	[LastUnpickAlertSentTime] [time](7) NULL,
	[UnpickAlertDate] [date] NULL,
	[LastUnpickAlertSentTeacherID] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Out] [int] NULL,
	[Outbit] [bit] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[EmailAfterConfirmAttendence] [bit] NULL,
	[EmailAfterConfirmPickUp] [bit] NULL,
	[ISImageEmail] [bit] NULL,
	[SentMailDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewStudentAttendence]    Script Date: 20/03/2020 1:41:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewStudentAttendence]
AS
SELECT        dbo.ISAttendance.ID, dbo.ISAttendance.Date, dbo.ISAttendance.StudentID, dbo.ISAttendance.TeacherID, dbo.ISAttendance.Status, dbo.ISAttendance.Time, dbo.ISAttendance.Active, dbo.ISAttendance.Deleted, 
                         dbo.ISAttendance.CreatedBy, dbo.ISAttendance.CreatedDateTime, dbo.ISAttendance.ModifyBy, dbo.ISAttendance.ModifyDateTime, dbo.ISAttendance.DeletedBy, dbo.ISAttendance.DeletedDateTime, 
                         dbo.ISStudent.ID AS StudentsID, dbo.ISStudent.StudentNo, dbo.ISStudent.ClassID, dbo.ISStudent.SchoolID, dbo.ISStudent.StudentName, dbo.ISStudent.Photo, dbo.ISStudent.ParantName1, 
                         dbo.ISStudent.ParantEmail1, dbo.ISStudent.ParantName2, dbo.ISStudent.ParantEmail2, dbo.ISStudent.Active AS StudentActive, dbo.ISStudent.Deleted AS StudentDeleted
FROM            dbo.ISStudent LEFT OUTER JOIN
                         dbo.ISAttendance ON dbo.ISStudent.ID = dbo.ISAttendance.StudentID




GO
/****** Object:  Table [dbo].[ISPickup]    Script Date: 20/03/2020 1:41:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISPickup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NULL,
	[ClassID] [int] NULL,
	[TeacherID] [int] NULL,
	[PickerID] [int] NULL,
	[PickTime] [datetime] NULL,
	[PickDate] [datetime] NULL,
	[PickStatus] [varchar](50) NULL,
	[CompletePickup] [bit] NULL,
	[OfficeFlag] [bit] NULL,
	[AfterSchoolFlag] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewStudentPickUp]    Script Date: 20/03/2020 1:41:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewStudentPickUp]
AS
SELECT        dbo.ISStudent.ID AS StudID, dbo.ISStudent.StudentNo, dbo.ISStudent.ClassID, dbo.ISStudent.SchoolID, dbo.ISStudent.StudentName, dbo.ISStudent.Photo, dbo.ISStudent.ParantName1, dbo.ISStudent.ParantEmail1, 
                         dbo.ISStudent.ParantName2, dbo.ISStudent.ParantEmail2, dbo.ISStudent.Active, dbo.ISStudent.Deleted, dbo.ISPickup.ID, dbo.ISPickup.StudentID, dbo.ISPickup.TeacherID, dbo.ISPickup.PickerID, 
                         dbo.ISPickup.PickTime, dbo.ISPickup.PickDate, dbo.ISPickup.PickStatus
FROM            dbo.ISStudent LEFT OUTER JOIN
                         dbo.ISPickup ON dbo.ISStudent.ID = dbo.ISPickup.StudentID




GO
/****** Object:  Table [dbo].[ISAccountStatus]    Script Date: 20/03/2020 1:41:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISAccountStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISAdminLogin]    Script Date: 20/03/2020 1:41:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISAdminLogin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Pass] [varchar](255) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[FullName] [nvarchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISAuthToken]    Script Date: 20/03/2020 1:41:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISAuthToken](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](50) NULL,
	[IMEINO] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDatetime] [datetime] NULL,
 CONSTRAINT [PK_ISAuthToken] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISClass]    Script Date: 20/03/2020 1:41:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISClass](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Year] [varchar](255) NULL,
	[TypeID] [int] NOT NULL,
	[AfterSchoolType] [varchar](255) NULL,
	[ExternalOrganisation] [varchar](255) NULL,
	[EndDate] [datetime] NULL,
	[PickupComplete] [bit] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedByType] [int] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[ISNonListed] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISClassType]    Script Date: 20/03/2020 1:41:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISClassType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISClassYear]    Script Date: 20/03/2020 1:41:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISClassYear](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Year] [varchar](150) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISCompleteAttendanceRun]    Script Date: 20/03/2020 1:41:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISCompleteAttendanceRun](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[ClassID] [int] NULL,
	[TeacherID] [int] NULL,
	[Time] [datetime] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISCompletePickupRun]    Script Date: 20/03/2020 1:41:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISCompletePickupRun](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[ClassID] [int] NULL,
	[TeacherID] [int] NULL,
	[Time] [datetime] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISCountries]    Script Date: 20/03/2020 1:41:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISCountries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISDataUploadHistory]    Script Date: 20/03/2020 1:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISDataUploadHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NULL,
	[TemplateName] [nvarchar](500) NULL,
	[CreatedCount] [int] NULL,
	[UpdatedCount] [int] NULL,
	[Date] [datetime] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedByType] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISErrorLog]    Script Date: 20/03/2020 1:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LogText] [varchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[Deleted] [bit] NULL,
	[FileName] [varchar](max) NULL,
	[LineNumber] [varchar](10) NULL,
	[ColumnNumber] [varchar](10) NULL,
	[Method] [varchar](1000) NULL,
	[Class] [varchar](1000) NULL,
 CONSTRAINT [PK_ISErrorLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISFAQ]    Script Date: 20/03/2020 1:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISFAQ](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](max) NULL,
	[Attachment] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedByName] [nvarchar](1) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISHoliday]    Script Date: 20/03/2020 1:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISHoliday](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
 CONSTRAINT [PK_ISHoliday] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISInvoice]    Script Date: 20/03/2020 1:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISInvoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PropertyID] [int] NULL,
	[InvoiceNo] [varchar](255) NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[TransactionDesc] [varchar](255) NULL,
	[TransactionAmount] [int] NULL,
	[TaxRate] [decimal](18, 2) NULL,
	[StatusID] [int] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISLogType]    Script Date: 20/03/2020 1:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISLogType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LogTypeName] [varchar](150) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISMessage]    Script Date: 20/03/2020 1:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](1000) NULL,
	[Attechment] [varchar](255) NULL,
	[Desc] [ntext] NULL,
	[ReceiveID] [int] NULL,
	[SendID] [int] NULL,
	[ReceiverType] [int] NULL,
	[SenderType] [int] NULL,
	[SchoolID] [int] NULL,
	[Time] [datetime] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISOrganisationUser]    Script Date: 20/03/2020 1:41:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISOrganisationUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](255) NULL,
	[LastName] [varchar](255) NULL,
	[OrgCode] [varchar](255) NULL,
	[Photo] [varchar](max) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[Town] [varchar](255) NULL,
	[CountryID] [int] NULL,
	[Email] [varchar](255) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[RoleID] [int] NULL,
	[Password] [varchar](50) NULL,
	[StatusID] [varchar](255) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[CreatedByName] [nvarchar](500) NULL,
	[ActivationBy] [nvarchar](500) NULL,
	[ActivationDate] [datetime] NULL,
	[LastUpdatedBy] [nvarchar](500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISPayment]    Script Date: 20/03/2020 1:41:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISPayment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [varchar](255) NOT NULL,
	[InvoiceID] [varchar](255) NOT NULL,
	[Amount] [varchar](255) NOT NULL,
	[TransactionTypeID] [varchar](255) NOT NULL,
	[Description] [ntext] NOT NULL,
	[StatusID] [varchar](255) NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISPickUpMessage]    Script Date: 20/03/2020 1:41:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISPickUpMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NULL,
	[Message] [varchar](1000) NULL,
	[ReceiverID] [int] NULL,
	[ClassID] [int] NULL,
	[SendID] [int] NULL,
	[SenderName] [varchar](200) NULL,
	[Viewed] [bit] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISPickUpStatus]    Script Date: 20/03/2020 1:41:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISPickUpStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](255) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISReport]    Script Date: 20/03/2020 1:41:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](1000) NULL,
	[URL] [varchar](max) NULL,
	[ReportTypeID] [int] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISRole]    Script Date: 20/03/2020 1:41:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](1000) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISSchool]    Script Date: 20/03/2020 1:41:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISSchool](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerNumber] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[Number] [varchar](255) NULL,
	[TypeID] [int] NULL,
	[Address1] [ntext] NULL,
	[Address2] [ntext] NULL,
	[Town] [varchar](255) NULL,
	[CountryID] [int] NULL,
	[Logo] [varchar](max) NULL,
	[AdminFirstName] [varchar](255) NULL,
	[AdminLastName] [varchar](255) NULL,
	[AdminEmail] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[PhoneNumber] [varchar](255) NULL,
	[Website] [varchar](255) NULL,
	[SupervisorFirstname] [varchar](255) NULL,
	[SupervisorLastname] [varchar](255) NULL,
	[SupervisorEmail] [varchar](255) NULL,
	[OpningTime] [datetime] NULL,
	[ClosingTime] [datetime] NULL,
	[LateMinAfterClosing] [varchar](50) NULL,
	[ChargeMinutesAfterClosing] [varchar](255) NULL,
	[ReportableMinutesAfterClosing] [varchar](50) NULL,
	[SetupTrainingStatus] [bit] NULL,
	[SetupTrainingDate] [datetime] NULL,
	[ActivationDate] [datetime] NULL,
	[SchoolEndDate] [datetime] NULL,
	[isAttendanceModule] [bit] NULL,
	[isNotificationPickup] [bit] NULL,
	[NotificationAttendance] [bit] NULL,
	[AttendanceModule] [varchar](255) NULL,
	[PostCode] [varchar](255) NULL,
	[BillingAddress] [ntext] NULL,
	[BillingAddress2] [ntext] NULL,
	[BillingPostCode] [varchar](255) NULL,
	[BillingCountryID] [int] NULL,
	[BillingTown] [varchar](255) NULL,
	[Classfile] [varchar](max) NULL,
	[Teacherfile] [varchar](max) NULL,
	[Studentfile] [varchar](max) NULL,
	[Reportable] [bit] NULL,
	[PaymentSystems] [bit] NULL,
	[CustSigned] [bit] NULL,
	[AccountStatusID] [int] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[CreatedByName] [nvarchar](500) NULL,
	[LastUpdatedBy] [nvarchar](500) NULL,
	[ActivatedBy] [nvarchar](500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISSchoolClass]    Script Date: 20/03/2020 1:41:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISSchoolClass](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[ClassID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISSchoolInvoice]    Script Date: 20/03/2020 1:41:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISSchoolInvoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NULL,
	[InvoiceNo] [varchar](255) NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[TransactionTypeID] [int] NULL,
	[TransactionDesc] [varchar](255) NULL,
	[TransactionAmount] [int] NULL,
	[TaxRate] [decimal](18, 2) NULL,
	[StatusID] [int] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[StatusUpdateBy] [nvarchar](200) NULL,
	[StatusUpdateDate] [datetime] NULL,
	[CreatedByName] [nvarchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISSchoolType]    Script Date: 20/03/2020 1:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISSchoolType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISStudentHistory]    Script Date: 20/03/2020 1:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISStudentHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudentNo] [varchar](250) NULL,
	[ClassID] [int] NULL,
	[SchoolID] [int] NULL,
	[StudentName] [varchar](255) NULL,
	[Photo] [varchar](255) NULL,
	[DOB] [date] NULL,
	[ParantName1] [varchar](255) NULL,
	[ParantEmail1] [varchar](50) NULL,
	[ParantPassword1] [varchar](255) NULL,
	[ParantPhone1] [varchar](11) NULL,
	[ParantRelation1] [varchar](255) NULL,
	[ParantPhoto1] [varchar](255) NULL,
	[ParantName2] [varchar](255) NULL,
	[ParantEmail2] [varchar](50) NULL,
	[ParantPassword2] [varchar](255) NULL,
	[ParantPhone2] [varchar](11) NULL,
	[ParantRelation2] [varchar](255) NULL,
	[ParantPhoto2] [varchar](255) NULL,
	[PickupMessageID] [varchar](255) NULL,
	[PickupMessageTime] [time](7) NULL,
	[PickupMessageDate] [date] NULL,
	[PickupMessageLastUpdatedParent] [varchar](50) NULL,
	[AttendanceEmailFlag] [varchar](50) NULL,
	[LastAttendanceEmailDate] [date] NULL,
	[LastAttendanceEmailTime] [time](7) NULL,
	[LastUnpickAlertSentTime] [time](7) NULL,
	[UnpickAlertDate] [date] NULL,
	[LastUnpickAlertSentTeacherID] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Out] [int] NULL,
	[Outbit] [bit] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[EmailAfterConfirmAttendence] [bit] NULL,
	[EmailAfterConfirmPickUp] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISStudentReassignHistory]    Script Date: 20/03/2020 1:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISStudentReassignHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NULL,
	[FromClass] [int] NULL,
	[ToClass] [int] NULL,
	[StduentID] [int] NULL,
	[Date] [datetime] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedByType] [int] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISSupport]    Script Date: 20/03/2020 1:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISSupport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TicketNo] [varchar](255) NOT NULL,
	[Request] [varchar](max) NULL,
	[SchoolID] [int] NULL,
	[StatusID] [int] NULL,
	[LogTypeID] [int] NULL,
	[SupportOfficerID] [int] NULL,
	[Priority] [int] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedByName] [nvarchar](500) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[AssignBy] [nvarchar](500) NULL,
	[AssignDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISSupportStatus]    Script Date: 20/03/2020 1:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISSupportStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISTeacher]    Script Date: 20/03/2020 1:41:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISTeacher](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[ClassID] [int] NULL,
	[RoleID] [int] NOT NULL,
	[TeacherNo] [varchar](255) NULL,
	[Title] [varchar](255) NULL,
	[Name] [varchar](255) NOT NULL,
	[PhoneNo] [varchar](15) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[EndDate] [datetime] NULL,
	[Photo] [varchar](max) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedByType] [int] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
	[Role] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISTeacherClassAssignment]    Script Date: 20/03/2020 1:41:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISTeacherClassAssignment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TeacherID] [int] NULL,
	[ClassID] [int] NULL,
	[Out] [int] NULL,
	[Outbit] [bit] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISTeacherReassignHistory]    Script Date: 20/03/2020 1:41:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISTeacherReassignHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NULL,
	[FromClass] [int] NULL,
	[ToClass] [int] NULL,
	[TeacherID] [int] NULL,
	[Date] [datetime] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedByType] [int] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISTicketMessage]    Script Date: 20/03/2020 1:41:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISTicketMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SupportID] [int] NULL,
	[SenderID] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[SelectFile] [varchar](max) NULL,
	[CreatedDatetime] [datetime] NULL,
	[UserTypeID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISTrasectionStatus]    Script Date: 20/03/2020 1:41:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISTrasectionStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISTrasectionType]    Script Date: 20/03/2020 1:41:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISTrasectionType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISUserActivity]    Script Date: 20/03/2020 1:41:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISUserActivity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NULL,
	[UserName] [varchar](200) NULL,
	[LogText] [varchar](max) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[DocumentCategory] [nvarchar](500) NULL,
 CONSTRAINT [PK_ISUserActivity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ISUserRole]    Script Date: 20/03/2020 1:41:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ISUserRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[RoleName] [varchar](255) NOT NULL,
	[RoleType] [int] NULL,
	[ManageTeacherFlag] [bit] NOT NULL,
	[ManageClassFlag] [bit] NOT NULL,
	[ManageSupportFlag] [bit] NOT NULL,
	[ManageStudentFlag] [bit] NOT NULL,
	[ManageViewAccountFlag] [bit] NOT NULL,
	[ManageNonTeacherFlag] [bit] NULL,
	[ManageHolidayFlag] [bit] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ISAccountStatus] ON 

INSERT [dbo].[ISAccountStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'InProcess', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAccountStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'Active', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAccountStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'InActive', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISAccountStatus] OFF
SET IDENTITY_INSERT [dbo].[ISAdminLogin] ON 

INSERT [dbo].[ISAdminLogin] ([ID], [Email], [Pass], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [FullName]) VALUES (1, N'admin@gmail.com', N'4bn9GpOHMkE=', 1, 1, 1, NULL, NULL, NULL, NULL, NULL, N'Admin')
SET IDENTITY_INSERT [dbo].[ISAdminLogin] OFF
SET IDENTITY_INSERT [dbo].[ISAttendance] ON 

INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (1, CAST(N'2019-12-19T00:00:00.000' AS DateTime), 1, 4, N'Absent', CAST(N'2018-01-01T09:22:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-19T09:22:29.787' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (2, CAST(N'2019-12-19T00:00:00.000' AS DateTime), 2, 4, N'Present', CAST(N'2018-01-01T09:24:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-19T09:24:02.587' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3, CAST(N'2019-12-19T00:00:00.000' AS DateTime), 4, 4, N'Present', CAST(N'2018-01-01T09:27:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-19T09:27:56.383' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (4, CAST(N'2019-12-19T00:00:00.000' AS DateTime), 5, 4, N'Present', CAST(N'2018-01-01T09:27:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-19T09:27:58.857' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5, CAST(N'2019-12-20T00:00:00.000' AS DateTime), 1, 1, N'Present', CAST(N'2018-01-01T13:57:00.000' AS DateTime), 1, 1, 1, CAST(N'2019-12-20T13:57:02.027' AS DateTime), 1, CAST(N'2019-12-20T14:13:59.500' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6, CAST(N'2019-12-20T00:00:00.000' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T17:01:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-20T17:01:08.903' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (7, CAST(N'2019-12-20T00:00:00.000' AS DateTime), 2, 4, N'Present', CAST(N'2018-01-01T17:01:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-20T17:01:14.893' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (11, CAST(N'2019-12-20T00:00:00.000' AS DateTime), 11, 3, N'Present', CAST(N'2018-01-01T20:49:00.000' AS DateTime), 1, 1, 3, CAST(N'2019-12-20T20:49:51.827' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (12, CAST(N'2019-12-21T00:00:00.000' AS DateTime), 1, 4, N'Present', CAST(N'2018-01-01T15:20:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-21T15:20:44.083' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (1005, CAST(N'2019-12-22T00:00:00.000' AS DateTime), 1, 4, N'Present', CAST(N'2018-01-01T23:14:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-22T23:14:44.480' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (2005, CAST(N'2019-12-26T00:00:00.000' AS DateTime), 1, 4, N'Present', CAST(N'2018-01-01T18:47:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-26T18:47:18.317' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (2006, CAST(N'2019-12-26T00:00:00.000' AS DateTime), 2, 4, N'Present', CAST(N'2018-01-01T19:38:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-26T19:38:40.620' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (2007, CAST(N'2019-12-26T00:00:00.000' AS DateTime), 9, 4, N'Present', CAST(N'2018-01-01T19:38:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-26T19:38:43.283' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (2008, CAST(N'2019-12-26T00:00:00.000' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T20:12:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-26T20:12:17.563' AS DateTime), 4, CAST(N'2019-12-26T20:12:38.000' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (2009, CAST(N'2019-12-26T00:00:00.000' AS DateTime), 16, 4, N'Present(Late)', CAST(N'2018-01-01T20:12:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-26T20:12:57.310' AS DateTime), 4, CAST(N'2019-12-26T20:13:14.933' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3009, CAST(N'2019-12-27T00:00:00.000' AS DateTime), 3015, 3, N'Absent', CAST(N'2018-01-01T18:06:00.000' AS DateTime), 1, 1, 3, CAST(N'2019-12-27T18:06:36.807' AS DateTime), 3, CAST(N'2019-12-27T19:23:56.457' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3010, CAST(N'2019-12-27T00:00:00.000' AS DateTime), 16, 4, N'Present', CAST(N'2018-01-01T18:07:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-27T18:07:54.207' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3011, CAST(N'2019-12-27T00:00:00.000' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T18:24:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-27T18:24:14.147' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3012, CAST(N'2019-12-27T00:00:00.000' AS DateTime), 2014, 3, N'Present', CAST(N'2018-01-01T18:25:00.000' AS DateTime), 1, 1, 3, CAST(N'2019-12-27T18:25:17.140' AS DateTime), 3, CAST(N'2019-12-27T18:25:18.173' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3013, CAST(N'2019-12-27T00:00:00.000' AS DateTime), 1, 4, N'Present', CAST(N'2018-01-01T19:16:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-27T19:16:01.990' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3015, CAST(N'2019-12-27T00:00:00.000' AS DateTime), 11, 3, N'Absent', CAST(N'2018-01-01T19:52:00.000' AS DateTime), 1, 1, 3, CAST(N'2019-12-27T19:52:22.567' AS DateTime), 4, CAST(N'2019-12-27T19:55:59.753' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3016, CAST(N'2019-12-27T00:00:00.000' AS DateTime), 13, 4, N'Present', CAST(N'2018-01-01T19:58:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-27T19:58:54.127' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (4009, CAST(N'2020-01-11T00:00:00.000' AS DateTime), 1, 4, N'Present', CAST(N'2018-01-01T04:33:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-01-11T04:33:17.153' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (4010, CAST(N'2020-01-11T00:00:00.000' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T04:33:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-01-11T04:33:20.187' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (4011, CAST(N'2020-01-11T00:00:00.000' AS DateTime), 4023, 4, N'Present', CAST(N'2018-01-01T04:33:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-01-11T04:33:25.300' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5009, CAST(N'2020-01-22T00:00:00.000' AS DateTime), 3, 3, N'Present', CAST(N'2018-01-01T12:01:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-22T12:01:22.493' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5010, CAST(N'2020-01-23T00:00:00.000' AS DateTime), 1, 1, N'Present', CAST(N'2018-01-01T11:06:00.000' AS DateTime), 1, 1, 1, CAST(N'2020-01-23T11:06:29.500' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5011, CAST(N'2020-01-23T00:00:00.000' AS DateTime), 5016, 3, N'Present', CAST(N'2018-01-01T12:48:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-23T12:48:06.947' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5012, CAST(N'2020-01-23T00:00:00.000' AS DateTime), 3015, 3, N'Present', CAST(N'2018-01-01T16:34:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-23T16:34:21.473' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5013, CAST(N'2020-01-23T00:00:00.000' AS DateTime), 4018, 3, N'Present', CAST(N'2018-01-01T16:51:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-23T16:51:34.087' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5014, CAST(N'2020-01-23T00:00:00.000' AS DateTime), 9, 1, N'Present', CAST(N'2018-01-01T17:07:00.000' AS DateTime), 1, 1, 1, CAST(N'2020-01-23T17:07:49.990' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5016, CAST(N'2020-01-26T04:36:57.763' AS DateTime), 1, 4, N'Present', CAST(N'2018-01-01T04:36:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-01-26T04:36:57.777' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5017, CAST(N'2020-01-26T05:03:36.913' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T05:03:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-01-26T05:03:36.947' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5018, CAST(N'2020-01-26T05:05:04.653' AS DateTime), 3015, 3, N'Present', CAST(N'2018-01-01T05:05:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-26T05:05:04.717' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5019, CAST(N'2020-01-26T05:05:12.937' AS DateTime), 4025, 3, N'Present', CAST(N'2018-01-01T05:05:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-26T05:05:12.967' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5020, CAST(N'2020-01-26T21:25:35.273' AS DateTime), 5018, 3, N'Present', CAST(N'2018-01-01T21:25:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-26T21:25:35.350' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5021, CAST(N'2020-01-26T21:36:14.810' AS DateTime), 4022, 3, N'Absent', CAST(N'2018-01-01T21:36:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-26T21:36:14.827' AS DateTime), 3, CAST(N'2020-01-26T21:43:31.937' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5022, CAST(N'2020-01-26T21:55:08.473' AS DateTime), 5022, 3, N'Present', CAST(N'2018-01-01T21:55:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-26T21:55:08.520' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5023, CAST(N'2020-01-26T22:01:01.410' AS DateTime), 4018, 3, N'Present', CAST(N'2018-01-01T22:01:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-26T22:01:01.457' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5027, CAST(N'2020-02-01T18:15:59.867' AS DateTime), 3, 3, N'Present', CAST(N'2018-01-01T18:16:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-01T18:16:00.047' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5029, CAST(N'2020-02-01T18:49:22.180' AS DateTime), 9, 1, N'Present', CAST(N'2018-01-01T18:49:00.000' AS DateTime), 1, 1, 1, CAST(N'2020-02-01T18:49:23.530' AS DateTime), 1, CAST(N'2020-02-01T18:52:16.827' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5030, CAST(N'2020-02-03T05:45:12.823' AS DateTime), 1014, 3, N'Present', CAST(N'2018-01-01T05:45:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-03T05:45:12.887' AS DateTime), 3, CAST(N'2020-02-03T05:45:32.567' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5031, CAST(N'2020-02-03T05:45:17.147' AS DateTime), 4022, 3, N'Absent', CAST(N'2018-01-01T05:45:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-03T05:45:17.180' AS DateTime), 3, CAST(N'2020-02-03T05:47:27.953' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5032, CAST(N'2020-02-03T05:49:50.467' AS DateTime), 5018, 3, N'Absent', CAST(N'2018-01-01T05:49:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-03T05:49:50.497' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5033, CAST(N'2020-02-03T05:49:53.907' AS DateTime), 3015, 3, N'Present', CAST(N'2018-01-01T05:49:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-03T05:49:54.000' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5034, CAST(N'2020-02-03T05:52:45.167' AS DateTime), 5017, 4, N'Present', CAST(N'2018-01-01T05:52:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-03T05:52:45.183' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5035, CAST(N'2020-02-04T16:53:01.830' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T16:53:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-04T16:53:01.927' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5036, CAST(N'2020-02-04T17:02:58.713' AS DateTime), 3, 3, N'Present', CAST(N'2018-01-01T17:02:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-04T17:02:58.857' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5037, CAST(N'2020-02-06T03:58:14.453' AS DateTime), 1, 4, N'Absent', CAST(N'2018-01-01T03:58:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-06T03:58:14.500' AS DateTime), 4, CAST(N'2020-02-06T04:03:50.677' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5038, CAST(N'2020-02-06T04:07:41.113' AS DateTime), 5018, 3, N'Present', CAST(N'2018-01-01T04:07:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-06T04:07:41.207' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5039, CAST(N'2020-02-06T04:08:16.557' AS DateTime), 5017, 4, N'Present', CAST(N'2018-01-01T04:08:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-06T04:08:16.573' AS DateTime), 4, CAST(N'2020-02-06T06:02:59.737' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5040, CAST(N'2020-02-06T04:13:43.910' AS DateTime), 5020, 3, N'Absent', CAST(N'2018-01-01T04:13:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-06T04:13:43.987' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5041, CAST(N'2020-02-06T04:14:07.550' AS DateTime), 5021, 4, N'Absent', CAST(N'2018-01-01T04:14:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-06T04:14:07.580' AS DateTime), 4, CAST(N'2020-02-06T04:14:37.537' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6037, CAST(N'2020-02-08T02:22:11.497' AS DateTime), 5020, 3, N'Present', CAST(N'2018-01-01T02:22:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T02:22:11.607' AS DateTime), 3, CAST(N'2020-02-08T02:33:49.733' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6038, CAST(N'2020-02-08T15:35:57.913' AS DateTime), 11, 3, N'Absent', CAST(N'2018-01-01T15:35:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T15:35:58.273' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6039, CAST(N'2020-02-08T15:36:08.123' AS DateTime), 3015, 3, N'Present', CAST(N'2018-01-01T15:36:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T15:36:08.187' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6040, CAST(N'2020-02-08T15:53:27.560' AS DateTime), 4018, 3, N'Present', CAST(N'2018-01-01T15:53:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T15:53:27.590' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6043, CAST(N'2020-02-08T21:12:44.437' AS DateTime), 5018, 3, N'Present', CAST(N'2018-01-01T21:12:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T21:12:44.780' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6044, CAST(N'2020-02-08T21:22:42.583' AS DateTime), 1014, 3, N'Present', CAST(N'2018-01-01T21:22:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T21:22:42.613' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6045, CAST(N'2020-02-08T21:22:55.907' AS DateTime), 2014, 3, N'Present', CAST(N'2018-01-01T21:22:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T21:22:55.970' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6046, CAST(N'2020-02-08T21:44:41.277' AS DateTime), 6033, 3, N'Present', CAST(N'2018-01-01T21:44:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T21:44:41.307' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6055, CAST(N'2020-02-10T01:17:37.820' AS DateTime), 5017, 4, N'Present', CAST(N'2018-01-01T01:17:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-10T01:17:37.853' AS DateTime), 4, CAST(N'2020-02-10T01:18:44.560' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6056, CAST(N'2020-02-10T01:25:21.813' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T01:25:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-10T01:25:21.860' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6060, CAST(N'2020-02-13T20:32:07.500' AS DateTime), 3, 3, N'Absent', CAST(N'2018-01-01T20:32:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-13T20:32:07.843' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6061, CAST(N'2020-02-13T20:32:11.357' AS DateTime), 1014, 3, N'Absent', CAST(N'2018-01-01T20:32:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-13T20:32:11.540' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6062, CAST(N'2020-02-13T20:34:00.250' AS DateTime), 1, 1, N'Present', CAST(N'2018-01-01T20:34:00.000' AS DateTime), 1, 1, 1, CAST(N'2020-02-13T20:34:02.963' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6063, CAST(N'2020-02-13T20:35:56.970' AS DateTime), 2, 1, N'Present', CAST(N'2018-01-01T20:35:00.000' AS DateTime), 1, 1, 1, CAST(N'2020-02-13T20:35:59.347' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6064, CAST(N'2020-02-13T20:38:25.753' AS DateTime), 4, 1, N'Present', CAST(N'2018-01-01T20:38:00.000' AS DateTime), 1, 1, 1, CAST(N'2020-02-13T20:38:28.187' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6065, CAST(N'2020-02-13T21:06:44.987' AS DateTime), 6037, 3, N'Present', CAST(N'2018-01-01T21:08:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-13T21:08:05.337' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6066, CAST(N'2020-02-13T21:10:47.550' AS DateTime), 10, 1, N'Present', CAST(N'2018-01-01T21:10:00.000' AS DateTime), 1, 1, 1, CAST(N'2020-02-13T21:10:47.707' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6067, CAST(N'2020-02-13T21:11:28.187' AS DateTime), 2014, 3, N'Absent', CAST(N'2018-01-01T21:15:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-13T21:15:43.233' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (8, CAST(N'2019-12-20T00:00:00.000' AS DateTime), 9, 4, N'Present', CAST(N'2018-01-01T17:01:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-20T17:01:16.910' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (9, CAST(N'2019-12-20T00:00:00.000' AS DateTime), 4, 4, N'Present', CAST(N'2018-01-01T17:01:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-20T17:01:21.197' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (10, CAST(N'2019-12-20T00:00:00.000' AS DateTime), 5, 4, N'Present', CAST(N'2018-01-01T17:01:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-20T17:01:23.243' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5026, CAST(N'2020-01-30T17:16:17.147' AS DateTime), 5024, 3, N'Present', CAST(N'2018-01-01T17:16:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-30T17:16:17.207' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6041, CAST(N'2020-02-08T16:11:03.807' AS DateTime), 4025, 3, N'Present', CAST(N'2018-01-01T16:11:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T16:11:03.840' AS DateTime), 3, CAST(N'2020-02-08T21:22:36.000' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6047, CAST(N'2020-02-09T05:45:28.167' AS DateTime), 1, 4, N'Present', CAST(N'2018-01-01T05:45:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-09T05:45:28.180' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6048, CAST(N'2020-02-09T06:06:04.717' AS DateTime), 2014, 3, N'Absent', CAST(N'2018-01-01T06:06:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-09T06:06:04.730' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6049, CAST(N'2020-02-09T22:07:44.070' AS DateTime), 1014, 3, N'Present', CAST(N'2018-01-01T22:07:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-09T22:07:44.320' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6050, CAST(N'2020-02-09T22:07:50.090' AS DateTime), 3015, 3, N'Absent', CAST(N'2018-01-01T22:07:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-09T22:07:50.310' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6051, CAST(N'2020-02-09T22:10:36.487' AS DateTime), 5020, 3, N'Absent', CAST(N'2018-01-01T22:10:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-09T22:10:36.533' AS DateTime), 3, CAST(N'2020-02-09T22:30:18.457' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6052, CAST(N'2020-02-09T22:13:29.583' AS DateTime), 11, 3, N'Present', CAST(N'2018-01-01T22:13:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-09T22:13:29.613' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6053, CAST(N'2020-02-09T22:26:10.920' AS DateTime), 4025, 3, N'Absent', CAST(N'2018-01-01T22:26:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-09T22:26:10.953' AS DateTime), 3, CAST(N'2020-02-09T22:30:31.983' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6054, CAST(N'2020-02-09T23:05:37.280' AS DateTime), 2, 4, N'Present', CAST(N'2018-01-01T23:05:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-09T23:05:37.297' AS DateTime), 4, CAST(N'2020-02-09T23:06:49.237' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6057, CAST(N'2020-02-10T02:16:24.473' AS DateTime), 1, 4, N'Present', CAST(N'2018-01-01T02:16:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-10T02:16:24.490' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (2010, CAST(N'2019-12-26T00:00:00.000' AS DateTime), 3016, 3, N'Present', CAST(N'2018-01-01T20:19:00.000' AS DateTime), 1, 1, 3, CAST(N'2019-12-26T20:19:52.457' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3019, CAST(N'2019-12-30T00:00:00.000' AS DateTime), 1, 4, N'Present', CAST(N'2018-01-01T17:05:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-30T17:05:05.027' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3020, CAST(N'2019-12-30T00:00:00.000' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T17:05:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-30T17:05:06.777' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3021, CAST(N'2019-12-30T00:00:00.000' AS DateTime), 3, 3, N'Present', CAST(N'2018-01-01T17:50:00.000' AS DateTime), 1, 1, 3, CAST(N'2019-12-30T17:50:21.373' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5028, CAST(N'2020-02-01T18:26:50.990' AS DateTime), 11, 3, N'Absent', CAST(N'2018-01-01T18:26:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-01T18:26:51.070' AS DateTime), 3, CAST(N'2020-02-01T18:51:31.253' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5042, CAST(N'2020-02-06T04:23:47.087' AS DateTime), 2, 4, N'Present', CAST(N'2018-01-01T04:23:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-06T04:23:47.103' AS DateTime), 4, CAST(N'2020-02-06T04:32:39.387' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5043, CAST(N'2020-02-06T04:50:27.287' AS DateTime), 9, 4, N'Present', CAST(N'2018-01-01T04:50:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-06T04:50:27.320' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5044, CAST(N'2020-02-06T04:50:31.950' AS DateTime), 16, 4, N'Present', CAST(N'2018-01-01T04:50:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-06T04:50:31.983' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5045, CAST(N'2020-02-06T06:05:10.310' AS DateTime), 13, 4, N'Absent', CAST(N'2018-01-01T06:05:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-06T06:05:10.327' AS DateTime), 4, CAST(N'2020-02-06T06:07:20.723' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5046, CAST(N'2020-02-06T06:06:36.287' AS DateTime), 4018, 3, N'Present', CAST(N'2018-01-01T06:06:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-06T06:06:36.787' AS DateTime), 3, CAST(N'2020-02-06T06:07:42.227' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5047, CAST(N'2020-02-07T09:39:18.850' AS DateTime), 5017, 4, N'Present', CAST(N'2018-01-01T09:39:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-07T09:39:18.943' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5048, CAST(N'2020-02-07T09:40:39.873' AS DateTime), 5018, 3, N'Present', CAST(N'2018-01-01T09:40:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-07T09:40:39.957' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5049, CAST(N'2020-02-07T09:43:25.050' AS DateTime), 5021, 4, N'Present', CAST(N'2018-01-01T09:43:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-07T09:43:25.083' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5050, CAST(N'2020-02-07T10:00:41.313' AS DateTime), 5032, 2007, N'Present', CAST(N'2018-01-01T10:00:00.000' AS DateTime), 1, 1, 2007, CAST(N'2020-02-07T10:00:41.377' AS DateTime), NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5051, CAST(N'2020-02-07T10:07:31.597' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T10:07:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-07T10:07:31.627' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5052, CAST(N'2020-02-07T10:09:03.203' AS DateTime), 9, 4, N'Present', CAST(N'2018-01-01T10:09:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-07T10:09:03.220' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5053, CAST(N'2020-02-07T10:16:35.300' AS DateTime), 13, 4, N'Present', CAST(N'2018-01-01T10:16:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-07T10:16:35.317' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3014, CAST(N'2019-12-27T00:00:00.000' AS DateTime), 9, 4, N'Present', CAST(N'2018-01-01T19:41:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-27T19:41:08.587' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3017, CAST(N'2019-12-28T00:00:00.000' AS DateTime), 10, 4, N'Present', CAST(N'2018-01-01T04:03:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-28T04:03:35.940' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (3018, CAST(N'2019-12-28T00:00:00.000' AS DateTime), 16, 4, N'Present', CAST(N'2018-01-01T04:03:00.000' AS DateTime), 1, 1, 4, CAST(N'2019-12-28T04:03:46.417' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5015, CAST(N'2020-01-25T12:44:29.993' AS DateTime), 11, 3, N'Present', CAST(N'2018-01-01T12:44:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-25T12:44:30.613' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5024, CAST(N'2020-01-30T14:42:00.963' AS DateTime), 5023, 3, N'Present', CAST(N'2018-01-01T14:42:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-30T14:42:01.073' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (5025, CAST(N'2020-01-30T14:53:12.787' AS DateTime), 3015, 3, N'Present', CAST(N'2018-01-01T14:53:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-01-30T14:53:12.817' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6042, CAST(N'2020-02-08T16:14:49.313' AS DateTime), 3, 3, N'Present', CAST(N'2018-01-01T16:14:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-08T16:14:49.343' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6058, CAST(N'2020-02-10T03:18:50.140' AS DateTime), 5021, 4, N'Present', CAST(N'2018-01-01T03:18:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-10T03:18:50.250' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6059, CAST(N'2020-02-10T03:19:44.177' AS DateTime), 16, 4, N'Present', CAST(N'2018-01-01T03:19:00.000' AS DateTime), 1, 1, 4, CAST(N'2020-02-10T03:19:44.197' AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISAttendance] ([ID], [Date], [StudentID], [TeacherID], [Status], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AttendenceComplete]) VALUES (6068, CAST(N'2020-02-13T21:20:45.643' AS DateTime), 6038, 3, N'Present', CAST(N'2018-01-01T21:21:00.000' AS DateTime), 1, 1, 3, CAST(N'2020-02-13T21:21:12.807' AS DateTime), NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISAttendance] OFF
SET IDENTITY_INSERT [dbo].[ISAuthToken] ON 

INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (1, N'234804ce-e0a3-4bb3-ac58-7d41e448d0f6', N'null', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (2, N'bc790653-d8f1-4a98-9d5e-e0b1b94f51b5', N'358240051111110', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (3, N'b1a360fb-7193-4557-a9c5-5299d9c69d72', N'null', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (4, N'20412122-dfe3-4d72-87ed-79558b70c020', N'358240051111110', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (5, N'0e7a4b45-7860-4ff9-b115-1c4a669200e3', N'null', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (6, N'17638df1-eb4b-4a5d-9e39-9634552643b1', N'358240051111110', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (7, N'117d58f9-a521-44d2-b788-9f5922b4f2d0', N'358240051111110', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (8, N'f4c7d029-69ac-4b75-9086-fe31ff895427', N'358240051111110', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (9, N'730d4656-cfdb-4123-9fc2-8f351cd2c751', N'null', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (10, N'ec3f9489-3a82-42b8-991e-d360992d33f1', N'c3ae0bb5ea6c7cdc', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (11, N'86a78c4d-f60f-472c-98a8-af27cb0f9bf4', N'null', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (12, N'47c29936-0985-44cf-9e77-217ad8710833', N'911596807497222', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (13, N'5e4ea23c-9baf-40db-94fc-02104c5ba699', N'911596807497222', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (14, N'748e1d8d-0511-471b-a874-48bd8be67cb0', N'null', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (15, N'40b6b659-e463-4c72-98bd-4da414ce0e1b', N'865833040176057', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (16, N'67f9a692-37ab-44bf-a3ec-7b9ead108e14', N'358240051111110', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (17, N'1e7ed611-6612-4148-a57e-0b195e58b91d', N'358240051111110', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (18, N'1f59cba0-dec3-4cac-a6ba-e6d0bfeae989', N'358240051111110', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (19, N'1f2a50db-3749-4883-95dc-0d180e81dd15', N'358240051111110', 1, 1, NULL, NULL)
INSERT [dbo].[ISAuthToken] ([ID], [Token], [IMEINO], [Active], [Deleted], [CreatedBy], [CreatedDatetime]) VALUES (20, N'15751342-b5f6-4ab6-9e1b-4fe5bc2d20f1', N'358240051111110', 1, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISAuthToken] OFF
SET IDENTITY_INSERT [dbo].[ISClass] ON 

INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (1, 1, N'Office Class(Office)', NULL, 5, NULL, NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:09:09.460' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (2, 2, N'Local Class', NULL, 1, NULL, NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-16T12:14:18.133' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (3, 2, N'Outside Class', NULL, 1, NULL, NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-16T12:14:18.223' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (4, 1, N'After School Class(After School Ex)', N'', 3, N'External', N'After Gatistavam School', CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-16T12:25:24.053' AS DateTime), 1, 1, CAST(N'2020-02-13T20:35:24.407' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (5, 1, N'Class 1', N'1', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-16T12:26:50.983' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (6, 1, N'Class 2', N'2', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-16T12:27:15.497' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (13, 1, N'Class 9', N'7', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-17T14:28:48.123' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (14, 3, N'Local Class', NULL, 1, NULL, NULL, NULL, NULL, 1, 1, 3, CAST(N'2020-02-07T09:19:51.543' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (15, 3, N'Outside Class', NULL, 1, NULL, NULL, NULL, NULL, 1, 1, 3, CAST(N'2020-02-07T09:19:51.573' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (16, 4, N'Office Class(Office)', NULL, 5, NULL, NULL, NULL, NULL, 1, 1, 4, CAST(N'2020-02-07T09:36:14.747' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (17, 4, N'Fox', N'5', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 4, CAST(N'2020-02-07T09:37:14.747' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (1014, 1, N'Classes1', N'5', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2020-02-28T15:19:53.667' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (1015, 1, N'Classes2', N'5', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2020-02-28T15:22:10.847' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (1016, 1, N'Classes3', N'3', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2020-02-28T15:26:56.137' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (1017, 1, N'Classes4', N'5', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2020-02-28T15:38:23.647' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (7, 1, N'Class 3', N'3', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-16T12:27:39.740' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (8, 1, N'Class 4', N'4', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-16T12:28:03.023' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (9, 1, N'Class 5', N'5', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-16T12:28:26.557' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (10, 1, N'Class 6', N'6', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-16T12:28:53.983' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (11, 1, N'Class 7', N'7', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-16T12:29:16.043' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ISClass] ([ID], [SchoolID], [Name], [Year], [TypeID], [AfterSchoolType], [ExternalOrganisation], [EndDate], [PickupComplete], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [ISNonListed]) VALUES (12, 1, N'Class 8', N'8', 1, NULL, NULL, CAST(N'2050-01-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, CAST(N'2019-12-16T12:29:41.453' AS DateTime), NULL, NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[ISClass] OFF
SET IDENTITY_INSERT [dbo].[ISClassType] ON 

INSERT [dbo].[ISClassType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Standard', 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClassType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'After School', 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClassType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (5, N'Office', 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISClassType] OFF
SET IDENTITY_INSERT [dbo].[ISClassYear] ON 

INSERT [dbo].[ISClassYear] ([ID], [Year], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Nursery', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClassYear] ([ID], [Year], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'Reception', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClassYear] ([ID], [Year], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'Year 1', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClassYear] ([ID], [Year], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, N'Year 2', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClassYear] ([ID], [Year], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (5, N'Year 3', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClassYear] ([ID], [Year], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (6, N'Year 4', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClassYear] ([ID], [Year], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (7, N'Year 5', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISClassYear] ([ID], [Year], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (8, N'Year 6', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISClassYear] OFF
SET IDENTITY_INSERT [dbo].[ISCompleteAttendanceRun] ON 

INSERT [dbo].[ISCompleteAttendanceRun] ([ID], [Date], [ClassID], [TeacherID], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, CAST(N'2019-12-19T09:24:21.960' AS DateTime), 7, 4, NULL, 1, 0, 4, CAST(N'2019-12-19T09:24:22.023' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISCompleteAttendanceRun] ([ID], [Date], [ClassID], [TeacherID], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, CAST(N'2019-12-26T20:12:42.660' AS DateTime), 5, 4, NULL, 1, 0, 4, CAST(N'2019-12-26T20:12:42.723' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISCompleteAttendanceRun] ([ID], [Date], [ClassID], [TeacherID], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, CAST(N'2019-12-26T20:13:04.363' AS DateTime), 7, 4, NULL, 1, 0, 4, CAST(N'2019-12-26T20:13:04.363' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISCompleteAttendanceRun] ([ID], [Date], [ClassID], [TeacherID], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1002, CAST(N'2020-02-06T04:50:45.067' AS DateTime), 7, 4, NULL, 1, 0, 4, CAST(N'2020-02-06T04:50:45.147' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISCompleteAttendanceRun] OFF
SET IDENTITY_INSERT [dbo].[ISCompletePickupRun] ON 

INSERT [dbo].[ISCompletePickupRun] ([ID], [Date], [ClassID], [TeacherID], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, CAST(N'2020-02-08T19:03:50.447' AS DateTime), 7, 4, NULL, 1, 0, 4, CAST(N'2020-02-08T19:03:50.493' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISCompletePickupRun] ([ID], [Date], [ClassID], [TeacherID], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, CAST(N'2020-02-08T19:13:21.787' AS DateTime), 6, 4, NULL, 1, 0, 4, CAST(N'2020-02-08T19:13:21.847' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISCompletePickupRun] ([ID], [Date], [ClassID], [TeacherID], [Time], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, CAST(N'2020-02-11T23:38:33.770' AS DateTime), 5, 4, NULL, 1, 0, 4, CAST(N'2020-02-11T23:38:34.520' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISCompletePickupRun] OFF
SET IDENTITY_INSERT [dbo].[ISCountries] ON 

INSERT [dbo].[ISCountries] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'UK', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISCountries] OFF

SET IDENTITY_INSERT [dbo].[ISHoliday] ON 

INSERT [dbo].[ISHoliday] ([ID], [SchoolID], [Name], [DateFrom], [DateTo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, 1, N'Xpas', CAST(N'2019-12-21T00:00:00.000' AS DateTime), CAST(N'2019-12-21T00:00:00.000' AS DateTime), 0, 0, 1, CAST(N'2019-12-18T18:58:17.210' AS DateTime), 1, CAST(N'2020-02-29T16:24:17.037' AS DateTime), 1, CAST(N'2020-02-29T16:29:02.057' AS DateTime))
INSERT [dbo].[ISHoliday] ([ID], [SchoolID], [Name], [DateFrom], [DateTo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, 1, N'Xpas', CAST(N'2019-12-21T00:00:00.000' AS DateTime), CAST(N'2019-12-22T00:00:00.000' AS DateTime), 1, 1, 1, CAST(N'2019-12-18T18:58:46.747' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISHoliday] ([ID], [SchoolID], [Name], [DateFrom], [DateTo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, 1, N'Xpas', CAST(N'2019-12-22T00:00:00.000' AS DateTime), CAST(N'2019-12-22T00:00:00.000' AS DateTime), 1, 1, 1, CAST(N'2019-12-18T18:59:08.603' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISHoliday] ([ID], [SchoolID], [Name], [DateFrom], [DateTo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, 1, N'Xpas', CAST(N'2019-12-22T00:00:00.000' AS DateTime), CAST(N'2019-12-23T00:00:00.000' AS DateTime), 1, 1, 1, CAST(N'2019-12-18T19:01:22.047' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISHoliday] ([ID], [SchoolID], [Name], [DateFrom], [DateTo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (5, 1, N'Sky', CAST(N'2020-01-20T00:00:00.000' AS DateTime), CAST(N'2020-01-20T00:00:00.000' AS DateTime), 1, 1, 1, CAST(N'2020-01-19T22:12:06.337' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISHoliday] ([ID], [SchoolID], [Name], [DateFrom], [DateTo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (6, 1, N'Christmas', CAST(N'2020-02-18T00:00:00.000' AS DateTime), CAST(N'2020-02-18T00:00:00.000' AS DateTime), 0, 1, 1, CAST(N'2020-02-18T04:40:40.260' AS DateTime), 1, CAST(N'2020-02-18T04:44:46.470' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISHoliday] OFF
SET IDENTITY_INSERT [dbo].[ISLogType] ON 

INSERT [dbo].[ISLogType] ([ID], [LogTypeName], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Incidence', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISLogType] ([ID], [LogTypeName], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'Error', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISLogType] ([ID], [LogTypeName], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'Enhancement', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISLogType] ([ID], [LogTypeName], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, N'Clear Logs', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISLogType] ([ID], [LogTypeName], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (5, N'Request Change to Profile', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISLogType] ([ID], [LogTypeName], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (6, N'General Request', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISLogType] OFF
SET IDENTITY_INSERT [dbo].[ISOrganisationUser] ON 

INSERT [dbo].[ISOrganisationUser] ([ID], [FirstName], [LastName], [OrgCode], [Photo], [Address1], [Address2], [Town], [CountryID], [Email], [StartDate], [EndDate], [RoleID], [Password], [StatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [CreatedByName], [ActivationBy], [ActivationDate], [LastUpdatedBy]) VALUES (1, N'Admin Officer', N'A1', N'ORG001', N'Upload/businessman.png', N'London, UK', N'London, UK', N'London', 1, N'adminofficer@gmail.com', CAST(N'2019-12-16T11:56:10.117' AS DateTime), CAST(N'2050-01-01T00:00:00.000' AS DateTime), 1, N'FjeFZFDSu3ozOOciL3Lz0g==', N'Active', 1, 1, 1, CAST(N'2019-12-16T11:56:10.117' AS DateTime), NULL, NULL, NULL, NULL, N'Admin', N'Admin', CAST(N'2019-12-16T11:56:10.117' AS DateTime), NULL)
INSERT [dbo].[ISOrganisationUser] ([ID], [FirstName], [LastName], [OrgCode], [Photo], [Address1], [Address2], [Town], [CountryID], [Email], [StartDate], [EndDate], [RoleID], [Password], [StatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [CreatedByName], [ActivationBy], [ActivationDate], [LastUpdatedBy]) VALUES (2, N'Buisness Dev. Officer', N'B1', N'ORG002', N'Upload/businessman.png', N'London, UK', N'London, UK', N'London', 1, N'businessdev@gmail.com', CAST(N'2019-12-16T11:59:12.563' AS DateTime), CAST(N'2050-01-01T00:00:00.000' AS DateTime), 2, N'FjeFZFDSu3ozOOciL3Lz0g==', N'Active', 1, 1, 1, CAST(N'2019-12-16T11:59:12.563' AS DateTime), NULL, NULL, NULL, NULL, N'Admin', N'Admin', CAST(N'2019-12-16T11:59:12.563' AS DateTime), NULL)
INSERT [dbo].[ISOrganisationUser] ([ID], [FirstName], [LastName], [OrgCode], [Photo], [Address1], [Address2], [Town], [CountryID], [Email], [StartDate], [EndDate], [RoleID], [Password], [StatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [CreatedByName], [ActivationBy], [ActivationDate], [LastUpdatedBy]) VALUES (3, N'Support Officer', N'C1', N'ORG003', N'Upload/businessman.png', N'London, UK', N'London, UK', N'London', 1, N'support@gmail.com', CAST(N'2019-12-16T12:02:14.127' AS DateTime), CAST(N'2050-01-01T00:00:00.000' AS DateTime), 3, N'FjeFZFDSu3ozOOciL3Lz0g==', N'Active', 1, 1, 1, CAST(N'2019-12-16T12:02:14.127' AS DateTime), NULL, NULL, NULL, NULL, N'Admin', N'Admin', CAST(N'2019-12-16T12:02:14.127' AS DateTime), NULL)
INSERT [dbo].[ISOrganisationUser] ([ID], [FirstName], [LastName], [OrgCode], [Photo], [Address1], [Address2], [Town], [CountryID], [Email], [StartDate], [EndDate], [RoleID], [Password], [StatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [CreatedByName], [ActivationBy], [ActivationDate], [LastUpdatedBy]) VALUES (4, N'Apps Admin', N'D1', N'ORG004', N'Upload/businesswoman2.png', N'London, UK', N'London, UK', N'London', 1, N'appsadmin@gmail.com', CAST(N'2019-12-16T12:03:36.927' AS DateTime), CAST(N'2050-01-01T00:00:00.000' AS DateTime), 4, N'FjeFZFDSu3ozOOciL3Lz0g==', N'Active', 1, 1, 1, CAST(N'2019-12-16T12:03:36.927' AS DateTime), NULL, NULL, NULL, NULL, N'Admin', N'Admin', CAST(N'2019-12-16T12:03:36.927' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ISOrganisationUser] OFF
SET IDENTITY_INSERT [dbo].[ISPicker] ON 

INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1, 1, 1, 1, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:31:32.293' AS DateTime), 1, CAST(N'2020-02-01T17:15:10.520' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (2, 1, 1, 1, NULL, N'Jagruti Parmar(Mother)', NULL, N'Upload/user.jpg', N'jaggu@gmail.com', N'9876543211', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:31:32.523' AS DateTime), 1, CAST(N'2019-12-30T17:56:19.070' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (3, 1, 1, 1, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:33:36.843' AS DateTime), 1, CAST(N'2020-02-01T17:15:12.680' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (4, 1, 2, 2, NULL, N'Jagruti Parmar(Mother)', NULL, N'Upload/user.jpg', N'jaggu@gmail.com', N'9876543211', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:33:37.043' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5, 2, 3, 3, NULL, N'Vismay Gohel(Father)', NULL, N'Upload/user.jpg', N'vismay@gmail.com', N'9876543215', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-16T12:51:08.700' AS DateTime), 2, CAST(N'2020-01-30T17:24:48.140' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6, 2, 3, 3, NULL, N'Sumer Parmar(Relative)', NULL, N'Upload/user.jpg', N'sumer@ymail.com', N'9876543218', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 2, CAST(N'2019-12-16T12:51:08.880' AS DateTime), NULL, NULL, 2, CAST(N'2020-01-30T17:24:06.127' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7, 1, 4, 4, NULL, N'Ayo Ojo(Father)', NULL, N'Upload/user.jpg', N'olatunyo@yahoo.co.uk', N'02002020200', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-18T03:29:28.607' AS DateTime), 1, CAST(N'2019-12-18T17:19:42.020' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (8, 1, 4, 4, NULL, N'Olude Ojo(Relative)', NULL, N'Upload/user.jpg', N'Debbo4@hotmail.com', N'23938383993', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-18T03:29:28.777' AS DateTime), 1, CAST(N'2019-12-18T17:19:42.067' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (9, 1, 5, 5, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-18T12:25:08.117' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.150' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (10, 1, 5, 5, NULL, N'Olude Ojo(Friends)', NULL, N'Upload/user.jpg', N'Debbo4@hotmail.com', N'9239393939', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-18T12:25:08.210' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (13, 1, 5, 5, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-19T09:28:27.580' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.180' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (14, 2, 7, 7, NULL, N'Olude Ojo(Friends)', NULL, N'Upload/user.jpg', N'Debbo4@hotmail.com', N'9239393939', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-19T09:28:27.600' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (17, 1, 5, 5, N'Mr.', N'James', N'Lee', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 5, CAST(N'2019-12-19T19:35:43.607' AS DateTime), 5, CAST(N'2019-12-19T19:44:45.193' AS DateTime), 5, CAST(N'2019-12-19T19:45:44.907' AS DateTime), 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (27, 1, 5, 5, N'', N'James', N'Ford', N'Upload/user.jpg', N'olatunyo@yahoo.com', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 5, CAST(N'2019-12-20T15:24:00.083' AS DateTime), NULL, NULL, 5, CAST(N'2019-12-20T15:25:07.553' AS DateTime), 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (28, 1, 5, 5, N'', N'James', N'Lee', N'Upload/user.jpg', N'olatunyo@yahoo.co.uk', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 5, CAST(N'2019-12-20T15:26:16.450' AS DateTime), NULL, NULL, 5, CAST(N'2019-12-20T15:37:58.757' AS DateTime), 1, N'ayobimbo2@yahoo.com', 1, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (29, 1, 5, 5, N'', N'Philip', N'Jones', N'Upload/user.jpg', N'olatunyo@yahoo.co.uk', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 5, CAST(N'2019-12-20T15:41:56.777' AS DateTime), NULL, NULL, 5, CAST(N'2019-12-21T17:02:57.623' AS DateTime), 1, N'ayobimbo2@yahoo.com', 1, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (30, 1, 1, 1, N'', N'Jaden', N'Crown', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-20T15:54:05.167' AS DateTime), NULL, NULL, NULL, NULL, 1, N'jaggu@gmail.com', 0, N'Jagruti Parmar')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (35, 2, 12, 12, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-20T20:59:25.500' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.213' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (36, 2, 12, 12, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-20T20:59:25.547' AS DateTime), 1, CAST(N'2020-02-01T17:15:50.307' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (37, 1, 13, 13, NULL, N'Olude Ojo(Father)', NULL, N'Upload/user.jpg', N'Debbo4@yahoo.com', N'02929292929', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-21T14:15:52.000' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (38, 1, 13, 13, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-21T14:15:52.140' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.243' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1020, 1, 1, 1, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-22T23:34:46.020' AS DateTime), 1, CAST(N'2020-02-01T17:15:14.900' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1021, 2, 15, 15, NULL, N'Jagruti Parmar(Mother)', NULL, N'Upload/user.jpg', N'jaggu@gmail.com', N'9876543211', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-22T23:34:46.037' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1022, 1, 1, 1, N'0', N'Tumbi', N'Gabriel', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-23T01:28:46.823' AS DateTime), 1, CAST(N'2020-01-13T14:48:19.300' AS DateTime), NULL, NULL, 1, N'rahul@gmail.com', 0, N'Rahul Parmar')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1023, 1, 5, 5, N'0', N'Tom', N'Leen', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2019-12-23T02:15:43.567' AS DateTime), 5, CAST(N'2020-01-26T18:59:45.363' AS DateTime), NULL, NULL, 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1024, 1, 5, 5, N'0', N'Tom', N'Lees', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2019-12-23T02:15:51.477' AS DateTime), 5, CAST(N'2020-01-26T19:01:01.480' AS DateTime), NULL, NULL, 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1025, 1, 16, 16, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-23T02:19:39.070' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.260' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1026, 1, 16, 16, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-23T02:19:39.103' AS DateTime), 1, CAST(N'2020-02-01T17:15:52.787' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1027, 1, 1, 1, N'', N'Fifi', N'Gabriel', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-23T02:27:56.443' AS DateTime), NULL, NULL, NULL, NULL, 1, N'rahul@gmail.com', 0, N'Rahul Parmar')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1028, 1, 5, 5, N'Mr.', N'Jammy', N'Lee', N'../Upload/8A050D46-0199-4C83-98D4-4C96F5E0B379.jpeg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2019-12-23T02:28:59.513' AS DateTime), 5, CAST(N'2020-01-01T15:15:52.687' AS DateTime), NULL, NULL, 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (1029, 1, 5, 5, N'', N'Garry', N'Levy', N'Upload/user.jpg', N'', N'', NULL, 1, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2019-12-23T03:06:36.027' AS DateTime), NULL, NULL, NULL, NULL, 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (2018, 2, 1014, 1014, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-24T19:27:05.243' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.273' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (2019, 2, 1014, 1014, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-24T19:27:05.400' AS DateTime), 1, CAST(N'2020-02-01T17:15:52.837' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (3018, 2, 2014, 2014, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-26T06:46:49.950' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.307' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (4021, 2, 3016, 3016, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-26T20:17:48.770' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.323' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5021, 2, 4016, 4016, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-27T18:13:15.220' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.357' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (4020, 2, 3015, 3015, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-26T19:05:26.317' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.340' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5022, 2, 4016, 4016, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-27T18:13:15.267' AS DateTime), 1, CAST(N'2020-02-01T17:15:52.880' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5023, 2, 4017, 4017, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-27T18:24:28.187' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.370' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5024, 2, 4017, 4017, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-27T18:24:28.203' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5025, 2, 4018, 4018, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-27T18:36:38.833' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.403' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5026, 2, 4019, 4019, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-27T19:41:30.910' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.417' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5027, 2, 4019, 4019, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-27T19:41:30.957' AS DateTime), 1, CAST(N'2020-02-01T17:15:52.923' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5030, 1, 1, 1, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-30T17:05:17.090' AS DateTime), 1, CAST(N'2020-02-01T17:15:17.397' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (18, 1, 9, 9, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-19T20:30:04.457' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.620' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (19, 1, 9, 9, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-19T20:38:24.483' AS DateTime), 1, CAST(N'2020-02-01T17:15:53.143' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (20, 1, 10, 10, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-19T20:46:24.057' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.653' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (21, 1, 10, 10, NULL, N'Ayodele Ojo(Relative)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'043939393', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 1, CAST(N'2019-12-19T20:51:50.103' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-20T06:08:58.307' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (4022, 2, 3016, 3016, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-26T20:17:48.800' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5031, 2, 4021, 4021, NULL, N'Jagruti Parmar(Mother)', NULL, N'Upload/user.jpg', N'jaggu@gmail.com', N'9876543211', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-30T17:05:17.283' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5032, 2, 4022, 4022, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 3, CAST(N'2019-12-30T17:09:35.210' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.433' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5033, 1, 4023, 4023, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-30T17:13:02.860' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.467' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5034, 2, 4024, 4024, NULL, N'Ayo Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'6676667888', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 3, CAST(N'2019-12-30T17:14:06.910' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5035, 1, 1, 1, N'', N'Hardik', N'The Bad Boy', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 1, CAST(N'2019-12-30T17:19:23.517' AS DateTime), NULL, NULL, 1, CAST(N'2020-01-13T14:48:33.723' AS DateTime), 1, N'rahul@gmail.com', 0, N'Rahul Parmar')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5036, 1, 1, 1, N'0', N'Anil', N'Anilsalgar', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-30T17:25:49.607' AS DateTime), 1, CAST(N'2020-01-26T19:08:27.267' AS DateTime), NULL, NULL, 1, N'rahul@gmail.com', 0, N'Rahul Parmar')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5037, 1, 4023, 4023, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2019-12-30T17:42:11.797' AS DateTime), 1, CAST(N'2020-02-01T17:15:52.970' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (5038, 2, 4025, 4025, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-30T17:44:41.047' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.480' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6021, 2, 4018, 4018, NULL, N'Semi Zyne(Relative)', NULL, N'Upload/user.jpg', N'Semi@ymail.com', N'9898965225', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-22T12:21:23.057' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6022, 2, 5016, 5016, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-23T11:06:53.783' AS DateTime), 1, CAST(N'2020-02-01T17:15:53.067' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6023, 2, 5016, 5016, NULL, N'Rahul Parmar(Father)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545352', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-23T11:06:53.857' AS DateTime), 1, CAST(N'2020-02-01T17:15:53.090' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6049, 1, 1, 1, N'', N'JAMES', N'BOND007', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-02-04T16:41:05.423' AS DateTime), NULL, NULL, NULL, NULL, 1, N'rahul@gmail.com', 0, N'Rahul Parmar')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6050, 2, 4022, 4022, NULL, N'Olatunde Tunyo(Relative)', NULL, N'Upload/user.jpg', N'Olatunyo@yahoo.co.uk', N'9595995950', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-04T16:50:38.513' AS DateTime), 2, CAST(N'2020-02-04T16:51:27.907' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6053, 3, 5030, 5030, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 3, CAST(N'2020-02-07T09:31:35.460' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.747' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6054, 3, 5031, 5031, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 3, CAST(N'2020-02-07T09:32:03.287' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.777' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6055, 3, 5032, 5032, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 3, CAST(N'2020-02-07T09:32:36.943' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.793' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6060, 2, 5035, 5035, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-07T10:08:02.470' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.823' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6061, 2, 5035, 5035, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-07T10:08:02.500' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6058, 2, 5034, 5034, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-07T09:44:11.813' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.840' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6059, 2, 5034, 5034, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-07T09:44:11.830' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6064, 2, 5037, 5037, NULL, N'Olude Ojo(Father)', NULL, N'Upload/user.jpg', N'Debbo4@yahoo.com', N'02929292929', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-07T10:22:12.870' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6065, 2, 5037, 5037, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-07T10:22:12.917' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.857' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6066, 1, 5, 5, N'', N'A', N'B', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2020-02-07T15:26:41.167' AS DateTime), NULL, NULL, NULL, NULL, 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojoshardik Olu')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7053, 2, 6030, 6030, NULL, N'Ayo Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'39339393939', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-08T15:45:14.487' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7054, 2, 6030, 6030, NULL, N'Rahul Patel(Relative)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'858585858', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-08T15:45:14.547' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7059, 2, 6033, 6033, NULL, N'Ayo Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'777777669', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-08T21:38:45.223' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7066, 2, 6037, 6037, NULL, N'Ayo Ojo(Father)', NULL, N'Upload/user.jpg', N'olatunyo@yahoo.co.uk', N'02002020200', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-13T21:06:23.597' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7067, 2, 6037, 6037, NULL, N'Olude Ojo(Relative)', NULL, N'Upload/user.jpg', N'Debbo4@hotmail.com', N'23938383993', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-13T21:06:23.673' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7068, 2, 6038, 6038, NULL, N'Ayodele Ojo(Relative)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'043939393', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-13T21:16:32.873' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7069, 2, 6038, 6038, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-13T21:16:32.920' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (8066, 1, 7037, 7037, NULL, N'ayo ojo(Relative)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'939303030', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-02-18T04:30:00.463' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (22, 1, 1, 1, N'', N'James', N'Lee', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 1, CAST(N'2019-12-20T06:21:17.170' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-20T15:18:56.647' AS DateTime), 1, N'rahul@gmail.com', 0, N'Rahul Parmar')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (23, 1, 5, 5, N'', N'James', N'Lee', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 5, CAST(N'2019-12-20T06:22:56.220' AS DateTime), NULL, NULL, 5, CAST(N'2019-12-20T15:13:51.940' AS DateTime), 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (24, 1, 5, 5, N'', N'Hyde Group', N'', N'Upload/DefaultOrg.png', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2019-12-20T06:23:37.173' AS DateTime), NULL, NULL, NULL, NULL, 2, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (25, 1, 5, 5, N'Mr.', N'Fola', N'Lee', N'Upload/user.jpg', N'olatunyo@yahoo.co.uk', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 5, CAST(N'2019-12-20T06:25:15.630' AS DateTime), 5, CAST(N'2019-12-20T06:28:58.350' AS DateTime), 5, CAST(N'2019-12-20T15:25:31.010' AS DateTime), 1, N'ayobimbo2@yahoo.com', 1, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (26, 1, 5, 5, N'', N'Ade ', N'Lee', N'Upload/user.jpg', N'abimbolah13@gmail.com', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2019-12-20T06:29:59.063' AS DateTime), NULL, NULL, NULL, NULL, 1, N'ayobimbo2@yahoo.com', 1, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (31, 1, 5, 5, N'', N'Tara', N'Jones', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2019-12-20T17:04:53.333' AS DateTime), NULL, NULL, NULL, NULL, 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (32, 1, 5, 5, N'', N'John', N'Williams', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2019-12-20T17:05:25.737' AS DateTime), NULL, NULL, NULL, NULL, 1, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (33, 1, 5, 5, N'', N'Design Group', N'', N'Upload/DefaultOrg.png', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 5, CAST(N'2019-12-20T17:07:24.167' AS DateTime), NULL, NULL, NULL, NULL, 2, N'ayobimbo2@yahoo.com', 0, N'Ayoola Ojo')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (34, 2, 11, 11, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2019-12-20T18:56:32.557' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.870' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7060, 4, 6034, 6034, NULL, N'Ayo Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'3883834838', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 4, CAST(N'2020-02-09T23:18:39.167' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7061, 1, 6035, 6035, NULL, N'Ayo Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'3283838383', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-02-13T04:01:15.523' AS DateTime), 1, CAST(N'2020-02-13T04:10:34.573' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7062, 1, 6035, 6035, NULL, N'Ola Ojo(Mother)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'8948484848', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-02-13T04:01:15.680' AS DateTime), 1, CAST(N'2020-02-13T04:10:34.603' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7063, 1, 1, 1, N'', N'Hardik', N'Ojo', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-02-13T04:12:19.297' AS DateTime), NULL, NULL, NULL, NULL, 1, N'rahul@gmail.com', 0, N'Rahul Parmar')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7064, 1, 6036, 6036, NULL, N'Ayo Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9398383838', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-02-13T04:20:42.347' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7065, 1, 6036, 6036, NULL, N'Ola Ojo(Mother)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'27272727', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-02-13T04:20:42.360' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6024, 1, 5017, 5017, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-01-26T21:18:29.907' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.497' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6025, 2, 5018, 5018, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-26T21:21:13.730' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.667' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6026, 2, 5019, 5019, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-26T21:31:24.490' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.510' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6027, 2, 5019, 5019, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-26T21:31:24.507' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6028, 2, 5020, 5020, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 3, CAST(N'2020-01-26T21:50:42.050' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.543' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6029, 1, 5021, 5021, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-01-26T21:53:26.087' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.557' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6030, 2, 5022, 5022, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-26T21:54:29.267' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.573' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6031, 2, 5022, 5022, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-26T21:54:29.297' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6044, 2, 5027, 5027, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-01T18:52:42.167' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.717' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6045, 2, 5027, 5027, NULL, N'Rahul Patel(Relative)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'858585858', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-01T18:52:42.243' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6046, 2, 5028, 5028, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-03T05:55:07.390' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.730' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6047, 2, 5028, 5028, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-03T05:55:07.423' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6048, 1, 1, 1, N'', N'TONY', N'BLAIR', N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 1, CAST(N'2020-02-03T06:03:30.450' AS DateTime), NULL, NULL, NULL, NULL, 1, N'rahul@gmail.com', 0, N'Rahul Parmar')
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6034, 2, 5024, 5024, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-30T17:14:23.513' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.607' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6035, 2, 5024, 5024, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-30T17:14:23.560' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6036, 2, 5025, 5025, NULL, N'Ayoola Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'9484848489', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-30T17:19:10.593' AS DateTime), 1, CAST(N'2020-02-07T15:25:32.683' AS DateTime), NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6037, 2, 5025, 5025, NULL, N'()', NULL, N'Upload/user.jpg', N'', N'', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-30T17:19:10.623' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6038, 2, 3, 3, NULL, N'Rakul Preet(Relative)', NULL, N'Upload/user.jpg', N'rakul@ymail.com', N'9876543212', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-01-30T17:24:48.173' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6041, 2, 4025, 4025, NULL, N'Rahul Parmar(Relative)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876543252', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 0, 0, 2, CAST(N'2020-02-01T17:23:59.703' AS DateTime), NULL, NULL, 2, CAST(N'2020-02-01T17:24:22.927' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6042, 2, 4025, 4025, NULL, N'Rahul Parmar(Relative)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876258252', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-01T17:25:35.843' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (6043, 2, 11, 11, NULL, N'Rahul Parmar(Relative)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'9876545355', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-01T18:02:19.650' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7057, 2, 6032, 6032, NULL, N'Ayo Ojo(Father)', NULL, N'Upload/user.jpg', N'ayobimbo2@yahoo.com', N'55778899909', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-08T16:10:10.300' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[ISPicker] ([ID], [SchoolID], [ParentID], [StudentID], [Title], [FirstName], [LastName], [Photo], [Email], [Phone], [PickupCodeCunter], [OneOffPickerFlag], [EndDate], [ActiveStatus], [ActiveStatusLastUpdatedDate], [ActiveStatusLastUpdatedParent], [ActiveStatusLastUpdatedParentEmail], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [PickerType], [CreatedByEmail], [EmergencyContact], [CreatedByName]) VALUES (7058, 2, 6032, 6032, NULL, N'Anil(Mother)', NULL, N'Upload/user.jpg', N'rahul@gmail.com', N'73747547575', NULL, 0, NULL, N'Active', NULL, NULL, NULL, 1, 1, 2, CAST(N'2020-02-08T16:10:10.330' AS DateTime), NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISPicker] OFF
SET IDENTITY_INSERT [dbo].[ISPickerAssignment] ON 

INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, 1, N'1', NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (2, 2, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (3, 3, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (4, 4, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5, 5, 3, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6, 6, 3, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7, 7, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (8, 8, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (9, 9, 5, N'127855', CAST(N'2019-12-28T04:00:30.037' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL, NULL, 1, N'5', CAST(N'2020-01-30T17:09:52.273' AS DateTime), NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (10, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (13, 13, 7, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (14, 14, 7, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (36, 27, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (37, 27, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (38, 27, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (39, 27, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (40, 28, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (41, 28, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (42, 28, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (43, 28, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (44, 29, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (45, 29, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (46, 29, 9, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2019-12-21T16:50:53.883' AS DateTime), N'1', 1, N'5', NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (47, 29, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (48, 30, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, 1, N'1', NULL, N'Jagruti Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (49, 30, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Jagruti Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (50, 24, 9, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2019-12-21T16:51:08.253' AS DateTime), N'1', NULL, NULL, NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (63, 35, 12, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (64, 36, 12, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (65, 37, 13, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (66, 38, 13, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (67, 29, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (68, 31, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, 1, N'5', NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (69, 32, 9, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2019-12-22T08:14:25.043' AS DateTime), N'1', NULL, NULL, NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1018, 31, 11, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1019, 33, 11, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1022, 1020, 15, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1023, 1021, 15, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1024, 1025, 16, N'808079', CAST(N'2020-02-08T21:19:35.513' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1025, 1026, 16, N'165898', CAST(N'2020-02-08T21:19:45.897' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1026, 1028, 16, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2019-12-23T02:30:23.267' AS DateTime), N'1', NULL, NULL, NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1027, 1028, 16, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1028, 1029, 5, NULL, NULL, NULL, CAST(N'2019-12-23T03:06:36.527' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-23T03:06:52.023' AS DateTime), N'5', 1, NULL, CAST(N'2019-12-23T03:06:36.527' AS DateTime), N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1029, 1029, 16, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (1030, 1029, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (2018, 2018, 1014, N'983970', CAST(N'2020-02-08T21:25:04.730' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (2019, 2019, 1014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (3018, 3018, 2014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (4021, 4021, 3016, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5021, 5021, 4016, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (4020, 4020, 3015, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5022, 5022, 4016, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5023, 5023, 4017, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5024, 5024, 4017, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5025, 5025, 4018, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5026, 5026, 4019, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5027, 5027, 4019, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5030, 1028, 3015, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5031, 1029, 3015, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5032, 1028, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, 1, N'5', CAST(N'2019-12-28T04:03:02.350' AS DateTime), N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5033, 5030, 4021, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5034, 5031, 4021, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5035, 5032, 4022, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5036, 5033, 4023, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5037, 5034, 4024, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5038, 5035, 1014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5039, 5035, 1014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5040, 5036, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, 1, N'1', CAST(N'2019-12-30T17:36:31.027' AS DateTime), N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5041, 5036, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5042, 5037, 4023, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5043, 5038, 4025, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5044, 1028, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5045, 1028, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, 1, N'5', NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5046, 1028, 1014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5047, 1028, 2014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5048, 1028, 4018, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5049, 1028, 4022, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5050, 1028, 4023, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5051, 1028, 4025, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5052, 1028, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5053, 1028, 11, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (5054, 1028, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (18, 18, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (19, 19, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (20, 20, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (21, 21, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (4022, 4022, 3016, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6021, 6021, 4018, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6022, 5036, 1, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2020-01-23T10:38:33.567' AS DateTime), N'1', NULL, NULL, NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6023, 6022, 5016, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6024, 6023, 5016, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6066, 6049, 16, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6067, 6050, 4022, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6070, 6053, 5030, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6071, 6054, 5031, N'143865', CAST(N'2020-02-08T21:24:32.960' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6072, 6055, 5032, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6077, 6060, 5035, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6078, 6061, 5035, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6075, 6058, 5034, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6076, 6059, 5034, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6081, 6064, 5037, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6082, 6065, 5037, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7070, 7053, 6030, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7071, 7054, 6030, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7076, 6066, 2014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7077, 1023, 2014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7078, 1023, 5017, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7079, 6066, 5017, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7080, 24, 5017, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7081, 1023, 1014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7082, 1023, 4023, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7083, 1023, 5031, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7084, 7059, 6033, N'204447', CAST(N'2020-02-08T21:43:03.543' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7085, 1023, 6033, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7097, 7066, 6037, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7098, 7067, 6037, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7099, 7068, 6038, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7100, 7069, 6038, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (8097, 8066, 7037, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (22, 22, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (23, 22, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (24, 23, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (25, 23, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (26, 23, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (27, 23, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (28, 24, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (29, 24, 5, N'840493', CAST(N'2020-02-09T05:33:44.407' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL, NULL, 1, N'5', NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (30, 24, 9, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2019-12-20T15:19:47.760' AS DateTime), N'5', NULL, NULL, NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (31, 24, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (32, 25, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (33, 25, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (34, 25, 9, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2019-12-20T15:19:52.030' AS DateTime), N'5', NULL, NULL, NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (35, 25, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (51, 31, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (52, 31, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, 1, N'5', CAST(N'2019-12-26T19:51:15.153' AS DateTime), N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (53, 31, 9, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2019-12-21T16:50:58.557' AS DateTime), N'1', NULL, NULL, NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (54, 31, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (55, 32, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (56, 32, 9, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2019-12-21T16:51:02.907' AS DateTime), N'1', NULL, NULL, NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (57, 33, 4, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (58, 33, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (59, 33, 9, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2019-12-21T16:51:12.317' AS DateTime), N'1', NULL, NULL, NULL, N'')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (60, 33, 10, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (61, 34, 11, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (62, 29, 11, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7086, 7060, 6034, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7087, 7061, 6035, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7088, 7062, 6035, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7089, 7063, 16, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7090, 7063, 1014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7091, 7063, 1014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7092, 6049, 1014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7093, 6049, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7094, 7063, 6035, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7095, 7064, 6036, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7096, 7065, 6036, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6025, 1023, 4018, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6026, 1029, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6027, 1029, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6028, 31, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6029, 31, 5, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6030, 1024, 5, N'691587', CAST(N'2020-02-09T05:31:45.830' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6031, 26, 2014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6032, 1029, 2014, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6033, 1028, 4018, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6034, 32, 4018, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6035, 33, 4018, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6036, 1023, 13, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6037, 1028, 13, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Ayoola Ojo')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6038, 5036, 16, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6039, 6024, 5017, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6040, 6025, 5018, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6041, 6026, 5019, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6042, 6027, 5019, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6043, 6028, 5020, N'969322', CAST(N'2020-02-08T21:20:41.123' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6044, 6029, 5021, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6045, 6030, 5022, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6046, 6031, 5022, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6060, 6044, 5027, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6061, 6045, 5027, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6062, 6046, 5028, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6063, 6047, 5028, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6064, 6048, 9, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6065, 6048, 11, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6049, 1022, 2, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'Rahul Parmar')
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6050, 6034, 5024, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6051, 6035, 5024, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6052, 6036, 5025, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6053, 6037, 5025, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6054, 6038, 3, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6057, 6041, 4025, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6058, 6042, 4025, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (6059, 6043, 11, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7074, 7057, 6032, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickerAssignment] ([ID], [PickerId], [StudentID], [PickerCode], [PickCodeExDate], [PickCodeLastUpdateDate], [PickCodayExDate], [PickTodayLastUpdateDate], [PickTodayLastUpdateParent], [RemoveChildStatus], [RemoveChildLastUpdateDate], [RemoveChildLastupdateParent], [StudentPickAssignFlag], [StudentPickAssignLastUpdateParent], [StudentPickAssignDate], [StudentAssignBy]) VALUES (7075, 7058, 6032, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISPickerAssignment] OFF
SET IDENTITY_INSERT [dbo].[ISPickup] ON 

INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (1, 1, 5, 4, NULL, CAST(N'2019-12-19T09:22:29.927' AS DateTime), CAST(N'2019-12-19T09:22:29.927' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (2, 2, 7, 4, NULL, CAST(N'2019-12-19T09:25:19.453' AS DateTime), CAST(N'2019-12-19T09:25:19.453' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (4, 4, 6, 4, 8, CAST(N'2019-12-19T09:39:41.350' AS DateTime), CAST(N'2019-12-19T09:39:41.350' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5, 5, 6, 4, NULL, CAST(N'2019-12-19T09:28:27.550' AS DateTime), CAST(N'2019-12-19T09:28:27.550' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6, 7, 3, 4, NULL, CAST(N'2019-12-19T09:28:27.617' AS DateTime), CAST(N'2019-12-19T09:28:27.617' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (8, 1, 5, 1, 1, CAST(N'2019-12-20T22:51:34.223' AS DateTime), CAST(N'2019-12-20T22:51:34.223' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (14, 11, 2, 3, 34, CAST(N'2019-12-20T21:06:03.787' AS DateTime), CAST(N'2019-12-20T21:06:03.787' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (15, 12, 3, 4, NULL, CAST(N'2019-12-20T20:59:25.560' AS DateTime), CAST(N'2019-12-20T20:59:25.560' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (16, 1, 5, 4, 1, CAST(N'2019-12-21T16:38:24.983' AS DateTime), CAST(N'2019-12-21T16:38:24.983' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (1008, 1, 5, 4, NULL, CAST(N'2019-12-22T23:34:45.383' AS DateTime), CAST(N'2019-12-22T23:34:45.383' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (1010, 15, 3, 4, NULL, CAST(N'2019-12-22T23:34:46.067' AS DateTime), CAST(N'2019-12-22T23:34:46.067' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (2008, 1, 5, 4, NULL, CAST(N'2019-12-26T19:26:26.717' AS DateTime), CAST(N'2019-12-26T19:26:26.717' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (2010, 2, 7, 4, NULL, CAST(N'2019-12-26T19:39:14.097' AS DateTime), CAST(N'2019-12-26T19:39:14.097' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (2011, 9, 7, 4, NULL, CAST(N'2019-12-26T19:39:00.527' AS DateTime), CAST(N'2019-12-26T19:39:00.527' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (2012, 10, 5, 4, NULL, CAST(N'2019-12-26T20:17:48.147' AS DateTime), CAST(N'2019-12-26T20:17:48.147' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (2013, 16, 7, 4, NULL, CAST(N'2019-12-26T20:12:57.327' AS DateTime), CAST(N'2019-12-26T20:12:57.327' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3013, 3015, 2, 3, NULL, CAST(N'2019-12-27T18:06:37.480' AS DateTime), CAST(N'2019-12-27T18:06:37.480' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3014, 16, 7, 4, NULL, CAST(N'2019-12-27T18:13:15.127' AS DateTime), CAST(N'2019-12-27T18:13:15.127' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3015, 4016, 3, 4, NULL, CAST(N'2019-12-27T18:13:15.280' AS DateTime), CAST(N'2019-12-27T18:13:15.280' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3016, 10, 5, 4, NULL, CAST(N'2019-12-27T18:24:28.170' AS DateTime), CAST(N'2019-12-27T18:24:28.170' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3019, 1, 5, 4, NULL, CAST(N'2019-12-27T19:17:00.510' AS DateTime), CAST(N'2019-12-27T19:17:00.510' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3022, 11, 2, 3, NULL, CAST(N'2019-12-27T19:52:22.600' AS DateTime), CAST(N'2019-12-27T19:52:22.600' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3023, 13, 6, 4, 37, CAST(N'2019-12-27T20:46:01.513' AS DateTime), CAST(N'2019-12-27T20:46:01.513' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (4013, 1, 5, 4, NULL, CAST(N'2020-01-11T04:34:05.453' AS DateTime), CAST(N'2020-01-11T04:34:05.453' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (4014, 10, 5, 4, NULL, CAST(N'2020-01-11T04:33:20.200' AS DateTime), CAST(N'2020-01-11T04:33:20.200' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (4015, 4023, 5, 4, NULL, CAST(N'2020-01-11T04:36:44.037' AS DateTime), CAST(N'2020-01-11T04:36:44.037' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5013, 3, 2, 3, 5, CAST(N'2020-01-22T12:38:26.107' AS DateTime), CAST(N'2020-01-22T12:38:26.107' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5014, 1, 5, 1, NULL, CAST(N'2020-01-23T11:06:53.513' AS DateTime), CAST(N'2020-01-23T11:06:53.513' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5015, 5016, 3, 1, NULL, CAST(N'2020-01-23T12:48:07.447' AS DateTime), CAST(N'2020-01-23T12:48:07.447' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5016, 3015, 2, 3, NULL, CAST(N'2020-01-23T16:34:22.950' AS DateTime), CAST(N'2020-01-23T16:34:22.950' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5017, 4018, 2, 3, NULL, CAST(N'2020-01-23T16:51:34.803' AS DateTime), CAST(N'2020-01-23T16:51:34.803' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5018, 9, 7, 1, NULL, CAST(N'2020-01-23T17:07:51.277' AS DateTime), CAST(N'2020-01-23T17:07:51.277' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5020, 1, 5, 4, NULL, CAST(N'2020-01-26T04:37:10.177' AS DateTime), CAST(N'2020-01-26T04:37:10.177' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5021, 10, 5, 4, 33, CAST(N'2020-01-26T22:36:51.040' AS DateTime), CAST(N'2020-01-26T22:36:51.040' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5022, 3015, 2, 3, NULL, CAST(N'2020-01-26T05:05:04.737' AS DateTime), CAST(N'2020-01-26T05:05:04.737' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5023, 4025, 2, 3, NULL, CAST(N'2020-01-26T05:05:12.983' AS DateTime), CAST(N'2020-01-26T05:05:12.983' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5024, 4023, 5, 4, 5033, CAST(N'2020-01-26T22:53:00.530' AS DateTime), CAST(N'2020-01-26T22:53:00.530' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5025, 5018, 2, 3, 6025, CAST(N'2020-01-26T21:27:07.057' AS DateTime), CAST(N'2020-01-26T21:27:07.057' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5026, 5017, 5, 4, NULL, CAST(N'2020-01-26T21:31:24.443' AS DateTime), CAST(N'2020-01-26T21:31:24.443' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5027, 5019, 3, 4, NULL, CAST(N'2020-01-26T21:31:24.537' AS DateTime), CAST(N'2020-01-26T21:31:24.537' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5028, 4022, 2, 3, NULL, CAST(N'2020-01-26T21:36:14.843' AS DateTime), CAST(N'2020-01-26T21:36:14.843' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5029, 5021, 5, 4, NULL, CAST(N'2020-01-26T21:54:29.217' AS DateTime), CAST(N'2020-01-26T21:54:29.217' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5030, 5022, 3, 4, 6030, CAST(N'2020-01-26T22:02:16.537' AS DateTime), CAST(N'2020-01-26T22:02:16.537' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5031, 4018, 2, 3, NULL, CAST(N'2020-01-26T22:01:01.457' AS DateTime), CAST(N'2020-01-26T22:01:01.457' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5033, 9, 7, 4, NULL, CAST(N'2020-01-26T23:07:59.047' AS DateTime), CAST(N'2020-01-26T23:07:59.047' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5044, 4, 6, 1, NULL, CAST(N'2020-01-30T17:31:11.387' AS DateTime), CAST(N'2020-01-30T17:31:11.387' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5045, 3, 2, 3, NULL, CAST(N'2020-02-01T18:16:00.113' AS DateTime), CAST(N'2020-02-01T18:16:00.113' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5047, 9, 7, 1, NULL, CAST(N'2020-02-01T18:52:41.517' AS DateTime), CAST(N'2020-02-01T18:52:41.517' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5048, 5027, 3, 1, NULL, CAST(N'2020-02-01T18:52:42.293' AS DateTime), CAST(N'2020-02-01T18:52:42.293' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5049, 1014, 2, 3, NULL, CAST(N'2020-02-03T05:45:12.997' AS DateTime), CAST(N'2020-02-03T05:45:12.997' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5050, 4022, 2, 3, NULL, CAST(N'2020-02-03T05:45:17.197' AS DateTime), CAST(N'2020-02-03T05:45:17.197' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5051, 5018, 2, 3, NULL, CAST(N'2020-02-03T05:49:50.513' AS DateTime), CAST(N'2020-02-03T05:49:50.513' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5052, 3015, 2, 3, NULL, CAST(N'2020-02-03T05:49:54.017' AS DateTime), CAST(N'2020-02-03T05:49:54.017' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5053, 5017, 5, 4, NULL, CAST(N'2020-02-03T05:55:07.313' AS DateTime), CAST(N'2020-02-03T05:55:07.313' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5054, 5028, 3, 4, NULL, CAST(N'2020-02-03T05:55:07.457' AS DateTime), CAST(N'2020-02-03T05:55:07.457' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5055, 10, 5, 4, NULL, CAST(N'2020-02-04T16:58:48.487' AS DateTime), CAST(N'2020-02-04T16:58:48.487' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5056, 3, 2, 3, 5, CAST(N'2020-02-04T17:03:19.017' AS DateTime), CAST(N'2020-02-04T17:03:19.017' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5057, 1, 5, 4, NULL, CAST(N'2020-02-06T03:58:14.640' AS DateTime), CAST(N'2020-02-06T03:58:14.640' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5058, 5018, 2, 3, 6025, CAST(N'2020-02-06T04:17:25.957' AS DateTime), CAST(N'2020-02-06T04:17:25.957' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5059, 5017, 5, 4, NULL, CAST(N'2020-02-06T04:08:16.587' AS DateTime), CAST(N'2020-02-06T04:08:16.587' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5060, 5020, 2, 3, NULL, CAST(N'2020-02-06T04:13:44.003' AS DateTime), CAST(N'2020-02-06T04:13:44.003' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5061, 5021, 5, 4, NULL, CAST(N'2020-02-06T04:14:07.597' AS DateTime), CAST(N'2020-02-06T04:14:07.597' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5080, 4, 6, 4, NULL, CAST(N'2020-02-07T12:55:44.673' AS DateTime), CAST(N'2020-02-07T12:55:44.673' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5081, 5, 6, 4, NULL, CAST(N'2020-02-07T12:56:05.297' AS DateTime), CAST(N'2020-02-07T12:56:05.297' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5082, 16, 7, 4, NULL, CAST(N'2020-02-07T12:57:36.037' AS DateTime), CAST(N'2020-02-07T12:57:36.037' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5083, 4023, 6, 4, NULL, CAST(N'2020-02-07T13:00:08.197' AS DateTime), CAST(N'2020-02-07T13:00:08.197' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6057, 5017, 5, 4, 6024, CAST(N'2020-02-08T02:15:20.337' AS DateTime), CAST(N'2020-02-08T02:15:20.337' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6059, 5017, 5, 4, NULL, CAST(N'2020-02-08T02:16:12.100' AS DateTime), CAST(N'2020-02-08T02:16:12.100' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6060, 5021, 5, 4, 6029, CAST(N'2020-02-08T02:28:30.320' AS DateTime), CAST(N'2020-02-08T02:28:30.320' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6061, 5020, 2, 3, 6028, CAST(N'2020-02-08T21:21:33.690' AS DateTime), CAST(N'2020-02-08T21:21:33.690' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6063, 11, 2, 3, NULL, CAST(N'2020-02-08T15:35:58.903' AS DateTime), CAST(N'2020-02-08T15:35:58.903' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6064, 3015, 2, 3, NULL, CAST(N'2020-02-08T15:36:08.687' AS DateTime), CAST(N'2020-02-08T15:36:08.687' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6065, 16, 7, 4, 1025, CAST(N'2020-02-08T15:45:06.247' AS DateTime), CAST(N'2020-02-08T15:45:06.247' AS DateTime), N'Picked', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6066, 9, 7, 4, NULL, CAST(N'2020-02-08T15:45:14.407' AS DateTime), CAST(N'2020-02-08T15:45:14.407' AS DateTime), N'After-School-Ex', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6067, 6030, 3, 4, NULL, CAST(N'2020-02-08T15:45:14.563' AS DateTime), CAST(N'2020-02-08T15:45:14.563' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6068, 4018, 2, 3, 5025, CAST(N'2020-02-08T15:56:03.120' AS DateTime), CAST(N'2020-02-08T15:56:03.120' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6080, 5018, 2, 3, NULL, CAST(N'2020-02-08T21:12:45.373' AS DateTime), CAST(N'2020-02-08T21:12:45.373' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6081, 1014, 2, 3, 2018, CAST(N'2020-02-08T21:25:55.033' AS DateTime), CAST(N'2020-02-08T21:25:55.033' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6082, 2014, 2, 3, NULL, CAST(N'2020-02-08T21:22:56.487' AS DateTime), CAST(N'2020-02-08T21:22:56.487' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6083, 6033, 2, 3, 7059, CAST(N'2020-02-08T21:45:02.080' AS DateTime), CAST(N'2020-02-08T21:45:02.080' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6099, 5017, 5, 4, NULL, CAST(N'2020-02-10T01:21:01.733' AS DateTime), CAST(N'2020-02-10T01:21:01.733' AS DateTime), N'After-School-Ex', NULL, 1, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6100, 10, 5, 4, NULL, CAST(N'2020-02-10T04:21:14.740' AS DateTime), CAST(N'2020-02-10T04:21:14.740' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6109, 3, 2, 3, NULL, CAST(N'2020-02-13T20:32:08.500' AS DateTime), CAST(N'2020-02-13T20:32:08.500' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6110, 1014, 2, 3, NULL, CAST(N'2020-02-13T20:32:11.587' AS DateTime), CAST(N'2020-02-13T20:32:11.587' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6111, 2, 7, 1, NULL, CAST(N'2020-02-13T20:36:16.200' AS DateTime), CAST(N'2020-02-13T20:36:16.200' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6112, 4, 6, 1, NULL, CAST(N'2020-02-13T21:06:22.923' AS DateTime), CAST(N'2020-02-13T21:06:22.923' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6113, 6037, 3, 1, NULL, CAST(N'2020-02-13T21:08:05.943' AS DateTime), CAST(N'2020-02-13T21:08:05.943' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6114, 10, 5, 1, NULL, CAST(N'2020-02-13T21:16:32.263' AS DateTime), CAST(N'2020-02-13T21:16:32.263' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6115, 2014, 2, 3, NULL, CAST(N'2020-02-13T21:15:43.890' AS DateTime), CAST(N'2020-02-13T21:15:43.890' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6116, 6038, 3, 1, NULL, CAST(N'2020-02-13T21:21:12.913' AS DateTime), CAST(N'2020-02-13T21:21:12.913' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (7109, 5021, 5, 4, NULL, CAST(N'2020-02-15T01:27:00.827' AS DateTime), CAST(N'2020-02-15T01:27:00.827' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (7110, 5021, 5, 4, NULL, CAST(N'2020-02-16T05:07:11.950' AS DateTime), CAST(N'2020-02-16T05:07:11.950' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (9, 10, 5, 4, 33, CAST(N'2019-12-20T17:08:17.520' AS DateTime), CAST(N'2019-12-20T17:08:17.520' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (10, 2, 7, 4, NULL, CAST(N'2019-12-20T17:01:14.927' AS DateTime), CAST(N'2019-12-20T17:01:14.927' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (11, 9, 7, 4, 33, CAST(N'2019-12-20T20:59:24.907' AS DateTime), CAST(N'2019-12-20T20:59:24.907' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (12, 4, 6, 4, NULL, CAST(N'2019-12-20T17:01:21.210' AS DateTime), CAST(N'2019-12-20T17:01:21.210' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (13, 5, 6, 4, NULL, CAST(N'2019-12-20T17:01:23.277' AS DateTime), CAST(N'2019-12-20T17:01:23.277' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5039, 5017, 5, 4, NULL, CAST(N'2020-01-30T17:14:23.423' AS DateTime), CAST(N'2020-01-30T17:14:23.423' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5040, 5024, 3, 4, 6034, CAST(N'2020-01-30T17:17:16.637' AS DateTime), CAST(N'2020-01-30T17:17:16.637' AS DateTime), N'Picked', NULL, NULL, 1)
GO
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5041, 10, 5, 4, NULL, CAST(N'2020-01-30T17:19:10.077' AS DateTime), CAST(N'2020-01-30T17:19:10.077' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5042, 5025, 3, 4, NULL, CAST(N'2020-01-30T17:19:10.637' AS DateTime), CAST(N'2020-01-30T17:19:10.637' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6069, 13, 6, 4, 37, CAST(N'2020-02-08T15:59:29.490' AS DateTime), CAST(N'2020-02-08T15:59:29.490' AS DateTime), N'Picked', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6075, 1, 5, 4, NULL, CAST(N'2020-02-08T18:54:49.687' AS DateTime), CAST(N'2020-02-08T18:54:49.687' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6076, 10, 5, 4, 20, CAST(N'2020-02-08T20:32:21.047' AS DateTime), CAST(N'2020-02-08T20:32:21.047' AS DateTime), N'Picked', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6077, 2, 7, 4, 3, CAST(N'2020-02-08T19:07:14.877' AS DateTime), CAST(N'2020-02-08T19:07:14.877' AS DateTime), N'Picked(Reportable)', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6078, 4, 6, 4, 24, CAST(N'2020-02-08T19:13:41.077' AS DateTime), CAST(N'2020-02-08T19:13:41.077' AS DateTime), N'Picked', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6079, 5, 6, 4, 10, CAST(N'2020-02-08T19:16:00.130' AS DateTime), CAST(N'2020-02-08T19:16:00.130' AS DateTime), N'Picked(Reportable)', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6084, 5, 6, 4, 24, CAST(N'2020-02-09T05:34:49.250' AS DateTime), CAST(N'2020-02-09T05:34:49.250' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6085, 5017, 5, 4, 6024, CAST(N'2020-02-09T06:07:58.907' AS DateTime), CAST(N'2020-02-09T06:07:58.907' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6086, 1, 5, 4, 1, CAST(N'2020-02-09T05:56:43.177' AS DateTime), CAST(N'2020-02-09T05:56:43.177' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6087, 10, 5, 4, 20, CAST(N'2020-02-09T06:08:11.773' AS DateTime), CAST(N'2020-02-09T06:08:11.773' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6088, 2014, 2, 3, NULL, CAST(N'2020-02-09T06:06:05.230' AS DateTime), CAST(N'2020-02-09T06:06:05.230' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6089, 5021, 5, 4, NULL, CAST(N'2020-02-09T06:09:48.087' AS DateTime), CAST(N'2020-02-09T06:09:48.087' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6090, 1014, 2, 3, NULL, CAST(N'2020-02-09T22:07:44.963' AS DateTime), CAST(N'2020-02-09T22:07:44.963' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6091, 3015, 2, 3, NULL, CAST(N'2020-02-09T22:07:50.310' AS DateTime), CAST(N'2020-02-09T22:07:50.310' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6092, 16, 7, 4, NULL, CAST(N'2020-02-09T22:35:53.387' AS DateTime), CAST(N'2020-02-09T22:35:53.387' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6093, 5020, 2, 3, NULL, CAST(N'2020-02-09T22:10:37.050' AS DateTime), CAST(N'2020-02-09T22:10:37.050' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6094, 11, 2, 3, NULL, CAST(N'2020-02-09T22:13:30.147' AS DateTime), CAST(N'2020-02-09T22:13:30.147' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6095, 9, 7, 4, NULL, CAST(N'2020-02-09T22:17:09.660' AS DateTime), CAST(N'2020-02-09T22:17:09.660' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6096, 4023, 6, 4, NULL, CAST(N'2020-02-09T22:31:44.293' AS DateTime), CAST(N'2020-02-09T22:31:44.293' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6097, 4025, 2, 3, NULL, CAST(N'2020-02-09T22:26:11.500' AS DateTime), CAST(N'2020-02-09T22:26:11.500' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6098, 2, 7, 4, NULL, CAST(N'2020-02-09T23:10:53.647' AS DateTime), CAST(N'2020-02-09T23:10:53.647' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6101, 1, 5, 4, NULL, CAST(N'2020-02-10T02:16:25.083' AS DateTime), CAST(N'2020-02-10T02:16:25.083' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6104, 1, 5, 4, NULL, CAST(N'2020-02-11T09:17:10.987' AS DateTime), CAST(N'2020-02-11T09:17:10.987' AS DateTime), N'After-School-Ex', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6106, 5021, 5, 4, 6029, CAST(N'2020-02-11T23:40:56.700' AS DateTime), CAST(N'2020-02-11T23:40:56.700' AS DateTime), N'Picked(Reportable)', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6107, 10, 5, 4, 20, CAST(N'2020-02-11T23:42:55.737' AS DateTime), CAST(N'2020-02-11T23:42:55.737' AS DateTime), N'Picked(Reportable)', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6108, 1, 5, 4, NULL, CAST(N'2020-02-13T20:34:22.607' AS DateTime), CAST(N'2020-02-13T20:34:22.607' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (2014, 3016, 3, 4, NULL, CAST(N'2019-12-26T20:19:52.983' AS DateTime), CAST(N'2019-12-26T20:19:52.983' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3027, 1, 5, 4, NULL, CAST(N'2019-12-30T17:05:16.793' AS DateTime), CAST(N'2019-12-30T17:05:16.793' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3028, 10, 5, 4, NULL, CAST(N'2019-12-30T17:05:06.827' AS DateTime), CAST(N'2019-12-30T17:05:06.827' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3029, 4021, 3, 4, NULL, CAST(N'2019-12-30T17:05:17.487' AS DateTime), CAST(N'2019-12-30T17:05:17.487' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3030, 3, 2, 3, NULL, CAST(N'2019-12-30T17:50:21.907' AS DateTime), CAST(N'2019-12-30T17:50:21.907' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5046, 11, 2, 3, NULL, CAST(N'2020-02-01T18:26:51.663' AS DateTime), CAST(N'2020-02-01T18:26:51.663' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5062, 2, 7, 4, 3, CAST(N'2020-02-06T04:36:28.690' AS DateTime), CAST(N'2020-02-06T04:36:28.690' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5064, 9, 7, 4, NULL, CAST(N'2020-02-06T04:50:27.333' AS DateTime), CAST(N'2020-02-06T04:50:27.333' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5065, 16, 7, 4, NULL, CAST(N'2020-02-06T04:50:31.983' AS DateTime), CAST(N'2020-02-06T04:50:31.983' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5066, 13, 6, 4, NULL, CAST(N'2020-02-06T06:05:10.390' AS DateTime), CAST(N'2020-02-06T06:05:10.390' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5067, 4018, 2, 3, NULL, CAST(N'2020-02-06T06:06:36.803' AS DateTime), CAST(N'2020-02-06T06:06:36.803' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5068, 5017, 5, 4, NULL, CAST(N'2020-02-07T09:44:57.950' AS DateTime), CAST(N'2020-02-07T09:44:57.950' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5073, 5032, 14, 2007, NULL, CAST(N'2020-02-07T10:00:41.393' AS DateTime), CAST(N'2020-02-07T10:00:41.393' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5070, 5018, 2, 3, NULL, CAST(N'2020-02-07T09:40:39.973' AS DateTime), CAST(N'2020-02-07T09:40:39.973' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5071, 5021, 5, 4, NULL, CAST(N'2020-02-07T09:44:11.783' AS DateTime), CAST(N'2020-02-07T09:44:11.783' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5072, 5034, 3, 4, NULL, CAST(N'2020-02-07T09:44:11.860' AS DateTime), CAST(N'2020-02-07T09:44:11.860' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5074, 10, 5, 4, NULL, CAST(N'2020-02-07T10:08:02.423' AS DateTime), CAST(N'2020-02-07T10:08:02.423' AS DateTime), N'After-School-Ex', NULL, 1, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5075, 5035, 3, 4, NULL, CAST(N'2020-02-07T10:08:02.517' AS DateTime), CAST(N'2020-02-07T10:08:02.517' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5076, 9, 7, 4, 18, CAST(N'2020-02-07T10:09:54.240' AS DateTime), CAST(N'2020-02-07T10:09:54.240' AS DateTime), N'Picked', NULL, 1, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5077, 13, 6, 4, NULL, CAST(N'2020-02-07T10:22:12.853' AS DateTime), CAST(N'2020-02-07T10:22:12.853' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5079, 5037, 3, 4, NULL, CAST(N'2020-02-07T10:22:12.930' AS DateTime), CAST(N'2020-02-07T10:22:12.930' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6062, 5021, 5, 4, NULL, CAST(N'2020-02-08T02:39:16.340' AS DateTime), CAST(N'2020-02-08T02:39:16.340' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3017, 4017, 3, 4, NULL, CAST(N'2019-12-27T18:24:28.233' AS DateTime), CAST(N'2019-12-27T18:24:28.233' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3018, 2014, 2, 3, NULL, CAST(N'2019-12-27T18:25:17.680' AS DateTime), CAST(N'2019-12-27T18:25:17.680' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3020, 9, 7, 4, NULL, CAST(N'2019-12-27T19:41:30.817' AS DateTime), CAST(N'2019-12-27T19:41:30.817' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3021, 4019, 3, 4, NULL, CAST(N'2019-12-27T19:41:30.987' AS DateTime), CAST(N'2019-12-27T19:41:30.987' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3025, 10, 5, 4, NULL, CAST(N'2019-12-28T04:03:36.050' AS DateTime), CAST(N'2019-12-28T04:03:36.050' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (3026, 16, 7, 4, 1025, CAST(N'2019-12-28T04:08:52.710' AS DateTime), CAST(N'2019-12-28T04:08:52.710' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5019, 11, 2, 3, NULL, CAST(N'2020-01-25T12:44:31.283' AS DateTime), CAST(N'2020-01-25T12:44:31.287' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5032, 4023, 5, 4, NULL, CAST(N'2020-01-26T22:53:32.477' AS DateTime), CAST(N'2020-01-26T22:53:32.477' AS DateTime), N'Mark as Absent', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5034, 5021, 5, 4, NULL, CAST(N'2020-01-30T14:41:06.443' AS DateTime), CAST(N'2020-01-30T14:41:06.443' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5037, 4023, 5, 4, 5033, CAST(N'2020-01-30T17:30:15.127' AS DateTime), CAST(N'2020-01-30T17:30:15.127' AS DateTime), N'Picked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5036, 1, 5, 4, 1, CAST(N'2020-01-30T17:28:34.443' AS DateTime), CAST(N'2020-01-30T17:28:34.443' AS DateTime), N'Picked', NULL, 1, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (5038, 3015, 2, 3, NULL, CAST(N'2020-01-30T14:53:12.833' AS DateTime), CAST(N'2020-01-30T14:53:12.833' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6071, 4023, 6, 4, NULL, CAST(N'2020-02-08T16:10:09.737' AS DateTime), CAST(N'2020-02-08T16:10:09.737' AS DateTime), N'After-School-Ex', 1, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6072, 6032, 3, 4, NULL, CAST(N'2020-02-08T16:10:10.377' AS DateTime), CAST(N'2020-02-08T16:10:10.377' AS DateTime), N'After-School-Ex', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6073, 4025, 2, 3, NULL, CAST(N'2020-02-08T16:11:04.370' AS DateTime), CAST(N'2020-02-08T16:11:04.370' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6074, 3, 2, 3, 6038, CAST(N'2020-02-08T16:17:11.493' AS DateTime), CAST(N'2020-02-08T16:17:11.493' AS DateTime), N'Picked', NULL, NULL, 1)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6102, 5021, 5, 4, NULL, CAST(N'2020-02-10T04:11:18.097' AS DateTime), CAST(N'2020-02-10T04:11:18.097' AS DateTime), N'Send to Office', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6103, 16, 7, 4, NULL, CAST(N'2020-02-10T03:19:44.737' AS DateTime), CAST(N'2020-02-10T03:19:44.737' AS DateTime), N'UnPicked', NULL, NULL, NULL)
INSERT [dbo].[ISPickup] ([ID], [StudentID], [ClassID], [TeacherID], [PickerID], [PickTime], [PickDate], [PickStatus], [CompletePickup], [OfficeFlag], [AfterSchoolFlag]) VALUES (6105, 5017, 5, 4, 24, CAST(N'2020-02-11T09:24:04.207' AS DateTime), CAST(N'2020-02-11T09:24:04.207' AS DateTime), N'Picked', 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISPickup] OFF
SET IDENTITY_INSERT [dbo].[ISPickUpMessage] ON 

INSERT [dbo].[ISPickUpMessage] ([ID], [SchoolID], [Message], [ReceiverID], [ClassID], [SendID], [SenderName], [Viewed], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, 1, N' Running late', 4, 6, 5, N'Ayoola Ojo', 1, 1, 1, 5, CAST(N'2019-12-19T19:06:57.197' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickUpMessage] ([ID], [SchoolID], [Message], [ReceiverID], [ClassID], [SendID], [SenderName], [Viewed], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, 1, N' Stuck in traffic', 4, 6, 4, N'Ayo Ojo', 1, 1, 1, 5, CAST(N'2019-12-19T23:27:06.043' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISPickUpMessage] OFF
SET IDENTITY_INSERT [dbo].[ISPickUpStatus] ON 

INSERT [dbo].[ISPickUpStatus] ([ID], [Status], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Picked', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickUpStatus] ([ID], [Status], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'UnPicked', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickUpStatus] ([ID], [Status], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'Mark as Absent', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickUpStatus] ([ID], [Status], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, N'Send to Office', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickUpStatus] ([ID], [Status], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (5, N'Send to After School', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickUpStatus] ([ID], [Status], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (6, N'Picked(Late)', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickUpStatus] ([ID], [Status], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (7, N'Picked(Chargeable)', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickUpStatus] ([ID], [Status], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (8, N'Picked(Reportable)', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISPickUpStatus] ([ID], [Status], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (9, N'School Closed', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISPickUpStatus] OFF
SET IDENTITY_INSERT [dbo].[ISReport] ON 

INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Re-assigned Teacher Report', N'~/Reports/ReAssignedTeacherReport.aspx', 1, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'Created Teacher Report', N'~/Reports/CreatedTeacherReport.aspx', 1, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'End-Dated Teacher Report', N'~/Reports/EndDatedTeacherReport.aspx', 1, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, N'Teacher PickUp History', N'~/Reports/TeacherPickupHistoryReport.aspx', 1, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (5, N'Created Class Report', N'~/Reports/CreatedClassReport.aspx', 2, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (6, N'Re-assigned Student Report', N'~/Reports/ReAssignedStudentReport.aspx', 2, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (7, N'Chargeable Student Report', N'~/Reports/ChargeableStudentReport.aspx', 2, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (8, N'Reportable Student Report', N'~/Reports/ReportableStudentReport.aspx', 2, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (9, N'Late Student Report', N'~/Reports/LateStudentReport.aspx', 2, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (10, N'Pickup Close Time Report', N'~/Reports/PickUpCloseTimeReport.aspx', 2, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (11, N'UnPick Student Report  ', N'~/Reports/UnPickStudentReport.aspx', 3, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (12, N'After School Student Pickup Report', N'~/Reports/AfterSchoolStudentPickUpReport.aspx', 3, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (13, N'Students With No Image Report', N'~/Reports/StudentWithNoImageReport.aspx', 3, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (14, N'Complete Pickup Time History', N'~/Reports/CompletePickUpTimeHistoryReport.aspx', 3, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (15, N'Absent Student Report', N'~/Reports/AbsentStudentReport.aspx', 4, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (16, N'Complete Attendance Time History', N'~/Reports/CompleteAttendanceTimeHistoryReport.aspx', 4, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (17, N'Teacher Attendance Mark History', N'~/Reports/TeacherAttendanceMarkHistoryReport.aspx', 4, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (18, N'Created Admin- Role Report', N'~/Reports/CreatedAdminRoleReport.aspx', 5, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (19, N'Data Upload History Report', N'~/Reports/DataUploadHistoryReport.aspx', 5, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISReport] ([ID], [Name], [URL], [ReportTypeID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (20, N'Export Template Report', N'~/Reports/ExportTemplateReport.aspx', 5, 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISReport] OFF
SET IDENTITY_INSERT [dbo].[ISRole] ON 

INSERT [dbo].[ISRole] ([ID], [Name], [SchoolID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Admin Officer', 1, 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISRole] ([ID], [Name], [SchoolID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'Business Development Officer', 1, 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISRole] ([ID], [Name], [SchoolID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'Support Officer', 1, 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISRole] ([ID], [Name], [SchoolID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, N'Apps Admin', 1, 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISRole] OFF
SET IDENTITY_INSERT [dbo].[ISSchool] ON 

INSERT [dbo].[ISSchool] ([ID], [CustomerNumber], [Name], [Number], [TypeID], [Address1], [Address2], [Town], [CountryID], [Logo], [AdminFirstName], [AdminLastName], [AdminEmail], [Password], [PhoneNumber], [Website], [SupervisorFirstname], [SupervisorLastname], [SupervisorEmail], [OpningTime], [ClosingTime], [LateMinAfterClosing], [ChargeMinutesAfterClosing], [ReportableMinutesAfterClosing], [SetupTrainingStatus], [SetupTrainingDate], [ActivationDate], [SchoolEndDate], [isAttendanceModule], [isNotificationPickup], [NotificationAttendance], [AttendanceModule], [PostCode], [BillingAddress], [BillingAddress2], [BillingPostCode], [BillingCountryID], [BillingTown], [Classfile], [Teacherfile], [Studentfile], [Reportable], [PaymentSystems], [CustSigned], [AccountStatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [CreatedByName], [LastUpdatedBy], [ActivatedBy]) VALUES (1, N'CUST00001', N'Gatistavam School for Management and Computer Education', N'SCH001', 2, N'London', N'UK', N'London', 1, N'Upload/Photo1583126485031.jpg', N'Hardik', N'Patadiya', N'hardik@gatistavamsoftech.net', N'FjeFZFDSu3ozOOciL3Lz0g==', N'9876543210', N'https://School.gsdemosystem.com', N'Radhika', N'Parmar', N'abimbolah13@yahoo.co.uk', CAST(N'2018-01-01T12:01:00.000' AS DateTime), CAST(N'2018-01-01T19:01:00.000' AS DateTime), N'2', N'2', N'2', 1, CAST(N'2019-12-16T00:00:00.000' AS DateTime), CAST(N'2019-12-16T00:00:00.000' AS DateTime), CAST(N'2050-01-01T00:00:00.000' AS DateTime), 0, 1, 1, NULL, N'TW0016', N'London', N'UK', N'TW0016', 1, N'London', NULL, NULL, NULL, NULL, 1, 1, 2, 1, 1, 1, CAST(N'2019-12-16T12:09:08.710' AS DateTime), 1, CAST(N'2019-12-18T18:13:03.023' AS DateTime), NULL, NULL, N'Admin', N'Admin', N'Admin')
INSERT [dbo].[ISSchool] ([ID], [CustomerNumber], [Name], [Number], [TypeID], [Address1], [Address2], [Town], [CountryID], [Logo], [AdminFirstName], [AdminLastName], [AdminEmail], [Password], [PhoneNumber], [Website], [SupervisorFirstname], [SupervisorLastname], [SupervisorEmail], [OpningTime], [ClosingTime], [LateMinAfterClosing], [ChargeMinutesAfterClosing], [ReportableMinutesAfterClosing], [SetupTrainingStatus], [SetupTrainingDate], [ActivationDate], [SchoolEndDate], [isAttendanceModule], [isNotificationPickup], [NotificationAttendance], [AttendanceModule], [PostCode], [BillingAddress], [BillingAddress2], [BillingPostCode], [BillingCountryID], [BillingTown], [Classfile], [Teacherfile], [Studentfile], [Reportable], [PaymentSystems], [CustSigned], [AccountStatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [CreatedByName], [LastUpdatedBy], [ActivatedBy]) VALUES (2, N'CUST00002', N'After Gatistavam School', N'SCH002', 1, N'London', N'UK', N'London', 1, N'Upload/DefaultOrg.png', N'Vihan', N'Parmar', N'vihan@ymail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', N'9876543210', N'https://AfterSchool.gsdemosystem.com', N'Rahul', N'Thakkar', N'rahul@yahoo.com', CAST(N'2018-01-01T17:01:00.000' AS DateTime), CAST(N'2018-01-01T19:01:00.000' AS DateTime), N'5', N'10', N'15', 1, CAST(N'2019-12-16T00:00:00.000' AS DateTime), CAST(N'2020-02-09T06:08:22.327' AS DateTime), CAST(N'2050-01-01T00:00:00.000' AS DateTime), 1, 1, 1, NULL, N'TW0016', N'London', N'UK', N'TW0016', 1, N'London', NULL, NULL, NULL, NULL, 1, 1, 2, 1, 1, 1, CAST(N'2019-12-16T12:14:17.763' AS DateTime), 1, CAST(N'2020-02-09T05:51:34.220' AS DateTime), NULL, NULL, N'Admin', N'Admin', N'Admin')
INSERT [dbo].[ISSchool] ([ID], [CustomerNumber], [Name], [Number], [TypeID], [Address1], [Address2], [Town], [CountryID], [Logo], [AdminFirstName], [AdminLastName], [AdminEmail], [Password], [PhoneNumber], [Website], [SupervisorFirstname], [SupervisorLastname], [SupervisorEmail], [OpningTime], [ClosingTime], [LateMinAfterClosing], [ChargeMinutesAfterClosing], [ReportableMinutesAfterClosing], [SetupTrainingStatus], [SetupTrainingDate], [ActivationDate], [SchoolEndDate], [isAttendanceModule], [isNotificationPickup], [NotificationAttendance], [AttendanceModule], [PostCode], [BillingAddress], [BillingAddress2], [BillingPostCode], [BillingCountryID], [BillingTown], [Classfile], [Teacherfile], [Studentfile], [Reportable], [PaymentSystems], [CustSigned], [AccountStatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [CreatedByName], [LastUpdatedBy], [ActivatedBy]) VALUES (3, N'CUST00003', N'Baptist School', N'6464646', 1, N'1111111', N'ryfyfyfy  dhdfhfh', N'India', 1, NULL, N'Abimbola', N'Ola', N'abimbolah13@yahoo.co.uk', N'FjeFZFDSu3ozOOciL3Lz0g==', N'88488848484', N'www.blueschool.ggmail.com', N'Deb', N'Ola', N'olatunyo@yahoo.co.uk', CAST(N'2018-01-01T08:15:00.000' AS DateTime), CAST(N'2018-01-01T14:10:00.000' AS DateTime), N'2', N'5', N'10', 1, CAST(N'2020-02-07T00:00:00.000' AS DateTime), CAST(N'2020-02-07T09:19:50.840' AS DateTime), CAST(N'2050-01-01T00:00:00.000' AS DateTime), 1, 1, 1, NULL, N'27232 2y2y2', N'1111111', N'ryfyfyfy  dhdfhfh', N'27232 2y2y2', 1, N'India', NULL, NULL, NULL, NULL, 1, 1, 2, 1, 1, 1, CAST(N'2020-02-07T09:19:50.887' AS DateTime), NULL, NULL, NULL, NULL, N'Admin', NULL, N'Admin')
INSERT [dbo].[ISSchool] ([ID], [CustomerNumber], [Name], [Number], [TypeID], [Address1], [Address2], [Town], [CountryID], [Logo], [AdminFirstName], [AdminLastName], [AdminEmail], [Password], [PhoneNumber], [Website], [SupervisorFirstname], [SupervisorLastname], [SupervisorEmail], [OpningTime], [ClosingTime], [LateMinAfterClosing], [ChargeMinutesAfterClosing], [ReportableMinutesAfterClosing], [SetupTrainingStatus], [SetupTrainingDate], [ActivationDate], [SchoolEndDate], [isAttendanceModule], [isNotificationPickup], [NotificationAttendance], [AttendanceModule], [PostCode], [BillingAddress], [BillingAddress2], [BillingPostCode], [BillingCountryID], [BillingTown], [Classfile], [Teacherfile], [Studentfile], [Reportable], [PaymentSystems], [CustSigned], [AccountStatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [CreatedByName], [LastUpdatedBy], [ActivatedBy]) VALUES (4, N'CUST00004', N'Green School', N'7474747', 2, N'7575757', N'r7r7hfhfhfhf fjhfhf', N'hdhdhdfhd', 1, NULL, N'John', N'Smith', N'wumi@ggmail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', N'595999999', N'www.grreereeeen.ggcom', N'Ray', N'Jones', N'jones@gggmail.com', CAST(N'2018-01-01T08:01:00.000' AS DateTime), CAST(N'2018-01-01T14:01:00.000' AS DateTime), N'1', N'2', N'3', 1, CAST(N'2020-02-07T00:00:00.000' AS DateTime), CAST(N'2020-02-07T00:00:00.000' AS DateTime), CAST(N'2050-01-01T00:00:00.000' AS DateTime), 1, 1, 1, NULL, N'dchd djhdh', N'7575757', N'r7r7hfhfhfhf fjhfhf', N'dchd djhdh', 1, N'hdhdhdfhd', NULL, NULL, NULL, NULL, 1, 1, 2, 1, 1, 1, CAST(N'2020-02-07T09:36:14.170' AS DateTime), 1, CAST(N'2020-02-09T23:15:49.763' AS DateTime), NULL, NULL, N'Admin', N'Admin', N'Admin')
SET IDENTITY_INSERT [dbo].[ISSchool] OFF
SET IDENTITY_INSERT [dbo].[ISSchoolInvoice] ON 

INSERT [dbo].[ISSchoolInvoice] ([ID], [SchoolID], [InvoiceNo], [DateFrom], [DateTo], [TransactionTypeID], [TransactionDesc], [TransactionAmount], [TaxRate], [StatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [StatusUpdateBy], [StatusUpdateDate], [CreatedByName]) VALUES (1, 1, N'INV001', CAST(N'2019-12-17T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), 2, N'Annual Maintainance', 1500, CAST(10.00 AS Decimal(18, 2)), 1, 1, 1, 1, CAST(N'2019-12-16T12:16:38.387' AS DateTime), NULL, NULL, NULL, NULL, N'Admin', CAST(N'2019-12-16T12:16:38.387' AS DateTime), N'Admin')
INSERT [dbo].[ISSchoolInvoice] ([ID], [SchoolID], [InvoiceNo], [DateFrom], [DateTo], [TransactionTypeID], [TransactionDesc], [TransactionAmount], [TaxRate], [StatusID], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [StatusUpdateBy], [StatusUpdateDate], [CreatedByName]) VALUES (2, 2, N'INV002', CAST(N'2019-12-17T00:00:00.000' AS DateTime), CAST(N'2019-12-25T00:00:00.000' AS DateTime), 2, N'Annual Maintainance', 1500, CAST(10.00 AS Decimal(18, 2)), 1, 1, 1, 1, CAST(N'2019-12-16T12:17:19.473' AS DateTime), NULL, NULL, NULL, NULL, N'Admin', CAST(N'2019-12-16T12:17:19.473' AS DateTime), N'Admin')
SET IDENTITY_INSERT [dbo].[ISSchoolInvoice] OFF
SET IDENTITY_INSERT [dbo].[ISSchoolType] ON 

INSERT [dbo].[ISSchoolType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'AfterSchool', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISSchoolType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'Standard', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISSchoolType] OFF
SET IDENTITY_INSERT [dbo].[ISStudent] ON 

INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (1, N'STU001', 5, 1, N'Charmi Parmar', N'Upload/user.jpg', NULL, N'Rahul Parmar', N'rahul@gmail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', N'9876545352', N'Father', NULL, N'Rahul Parmar', N'rahul@gmail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', N'9876545352', N'Father', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2019-12-16T12:31:32.210' AS DateTime), 1, CAST(N'2020-01-30T16:56:57.380' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (2, N'STU002', 7, 1, N'Mahek Parmar', N'Upload/user.jpg', NULL, N'Rahul Parmar', N'rahul@gmail.com', N'uMutqpFgY9E=', N'9876543210', N'Father', NULL, N'Jagruti Parmar', N'jaggu@gmail.com', N'uMutqpFgY9E=', N'9876543211', N'Mother', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2019-12-16T12:33:36.790' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (3, N'STU999', 2, 2, N'Aakruti Gohel', N'Upload/user.jpg', NULL, N'Vismay Gohel', N'vismay@gmail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', N'9876543215', N'Father', NULL, N'Rakul Preet', N'rakul@ymail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', N'9876543212', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 2, CAST(N'2019-12-16T12:51:08.130' AS DateTime), 2, CAST(N'2020-01-30T17:24:47.643' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4, N'10000', 6, 1, N'Sola Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'olatunyo@yahoo.co.uk', N'FjeFZFDSu3ozOOciL3Lz0g==', N'02002020200', N'Father', NULL, N'Olude Ojo', N'Debbo4@hotmail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', N'23938383993', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2019-12-18T03:29:28.263' AS DateTime), 1, CAST(N'2019-12-18T18:30:58.570' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5, N'12000', 6, 1, N'Titi Ojo', N'Upload/user.jpg', NULL, N'Ayoola Ojo', N'ayobimbo2@yahoo.com', N'FjeFZFDSu3ozOOciL3Lz0g==', N'9484848489', N'Father', NULL, N'Ayoola Ojo', N'ayobimbo2@yahoo.com', N'upftaE7D1xM=', N'9484848489', N'Father', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2019-12-18T12:25:08.100' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (7, N'12000', 3, 2, N'Titi Ojo', N'Upload/user.jpg', NULL, N'Ayoola Ojo', N'ayobimbo2@yahoo.com', N'5o~l5SD/C04=', N'9484848489', N'Father', NULL, N'Olude Ojo', N'Debbo4@hotmail.com', N'5o~l5SD/C04=', N'9239393939', N'Friends', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2019-12-19T09:28:27.567' AS DateTime), CAST(N'2019-12-19T09:28:27.567' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2019-12-19T09:28:27.567' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (12, N'1444', 3, 2, N'Seyi Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'Binz7UGeXX4=', N'39339393939', N'Father', NULL, N'Rahul Patel', N'rahul@gmail.com', N'Binz7UGeXX4=', N'858585858', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2019-12-20T20:59:25.437' AS DateTime), CAST(N'2019-12-20T20:59:25.437' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2019-12-20T20:59:25.437' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (13, N'15555', 6, 1, N'Fola Ojo', N'Upload/user.jpg', NULL, N'Olude Ojo', N'Debbo4@yahoo.com', N'7gaEhdRwZgc=', N'02929292929', N'Father', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'7gaEhdRwZgc=', N'3939230202', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2019-12-21T14:15:51.937' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (15, N'STU001', 3, 2, N'Charmi Parmar', N'Upload/user.jpg', NULL, N'Rahul Parmar', N'rahul@gmail.com', N'gYi7eCBOoZw=', N'9876543210', N'Father', NULL, N'Jagruti Parmar', N'jaggu@gmail.com', N'gYi7eCBOoZw=', N'9876543211', N'Mother', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2019-12-22T23:34:46.007' AS DateTime), CAST(N'2019-12-22T23:34:46.007' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2019-12-22T23:34:46.007' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (16, N'9229', 7, 1, N'Tolu Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'PsOSOllfgLI=', N'9292392999', N'Father', NULL, N'Rahu Ojo', N'rahul@gmail.com', N'PsOSOllfgLI=', N'929292929', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2019-12-23T02:19:39.057' AS DateTime), 1, CAST(N'2019-12-23T02:56:38.287' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-23T02:19:39.057' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (1014, N'7757575', 2, 2, N'Biti Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'UmPXi2iVi8E=', N'4848484949', N'Father', NULL, N'Rahul Ojo', N'rahul@gmail.com', N'UmPXi2iVi8E=', N'939393939', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 2, CAST(N'2019-12-24T19:27:04.243' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (2014, N'3556', 2, 2, N'Amy Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'htCqPJCK/7M=', N'66666778', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 2, CAST(N'2019-12-26T06:46:48.997' AS DateTime), 2, CAST(N'2019-12-26T19:11:31.697' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (3016, N'33399', 3, 2, N'Amy Ojo', N'Upload/user.jpg', NULL, N'Ayodele Ojo', N'ayobimbo2@yahoo.com', N'1SLw4FCpw68=', N'043939393', N'Relative', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2019-12-26T20:17:48.740' AS DateTime), CAST(N'2019-12-26T20:17:48.740' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2019-12-26T20:17:48.740' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (3015, N'646464', 2, 2, N'Tolu Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'J1ibxRJpzXA=', N'939494949', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 2, CAST(N'2019-12-26T19:05:26.303' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4016, N'9229', 3, 2, N'Tolu Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'BaSRUUmM2bg=', N'9292392999', N'Father', NULL, N'Rahu Ojo', N'rahul@gmail.com', N'quJlpFAqhF4=', N'929292929', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2019-12-27T18:13:15.173' AS DateTime), CAST(N'2019-12-27T18:13:15.173' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2019-12-27T18:13:15.173' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4017, N'33399', 3, 2, N'Amy Ojo', N'Upload/user.jpg', NULL, N'Ayodele Ojo', N'ayobimbo2@yahoo.com', N'ytAQ3wt2hZM=', N'043939393', N'Relative', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2019-12-27T18:24:28.170' AS DateTime), CAST(N'2019-12-27T18:24:28.170' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2019-12-27T18:24:28.170' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4018, N'39393939', 2, 2, N'Fola Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'j64GVthAd6I=', N'58585868689', N'Father', NULL, N'Semi Zyne', N'Semi@ymail.com', NULL, N'9898965225', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 2, CAST(N'2019-12-27T18:36:38.287' AS DateTime), 2, CAST(N'2020-01-22T12:21:18.837' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4019, N'1444', 3, 2, N'Seyi Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'uxaBjg7w0FY=', N'39339393939', N'Father', NULL, N'Rahul Patel', N'rahul@gmail.com', N'sd57lLW9rpo=', N'858585858', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2019-12-27T19:41:30.863' AS DateTime), CAST(N'2019-12-27T19:41:30.863' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2019-12-27T19:41:30.863' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4022, N'55566', 2, 2, N'Bola Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'BaJURJlUatc=', N'8889890999', N'Father', NULL, N'Olatunde Tunyo', N'Olatunyo@yahoo.co.uk', NULL, N'9595995950', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 3, CAST(N'2019-12-30T17:09:35.193' AS DateTime), 2, CAST(N'2020-02-04T16:51:27.877' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4023, N'7777798', 6, 1, N'James Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'9YNAHci9A0E=', N'55778899909', N'Father', NULL, N'Anil', N'rahul@gmail.com', NULL, N'73747547575', N'Mother', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2019-12-30T17:13:02.830' AS DateTime), 1, CAST(N'2020-02-01T18:02:45.237' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-30T17:13:02.830' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4024, N'667778', 2, 2, N'James Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N's4pVafsh/RA=', N'6676667888', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 3, CAST(N'2019-12-30T17:14:06.890' AS DateTime), NULL, NULL, 2, CAST(N'2019-12-30T17:43:51.173' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4025, N'756769696', 2, 2, N'James Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'QsMHJOVYKsw=', N'8585858585', N'Father', NULL, N'Rahul Parmar', N'rahul@gmail.com', NULL, N'9876258252', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 2, CAST(N'2019-12-30T17:44:40.523' AS DateTime), 2, CAST(N'2020-02-01T17:25:35.750' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5016, N'STU001', 3, 2, N'Charmi Parmar', N'Upload/user.jpg', NULL, N'Rahul Parmar', N'rahul@gmail.com', N'M5ILUE3sjBk=', N'9876545352', N'Father', NULL, N'Rahul Parmar', N'rahul@gmail.com', N'M5ILUE3sjBk=', N'9876545352', N'Father', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-23T11:06:53.573' AS DateTime), CAST(N'2020-01-23T11:06:53.573' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-01-23T11:06:53.573' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5017, N'7474747', 5, 1, N'Brad Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'YyQMku3LG80=', N'939303030', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2020-01-26T21:18:29.847' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2020-01-26T21:18:29.847' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5019, N'7474747', 3, 2, N'Brad Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'Ht0qwDjPCBk=', N'939303030', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-26T21:31:24.457' AS DateTime), CAST(N'2020-01-26T21:31:24.457' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-01-26T21:31:24.457' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5020, N'7474747', 2, 2, N'Philip Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'ACu9E52HILs=', N'83484848', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 3, CAST(N'2020-01-26T21:50:42.050' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5021, N'98448858', 5, 1, N'Philip Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'vu~c7lKBEt4=', N'9499494949', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2020-01-26T21:53:26.070' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2020-01-26T21:53:26.070' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5022, N'98448858', 3, 2, N'Philip Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'XQWP6TV38RQ=', N'9499494949', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-26T21:54:29.233' AS DateTime), CAST(N'2020-01-26T21:54:29.233' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-01-26T21:54:29.233' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5023, N'98448858', 3, 2, N'Philip Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'teSKMhZA~z8=', N'9499494949', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-30T14:41:06.473' AS DateTime), CAST(N'2020-01-30T14:41:06.473' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-01-30T14:41:06.473' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5024, N'7474747', 3, 2, N'Brad Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'axMWsU9bykc=', N'939303030', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-30T17:14:23.493' AS DateTime), CAST(N'2020-01-30T17:14:23.493' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-01-30T17:14:23.493' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (9, N'1444', 7, 1, N'Seyi Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'BgyL14ZVZjM=', N'39339393939', N'Father', NULL, N'Rahul Patel', N'rahul@gmail.com', NULL, N'858585858', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2019-12-19T20:30:04.127' AS DateTime), 1, CAST(N'2019-12-23T02:18:24.953' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (10, N'33399', 5, 1, N'Amy Ojo', N'Upload/user.jpg', NULL, N'Ayodele Ojo', N'ayobimbo2@yahoo.com', N'rTcTV1Jzu4k=', N'043939393', N'Relative', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2019-12-19T20:46:24.040' AS DateTime), 1, CAST(N'2019-12-20T06:08:57.603' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (4021, N'STU001', 3, 2, N'Charmi Parmar', N'Upload/user.jpg', NULL, N'Rahul Parmar', N'rahul@gmail.com', N'e~OQH1SvJkc=', N'9876543210', N'Father', NULL, N'Jagruti Parmar', N'jaggu@gmail.com', N'e~OQH1SvJkc=', N'9876543211', N'Mother', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2019-12-30T17:05:16.837' AS DateTime), CAST(N'2019-12-30T17:05:16.837' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2019-12-30T17:05:16.837' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5018, N'43884848', 2, 2, N'Brad Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'NoLZT4W~SOA=', N'92393939393', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 2, CAST(N'2020-01-26T21:21:13.217' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5025, N'33399', 3, 2, N'Amy Ojo', N'Upload/user.jpg', NULL, N'Ayodele Ojo', N'ayobimbo2@yahoo.com', N'Pi3TEreSTdk=', N'043939393', N'Relative', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-30T17:19:10.577' AS DateTime), CAST(N'2020-01-30T17:19:10.577' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-01-30T17:19:10.577' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5027, N'1444', 3, 2, N'Seyi Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'75IH~G6/wlc=', N'39339393939', N'Father', NULL, N'Rahul Patel', N'rahul@gmail.com', N'75IH~G6/wlc=', N'858585858', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-02-01T18:52:42.103' AS DateTime), CAST(N'2020-02-01T18:52:42.103' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-02-01T18:52:42.103' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5028, N'7474747', 3, 2, N'Brad Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'8QInuiSaWTE=', N'939303030', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-02-03T05:55:07.347' AS DateTime), CAST(N'2020-02-03T05:55:07.347' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-02-03T05:55:07.347' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5030, N'88490', 14, 3, N'Brad Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'ojvOw3D8UY4=', N'848484848', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 3, CAST(N'2020-02-07T09:31:35.320' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5031, N'88491', 14, 3, N'Amy Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'04ULNTLOJDQ=', N'848484848', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 3, CAST(N'2020-02-07T09:32:03.287' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5032, N'88492', 14, 3, N'Philip Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'elFJpai2rzw=', N'848484848', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 3, CAST(N'2020-02-07T09:32:36.930' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5035, N'33399', 3, 2, N'Amy Ojo', N'Upload/user.jpg', NULL, N'Ayodele Ojo', N'ayobimbo2@yahoo.com', N'zBGFaBNH5fs=', N'043939393', N'Relative', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-02-07T10:08:02.437' AS DateTime), CAST(N'2020-02-07T10:08:02.437' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-02-07T10:08:02.437' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5034, N'98448858', 3, 2, N'Philip Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'ZiJgMqIrKdY=', N'9499494949', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-02-07T09:44:11.797' AS DateTime), CAST(N'2020-02-07T09:44:11.797' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-02-07T09:44:11.797' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (5037, N'15555', 3, 2, N'Fola Ojo', N'Upload/user.jpg', NULL, N'Olude Ojo', N'Debbo4@yahoo.com', N'5sHdFUgdols=', N'02929292929', N'Father', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'5sHdFUgdols=', N'3939230202', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-02-07T10:22:12.870' AS DateTime), CAST(N'2020-02-07T10:22:12.870' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-02-07T10:22:12.870' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (6030, N'1444', 3, 2, N'Seyi Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'm50fRHp9Yo0=', N'39339393939', N'Father', NULL, N'Rahul Patel', N'rahul@gmail.com', N'64ztUTT0pLw=', N'858585858', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-02-08T15:45:14.440' AS DateTime), CAST(N'2020-02-08T15:45:14.440' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-02-08T15:45:14.440' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (6032, N'7777798', 3, 2, N'James Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'04hljpZSCT0=', N'55778899909', N'Father', NULL, N'Anil', N'rahul@gmail.com', N'04hljpZSCT0=', N'73747547575', N'Mother', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-02-08T16:10:10.287' AS DateTime), CAST(N'2020-02-08T16:10:10.287' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-02-08T16:10:10.287' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (6033, N'667777', 2, 2, N'Fifi Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'wq/6mgm1Bp4=', N'777777669', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 2, CAST(N'2020-02-08T21:38:45.177' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (6037, N'10000', 3, 2, N'Sola Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'olatunyo@yahoo.co.uk', N'0I~ZPEfTieU=', N'02002020200', N'Father', NULL, N'Olude Ojo', N'Debbo4@hotmail.com', N'wShodWTTlhM=', N'23938383993', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-02-13T21:06:23.530' AS DateTime), CAST(N'2020-02-13T21:06:23.530' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-02-13T21:06:23.530' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (6038, N'33399', 3, 2, N'Amy Ojo', N'Upload/user.jpg', NULL, N'Ayodele Ojo', N'ayobimbo2@yahoo.com', N'D8g/E2ukwgw=', N'043939393', N'Relative', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-02-13T21:16:32.813' AS DateTime), CAST(N'2020-02-13T21:16:32.813' AS DateTime), 0, 0, 1, 1, 2, CAST(N'2020-02-13T21:16:32.813' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (7037, N'283838', 13, 1, N'rad ojo', N'Upload/user.jpg', NULL, N'ayo ojo', N'ayobimbo2@yahoo.com', N'GgMn1zZonJ0=', N'939303030', N'Relative', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 1, CAST(N'2020-02-18T04:29:59.827' AS DateTime), NULL, NULL, 1, CAST(N'2020-02-18T04:30:17.857' AS DateTime), NULL, NULL, 0, CAST(N'2020-02-18T04:29:59.827' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (11, N'49494', 2, 2, N'Seyi Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'UbcPOr3z2q0=', N'04040540595', N'Father', NULL, N'Rahul Parmar', N'rahul@gmail.com', NULL, N'9876545355', N'Relative', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 2, CAST(N'2019-12-20T18:56:32.497' AS DateTime), 2, CAST(N'2020-02-01T18:02:18.690' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2019-12-16T12:31:32.210' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (6034, N'8484848', 17, 4, N'Seyiola Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'CHzn8r4a9JM=', N'3883834838', N'Father', NULL, N'', N'', NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 4, CAST(N'2020-02-09T23:18:38.523' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2020-02-09T23:18:38.523' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (6035, N'373737', 5, 1, N'Debbie Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'vLaATvFJrZg=', N'3283838383', N'Father', NULL, N'Ola Ojo', N'rahul@gmail.com', N'vLaATvFJrZg=', N'8948484848', N'Mother', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2020-02-13T04:01:14.617' AS DateTime), 1, CAST(N'2020-02-13T04:10:33.913' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2020-02-13T04:01:14.617' AS DateTime))
INSERT [dbo].[ISStudent] ([ID], [StudentNo], [ClassID], [SchoolID], [StudentName], [Photo], [DOB], [ParantName1], [ParantEmail1], [ParantPassword1], [ParantPhone1], [ParantRelation1], [ParantPhoto1], [ParantName2], [ParantEmail2], [ParantPassword2], [ParantPhone2], [ParantRelation2], [ParantPhoto2], [PickupMessageID], [PickupMessageTime], [PickupMessageDate], [PickupMessageLastUpdatedParent], [AttendanceEmailFlag], [LastAttendanceEmailDate], [LastAttendanceEmailTime], [LastUnpickAlertSentTime], [UnpickAlertDate], [LastUnpickAlertSentTeacherID], [StartDate], [EndDate], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [EmailAfterConfirmAttendence], [EmailAfterConfirmPickUp], [ISImageEmail], [SentMailDate]) VALUES (6036, N'9383838', 5, 1, N'Chris Ojo', N'Upload/user.jpg', NULL, N'Ayo Ojo', N'ayobimbo2@yahoo.com', N'Hv3LBa~ubbM=', N'9398383838', N'Father', NULL, N'Ola Ojo', N'rahul@gmail.com', N'Hv3LBa~ubbM=', N'27272727', N'Mother', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 1, 1, CAST(N'2020-02-13T04:20:41.737' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2020-02-13T04:20:41.737' AS DateTime))
SET IDENTITY_INSERT [dbo].[ISStudent] OFF
SET IDENTITY_INSERT [dbo].[ISStudentReassignHistory] ON 

INSERT [dbo].[ISStudentReassignHistory] ([ID], [SchoolID], [FromClass], [ToClass], [StduentID], [Date], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, 1, 5, 6, 4, CAST(N'2019-12-18T18:30:46.930' AS DateTime), 1, 1, 1, CAST(N'2019-12-18T18:30:46.930' AS DateTime), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISStudentReassignHistory] ([ID], [SchoolID], [FromClass], [ToClass], [StduentID], [Date], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, 1, 5, 6, 4023, CAST(N'2020-02-01T18:02:24.050' AS DateTime), 1, 1, 1, CAST(N'2020-02-01T18:02:24.050' AS DateTime), 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISStudentReassignHistory] OFF
SET IDENTITY_INSERT [dbo].[ISSupport] ON 

INSERT [dbo].[ISSupport] ([ID], [TicketNo], [Request], [SchoolID], [StatusID], [LogTypeID], [SupportOfficerID], [Priority], [Active], [Deleted], [CreatedBy], [CreatedByName], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [AssignBy], [AssignDate]) VALUES (1, N'SUP00001', N'Aasss', 1, 1, 1, NULL, NULL, 1, 1, 1, N'Hardik Patadiya', CAST(N'2020-02-13T21:53:54.717' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISSupport] OFF
SET IDENTITY_INSERT [dbo].[ISSupportStatus] ON 

INSERT [dbo].[ISSupportStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'UnResolved', 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISSupportStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'InProgress', 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISSupportStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'Resolved', 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISSupportStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, N'Cancelled', 1, 1, 1, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISSupportStatus] OFF
SET IDENTITY_INSERT [dbo].[ISTeacher] ON 

INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (1, 1, 1, 2, N'Teach001', N'Dr.', N'Siddharth Parmar', N'9876543210', N'siddharth@gmail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 1, CAST(N'2019-12-16T12:40:20.527' AS DateTime), 1, 1, CAST(N'2019-12-30T14:54:39.090' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (2, 1, 1, 3, N'NonTeach001', N'Mr.', N'Swami Ashish', N'9876543211', N'smami@ymail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 1, CAST(N'2019-12-16T12:44:06.027' AS DateTime), NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (3, 2, 2, 5, N'Teach002', N'Dr.', N'Vivek Gohil', N'9876543212', N'vivek@gmail.com', N'FjeFZFDSu3ozOOciL3Lz0g==', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 2, CAST(N'2019-12-16T12:48:51.853' AS DateTime), 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (4, 1, 5, 2, N'10000', N'Mr.', N'Ayo Ojo', N'3943949494', N'ayobimbo2@yahoo.com', N'FjeFZFDSu3ozOOciL3Lz0g==', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 1, CAST(N'2019-12-18T12:45:00.347' AS DateTime), 1, 1, CAST(N'2020-02-09T05:53:59.787' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (5, 1, 9, 2, N'1333', N'', N'Olatunyo', N'2004040404', N'olatunyo@yahoo.co.uk', N'4o4Qjr8LBkU=', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 1, CAST(N'2019-12-18T17:21:11.283' AS DateTime), 1, 1, CAST(N'2019-12-18T18:14:34.873' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (6, 1, 1, 3, N'13336', N'Mr.', N'Titi Bello', N'4005050505', N'olatunyo@gggmail.com', N'4mMk6jy1jsQ=', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 1, CAST(N'2019-12-18T18:48:22.323' AS DateTime), NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (1007, 1, 1, 2, N'Teach589', N'Mr.', N'Dilip Patel', N'9876543215', N'dilip@ymail.com', N'GHPemiQgAl4=', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 1, CAST(N'2020-01-13T14:41:26.223' AS DateTime), 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (2007, 3, 14, 2008, N'85858', N'Mr.', N'Ade Ojo', N'777777770', N'jammieola@yahoo.com', N'JWM8OYz4IXM=', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 3, CAST(N'2020-02-07T09:48:23.863' AS DateTime), 1, 3, CAST(N'2020-02-07T09:52:15.623' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (3007, 1, 6, 1, N'13333', N'Dr.', N'olatunyo', N'20040404041', N'olatunyo@gggmail.co.uk', N'WAPbfbhKGPM=', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 1, CAST(N'2020-02-18T04:03:49.407' AS DateTime), 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (3008, 1, 8, 2, N'12233', N'', N'olatunyo', N'20040404042', N'olatunyo@ggymail.com', N'tt/3Y36kapg=', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 1, CAST(N'2020-02-18T04:18:15.267' AS DateTime), 1, 1, CAST(N'2020-02-21T18:22:37.287' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[ISTeacher] ([ID], [SchoolID], [ClassID], [RoleID], [TeacherNo], [Title], [Name], [PhoneNo], [Email], [Password], [EndDate], [Photo], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime], [Role]) VALUES (7, 1, 5, 2, N'Teach334', N'Dr.', N'Hardik Parmar', N'9898989898', N'hardikpatadiya@gmail.com', N'4dkC8B~MCTY=', CAST(N'2050-01-01T00:00:00.000' AS DateTime), N'Upload/user.jpg', 1, 1, 1, CAST(N'2019-12-20T15:00:35.623' AS DateTime), 1, 1, CAST(N'2019-12-30T14:49:33.883' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[ISTeacher] OFF
SET IDENTITY_INSERT [dbo].[ISTeacherClassAssignment] ON 

INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1029, 1, 1, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:54:39.257' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1030, 1, 5, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:54:39.300' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1032, 1, 6, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:54:39.390' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, 2, 1, 0, 0, 1, 1, 1, CAST(N'2019-12-16T12:44:06.570' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (5, 3, 2, 0, 0, 1, 1, 2, CAST(N'2019-12-16T12:48:52.400' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (6, 3, 3, 0, 0, 1, 1, 2, CAST(N'2019-12-16T12:48:52.483' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4019, 4, 5, 0, 0, 1, 1, 1, CAST(N'2020-02-09T05:54:00.413' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4020, 4, 7, 0, 0, 1, 1, 1, CAST(N'2020-02-09T05:54:00.427' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4021, 4, 1, 0, 0, 1, 1, 1, CAST(N'2020-02-09T05:54:00.443' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (14, 6, 1, 0, 0, 1, 1, 1, CAST(N'2019-12-18T18:48:22.357' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4022, 4, 6, 0, 0, 1, 1, 1, CAST(N'2020-02-09T05:54:00.443' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1019, 7, 1, 0, 0, 0, 0, 1, CAST(N'2019-12-30T14:48:57.073' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1020, 7, 5, 0, 0, 0, 0, 1, CAST(N'2019-12-30T14:48:57.173' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1021, 7, 6, 0, 0, 0, 0, 1, CAST(N'2019-12-30T14:48:57.223' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1022, 7, 7, 0, 0, 0, 0, 1, CAST(N'2019-12-30T14:48:57.260' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1023, 7, 8, 0, 0, 0, 0, 1, CAST(N'2019-12-30T14:48:57.307' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1024, 7, 5, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:49:33.933' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1025, 7, 6, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:49:33.947' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1026, 7, 7, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:49:33.993' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1027, 7, 8, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:49:34.023' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1028, 7, 9, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:49:34.057' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1033, 1, 8, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:54:39.417' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2019, 1007, 1, 0, 0, 1, 1, 1, CAST(N'2020-01-13T14:41:26.723' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2020, 1007, 5, 0, 0, 1, 1, 1, CAST(N'2020-01-13T14:41:26.787' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2021, 1007, 6, 0, 0, 1, 1, 1, CAST(N'2020-01-13T14:41:26.800' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3019, 2007, 14, 0, 0, 1, 1, 3, CAST(N'2020-02-07T09:48:23.910' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3020, 2007, 15, 0, 0, 1, 1, 3, CAST(N'2020-02-07T09:48:23.943' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4024, 3007, 6, 0, 0, 1, 1, 1, CAST(N'2020-02-18T04:03:50.550' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4025, 3008, 8, 0, 0, 1, 1, 1, CAST(N'2020-02-18T04:18:15.843' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1031, 1, 7, 0, 0, 1, 1, 1, CAST(N'2019-12-30T14:54:39.337' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherClassAssignment] ([ID], [TeacherID], [ClassID], [Out], [Outbit], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (13, 5, 9, 0, 0, 1, 1, 1, CAST(N'2019-12-18T18:14:34.890' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISTeacherClassAssignment] OFF
SET IDENTITY_INSERT [dbo].[ISTeacherReassignHistory] ON 

INSERT [dbo].[ISTeacherReassignHistory] ([ID], [SchoolID], [FromClass], [ToClass], [TeacherID], [Date], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, 1, 6, 7, 5, CAST(N'2019-12-18T18:10:18.370' AS DateTime), 1, 1, 1, CAST(N'2019-12-18T18:10:18.370' AS DateTime), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTeacherReassignHistory] ([ID], [SchoolID], [FromClass], [ToClass], [TeacherID], [Date], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [CreatedByType], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, 1, 7, 9, 5, CAST(N'2019-12-18T18:14:04.540' AS DateTime), 1, 1, 1, CAST(N'2019-12-18T18:14:04.540' AS DateTime), 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISTeacherReassignHistory] OFF
SET IDENTITY_INSERT [dbo].[ISTicketMessage] ON 

INSERT [dbo].[ISTicketMessage] ([ID], [SupportID], [SenderID], [Message], [SelectFile], [CreatedDatetime], [UserTypeID]) VALUES (1, 1, 1, N'Ccccvvg', NULL, CAST(N'2020-02-13T21:53:55.280' AS DateTime), 1)
INSERT [dbo].[ISTicketMessage] ([ID], [SupportID], [SenderID], [Message], [SelectFile], [CreatedDatetime], [UserTypeID]) VALUES (2, 1, 1, N'hello', N'../Upload/', CAST(N'2020-02-29T16:40:22.400' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[ISTicketMessage] OFF
SET IDENTITY_INSERT [dbo].[ISTrasectionStatus] ON 

INSERT [dbo].[ISTrasectionStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Pending', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTrasectionStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'Refund', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTrasectionStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'Paid', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTrasectionStatus] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, N'Cancel', 1, 1, 1, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISTrasectionStatus] OFF
SET IDENTITY_INSERT [dbo].[ISTrasectionType] ON 

INSERT [dbo].[ISTrasectionType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Training', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTrasectionType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, N'Annual Maintainance', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTrasectionType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, N'Project', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTrasectionType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, N'Starter Cost', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTrasectionType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (5, N'Travel', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ISTrasectionType] ([ID], [Name], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (6, N'Miscellaneous', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISTrasectionType] OFF
SET IDENTITY_INSERT [dbo].[ISUserActivity] ON 

INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (1, 1, N'Hardik Patadiya', N'Class Created Successfully Name : After School Class(After School Ex) Document Category : Class', 1, 1, 1, CAST(N'2019-12-16T12:25:32.670' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (2, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Class 1 Document Category : Class', 1, 1, 1, CAST(N'2019-12-16T12:26:57.920' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (3, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Class 2 Document Category : Class', 1, 1, 1, CAST(N'2019-12-16T12:27:22.710' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Class 3 Document Category : Class', 1, 1, 1, CAST(N'2019-12-16T12:27:46.553' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (5, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Class 4 Document Category : Class', 1, 1, 1, CAST(N'2019-12-16T12:28:09.823' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Class 5 Document Category : Class', 1, 1, 1, CAST(N'2019-12-16T12:28:33.383' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Class 6 Document Category : Class', 1, 1, 1, CAST(N'2019-12-16T12:29:00.373' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Class 7 Document Category : Class', 1, 1, 1, CAST(N'2019-12-16T12:29:23.043' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (9, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Class 8 Document Category : Class', 1, 1, 1, CAST(N'2019-12-16T12:29:48.433' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (10, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Charmi ParmarDocument Category : StudentClass', 1, 1, 1, CAST(N'2019-12-16T12:31:45.533' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (11, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Mahek ParmarDocument Category : StudentClass', 1, 1, 1, CAST(N'2019-12-16T12:33:49.870' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (12, 1, N'Hardik Patadiya', N'Teacher Created Successfully Name : Siddharth Parmar Document Category : Teacher', 1, 1, 1, CAST(N'2019-12-16T12:40:20.760' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (13, 1, N'Hardik Patadiya', N'Non Teacher Created Successfully Name : Swami Ashish Document Category : NonTeacher', 1, 1, 1, CAST(N'2019-12-16T12:44:19.107' AS DateTime), N'NonTeacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (14, 2, N'Vihan Parmar', N'Teacher Created Successfully Name : Vivek Gohil Document Category : Teacher', 1, 1, 2, CAST(N'2019-12-16T12:48:52.583' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (15, 2, N'Vihan Parmar', N'Student Saved Successfully Name : Aakruti GohelDocument Category : StudentClass', 1, 1, 2, CAST(N'2019-12-16T12:51:21.860' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (16, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Class 9 Document Category : Class', 1, 1, 1, CAST(N'2019-12-17T14:28:55.837' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (17, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Sola OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2019-12-18T03:29:42.647' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (18, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Sola Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-18T12:21:59.730' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (19, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Titi OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2019-12-18T12:25:21.157' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (20, 1, N'Hardik Patadiya', N'Teacher Created Successfully Name : Ayo Ojo Document Category : Teacher', 1, 1, 1, CAST(N'2019-12-18T12:45:01.003' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (21, 1, N'Hardik Patadiya', N'Teacher Password Reset Successfully Name : Ayo Ojo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-18T12:47:45.783' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (22, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Sola Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-18T17:19:49.277' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (23, 1, N'Hardik Patadiya', N'Teacher Created Successfully Name : Olatunyo Document Category : Teacher', 1, 1, 1, CAST(N'2019-12-18T17:21:11.833' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (24, 1, N'Hardik Patadiya', N'Student RollOver Successfully  From Class : Class 6 To Class :Class 7 Document Category : BulkData', 1, 1, 1, CAST(N'2019-12-18T17:27:02.367' AS DateTime), N'BulkData')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (25, 1, N'Hardik Patadiya', N'Student RollOver Successfully  From Class : Class 5 To Class :Class 6 Document Category : BulkData', 1, 1, 1, CAST(N'2019-12-18T17:27:15.063' AS DateTime), N'BulkData')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (26, 1, N'Hardik Patadiya', N'Student RollOver Successfully  From Class : Class 4 To Class :Class 5 Document Category : BulkData', 1, 1, 1, CAST(N'2019-12-18T17:27:23.460' AS DateTime), N'BulkData')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (27, 1, N'Hardik Patadiya', N'Teacher Password Reset Successfully Name : Olatunyo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-18T18:10:02.357' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (28, 1, N'Hardik Patadiya', N'Teacher ReAssign Successfully Name : Olatunyo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-18T18:10:28.240' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (29, 1, N'Hardik Patadiya', N'Teacher ReAssign Successfully Name : Olatunyo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-18T18:14:12.030' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (30, 1, N'Hardik Patadiya', N'Teacher Password Reset Successfully Name : Olatunyo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-18T18:14:18.597' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (31, 1, N'Hardik Patadiya', N'Teacher Updated Successfully Name : Olatunyo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-18T18:14:34.907' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (32, 1, N'Hardik Patadiya', N'Student Class ReAssigned Successfully Name : Sola Ojo Document Category : StudentClass', 1, 1, 1, CAST(N'2019-12-18T18:30:58.617' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (33, 1, N'Hardik Patadiya', N'Non Teacher Created Successfully Name : Titi Bello Document Category : NonTeacher', 1, 1, 1, CAST(N'2019-12-18T18:48:32.277' AS DateTime), N'NonTeacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (34, 1, N'Hardik Patadiya', N'Holiday Created Successfully Name : Xpas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2019-12-18T18:58:22.477' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (35, 1, N'Hardik Patadiya', N'Holiday Created Successfully Name : Xpas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2019-12-18T18:58:51.467' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (36, 1, N'Hardik Patadiya', N'Holiday Created Successfully Name : Xpas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2019-12-18T18:59:16.627' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (37, 1, N'Hardik Patadiya', N'Holiday Created Successfully Name : Xpas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2019-12-18T19:01:26.757' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (38, 1, N'Hardik Patadiya', N'Teacher Updated Successfully Name : Ayo Ojo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-19T09:26:50.847' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (39, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Seyi OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2019-12-19T20:30:11.933' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (40, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Seyi Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-19T20:35:24.467' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (41, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Seyi Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-19T20:38:31.097' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (42, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Seyi Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-19T20:45:20.253' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (43, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Amy OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2019-12-19T20:46:30.920' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (44, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Amy Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-19T20:51:57.077' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (45, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Amy Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-20T06:09:02.323' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (46, 1, N'Hardik Patadiya', N'Teacher Password Reset Successfully Name : Ayo Ojo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-20T14:45:32.537' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (47, 1, N'Hardik Patadiya', N'Teacher Created Successfully Name : Hardik Parmar Document Category : Teacher', 1, 1, 1, CAST(N'2019-12-20T15:00:37.023' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (48, 1, N'Hardik Patadiya', N'Teacher Password Reset Successfully Name : Hardik Parmar Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-20T15:05:11.530' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (49, 1, N'Hardik Patadiya', N'Teacher Password Reset Successfully Name : Hardik Parmar Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-20T15:12:38.097' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (50, 1, N'Hardik Patadiya', N'Teacher Password Reset Successfully Name : Ayo Ojo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-20T16:07:01.110' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (51, 2, N'Vihan Parmar', N'Student Saved Successfully Name : Seyiola OjoDocument Category : StudentClass', 1, 1, 2, CAST(N'2019-12-20T18:56:39.877' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (52, 2, N'Vihan Parmar', N'Student Updated Successfully Name : Seyi Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2019-12-20T19:02:24.963' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (53, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2019-12-21T14:12:50.837' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (54, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Fola OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2019-12-21T14:16:02.383' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (55, 1, N'Hardik Patadiya', N'Role Added Successfully Name : Assistant Admin Document Category : UserRole', 1, 1, 1, CAST(N'2019-12-21T14:20:18.803' AS DateTime), N'UserRole')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (56, 1, N'Hardik Patadiya', N'Role Added Successfully Name : Assistant Operator Document Category : UserRole', 1, 1, 1, CAST(N'2019-12-21T14:21:35.150' AS DateTime), N'UserRole')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (57, 1, N'Hardik Patadiya', N'Role Added Successfully Name : Assistant Operator Document Category : UserRole', 1, 1, 1, CAST(N'2019-12-21T14:22:42.723' AS DateTime), N'UserRole')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (58, 1, N'Hardik Patadiya', N'Role Deleted Successfully Name : Assistant Operator Document Category : UserRole', 1, 1, 1, CAST(N'2019-12-21T14:22:56.733' AS DateTime), N'UserRole')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (59, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2019-12-21T15:48:20.857' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (60, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2019-12-21T15:48:37.120' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (61, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2019-12-21T15:50:06.043' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (62, 1, N'Hardik Patadiya', N'Student RollOver Successfully  From Class : Class 9 To Class :Class 8 Document Category : BulkData', 1, 1, 1, CAST(N'2019-12-21T15:55:50.807' AS DateTime), N'BulkData')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (63, 1, N'Hardik Patadiya', N'Student RollOver Successfully  From Class : Class 7 To Class :Class 8 Document Category : BulkData', 1, 1, 1, CAST(N'2019-12-21T15:55:50.853' AS DateTime), N'BulkData')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (64, 1, N'Hardik Patadiya', N'Student RollOver Successfully  From Class : Class 6 To Class :Class 9 Document Category : BulkData', 1, 1, 1, CAST(N'2019-12-21T15:56:52.397' AS DateTime), N'BulkData')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (1039, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2019-12-22T23:16:38.733' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (1040, 1, N'Ayo Ojo', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 4, CAST(N'2019-12-22T23:28:30.983' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (1041, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Seyi Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-23T01:38:09.737' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (1042, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Seyi Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-23T02:18:31.707' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (1043, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Tolu OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2019-12-23T02:19:45.733' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (1044, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Tolu Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-23T02:56:45.867' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (2039, 2, N'Vihan Parmar', N'Student Saved Successfully Name : Biti OjoDocument Category : StudentClass', 1, 1, 2, CAST(N'2019-12-24T19:27:19.783' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (3039, 2, N'Vihan Parmar', N'Student Saved Successfully Name : Any OjoDocument Category : StudentClass', 1, 1, 2, CAST(N'2019-12-26T06:46:58.237' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4039, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2019-12-26T18:24:28.693' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4040, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2019-12-26T18:30:05.307' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4041, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2019-12-26T18:50:59.803' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4042, 2, N'Vihan Parmar', N'Student Saved Successfully Name : Tolu OjoDocument Category : StudentClass', 1, 1, 2, CAST(N'2019-12-26T19:05:34.273' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4043, 2, N'Vihan Parmar', N'Student Updated Successfully Name : Amy Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2019-12-26T19:11:35.140' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4044, 1, N'Hardik Patadiya', N'Message Sent SuccessfullySubject :  Document Category : Message', 1, 1, 1, CAST(N'2019-12-26T19:19:22.667' AS DateTime), N'Message')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4045, 1, N'Hardik Patadiya', N'Role Updated Successfully Name : Assistant Admin Document Category : UserRole', 1, 1, 1, CAST(N'2019-12-26T19:23:37.407' AS DateTime), N'UserRole')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4046, 2, N'Vihan Parmar', N'Student Saved Successfully Name : Fola OjoDocument Category : StudentClass', 1, 1, 2, CAST(N'2019-12-27T18:36:46.260' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4047, 1, N'Hardik Patadiya', N'Teacher Updated Successfully Name : Hardik Parmar Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-30T14:48:57.337' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4048, 1, N'Hardik Patadiya', N'Teacher Updated Successfully Name : Hardik Parmar Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-30T14:49:34.093' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4049, 1, N'Hardik Patadiya', N'Teacher Updated Successfully Name : Siddharth Parmar Document Category : TeacherProfile', 1, 1, 1, CAST(N'2019-12-30T14:54:39.447' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4050, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2019-12-30T17:02:22.373' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4051, 2, N'Vivek Gohil', N'Student Saved Successfully Name : Bola OjoDocument Category : StudentClass', 1, 1, 3, CAST(N'2019-12-30T17:09:42.093' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4052, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : James OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2019-12-30T17:13:06.147' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4053, 2, N'Vivek Gohil', N'Student Saved Successfully Name : James OjoDocument Category : StudentClass', 1, 1, 3, CAST(N'2019-12-30T17:14:13.570' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4054, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Charmi Parmar Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-30T17:28:19.170' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4055, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : James Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-30T17:42:18.397' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4056, 2, N'Vihan Parmar', N'Student Deleted Successfully Name : James Ojo Document Category : StudentClass', 1, 1, 2, CAST(N'2019-12-30T17:43:58.050' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4057, 2, N'Vihan Parmar', N'Student Saved Successfully Name : Jame OjoDocument Category : StudentClass', 1, 1, 2, CAST(N'2019-12-30T17:44:47.923' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4058, 2, N'Vihan Parmar', N'Student Updated Successfully Name : James Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2019-12-30T17:46:12.580' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4059, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Charmi Parmar Document Category : StudentProfile', 1, 1, 1, CAST(N'2019-12-30T17:56:26.287' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (4060, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-04T15:50:48.363' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (5046, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-11T00:37:01.603' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (5047, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-11T00:37:23.227' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (5048, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-11T00:41:16.267' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (5049, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-11T00:41:54.760' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (5050, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-11T04:31:21.490' AS DateTime), N'Class')
GO
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (5051, 1, N'Hardik Patadiya', N'Role Updated Successfully Name : Assistant Admin Document Category : UserRole', 1, 1, 1, CAST(N'2020-01-11T04:45:55.647' AS DateTime), N'UserRole')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (5052, 1, N'Hardik Patadiya', N'Role Added Successfully Name : Test New Role Document Category : UserRole', 1, 1, 1, CAST(N'2020-01-11T04:47:18.193' AS DateTime), N'UserRole')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (5053, 1, N'Hardik Patadiya', N'Teacher Created Successfully Name : Dilip Patel Document Category : Teacher', 1, 1, 1, CAST(N'2020-01-13T14:41:26.817' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6046, 1, N'Hardik Patadiya', N'Holiday Created Successfully Name : Sky Document Category : HolidayAdd', 1, 1, 1, CAST(N'2020-01-19T22:12:14.640' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6047, 2, N'Vihan Parmar', N'Student Updated Successfully Name : Fola Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2020-01-22T12:21:36.847' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6048, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-23T11:01:16.607' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6049, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-26T04:35:51.783' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6050, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-26T04:35:58.990' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6051, 1, N'Hardik Patadiya', N'Role Added Successfully Name : Test 2 Document Category : UserRole', 1, 1, 1, CAST(N'2020-01-26T04:58:52.730' AS DateTime), N'UserRole')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6052, 1, N'Hardik Patadiya', N'Message Sent SuccessfullySubject :  Document Category : Message', 1, 1, 1, CAST(N'2020-01-26T05:02:08.293' AS DateTime), N'Message')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6053, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-01-26T05:07:24.997' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6054, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Brad OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2020-01-26T21:18:33.507' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6055, 2, N'Vihan Parmar', N'Student Saved Successfully Name : Brad OjoDocument Category : StudentClass', 1, 1, 2, CAST(N'2020-01-26T21:21:20.343' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6056, 1, N'Ayo Ojo', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 4, CAST(N'2020-01-26T21:30:52.467' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6057, 2, N'Vivek Gohil', N'Student Saved Successfully Name : Philip OjoDocument Category : StudentClass', 1, 1, 3, CAST(N'2020-01-26T21:50:49.097' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6058, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Philip OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2020-01-26T21:53:29.217' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6059, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-27T03:09:21.363' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6060, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-27T03:09:59.853' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6061, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-27T03:10:26.557' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6062, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-01-27T03:11:09.973' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6063, 1, N'Ayo Ojo', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 4, CAST(N'2020-01-30T14:40:30.870' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6064, 2, N'Vihan Parmar', N'Student Updated Successfully Name : Aakruti Gohel Document Category : StudentProfile', 1, 1, 2, CAST(N'2020-01-30T17:24:10.080' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6065, 2, N'Vihan Parmar', N'Student Updated Successfully Name : Aakruti Gohel Document Category : StudentProfile', 1, 1, 2, CAST(N'2020-01-30T17:24:54.537' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6066, 2, N'Vihan Parmar', N'Student Updated Successfully Name : James Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2020-02-01T17:24:07.963' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6067, 2, N'Vihan Parmar', N'Student Updated Successfully Name : James Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2020-02-01T17:24:26.870' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6068, 2, N'Vihan Parmar', N'Student Updated Successfully Name : James Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2020-02-01T17:25:42.860' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6069, 2, N'Vihan Parmar', N'Student Updated Successfully Name : Seyi Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2020-02-01T18:02:27.663' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6070, 1, N'Hardik Patadiya', N'Student Class Assigned Successfully Name : James Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2020-02-01T18:02:45.867' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6071, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-01T18:45:47.660' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6072, 2, N'Vihan Parmar', N'Student Updated Successfully Name : Bola Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2020-02-04T16:50:45.347' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6073, 2, N'Vihan Parmar', N'Student Updated Successfully Name : Bola Ojo Document Category : StudentProfile', 1, 1, 2, CAST(N'2020-02-04T16:51:34.723' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6074, 3, N'Abimbola Ola', N'Student Saved Successfully Name : Brad OjoDocument Category : StudentClass', 1, 1, 3, CAST(N'2020-02-07T09:31:43.577' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6075, 3, N'Abimbola Ola', N'Student Saved Successfully Name : Amy OjoDocument Category : StudentClass', 1, 1, 3, CAST(N'2020-02-07T09:32:10.723' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6076, 3, N'Abimbola Ola', N'Student Saved Successfully Name : Philip OjoDocument Category : StudentClass', 1, 1, 3, CAST(N'2020-02-07T09:32:43.593' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6077, 4, N'John Smith', N'Class Created Successfully Name : Fox Document Category : Class', 1, 1, 4, CAST(N'2020-02-07T09:37:21.453' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6078, 3, N'Abimbola Ola', N'Teacher Created Successfully Name : Ade Ojo Document Category : Teacher', 1, 1, 3, CAST(N'2020-02-07T09:48:23.943' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6079, 3, N'Abimbola Ola', N'Teacher Password Reset Successfully Name : Ade Ojo Document Category : TeacherProfile', 1, 1, 3, CAST(N'2020-02-07T09:52:18.810' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6080, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-07T12:53:28.900' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6081, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-07T12:53:35.890' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (6082, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-07T13:02:40.400' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7074, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-08T02:11:04.260' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7075, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-08T18:56:16.747' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7076, 2, N'Vihan Parmar', N'Student Saved Successfully Name : Fifi OjoDocument Category : StudentClass', 1, 1, 2, CAST(N'2020-02-08T21:38:52.010' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7077, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-02-09T05:41:31.910' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7078, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-09T05:44:55.837' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7079, 1, N'Hardik Patadiya', N'Teacher Updated Successfully Name : Ayo Ojo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2020-02-09T05:54:00.477' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7080, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-09T06:04:23.543' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7081, 1, N'Ayo Ojo', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 4, CAST(N'2020-02-09T06:09:14.953' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7082, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-09T23:02:49.613' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7083, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-02-09T23:03:32.737' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7084, 4, N'John Smith', N'Student Saved Successfully Name : Seyiola OjoDocument Category : StudentClass', 1, 1, 4, CAST(N'2020-02-09T23:18:42.797' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7085, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-02-10T01:03:17.160' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7086, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-02-10T01:17:01.207' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7087, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-11T09:15:52.447' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7088, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-11T09:15:59.260' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7089, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-11T09:16:06.190' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7090, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Debbie OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2020-02-13T04:01:23.357' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7091, 1, N'Hardik Patadiya', N'Student Updated Successfully Name : Debbie Ojo Document Category : StudentProfile', 1, 1, 1, CAST(N'2020-02-13T04:10:41.283' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7092, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : Chris OjoDocument Category : StudentClass', 1, 1, 1, CAST(N'2020-02-13T04:20:48.910' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7093, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-13T20:33:10.973' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7094, 1, N'Hardik Patadiya', N'Class Updated SuccessfullyName : After School Class Document Category : Class', 1, 1, 1, CAST(N'2020-02-13T20:35:31.320' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (7095, 1, N'Hardik Patadiya', N'Support Created Successfully Subject :  Document Category : SupportLogCreate', 1, 1, 1, CAST(N'2020-02-13T21:53:59.627' AS DateTime), N'Support')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8093, 1, N'Hardik Patadiya', N'School Profile Updated Successfully Name : Gatistavam School for Management and Computer Education Document Category : Profile', 1, 1, 1, CAST(N'2020-02-15T01:26:26.687' AS DateTime), N'Profile')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8094, 1, N'Hardik Patadiya', N'Teacher Created Successfully Name : olatunyo Document Category : Teacher', 1, 1, 1, CAST(N'2020-02-18T04:03:50.643' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8095, 1, N'Hardik Patadiya', N'Teacher Created Successfully Name : olatunyo Document Category : Teacher', 1, 1, 1, CAST(N'2020-02-18T04:18:15.860' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8096, 1, N'Hardik Patadiya', N'Student Saved Successfully Name : rad ojoDocument Category : StudentClass', 1, 1, 1, CAST(N'2020-02-18T04:30:04.207' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8097, 1, N'Hardik Patadiya', N'Student Deleted Successfully ID : 7037 Document Category : StudentProfile', 1, 1, 1, CAST(N'2020-02-18T04:30:18.357' AS DateTime), N'Student')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8098, 1, N'Hardik Patadiya', N'Holiday Created Successfully Name : Christmas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2020-02-18T04:40:46.827' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8099, 1, N'Hardik Patadiya', N'Holiday Updated Successfully Name : Christmas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2020-02-18T04:41:16.070' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8100, 1, N'Hardik Patadiya', N'Holiday Updated Successfully Name : Christmas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2020-02-18T04:41:55.877' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8101, 1, N'Hardik Patadiya', N'Holiday Updated Successfully Name : Christmas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2020-02-18T04:44:53.523' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8102, 1, N'Hardik Patadiya', N'Teacher Password Reset Successfully Name : olatunyo Document Category : TeacherProfile', 1, 1, 1, CAST(N'2020-02-21T18:22:41.700' AS DateTime), N'Teacher')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8103, 1, N'Hardik Patadiya', N'Holiday Updated Successfully Name : Xpas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2020-02-21T19:01:29.890' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8104, 1, N'Hardik Patadiya', N'Holiday Updated Successfully Name : Xpas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2020-02-21T19:02:06.203' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8105, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Classes1 Document Category : Class', 1, 1, 1, CAST(N'2020-02-28T15:19:59.937' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8106, 1, N'Hardik Patadiya', N'Class Created Successfully Name : Classes4 Document Category : Class', 1, 1, 1, CAST(N'2020-02-28T15:39:01.580' AS DateTime), N'Class')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8107, 1, N'Hardik Patadiya', N'Holiday Updated Successfully Name : Xpas Document Category : HolidayAdd', 1, 1, 1, CAST(N'2020-02-29T16:24:17.847' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8108, 1, N'Hardik Patadiya', N'Holiday Deleted Successfully Name : Xpas Document Category : HolidayList', 1, 1, 1, CAST(N'2020-02-29T16:29:02.103' AS DateTime), N'Holiday')
INSERT [dbo].[ISUserActivity] ([ID], [SchoolID], [UserName], [LogText], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [DocumentCategory]) VALUES (8109, 1, N'Hardik Patadiya', N'Support Sent Successfully ID : 1 Document Category : SendSupportViewed', 1, 1, 1, CAST(N'2020-02-29T16:40:22.447' AS DateTime), N'Support')
SET IDENTITY_INSERT [dbo].[ISUserActivity] OFF
SET IDENTITY_INSERT [dbo].[ISUserRole] ON 

INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1, 1, N'Standard', 1, 0, 0, 0, 0, 0, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:09:09.100' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2, 1, N'Admin', 1, 1, 1, 1, 1, 1, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:09:09.263' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (3, 1, N'Admin', 2, 1, 1, 1, 1, 1, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:09:09.393' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (4, 2, N'Standard', 1, 0, 0, 0, 0, 0, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:14:17.873' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (5, 2, N'Admin', 1, 1, 1, 1, 1, 1, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:14:17.933' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (6, 2, N'Admin', 2, 1, 1, 1, 1, 1, NULL, NULL, 1, 1, 1, CAST(N'2019-12-16T12:14:18.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (7, 1, N'Assistant Admin', 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, CAST(N'2019-12-21T14:20:14.217' AS DateTime), 1, CAST(N'2020-01-11T04:45:48.030' AS DateTime), NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (8, 1, N'Assistant Operator', 2, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, CAST(N'2019-12-21T14:21:30.600' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (9, 1, N'Assistant Operator', 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, CAST(N'2019-12-21T14:22:38.113' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-21T14:22:51.777' AS DateTime))
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (1007, 1, N'Test New Role', 1, 0, 1, 0, 1, 0, 0, 1, 1, 1, 1, CAST(N'2020-01-11T04:47:11.637' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2007, 1, N'Test 2', 2, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, CAST(N'2020-01-26T04:58:45.970' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2008, 3, N'Standard', 1, 0, 0, 0, 0, 0, NULL, NULL, 1, 1, 1, CAST(N'2020-02-07T09:19:51.467' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2009, 3, N'Admin', 1, 1, 1, 1, 1, 1, NULL, NULL, 1, 1, 1, CAST(N'2020-02-07T09:19:51.527' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2010, 3, N'Admin', 2, 1, 1, 1, 1, 1, NULL, NULL, 1, 1, 1, CAST(N'2020-02-07T09:19:51.527' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2011, 4, N'Standard', 1, 0, 0, 0, 0, 0, NULL, NULL, 1, 1, 1, CAST(N'2020-02-07T09:36:14.717' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2012, 4, N'Admin', 1, 1, 1, 1, 1, 1, NULL, NULL, 1, 1, 1, CAST(N'2020-02-07T09:36:14.717' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[ISUserRole] ([ID], [SchoolID], [RoleName], [RoleType], [ManageTeacherFlag], [ManageClassFlag], [ManageSupportFlag], [ManageStudentFlag], [ManageViewAccountFlag], [ManageNonTeacherFlag], [ManageHolidayFlag], [Active], [Deleted], [CreatedBy], [CreatedDateTime], [ModifyBy], [ModifyDateTime], [DeletedBy], [DeletedDateTime]) VALUES (2013, 4, N'Admin', 2, 1, 1, 1, 1, 1, NULL, NULL, 1, 1, 1, CAST(N'2020-02-07T09:36:14.733' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ISUserRole] OFF
/****** Object:  Index [ISAccountStatus_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISAccountStatus] ADD  CONSTRAINT [ISAccountStatus_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISAdminLogin_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISAdminLogin] ADD  CONSTRAINT [ISAdminLogin_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISAttendance_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISAttendance] ADD  CONSTRAINT [ISAttendance_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISClass_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISClass] ADD  CONSTRAINT [ISClass_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISClassType_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISClassType] ADD  CONSTRAINT [ISClassType_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISClassYear_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISClassYear] ADD  CONSTRAINT [ISClassYear_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISCompleteAttendanceRun_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISCompleteAttendanceRun] ADD  CONSTRAINT [ISCompleteAttendanceRun_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISCompletePickupRun_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISCompletePickupRun] ADD  CONSTRAINT [ISCompletePickupRun_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISCountries_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISCountries] ADD  CONSTRAINT [ISCountries_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISDataUploadHistory_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISDataUploadHistory] ADD  CONSTRAINT [ISDataUploadHistory_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISFAQ_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISFAQ] ADD  CONSTRAINT [ISFAQ_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISInvoice_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISInvoice] ADD  CONSTRAINT [ISInvoice_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISLogType_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISLogType] ADD  CONSTRAINT [ISLogType_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISMessage_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISMessage] ADD  CONSTRAINT [ISMessage_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISOrganisationUser_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISOrganisationUser] ADD  CONSTRAINT [ISOrganisationUser_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISPayment_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISPayment] ADD  CONSTRAINT [ISPayment_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISPicker_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISPicker] ADD  CONSTRAINT [ISPicker_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISPickerAssignment_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISPickerAssignment] ADD  CONSTRAINT [ISPickerAssignment_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISPickup_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISPickup] ADD  CONSTRAINT [ISPickup_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISPickUpMessage_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISPickUpMessage] ADD  CONSTRAINT [ISPickUpMessage_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISPickUpStatus_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISPickUpStatus] ADD  CONSTRAINT [ISPickUpStatus_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISReport_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISReport] ADD  CONSTRAINT [ISReport_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISRole_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISRole] ADD  CONSTRAINT [ISRole_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISSchool_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISSchool] ADD  CONSTRAINT [ISSchool_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISSchoolClass_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISSchoolClass] ADD  CONSTRAINT [ISSchoolClass_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISSchoolInvoice_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISSchoolInvoice] ADD  CONSTRAINT [ISSchoolInvoice_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISSchoolType_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISSchoolType] ADD  CONSTRAINT [ISSchoolType_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISStudent_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [ISStudent_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISStudentHistory_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISStudentHistory] ADD  CONSTRAINT [ISStudentHistory_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISStudentReassignHistory_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISStudentReassignHistory] ADD  CONSTRAINT [ISStudentReassignHistory_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISSupport_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISSupport] ADD  CONSTRAINT [ISSupport_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISSupportStatus_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISSupportStatus] ADD  CONSTRAINT [ISSupportStatus_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISTeacher_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISTeacher] ADD  CONSTRAINT [ISTeacher_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISTeacherClassAssignment_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISTeacherClassAssignment] ADD  CONSTRAINT [ISTeacherClassAssignment_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISTeacherReassignHistory_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISTeacherReassignHistory] ADD  CONSTRAINT [ISTeacherReassignHistory_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISTicketMessage_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISTicketMessage] ADD  CONSTRAINT [ISTicketMessage_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISTrasectionStatus_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISTrasectionStatus] ADD  CONSTRAINT [ISTrasectionStatus_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISTrasectionType_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISTrasectionType] ADD  CONSTRAINT [ISTrasectionType_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ISUserRole_PRIMARY]    Script Date: 20/03/2020 1:41:40 PM ******/
ALTER TABLE [dbo].[ISUserRole] ADD  CONSTRAINT [ISUserRole_PRIMARY] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__Picku__4AB81AF0]  DEFAULT (NULL) FOR [PickupMessageID]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__Picku__4BAC3F29]  DEFAULT (NULL) FOR [PickupMessageTime]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__Picku__4CA06362]  DEFAULT (NULL) FOR [PickupMessageDate]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__Picku__4D94879B]  DEFAULT (NULL) FOR [PickupMessageLastUpdatedParent]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__Atten__4E88ABD4]  DEFAULT (NULL) FOR [AttendanceEmailFlag]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__LastA__4F7CD00D]  DEFAULT (NULL) FOR [LastAttendanceEmailDate]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__LastA__5070F446]  DEFAULT (NULL) FOR [LastAttendanceEmailTime]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__LastU__5165187F]  DEFAULT (NULL) FOR [LastUnpickAlertSentTime]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__Unpic__52593CB8]  DEFAULT (NULL) FOR [UnpickAlertDate]
GO
ALTER TABLE [dbo].[ISStudent] ADD  CONSTRAINT [DF__ISStudent__LastU__534D60F1]  DEFAULT (NULL) FOR [LastUnpickAlertSentTeacherID]
GO
ALTER TABLE [dbo].[ISAttendance]  WITH CHECK ADD  CONSTRAINT [FK__ISAttenda__Stude__5165187F] FOREIGN KEY([StudentID])
REFERENCES [dbo].[ISStudent] ([ID])
GO
ALTER TABLE [dbo].[ISAttendance] CHECK CONSTRAINT [FK__ISAttenda__Stude__5165187F]
GO
ALTER TABLE [dbo].[ISAttendance]  WITH CHECK ADD  CONSTRAINT [FK__ISAttenda__Teach__52593CB8] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[ISTeacher] ([ID])
GO
ALTER TABLE [dbo].[ISAttendance] CHECK CONSTRAINT [FK__ISAttenda__Teach__52593CB8]
GO
ALTER TABLE [dbo].[ISClass]  WITH CHECK ADD  CONSTRAINT [FK_ISClass_ISClassType] FOREIGN KEY([TypeID])
REFERENCES [dbo].[ISClassType] ([ID])
GO
ALTER TABLE [dbo].[ISClass] CHECK CONSTRAINT [FK_ISClass_ISClassType]
GO
ALTER TABLE [dbo].[ISCompleteAttendanceRun]  WITH CHECK ADD  CONSTRAINT [FK__ISCompleteAttendanceRun__ISClass] FOREIGN KEY([ClassID])
REFERENCES [dbo].[ISClass] ([ID])
GO
ALTER TABLE [dbo].[ISCompleteAttendanceRun] CHECK CONSTRAINT [FK__ISCompleteAttendanceRun__ISClass]
GO
ALTER TABLE [dbo].[ISCompleteAttendanceRun]  WITH CHECK ADD  CONSTRAINT [FK__ISCompleteAttendanceRun__ISTeacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[ISTeacher] ([ID])
GO
ALTER TABLE [dbo].[ISCompleteAttendanceRun] CHECK CONSTRAINT [FK__ISCompleteAttendanceRun__ISTeacher]
GO
ALTER TABLE [dbo].[ISCompletePickupRun]  WITH CHECK ADD  CONSTRAINT [FK__ISCompletePickupRun__ISClass] FOREIGN KEY([ClassID])
REFERENCES [dbo].[ISClass] ([ID])
GO
ALTER TABLE [dbo].[ISCompletePickupRun] CHECK CONSTRAINT [FK__ISCompletePickupRun__ISClass]
GO
ALTER TABLE [dbo].[ISCompletePickupRun]  WITH CHECK ADD  CONSTRAINT [FK__ISCompletePickupRun__ISTeacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[ISTeacher] ([ID])
GO
ALTER TABLE [dbo].[ISCompletePickupRun] CHECK CONSTRAINT [FK__ISCompletePickupRun__ISTeacher]
GO
ALTER TABLE [dbo].[ISHoliday]  WITH CHECK ADD  CONSTRAINT [FK__ISHoliday__Schoo__5CD6CB2B] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[ISSchool] ([ID])
GO
ALTER TABLE [dbo].[ISHoliday] CHECK CONSTRAINT [FK__ISHoliday__Schoo__5CD6CB2B]
GO
ALTER TABLE [dbo].[ISOrganisationUser]  WITH CHECK ADD FOREIGN KEY([CountryID])
REFERENCES [dbo].[ISCountries] ([ID])
GO
ALTER TABLE [dbo].[ISOrganisationUser]  WITH CHECK ADD  CONSTRAINT [FK_ISOrganisationUser_ISRole] FOREIGN KEY([RoleID])
REFERENCES [dbo].[ISRole] ([ID])
GO
ALTER TABLE [dbo].[ISOrganisationUser] CHECK CONSTRAINT [FK_ISOrganisationUser_ISRole]
GO
ALTER TABLE [dbo].[ISPicker]  WITH CHECK ADD  CONSTRAINT [FK__ISPicker__Parent__671F4F74] FOREIGN KEY([ParentID])
REFERENCES [dbo].[ISStudent] ([ID])
GO
ALTER TABLE [dbo].[ISPicker] CHECK CONSTRAINT [FK__ISPicker__Parent__671F4F74]
GO
ALTER TABLE [dbo].[ISPicker]  WITH CHECK ADD FOREIGN KEY([SchoolID])
REFERENCES [dbo].[ISSchool] ([ID])
GO
ALTER TABLE [dbo].[ISPicker]  WITH CHECK ADD FOREIGN KEY([SchoolID])
REFERENCES [dbo].[ISSchool] ([ID])
GO
ALTER TABLE [dbo].[ISPicker]  WITH CHECK ADD  CONSTRAINT [FK__ISPicker__Studen__662B2B3B] FOREIGN KEY([StudentID])
REFERENCES [dbo].[ISStudent] ([ID])
GO
ALTER TABLE [dbo].[ISPicker] CHECK CONSTRAINT [FK__ISPicker__Studen__662B2B3B]
GO
ALTER TABLE [dbo].[ISPickup]  WITH CHECK ADD  CONSTRAINT [FK__ISPickup__Picker__5BAD9CC8] FOREIGN KEY([PickerID])
REFERENCES [dbo].[ISPicker] ([ID])
GO
ALTER TABLE [dbo].[ISPickup] CHECK CONSTRAINT [FK__ISPickup__Picker__5BAD9CC8]
GO
ALTER TABLE [dbo].[ISPickup]  WITH CHECK ADD  CONSTRAINT [FK__ISPickup__Studen__59C55456] FOREIGN KEY([StudentID])
REFERENCES [dbo].[ISStudent] ([ID])
GO
ALTER TABLE [dbo].[ISPickup] CHECK CONSTRAINT [FK__ISPickup__Studen__59C55456]
GO
ALTER TABLE [dbo].[ISPickup]  WITH CHECK ADD  CONSTRAINT [FK__ISPickup__Teache__5AB9788F] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[ISTeacher] ([ID])
GO
ALTER TABLE [dbo].[ISPickup] CHECK CONSTRAINT [FK__ISPickup__Teache__5AB9788F]
GO
ALTER TABLE [dbo].[ISSchool]  WITH CHECK ADD  CONSTRAINT [FK__ISSchool__Accoun__70A8B9AE] FOREIGN KEY([AccountStatusID])
REFERENCES [dbo].[ISAccountStatus] ([ID])
GO
ALTER TABLE [dbo].[ISSchool] CHECK CONSTRAINT [FK__ISSchool__Accoun__70A8B9AE]
GO
ALTER TABLE [dbo].[ISSchool]  WITH CHECK ADD FOREIGN KEY([BillingCountryID])
REFERENCES [dbo].[ISCountries] ([ID])
GO
ALTER TABLE [dbo].[ISSchool]  WITH CHECK ADD  CONSTRAINT [FK_ISSchool_ISSchool] FOREIGN KEY([ID])
REFERENCES [dbo].[ISSchool] ([ID])
GO
ALTER TABLE [dbo].[ISSchool] CHECK CONSTRAINT [FK_ISSchool_ISSchool]
GO
ALTER TABLE [dbo].[ISSchool]  WITH CHECK ADD  CONSTRAINT [FK_ISSchool_ISSchoolType] FOREIGN KEY([TypeID])
REFERENCES [dbo].[ISSchoolType] ([ID])
GO
ALTER TABLE [dbo].[ISSchool] CHECK CONSTRAINT [FK_ISSchool_ISSchoolType]
GO
ALTER TABLE [dbo].[ISSchoolInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ISSchoolInvoice_ISSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[ISSchool] ([ID])
GO
ALTER TABLE [dbo].[ISSchoolInvoice] CHECK CONSTRAINT [FK_ISSchoolInvoice_ISSchool]
GO
ALTER TABLE [dbo].[ISSchoolInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ISSchoolInvoice_ISTrasectionStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[ISTrasectionStatus] ([ID])
GO
ALTER TABLE [dbo].[ISSchoolInvoice] CHECK CONSTRAINT [FK_ISSchoolInvoice_ISTrasectionStatus]
GO
ALTER TABLE [dbo].[ISSchoolInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ISSchoolInvoice_ISTrasectionType] FOREIGN KEY([TransactionTypeID])
REFERENCES [dbo].[ISTrasectionType] ([ID])
GO
ALTER TABLE [dbo].[ISSchoolInvoice] CHECK CONSTRAINT [FK_ISSchoolInvoice_ISTrasectionType]
GO
ALTER TABLE [dbo].[ISStudent]  WITH CHECK ADD  CONSTRAINT [FK__ISStudent__Class__65370702] FOREIGN KEY([ClassID])
REFERENCES [dbo].[ISClass] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ISStudent] CHECK CONSTRAINT [FK__ISStudent__Class__65370702]
GO
ALTER TABLE [dbo].[ISStudent]  WITH CHECK ADD  CONSTRAINT [FK__ISStudent__Schoo__6442E2C9] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[ISSchool] ([ID])
GO
ALTER TABLE [dbo].[ISStudent] CHECK CONSTRAINT [FK__ISStudent__Schoo__6442E2C9]
GO
ALTER TABLE [dbo].[ISSupport]  WITH CHECK ADD  CONSTRAINT [FK__ISSupport__LogTy__6477ECF3] FOREIGN KEY([LogTypeID])
REFERENCES [dbo].[ISLogType] ([ID])
GO
ALTER TABLE [dbo].[ISSupport] CHECK CONSTRAINT [FK__ISSupport__LogTy__6477ECF3]
GO
ALTER TABLE [dbo].[ISSupport]  WITH CHECK ADD  CONSTRAINT [FK_ISSupport_ISSupportStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[ISSupportStatus] ([ID])
GO
ALTER TABLE [dbo].[ISSupport] CHECK CONSTRAINT [FK_ISSupport_ISSupportStatus]
GO
ALTER TABLE [dbo].[ISTeacher]  WITH CHECK ADD  CONSTRAINT [FK__ISTeacher__RoleI__6754599E] FOREIGN KEY([RoleID])
REFERENCES [dbo].[ISUserRole] ([ID])
GO
ALTER TABLE [dbo].[ISTeacher] CHECK CONSTRAINT [FK__ISTeacher__RoleI__6754599E]
GO
ALTER TABLE [dbo].[ISTeacher]  WITH CHECK ADD  CONSTRAINT [FK__ISTeacher__Schoo__46B27FE2] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[ISSchool] ([ID])
GO
ALTER TABLE [dbo].[ISTeacher] CHECK CONSTRAINT [FK__ISTeacher__Schoo__46B27FE2]
GO
ALTER TABLE [dbo].[ISTeacherClassAssignment]  WITH CHECK ADD  CONSTRAINT [FK__ISTeacher__Class__0E391C95] FOREIGN KEY([ClassID])
REFERENCES [dbo].[ISClass] ([ID])
GO
ALTER TABLE [dbo].[ISTeacherClassAssignment] CHECK CONSTRAINT [FK__ISTeacher__Class__0E391C95]
GO
ALTER TABLE [dbo].[ISTeacherClassAssignment]  WITH CHECK ADD  CONSTRAINT [FK__ISTeacher__Teach__6A30C649] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[ISTeacher] ([ID])
GO
ALTER TABLE [dbo].[ISTeacherClassAssignment] CHECK CONSTRAINT [FK__ISTeacher__Teach__6A30C649]
GO
ALTER TABLE [dbo].[ISTicketMessage]  WITH CHECK ADD  CONSTRAINT [FK_ISTicketMessage_ISSupport] FOREIGN KEY([SupportID])
REFERENCES [dbo].[ISSupport] ([ID])
GO
ALTER TABLE [dbo].[ISTicketMessage] CHECK CONSTRAINT [FK_ISTicketMessage_ISSupport]
GO
ALTER TABLE [dbo].[ISUserActivity]  WITH CHECK ADD  CONSTRAINT [FK__ISUserAct__Schoo__2704CA5F] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[ISSchool] ([ID])
GO
ALTER TABLE [dbo].[ISUserActivity] CHECK CONSTRAINT [FK__ISUserAct__Schoo__2704CA5F]
GO
ALTER TABLE [dbo].[ISUserRole]  WITH CHECK ADD  CONSTRAINT [FK_ISUserRole_ISSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[ISSchool] ([ID])
GO
ALTER TABLE [dbo].[ISUserRole] CHECK CONSTRAINT [FK_ISUserRole_ISSchool]
GO
/****** Object:  StoredProcedure [dbo].[getAttandanceData]    Script Date: 20/03/2020 1:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getAttandanceData]
@date datetime null
AS
BEGIN
	
	SET NOCOUNT ON;


	if(@date is null)
	begin
	set @date=GETDATE()
	end


   SELECT        A.ID, A.Date, A.StudentID, A.TeacherID, A.Status, A.Date as Dates, A.Time, A.Active, A.Deleted, 
                         A.CreatedBy, A.CreatedDateTime, A.ModifyBy, A.ModifyDateTime, A.DeletedBy, A.DeletedDateTime, 
                         dbo.ISStudent.ID AS StudentsID, dbo.ISStudent.StudentNo, dbo.ISStudent.ClassID, dbo.ISStudent.SchoolID, dbo.ISStudent.StudentName, dbo.ISStudent.Photo, dbo.ISStudent.ParantName1, 
                         dbo.ISStudent.ParantEmail1, dbo.ISStudent.ParantName2, dbo.ISStudent.ParantEmail2, dbo.ISStudent.Active AS StudentActive, dbo.ISStudent.Deleted AS StudentDeleted, dbo.ISStudent.StartDate, dbo.ISStudent.EndDate
FROM            dbo.ISStudent LEFT JOIN
                         (select * from dbo.ISAttendance AA where  datepart(DAY,AA.Date)=datepart(DAY,@date) and datepart(MONTH,AA.Date)=datepart(MONTH,@date) and datepart(YEAR,AA.Date)=datepart(YEAR,@date)) A ON dbo.ISStudent.ID = A.StudentID 
						 where datepart(DAY,@date) <= datepart(DAY,ISStudent.DeletedDateTime) and datepart(MONTH,@date) <= datepart(MONTH,ISStudent.DeletedDateTime) and datepart(YEAR,@date) <= datepart(YEAR,ISStudent.DeletedDateTime) or ISStudent.DeletedDateTime IS NULL
END



GO
/****** Object:  StoredProcedure [dbo].[getClassReport]    Script Date: 20/03/2020 1:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getClassReport] 
	@ClassID int null
AS
BEGIN
SET NOCOUNT ON;

	SELECT        dbo.ISStudent.ID AS StudID, dbo.ISStudent.StudentNo, dbo.ISStudent.ClassID, dbo.ISStudent.SchoolID, dbo.ISStudent.StudentName, dbo.ISStudent.Photo, dbo.ISStudent.ParantName1, dbo.ISStudent.ParantEmail1, 
                         dbo.ISStudent.ParantName2, dbo.ISStudent.ParantEmail2, dbo.ISStudent.Active, dbo.ISStudent.Deleted, P.ID, P.StudentID, P.TeacherID, P.PickerID, 
                         P.PickTime, P.PickDate, P.PickStatus
FROM            dbo.ISStudent LEFT JOIN
                         (select * from dbo.ISPickup AA) P ON dbo.ISStudent.ID = P.StudentID where dbo.ISStudent.ClassID = @ClassID
END



GO
/****** Object:  StoredProcedure [dbo].[getPickUpData]    Script Date: 20/03/2020 1:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getPickUpData]
	@date datetime null
AS
BEGIN
	
	SET NOCOUNT ON;


	if(@date is null)
	begin
	set @date=GETDATE()
	end


   SELECT        dbo.ISStudent.ID AS StudID, dbo.ISStudent.StudentNo, dbo.ISStudent.ClassID, dbo.ISStudent.SchoolID, dbo.ISStudent.StudentName, dbo.ISStudent.Photo, dbo.ISStudent.ParantName1, dbo.ISStudent.ParantEmail1, 
                         dbo.ISStudent.ParantName2, dbo.ISStudent.ParantEmail2, dbo.ISStudent.ParantPhone1, dbo.ISStudent.ParantPhone2, dbo.ISStudent.StartDate, dbo.ISStudent.Active, dbo.ISStudent.Deleted, P.ID, P.StudentID, P.TeacherID, P.PickerID, 
                         P.PickTime, P.PickDate, P.PickStatus
FROM            dbo.ISStudent LEFT JOIN
                         (select * from dbo.ISPickup AA where  datepart(DAY,AA.PickDate)=datepart(DAY,@date) and datepart(MONTH,AA.PickDate)=datepart(MONTH,@date) and datepart(YEAR,AA.PickDate)=datepart(YEAR,@date)) P ON dbo.ISStudent.ID = P.StudentID 
						 where datepart(DAY,@date) <= datepart(DAY,ISStudent.DeletedDateTime) and datepart(MONTH,@date) <= datepart(MONTH,ISStudent.DeletedDateTime) and datepart(YEAR,@date) <= datepart(YEAR,ISStudent.DeletedDateTime) or ISStudent.DeletedDateTime IS NULL
END




GO
/****** Object:  StoredProcedure [dbo].[getStudentReport]    Script Date: 20/03/2020 1:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getStudentReport]
	@ParantEmail1 nvarchar(200) null,
	@ParantEmail2 nvarchar(200) null
AS
BEGIN
	SELECT        dbo.ISStudent.ID AS StudID, dbo.ISStudent.StudentNo, dbo.ISStudent.ClassID, dbo.ISStudent.SchoolID, dbo.ISStudent.StudentName, dbo.ISStudent.Photo, dbo.ISStudent.ParantName1, dbo.ISStudent.ParantEmail1, 
                         dbo.ISStudent.ParantName2, dbo.ISStudent.ParantEmail2, dbo.ISStudent.Active, dbo.ISStudent.Deleted, P.ID, P.StudentID, P.TeacherID, P.PickerID, 
                         P.PickTime, P.PickDate, P.PickStatus
FROM            dbo.ISStudent LEFT JOIN
                         (select * from dbo.ISPickup AA) P ON dbo.ISStudent.ID = P.StudentID where dbo.ISStudent.ParantEmail1 = @ParantEmail1 and dbo.ISStudent.ParantEmail2 = @ParantEmail2
END



GO
/****** Object:  StoredProcedure [dbo].[getTeacherPickUpData]    Script Date: 20/03/2020 1:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[getTeacherPickUpData]
	@fromdate datetime null,
	@todate datetime null
AS
BEGIN
	
	SET NOCOUNT ON;


	if(@fromdate is null)
	begin
	set @fromdate=GETDATE()
	end
	if(@todate is null)
	begin
	set @todate=GETDATE()
	end

   SELECT        dbo.ISStudent.ID AS StudID, dbo.ISStudent.StudentNo, dbo.ISStudent.ClassID, dbo.ISStudent.SchoolID, dbo.ISStudent.StudentName, dbo.ISStudent.Photo, dbo.ISStudent.ParantName1, dbo.ISStudent.ParantEmail1, 
                         dbo.ISStudent.ParantName2, dbo.ISStudent.ParantEmail2, dbo.ISStudent.StartDate, dbo.ISStudent.Active, dbo.ISStudent.Deleted, P.ID, P.StudentID, P.TeacherID, P.PickerID, 
                         P.PickTime, P.PickDate, P.PickStatus
FROM            dbo.ISStudent LEFT JOIN
                         (select * from dbo.ISPickup AA where  cast(AA.PickDate as date)>=cast(@fromdate as date) and cast(AA.PickDate as date)<=cast(@todate as date)) P ON dbo.ISStudent.ID = P.StudentID 

						 where datepart(DAY,@todate) <= datepart(DAY,ISStudent.DeletedDateTime) 
						 and datepart(MONTH,@todate) <= datepart(MONTH,ISStudent.DeletedDateTime) 
						 and datepart(YEAR,@todate) <= datepart(YEAR,ISStudent.DeletedDateTime) or ISStudent.DeletedDateTime IS NULL
END

GO

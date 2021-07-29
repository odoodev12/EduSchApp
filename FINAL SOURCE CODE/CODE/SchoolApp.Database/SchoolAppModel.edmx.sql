
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/24/2020 21:00:37
-- Generated from EDMX file: F:\FINAL SOURCE CODE_ola\New\FINAL SOURCE CODE_ola\FINAL SOURCE CODE\CODE\SchoolApp.Database\SchoolAppModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SchoolApp_Live];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__ISAttenda__Stude__5165187F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISAttendance] DROP CONSTRAINT [FK__ISAttenda__Stude__5165187F];
GO
IF OBJECT_ID(N'[dbo].[FK__ISAttenda__Teach__52593CB8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISAttendance] DROP CONSTRAINT [FK__ISAttenda__Teach__52593CB8];
GO
IF OBJECT_ID(N'[dbo].[FK__ISCompleteAttendanceRun__ISClass]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISCompleteAttendanceRun] DROP CONSTRAINT [FK__ISCompleteAttendanceRun__ISClass];
GO
IF OBJECT_ID(N'[dbo].[FK__ISCompleteAttendanceRun__ISTeacher]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISCompleteAttendanceRun] DROP CONSTRAINT [FK__ISCompleteAttendanceRun__ISTeacher];
GO
IF OBJECT_ID(N'[dbo].[FK__ISCompletePickupRun__ISClass]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISCompletePickupRun] DROP CONSTRAINT [FK__ISCompletePickupRun__ISClass];
GO
IF OBJECT_ID(N'[dbo].[FK__ISCompletePickupRun__ISTeacher]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISCompletePickupRun] DROP CONSTRAINT [FK__ISCompletePickupRun__ISTeacher];
GO
IF OBJECT_ID(N'[dbo].[FK__ISHoliday__Schoo__5CD6CB2B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISHoliday] DROP CONSTRAINT [FK__ISHoliday__Schoo__5CD6CB2B];
GO
IF OBJECT_ID(N'[dbo].[FK__ISOrganis__Count__17F790F9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISOrganisationUser] DROP CONSTRAINT [FK__ISOrganis__Count__17F790F9];
GO
IF OBJECT_ID(N'[dbo].[FK__ISPicker__Parent__671F4F74]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISPicker] DROP CONSTRAINT [FK__ISPicker__Parent__671F4F74];
GO
IF OBJECT_ID(N'[dbo].[FK__ISPicker__School__1AD3FDA4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISPicker] DROP CONSTRAINT [FK__ISPicker__School__1AD3FDA4];
GO
IF OBJECT_ID(N'[dbo].[FK__ISPicker__School__1BC821DD]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISPicker] DROP CONSTRAINT [FK__ISPicker__School__1BC821DD];
GO
IF OBJECT_ID(N'[dbo].[FK__ISPicker__Studen__662B2B3B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISPicker] DROP CONSTRAINT [FK__ISPicker__Studen__662B2B3B];
GO
IF OBJECT_ID(N'[dbo].[FK__ISPickup__Picker__5BAD9CC8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISPickup] DROP CONSTRAINT [FK__ISPickup__Picker__5BAD9CC8];
GO
IF OBJECT_ID(N'[dbo].[FK__ISPickup__Studen__59C55456]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISPickup] DROP CONSTRAINT [FK__ISPickup__Studen__59C55456];
GO
IF OBJECT_ID(N'[dbo].[FK__ISPickup__Teache__5AB9788F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISPickup] DROP CONSTRAINT [FK__ISPickup__Teache__5AB9788F];
GO
IF OBJECT_ID(N'[dbo].[FK__ISSchool__Accoun__70A8B9AE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISSchool] DROP CONSTRAINT [FK__ISSchool__Accoun__70A8B9AE];
GO
IF OBJECT_ID(N'[dbo].[FK__ISSchool__Billin__2180FB33]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISSchool] DROP CONSTRAINT [FK__ISSchool__Billin__2180FB33];
GO
IF OBJECT_ID(N'[dbo].[FK__ISStudent__Class__65370702]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISStudent] DROP CONSTRAINT [FK__ISStudent__Class__65370702];
GO
IF OBJECT_ID(N'[dbo].[FK__ISStudent__Schoo__6442E2C9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISStudent] DROP CONSTRAINT [FK__ISStudent__Schoo__6442E2C9];
GO
IF OBJECT_ID(N'[dbo].[FK__ISSupport__LogTy__6477ECF3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISSupport] DROP CONSTRAINT [FK__ISSupport__LogTy__6477ECF3];
GO
IF OBJECT_ID(N'[dbo].[FK__ISTeacher__Class__0E391C95]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISTeacherClassAssignment] DROP CONSTRAINT [FK__ISTeacher__Class__0E391C95];
GO
IF OBJECT_ID(N'[dbo].[FK__ISTeacher__RoleI__6754599E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISTeacher] DROP CONSTRAINT [FK__ISTeacher__RoleI__6754599E];
GO
IF OBJECT_ID(N'[dbo].[FK__ISTeacher__Schoo__46B27FE2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISTeacher] DROP CONSTRAINT [FK__ISTeacher__Schoo__46B27FE2];
GO
IF OBJECT_ID(N'[dbo].[FK__ISTeacher__Teach__6A30C649]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISTeacherClassAssignment] DROP CONSTRAINT [FK__ISTeacher__Teach__6A30C649];
GO
IF OBJECT_ID(N'[dbo].[FK__ISUserAct__Schoo__2704CA5F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISUserActivity] DROP CONSTRAINT [FK__ISUserAct__Schoo__2704CA5F];
GO
IF OBJECT_ID(N'[dbo].[FK_ISClass_ISClassType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISClass] DROP CONSTRAINT [FK_ISClass_ISClassType];
GO
IF OBJECT_ID(N'[dbo].[FK_ISOrganisationUser_ISRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISOrganisationUser] DROP CONSTRAINT [FK_ISOrganisationUser_ISRole];
GO
IF OBJECT_ID(N'[dbo].[FK_ISSchool_ISSchool]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISSchool] DROP CONSTRAINT [FK_ISSchool_ISSchool];
GO
IF OBJECT_ID(N'[dbo].[FK_ISSchool_ISSchoolType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISSchool] DROP CONSTRAINT [FK_ISSchool_ISSchoolType];
GO
IF OBJECT_ID(N'[dbo].[FK_ISSchoolInvoice_ISSchool]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISSchoolInvoice] DROP CONSTRAINT [FK_ISSchoolInvoice_ISSchool];
GO
IF OBJECT_ID(N'[dbo].[FK_ISSchoolInvoice_ISTrasectionStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISSchoolInvoice] DROP CONSTRAINT [FK_ISSchoolInvoice_ISTrasectionStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_ISSchoolInvoice_ISTrasectionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISSchoolInvoice] DROP CONSTRAINT [FK_ISSchoolInvoice_ISTrasectionType];
GO
IF OBJECT_ID(N'[dbo].[FK_ISSupport_ISSupportStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISSupport] DROP CONSTRAINT [FK_ISSupport_ISSupportStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_ISTicketMessage_ISSupport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISTicketMessage] DROP CONSTRAINT [FK_ISTicketMessage_ISSupport];
GO
IF OBJECT_ID(N'[dbo].[FK_ISUserRole_ISSchool]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ISUserRole] DROP CONSTRAINT [FK_ISUserRole_ISSchool];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ISAccountStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISAccountStatus];
GO
IF OBJECT_ID(N'[dbo].[ISAdminLogin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISAdminLogin];
GO
IF OBJECT_ID(N'[dbo].[ISAttendance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISAttendance];
GO
IF OBJECT_ID(N'[dbo].[ISAuthToken]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISAuthToken];
GO
IF OBJECT_ID(N'[dbo].[ISClass]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISClass];
GO
IF OBJECT_ID(N'[dbo].[ISClassType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISClassType];
GO
IF OBJECT_ID(N'[dbo].[ISClassYear]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISClassYear];
GO
IF OBJECT_ID(N'[dbo].[ISCompleteAttendanceRun]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISCompleteAttendanceRun];
GO
IF OBJECT_ID(N'[dbo].[ISCompletePickupRun]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISCompletePickupRun];
GO
IF OBJECT_ID(N'[dbo].[ISCountries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISCountries];
GO
IF OBJECT_ID(N'[dbo].[ISDataUploadHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISDataUploadHistory];
GO
IF OBJECT_ID(N'[dbo].[ISErrorLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISErrorLog];
GO
IF OBJECT_ID(N'[dbo].[ISFAQ]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISFAQ];
GO
IF OBJECT_ID(N'[dbo].[ISHoliday]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISHoliday];
GO
IF OBJECT_ID(N'[dbo].[ISInvoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISInvoice];
GO
IF OBJECT_ID(N'[dbo].[ISLogType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISLogType];
GO
IF OBJECT_ID(N'[dbo].[ISMessage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISMessage];
GO
IF OBJECT_ID(N'[dbo].[ISOrganisationUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISOrganisationUser];
GO
IF OBJECT_ID(N'[dbo].[ISPayment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISPayment];
GO
IF OBJECT_ID(N'[dbo].[ISPicker]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISPicker];
GO
IF OBJECT_ID(N'[dbo].[ISPickerAssignment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISPickerAssignment];
GO
IF OBJECT_ID(N'[dbo].[ISPickup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISPickup];
GO
IF OBJECT_ID(N'[dbo].[ISPickUpMessage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISPickUpMessage];
GO
IF OBJECT_ID(N'[dbo].[ISPickUpStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISPickUpStatus];
GO
IF OBJECT_ID(N'[dbo].[ISReport]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISReport];
GO
IF OBJECT_ID(N'[dbo].[ISRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISRole];
GO
IF OBJECT_ID(N'[dbo].[ISSchool]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISSchool];
GO
IF OBJECT_ID(N'[dbo].[ISSchoolClass]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISSchoolClass];
GO
IF OBJECT_ID(N'[dbo].[ISSchoolInvoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISSchoolInvoice];
GO
IF OBJECT_ID(N'[dbo].[ISSchoolType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISSchoolType];
GO
IF OBJECT_ID(N'[dbo].[ISStudent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISStudent];
GO
IF OBJECT_ID(N'[dbo].[ISStudentHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISStudentHistory];
GO
IF OBJECT_ID(N'[dbo].[ISStudentReassignHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISStudentReassignHistory];
GO
IF OBJECT_ID(N'[dbo].[ISSupport]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISSupport];
GO
IF OBJECT_ID(N'[dbo].[ISSupportStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISSupportStatus];
GO
IF OBJECT_ID(N'[dbo].[ISTeacher]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISTeacher];
GO
IF OBJECT_ID(N'[dbo].[ISTeacherClassAssignment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISTeacherClassAssignment];
GO
IF OBJECT_ID(N'[dbo].[ISTeacherReassignHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISTeacherReassignHistory];
GO
IF OBJECT_ID(N'[dbo].[ISTicketMessage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISTicketMessage];
GO
IF OBJECT_ID(N'[dbo].[ISTrasectionStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISTrasectionStatus];
GO
IF OBJECT_ID(N'[dbo].[ISTrasectionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISTrasectionType];
GO
IF OBJECT_ID(N'[dbo].[ISUserActivity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISUserActivity];
GO
IF OBJECT_ID(N'[dbo].[ISUserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ISUserRole];
GO
IF OBJECT_ID(N'[SchoolAppModelStoreContainer].[ViewNotAssignPicker]', 'U') IS NOT NULL
    DROP TABLE [SchoolAppModelStoreContainer].[ViewNotAssignPicker];
GO
IF OBJECT_ID(N'[SchoolAppModelStoreContainer].[ViewStudentAttendence]', 'U') IS NOT NULL
    DROP TABLE [SchoolAppModelStoreContainer].[ViewStudentAttendence];
GO
IF OBJECT_ID(N'[SchoolAppModelStoreContainer].[ViewStudentPickUp]', 'U') IS NOT NULL
    DROP TABLE [SchoolAppModelStoreContainer].[ViewStudentPickUp];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ISAccountStatus'
CREATE TABLE [dbo].[ISAccountStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(150)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISAdminLogins'
CREATE TABLE [dbo].[ISAdminLogins] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(255)  NOT NULL,
    [Pass] varchar(255)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [FullName] nvarchar(500)  NULL
);
GO

-- Creating table 'ISAttendances'
CREATE TABLE [dbo].[ISAttendances] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [StudentID] int  NULL,
    [TeacherID] int  NULL,
    [Status] varchar(255)  NULL,
    [Time] datetime  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [AttendenceComplete] bit  NULL
);
GO

-- Creating table 'ISAuthTokens'
CREATE TABLE [dbo].[ISAuthTokens] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Token] nvarchar(50)  NULL,
    [IMEINO] nvarchar(50)  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDatetime] datetime  NULL
);
GO

-- Creating table 'ISClasses'
CREATE TABLE [dbo].[ISClasses] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Year] varchar(255)  NULL,
    [TypeID] int  NOT NULL,
    [AfterSchoolType] varchar(255)  NULL,
    [ExternalOrganisation] varchar(255)  NULL,
    [EndDate] datetime  NULL,
    [PickupComplete] bit  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [CreatedByType] int  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [ISNonListed] bit  NULL
);
GO

-- Creating table 'ISClassTypes'
CREATE TABLE [dbo].[ISClassTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(150)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISClassYears'
CREATE TABLE [dbo].[ISClassYears] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Year] varchar(150)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISCompleteAttendanceRuns'
CREATE TABLE [dbo].[ISCompleteAttendanceRuns] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [ClassID] int  NULL,
    [TeacherID] int  NULL,
    [Time] datetime  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISCompletePickupRuns'
CREATE TABLE [dbo].[ISCompletePickupRuns] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [ClassID] int  NULL,
    [TeacherID] int  NULL,
    [Time] datetime  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISCountries'
CREATE TABLE [dbo].[ISCountries] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(150)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISDataUploadHistories'
CREATE TABLE [dbo].[ISDataUploadHistories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NULL,
    [TemplateName] nvarchar(500)  NULL,
    [CreatedCount] int  NULL,
    [UpdatedCount] int  NULL,
    [Date] datetime  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedByType] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISErrorLogs'
CREATE TABLE [dbo].[ISErrorLogs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [LogText] varchar(max)  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [Deleted] bit  NULL,
    [FileName] varchar(max)  NULL,
    [LineNumber] varchar(10)  NULL,
    [ColumnNumber] varchar(10)  NULL,
    [Method] varchar(1000)  NULL,
    [Class] varchar(1000)  NULL
);
GO

-- Creating table 'ISFAQs'
CREATE TABLE [dbo].[ISFAQs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Question] nvarchar(max)  NULL,
    [Attachment] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedByName] nvarchar(1)  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISHolidays'
CREATE TABLE [dbo].[ISHolidays] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NULL,
    [Name] nvarchar(100)  NOT NULL,
    [DateFrom] datetime  NULL,
    [DateTo] datetime  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISInvoices'
CREATE TABLE [dbo].[ISInvoices] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PropertyID] int  NULL,
    [InvoiceNo] varchar(255)  NULL,
    [DateFrom] datetime  NULL,
    [DateTo] datetime  NULL,
    [TransactionDesc] varchar(255)  NULL,
    [TransactionAmount] int  NULL,
    [TaxRate] decimal(18,2)  NULL,
    [StatusID] int  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISLogTypes'
CREATE TABLE [dbo].[ISLogTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [LogTypeName] varchar(150)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISMessages'
CREATE TABLE [dbo].[ISMessages] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(1000)  NULL,
    [Attechment] varchar(255)  NULL,
    [Desc] nvarchar(max)  NULL,
    [ReceiveID] int  NULL,
    [SendID] int  NULL,
    [ReceiverType] int  NULL,
    [SenderType] int  NULL,
    [SchoolID] int  NULL,
    [Time] datetime  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISOrganisationUsers'
CREATE TABLE [dbo].[ISOrganisationUsers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] varchar(255)  NULL,
    [LastName] varchar(255)  NULL,
    [OrgCode] varchar(255)  NULL,
    [Photo] varchar(max)  NULL,
    [Address1] nvarchar(max)  NULL,
    [Address2] nvarchar(max)  NULL,
    [Town] varchar(255)  NULL,
    [CountryID] int  NULL,
    [Email] varchar(255)  NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [RoleID] int  NULL,
    [Password] varchar(50)  NULL,
    [StatusID] varchar(255)  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [CreatedByName] nvarchar(500)  NULL,
    [ActivationBy] nvarchar(500)  NULL,
    [ActivationDate] datetime  NULL,
    [LastUpdatedBy] nvarchar(500)  NULL
);
GO

-- Creating table 'ISPayments'
CREATE TABLE [dbo].[ISPayments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] varchar(255)  NOT NULL,
    [InvoiceID] varchar(255)  NOT NULL,
    [Amount] varchar(255)  NOT NULL,
    [TransactionTypeID] varchar(255)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [StatusID] varchar(255)  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL
);
GO

-- Creating table 'ISPickers'
CREATE TABLE [dbo].[ISPickers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NULL,
    [ParentID] int  NULL,
    [StudentID] int  NULL,
    [Title] varchar(255)  NULL,
    [FirstName] varchar(255)  NULL,
    [LastName] varchar(255)  NULL,
    [Photo] varchar(255)  NULL,
    [Email] varchar(255)  NULL,
    [Phone] varchar(255)  NULL,
    [PickupCodeCunter] varchar(255)  NULL,
    [OneOffPickerFlag] bit  NULL,
    [EndDate] datetime  NULL,
    [ActiveStatus] varchar(255)  NULL,
    [ActiveStatusLastUpdatedDate] datetime  NULL,
    [ActiveStatusLastUpdatedParent] varchar(255)  NULL,
    [ActiveStatusLastUpdatedParentEmail] varchar(255)  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [PickerType] int  NULL,
    [CreatedByEmail] nvarchar(500)  NULL,
    [EmergencyContact] bit  NULL,
    [CreatedByName] nvarchar(500)  NULL
);
GO

-- Creating table 'ISPickerAssignments'
CREATE TABLE [dbo].[ISPickerAssignments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PickerId] int  NULL,
    [StudentID] int  NULL,
    [PickerCode] varchar(255)  NULL,
    [PickCodeExDate] datetime  NULL,
    [PickCodeLastUpdateDate] datetime  NULL,
    [PickCodayExDate] datetime  NULL,
    [PickTodayLastUpdateDate] datetime  NULL,
    [PickTodayLastUpdateParent] varchar(255)  NULL,
    [RemoveChildStatus] int  NULL,
    [RemoveChildLastUpdateDate] datetime  NULL,
    [RemoveChildLastupdateParent] varchar(255)  NULL,
    [StudentPickAssignFlag] int  NULL,
    [StudentPickAssignLastUpdateParent] varchar(255)  NULL,
    [StudentPickAssignDate] datetime  NULL,
    [StudentAssignBy] nvarchar(200)  NULL
);
GO

-- Creating table 'ISPickups'
CREATE TABLE [dbo].[ISPickups] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StudentID] int  NULL,
    [ClassID] int  NULL,
    [TeacherID] int  NULL,
    [PickerID] int  NULL,
    [PickTime] datetime  NULL,
    [PickDate] datetime  NULL,
    [PickStatus] varchar(50)  NULL,
    [CompletePickup] bit  NULL,
    [OfficeFlag] bit  NULL,
    [AfterSchoolFlag] bit  NULL
);
GO

-- Creating table 'ISPickUpMessages'
CREATE TABLE [dbo].[ISPickUpMessages] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NULL,
    [Message] varchar(1000)  NULL,
    [ReceiverID] int  NULL,
    [ClassID] int  NULL,
    [SendID] int  NULL,
    [SenderName] varchar(200)  NULL,
    [Viewed] bit  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISPickUpStatus'
CREATE TABLE [dbo].[ISPickUpStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Status] varchar(255)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISReports'
CREATE TABLE [dbo].[ISReports] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(1000)  NULL,
    [URL] varchar(max)  NULL,
    [ReportTypeID] int  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISRoles'
CREATE TABLE [dbo].[ISRoles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(1000)  NOT NULL,
    [SchoolID] int  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISSchools'
CREATE TABLE [dbo].[ISSchools] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CustomerNumber] varchar(255)  NULL,
    [Name] varchar(255)  NULL,
    [Number] varchar(255)  NULL,
    [TypeID] int  NULL,
    [Address1] nvarchar(max)  NULL,
    [Address2] nvarchar(max)  NULL,
    [Town] varchar(255)  NULL,
    [CountryID] int  NULL,
    [Logo] varchar(max)  NULL,
    [AdminFirstName] varchar(255)  NULL,
    [AdminLastName] varchar(255)  NULL,
    [AdminEmail] varchar(255)  NULL,
    [Password] varchar(255)  NULL,
    [PhoneNumber] varchar(255)  NULL,
    [Website] varchar(255)  NULL,
    [SupervisorFirstname] varchar(255)  NULL,
    [SupervisorLastname] varchar(255)  NULL,
    [SupervisorEmail] varchar(255)  NULL,
    [OpningTime] datetime  NULL,
    [ClosingTime] datetime  NULL,
    [LateMinAfterClosing] varchar(50)  NULL,
    [ChargeMinutesAfterClosing] varchar(255)  NULL,
    [ReportableMinutesAfterClosing] varchar(50)  NULL,
    [SetupTrainingStatus] bit  NULL,
    [SetupTrainingDate] datetime  NULL,
    [ActivationDate] datetime  NULL,
    [SchoolEndDate] datetime  NULL,
    [isAttendanceModule] bit  NULL,
    [isNotificationPickup] bit  NULL,
    [NotificationAttendance] bit  NULL,
    [AttendanceModule] varchar(255)  NULL,
    [PostCode] varchar(255)  NULL,
    [BillingAddress] nvarchar(max)  NULL,
    [BillingAddress2] nvarchar(max)  NULL,
    [BillingPostCode] varchar(255)  NULL,
    [BillingCountryID] int  NULL,
    [BillingTown] varchar(255)  NULL,
    [Classfile] varchar(max)  NULL,
    [Teacherfile] varchar(max)  NULL,
    [Studentfile] varchar(max)  NULL,
    [Reportable] bit  NULL,
    [PaymentSystems] bit  NULL,
    [CustSigned] bit  NULL,
    [AccountStatusID] int  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [CreatedByName] nvarchar(500)  NULL,
    [LastUpdatedBy] nvarchar(500)  NULL,
    [ActivatedBy] nvarchar(500)  NULL
);
GO

-- Creating table 'ISSchoolClasses'
CREATE TABLE [dbo].[ISSchoolClasses] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NOT NULL,
    [ClassID] int  NOT NULL
);
GO

-- Creating table 'ISSchoolInvoices'
CREATE TABLE [dbo].[ISSchoolInvoices] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NULL,
    [InvoiceNo] varchar(255)  NULL,
    [DateFrom] datetime  NULL,
    [DateTo] datetime  NULL,
    [TransactionTypeID] int  NULL,
    [TransactionDesc] varchar(255)  NULL,
    [TransactionAmount] int  NULL,
    [TaxRate] decimal(18,2)  NULL,
    [StatusID] int  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [StatusUpdateBy] nvarchar(200)  NULL,
    [StatusUpdateDate] datetime  NULL,
    [CreatedByName] nvarchar(500)  NULL
);
GO

-- Creating table 'ISSchoolTypes'
CREATE TABLE [dbo].[ISSchoolTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(150)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISStudents'
CREATE TABLE [dbo].[ISStudents] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StudentNo] varchar(250)  NULL,
    [ClassID] int  NULL,
    [SchoolID] int  NULL,
    [StudentName] varchar(255)  NULL,
    [Photo] varchar(255)  NULL,
    [DOB] datetime  NULL,
    [ParantName1] varchar(255)  NULL,
    [ParantEmail1] varchar(50)  NULL,
    [ParantPassword1] varchar(255)  NULL,
    [ParantPhone1] varchar(11)  NULL,
    [ParantRelation1] varchar(255)  NULL,
    [ParantPhoto1] varchar(255)  NULL,
    [ParantName2] varchar(255)  NULL,
    [ParantEmail2] varchar(50)  NULL,
    [ParantPassword2] varchar(255)  NULL,
    [ParantPhone2] varchar(11)  NULL,
    [ParantRelation2] varchar(255)  NULL,
    [ParantPhoto2] varchar(255)  NULL,
    [PickupMessageID] varchar(255)  NULL,
    [PickupMessageTime] time  NULL,
    [PickupMessageDate] datetime  NULL,
    [PickupMessageLastUpdatedParent] varchar(50)  NULL,
    [AttendanceEmailFlag] varchar(50)  NULL,
    [LastAttendanceEmailDate] datetime  NULL,
    [LastAttendanceEmailTime] time  NULL,
    [LastUnpickAlertSentTime] time  NULL,
    [UnpickAlertDate] datetime  NULL,
    [LastUnpickAlertSentTeacherID] int  NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Out] int  NULL,
    [Outbit] bit  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [EmailAfterConfirmAttendence] bit  NULL,
    [EmailAfterConfirmPickUp] bit  NULL,
    [ISImageEmail] bit  NULL,
    [SentMailDate] datetime  NULL
);
GO

-- Creating table 'ISStudentHistories'
CREATE TABLE [dbo].[ISStudentHistories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StudentNo] varchar(250)  NULL,
    [ClassID] int  NULL,
    [SchoolID] int  NULL,
    [StudentName] varchar(255)  NULL,
    [Photo] varchar(255)  NULL,
    [DOB] datetime  NULL,
    [ParantName1] varchar(255)  NULL,
    [ParantEmail1] varchar(50)  NULL,
    [ParantPassword1] varchar(255)  NULL,
    [ParantPhone1] varchar(11)  NULL,
    [ParantRelation1] varchar(255)  NULL,
    [ParantPhoto1] varchar(255)  NULL,
    [ParantName2] varchar(255)  NULL,
    [ParantEmail2] varchar(50)  NULL,
    [ParantPassword2] varchar(255)  NULL,
    [ParantPhone2] varchar(11)  NULL,
    [ParantRelation2] varchar(255)  NULL,
    [ParantPhoto2] varchar(255)  NULL,
    [PickupMessageID] varchar(255)  NULL,
    [PickupMessageTime] time  NULL,
    [PickupMessageDate] datetime  NULL,
    [PickupMessageLastUpdatedParent] varchar(50)  NULL,
    [AttendanceEmailFlag] varchar(50)  NULL,
    [LastAttendanceEmailDate] datetime  NULL,
    [LastAttendanceEmailTime] time  NULL,
    [LastUnpickAlertSentTime] time  NULL,
    [UnpickAlertDate] datetime  NULL,
    [LastUnpickAlertSentTeacherID] int  NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Out] int  NULL,
    [Outbit] bit  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [EmailAfterConfirmAttendence] bit  NULL,
    [EmailAfterConfirmPickUp] bit  NULL
);
GO

-- Creating table 'ISStudentReassignHistories'
CREATE TABLE [dbo].[ISStudentReassignHistories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NULL,
    [FromClass] int  NULL,
    [ToClass] int  NULL,
    [StduentID] int  NULL,
    [Date] datetime  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [CreatedByType] int  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISSupports'
CREATE TABLE [dbo].[ISSupports] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TicketNo] varchar(255)  NOT NULL,
    [Request] varchar(max)  NULL,
    [SchoolID] int  NULL,
    [StatusID] int  NULL,
    [LogTypeID] int  NULL,
    [SupportOfficerID] int  NULL,
    [Priority] int  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedByName] nvarchar(500)  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [AssignBy] nvarchar(500)  NULL,
    [AssignDate] datetime  NULL
);
GO

-- Creating table 'ISSupportStatus'
CREATE TABLE [dbo].[ISSupportStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(150)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISTeachers'
CREATE TABLE [dbo].[ISTeachers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NOT NULL,
    [ClassID] int  NULL,
    [RoleID] int  NOT NULL,
    [TeacherNo] varchar(255)  NULL,
    [Title] varchar(255)  NULL,
    [Name] varchar(255)  NOT NULL,
    [PhoneNo] varchar(15)  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [EndDate] datetime  NULL,
    [Photo] varchar(max)  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [CreatedByType] int  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [Role] int  NULL
);
GO

-- Creating table 'ISTeacherClassAssignments'
CREATE TABLE [dbo].[ISTeacherClassAssignments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TeacherID] int  NULL,
    [ClassID] int  NULL,
    [Out] int  NULL,
    [Outbit] bit  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISTeacherReassignHistories'
CREATE TABLE [dbo].[ISTeacherReassignHistories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NULL,
    [FromClass] int  NULL,
    [ToClass] int  NULL,
    [TeacherID] int  NULL,
    [Date] datetime  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [CreatedByType] int  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISTicketMessages'
CREATE TABLE [dbo].[ISTicketMessages] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SupportID] int  NULL,
    [SenderID] int  NULL,
    [Message] nvarchar(max)  NULL,
    [SelectFile] varchar(max)  NULL,
    [CreatedDatetime] datetime  NULL,
    [UserTypeID] int  NULL
);
GO

-- Creating table 'ISTrasectionStatus'
CREATE TABLE [dbo].[ISTrasectionStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(150)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISTrasectionTypes'
CREATE TABLE [dbo].[ISTrasectionTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(150)  NOT NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ISUserActivities'
CREATE TABLE [dbo].[ISUserActivities] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NULL,
    [UserName] varchar(200)  NULL,
    [LogText] varchar(max)  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [DocumentCategory] nvarchar(500)  NULL
);
GO

-- Creating table 'ISUserRoles'
CREATE TABLE [dbo].[ISUserRoles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NOT NULL,
    [RoleName] varchar(255)  NOT NULL,
    [RoleType] int  NULL,
    [ManageTeacherFlag] bit  NOT NULL,
    [ManageClassFlag] bit  NOT NULL,
    [ManageSupportFlag] bit  NOT NULL,
    [ManageStudentFlag] bit  NOT NULL,
    [ManageViewAccountFlag] bit  NOT NULL,
    [ManageNonTeacherFlag] bit  NULL,
    [ManageHolidayFlag] bit  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ViewNotAssignPickers'
CREATE TABLE [dbo].[ViewNotAssignPickers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SchoolID] int  NULL,
    [ParentID] int  NULL,
    [StudentID] int  NULL,
    [Title] varchar(255)  NULL,
    [FirstName] varchar(255)  NULL,
    [LastName] varchar(255)  NULL,
    [Photo] varchar(255)  NULL,
    [Email] varchar(255)  NULL,
    [Phone] varchar(255)  NULL,
    [PickupCodeCunter] varchar(255)  NULL,
    [OneOffPickerFlag] bit  NULL,
    [EndDate] datetime  NULL,
    [ActiveStatus] varchar(255)  NULL,
    [ActiveStatusLastUpdatedDate] datetime  NULL,
    [ActiveStatusLastUpdatedParent] varchar(255)  NULL,
    [ActiveStatusLastUpdatedParentEmail] varchar(255)  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL
);
GO

-- Creating table 'ViewStudentAttendences'
CREATE TABLE [dbo].[ViewStudentAttendences] (
    [ID] int  NULL,
    [Date] datetime  NULL,
    [StudentID] int  NULL,
    [TeacherID] int  NULL,
    [Status] varchar(255)  NULL,
    [Time] datetime  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [CreatedBy] int  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifyBy] int  NULL,
    [ModifyDateTime] datetime  NULL,
    [DeletedBy] int  NULL,
    [DeletedDateTime] datetime  NULL,
    [StudentsID] int  NOT NULL,
    [StudentNo] varchar(250)  NULL,
    [ClassID] int  NULL,
    [SchoolID] int  NULL,
    [StudentName] varchar(255)  NULL,
    [Photo] varchar(255)  NULL,
    [ParantName1] varchar(255)  NULL,
    [ParantEmail1] varchar(50)  NULL,
    [ParantName2] varchar(255)  NULL,
    [ParantEmail2] varchar(50)  NULL,
    [StudentActive] bit  NULL,
    [StudentDeleted] bit  NULL
);
GO

-- Creating table 'ViewStudentPickUps'
CREATE TABLE [dbo].[ViewStudentPickUps] (
    [StudID] int  NOT NULL,
    [StudentNo] varchar(250)  NULL,
    [ClassID] int  NULL,
    [SchoolID] int  NULL,
    [StudentName] varchar(255)  NULL,
    [Photo] varchar(255)  NULL,
    [ParantName1] varchar(255)  NULL,
    [ParantEmail1] varchar(50)  NULL,
    [ParantName2] varchar(255)  NULL,
    [ParantEmail2] varchar(50)  NULL,
    [Active] bit  NULL,
    [Deleted] bit  NULL,
    [ID] int  NULL,
    [StudentID] int  NULL,
    [TeacherID] int  NULL,
    [PickerID] int  NULL,
    [PickTime] datetime  NULL,
    [PickDate] datetime  NULL,
    [PickStatus] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'ISAccountStatus'
ALTER TABLE [dbo].[ISAccountStatus]
ADD CONSTRAINT [PK_ISAccountStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISAdminLogins'
ALTER TABLE [dbo].[ISAdminLogins]
ADD CONSTRAINT [PK_ISAdminLogins]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISAttendances'
ALTER TABLE [dbo].[ISAttendances]
ADD CONSTRAINT [PK_ISAttendances]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISAuthTokens'
ALTER TABLE [dbo].[ISAuthTokens]
ADD CONSTRAINT [PK_ISAuthTokens]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISClasses'
ALTER TABLE [dbo].[ISClasses]
ADD CONSTRAINT [PK_ISClasses]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISClassTypes'
ALTER TABLE [dbo].[ISClassTypes]
ADD CONSTRAINT [PK_ISClassTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISClassYears'
ALTER TABLE [dbo].[ISClassYears]
ADD CONSTRAINT [PK_ISClassYears]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISCompleteAttendanceRuns'
ALTER TABLE [dbo].[ISCompleteAttendanceRuns]
ADD CONSTRAINT [PK_ISCompleteAttendanceRuns]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISCompletePickupRuns'
ALTER TABLE [dbo].[ISCompletePickupRuns]
ADD CONSTRAINT [PK_ISCompletePickupRuns]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISCountries'
ALTER TABLE [dbo].[ISCountries]
ADD CONSTRAINT [PK_ISCountries]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISDataUploadHistories'
ALTER TABLE [dbo].[ISDataUploadHistories]
ADD CONSTRAINT [PK_ISDataUploadHistories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISErrorLogs'
ALTER TABLE [dbo].[ISErrorLogs]
ADD CONSTRAINT [PK_ISErrorLogs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISFAQs'
ALTER TABLE [dbo].[ISFAQs]
ADD CONSTRAINT [PK_ISFAQs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISHolidays'
ALTER TABLE [dbo].[ISHolidays]
ADD CONSTRAINT [PK_ISHolidays]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISInvoices'
ALTER TABLE [dbo].[ISInvoices]
ADD CONSTRAINT [PK_ISInvoices]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISLogTypes'
ALTER TABLE [dbo].[ISLogTypes]
ADD CONSTRAINT [PK_ISLogTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISMessages'
ALTER TABLE [dbo].[ISMessages]
ADD CONSTRAINT [PK_ISMessages]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISOrganisationUsers'
ALTER TABLE [dbo].[ISOrganisationUsers]
ADD CONSTRAINT [PK_ISOrganisationUsers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISPayments'
ALTER TABLE [dbo].[ISPayments]
ADD CONSTRAINT [PK_ISPayments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISPickers'
ALTER TABLE [dbo].[ISPickers]
ADD CONSTRAINT [PK_ISPickers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISPickerAssignments'
ALTER TABLE [dbo].[ISPickerAssignments]
ADD CONSTRAINT [PK_ISPickerAssignments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISPickups'
ALTER TABLE [dbo].[ISPickups]
ADD CONSTRAINT [PK_ISPickups]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISPickUpMessages'
ALTER TABLE [dbo].[ISPickUpMessages]
ADD CONSTRAINT [PK_ISPickUpMessages]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISPickUpStatus'
ALTER TABLE [dbo].[ISPickUpStatus]
ADD CONSTRAINT [PK_ISPickUpStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISReports'
ALTER TABLE [dbo].[ISReports]
ADD CONSTRAINT [PK_ISReports]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISRoles'
ALTER TABLE [dbo].[ISRoles]
ADD CONSTRAINT [PK_ISRoles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISSchools'
ALTER TABLE [dbo].[ISSchools]
ADD CONSTRAINT [PK_ISSchools]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISSchoolClasses'
ALTER TABLE [dbo].[ISSchoolClasses]
ADD CONSTRAINT [PK_ISSchoolClasses]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISSchoolInvoices'
ALTER TABLE [dbo].[ISSchoolInvoices]
ADD CONSTRAINT [PK_ISSchoolInvoices]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISSchoolTypes'
ALTER TABLE [dbo].[ISSchoolTypes]
ADD CONSTRAINT [PK_ISSchoolTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISStudents'
ALTER TABLE [dbo].[ISStudents]
ADD CONSTRAINT [PK_ISStudents]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISStudentHistories'
ALTER TABLE [dbo].[ISStudentHistories]
ADD CONSTRAINT [PK_ISStudentHistories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISStudentReassignHistories'
ALTER TABLE [dbo].[ISStudentReassignHistories]
ADD CONSTRAINT [PK_ISStudentReassignHistories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISSupports'
ALTER TABLE [dbo].[ISSupports]
ADD CONSTRAINT [PK_ISSupports]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISSupportStatus'
ALTER TABLE [dbo].[ISSupportStatus]
ADD CONSTRAINT [PK_ISSupportStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISTeachers'
ALTER TABLE [dbo].[ISTeachers]
ADD CONSTRAINT [PK_ISTeachers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISTeacherClassAssignments'
ALTER TABLE [dbo].[ISTeacherClassAssignments]
ADD CONSTRAINT [PK_ISTeacherClassAssignments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISTeacherReassignHistories'
ALTER TABLE [dbo].[ISTeacherReassignHistories]
ADD CONSTRAINT [PK_ISTeacherReassignHistories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISTicketMessages'
ALTER TABLE [dbo].[ISTicketMessages]
ADD CONSTRAINT [PK_ISTicketMessages]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISTrasectionStatus'
ALTER TABLE [dbo].[ISTrasectionStatus]
ADD CONSTRAINT [PK_ISTrasectionStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISTrasectionTypes'
ALTER TABLE [dbo].[ISTrasectionTypes]
ADD CONSTRAINT [PK_ISTrasectionTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISUserActivities'
ALTER TABLE [dbo].[ISUserActivities]
ADD CONSTRAINT [PK_ISUserActivities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ISUserRoles'
ALTER TABLE [dbo].[ISUserRoles]
ADD CONSTRAINT [PK_ISUserRoles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ViewNotAssignPickers'
ALTER TABLE [dbo].[ViewNotAssignPickers]
ADD CONSTRAINT [PK_ViewNotAssignPickers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [StudentsID] in table 'ViewStudentAttendences'
ALTER TABLE [dbo].[ViewStudentAttendences]
ADD CONSTRAINT [PK_ViewStudentAttendences]
    PRIMARY KEY CLUSTERED ([StudentsID] ASC);
GO

-- Creating primary key on [StudID] in table 'ViewStudentPickUps'
ALTER TABLE [dbo].[ViewStudentPickUps]
ADD CONSTRAINT [PK_ViewStudentPickUps]
    PRIMARY KEY CLUSTERED ([StudID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AccountStatusID] in table 'ISSchools'
ALTER TABLE [dbo].[ISSchools]
ADD CONSTRAINT [FK__ISSchool__Accoun__70A8B9AE]
    FOREIGN KEY ([AccountStatusID])
    REFERENCES [dbo].[ISAccountStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISSchool__Accoun__70A8B9AE'
CREATE INDEX [IX_FK__ISSchool__Accoun__70A8B9AE]
ON [dbo].[ISSchools]
    ([AccountStatusID]);
GO

-- Creating foreign key on [StudentID] in table 'ISAttendances'
ALTER TABLE [dbo].[ISAttendances]
ADD CONSTRAINT [FK__ISAttenda__Stude__5165187F]
    FOREIGN KEY ([StudentID])
    REFERENCES [dbo].[ISStudents]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISAttenda__Stude__5165187F'
CREATE INDEX [IX_FK__ISAttenda__Stude__5165187F]
ON [dbo].[ISAttendances]
    ([StudentID]);
GO

-- Creating foreign key on [TeacherID] in table 'ISAttendances'
ALTER TABLE [dbo].[ISAttendances]
ADD CONSTRAINT [FK__ISAttenda__Teach__52593CB8]
    FOREIGN KEY ([TeacherID])
    REFERENCES [dbo].[ISTeachers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISAttenda__Teach__52593CB8'
CREATE INDEX [IX_FK__ISAttenda__Teach__52593CB8]
ON [dbo].[ISAttendances]
    ([TeacherID]);
GO

-- Creating foreign key on [ClassID] in table 'ISCompleteAttendanceRuns'
ALTER TABLE [dbo].[ISCompleteAttendanceRuns]
ADD CONSTRAINT [FK__ISCompleteAttendanceRun__ISClass]
    FOREIGN KEY ([ClassID])
    REFERENCES [dbo].[ISClasses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISCompleteAttendanceRun__ISClass'
CREATE INDEX [IX_FK__ISCompleteAttendanceRun__ISClass]
ON [dbo].[ISCompleteAttendanceRuns]
    ([ClassID]);
GO

-- Creating foreign key on [ClassID] in table 'ISCompletePickupRuns'
ALTER TABLE [dbo].[ISCompletePickupRuns]
ADD CONSTRAINT [FK__ISCompletePickupRun__ISClass]
    FOREIGN KEY ([ClassID])
    REFERENCES [dbo].[ISClasses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISCompletePickupRun__ISClass'
CREATE INDEX [IX_FK__ISCompletePickupRun__ISClass]
ON [dbo].[ISCompletePickupRuns]
    ([ClassID]);
GO

-- Creating foreign key on [ClassID] in table 'ISStudents'
ALTER TABLE [dbo].[ISStudents]
ADD CONSTRAINT [FK__ISStudent__Class__65370702]
    FOREIGN KEY ([ClassID])
    REFERENCES [dbo].[ISClasses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISStudent__Class__65370702'
CREATE INDEX [IX_FK__ISStudent__Class__65370702]
ON [dbo].[ISStudents]
    ([ClassID]);
GO

-- Creating foreign key on [ClassID] in table 'ISTeacherClassAssignments'
ALTER TABLE [dbo].[ISTeacherClassAssignments]
ADD CONSTRAINT [FK__ISTeacher__Class__0E391C95]
    FOREIGN KEY ([ClassID])
    REFERENCES [dbo].[ISClasses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISTeacher__Class__0E391C95'
CREATE INDEX [IX_FK__ISTeacher__Class__0E391C95]
ON [dbo].[ISTeacherClassAssignments]
    ([ClassID]);
GO

-- Creating foreign key on [TypeID] in table 'ISClasses'
ALTER TABLE [dbo].[ISClasses]
ADD CONSTRAINT [FK_ISClass_ISClassType]
    FOREIGN KEY ([TypeID])
    REFERENCES [dbo].[ISClassTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISClass_ISClassType'
CREATE INDEX [IX_FK_ISClass_ISClassType]
ON [dbo].[ISClasses]
    ([TypeID]);
GO

-- Creating foreign key on [TeacherID] in table 'ISCompleteAttendanceRuns'
ALTER TABLE [dbo].[ISCompleteAttendanceRuns]
ADD CONSTRAINT [FK__ISCompleteAttendanceRun__ISTeacher]
    FOREIGN KEY ([TeacherID])
    REFERENCES [dbo].[ISTeachers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISCompleteAttendanceRun__ISTeacher'
CREATE INDEX [IX_FK__ISCompleteAttendanceRun__ISTeacher]
ON [dbo].[ISCompleteAttendanceRuns]
    ([TeacherID]);
GO

-- Creating foreign key on [TeacherID] in table 'ISCompletePickupRuns'
ALTER TABLE [dbo].[ISCompletePickupRuns]
ADD CONSTRAINT [FK__ISCompletePickupRun__ISTeacher]
    FOREIGN KEY ([TeacherID])
    REFERENCES [dbo].[ISTeachers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISCompletePickupRun__ISTeacher'
CREATE INDEX [IX_FK__ISCompletePickupRun__ISTeacher]
ON [dbo].[ISCompletePickupRuns]
    ([TeacherID]);
GO

-- Creating foreign key on [CountryID] in table 'ISOrganisationUsers'
ALTER TABLE [dbo].[ISOrganisationUsers]
ADD CONSTRAINT [FK__ISOrganis__Count__17F790F9]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[ISCountries]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISOrganis__Count__17F790F9'
CREATE INDEX [IX_FK__ISOrganis__Count__17F790F9]
ON [dbo].[ISOrganisationUsers]
    ([CountryID]);
GO

-- Creating foreign key on [BillingCountryID] in table 'ISSchools'
ALTER TABLE [dbo].[ISSchools]
ADD CONSTRAINT [FK__ISSchool__Billin__2180FB33]
    FOREIGN KEY ([BillingCountryID])
    REFERENCES [dbo].[ISCountries]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISSchool__Billin__2180FB33'
CREATE INDEX [IX_FK__ISSchool__Billin__2180FB33]
ON [dbo].[ISSchools]
    ([BillingCountryID]);
GO

-- Creating foreign key on [SchoolID] in table 'ISHolidays'
ALTER TABLE [dbo].[ISHolidays]
ADD CONSTRAINT [FK__ISHoliday__Schoo__5CD6CB2B]
    FOREIGN KEY ([SchoolID])
    REFERENCES [dbo].[ISSchools]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISHoliday__Schoo__5CD6CB2B'
CREATE INDEX [IX_FK__ISHoliday__Schoo__5CD6CB2B]
ON [dbo].[ISHolidays]
    ([SchoolID]);
GO

-- Creating foreign key on [LogTypeID] in table 'ISSupports'
ALTER TABLE [dbo].[ISSupports]
ADD CONSTRAINT [FK__ISSupport__LogTy__6477ECF3]
    FOREIGN KEY ([LogTypeID])
    REFERENCES [dbo].[ISLogTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISSupport__LogTy__6477ECF3'
CREATE INDEX [IX_FK__ISSupport__LogTy__6477ECF3]
ON [dbo].[ISSupports]
    ([LogTypeID]);
GO

-- Creating foreign key on [RoleID] in table 'ISOrganisationUsers'
ALTER TABLE [dbo].[ISOrganisationUsers]
ADD CONSTRAINT [FK_ISOrganisationUser_ISRole]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[ISRoles]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISOrganisationUser_ISRole'
CREATE INDEX [IX_FK_ISOrganisationUser_ISRole]
ON [dbo].[ISOrganisationUsers]
    ([RoleID]);
GO

-- Creating foreign key on [ParentID] in table 'ISPickers'
ALTER TABLE [dbo].[ISPickers]
ADD CONSTRAINT [FK__ISPicker__Parent__671F4F74]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[ISStudents]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISPicker__Parent__671F4F74'
CREATE INDEX [IX_FK__ISPicker__Parent__671F4F74]
ON [dbo].[ISPickers]
    ([ParentID]);
GO

-- Creating foreign key on [SchoolID] in table 'ISPickers'
ALTER TABLE [dbo].[ISPickers]
ADD CONSTRAINT [FK__ISPicker__School__1AD3FDA4]
    FOREIGN KEY ([SchoolID])
    REFERENCES [dbo].[ISSchools]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISPicker__School__1AD3FDA4'
CREATE INDEX [IX_FK__ISPicker__School__1AD3FDA4]
ON [dbo].[ISPickers]
    ([SchoolID]);
GO

-- Creating foreign key on [SchoolID] in table 'ISPickers'
ALTER TABLE [dbo].[ISPickers]
ADD CONSTRAINT [FK__ISPicker__School__1BC821DD]
    FOREIGN KEY ([SchoolID])
    REFERENCES [dbo].[ISSchools]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISPicker__School__1BC821DD'
CREATE INDEX [IX_FK__ISPicker__School__1BC821DD]
ON [dbo].[ISPickers]
    ([SchoolID]);
GO

-- Creating foreign key on [StudentID] in table 'ISPickers'
ALTER TABLE [dbo].[ISPickers]
ADD CONSTRAINT [FK__ISPicker__Studen__662B2B3B]
    FOREIGN KEY ([StudentID])
    REFERENCES [dbo].[ISStudents]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISPicker__Studen__662B2B3B'
CREATE INDEX [IX_FK__ISPicker__Studen__662B2B3B]
ON [dbo].[ISPickers]
    ([StudentID]);
GO

-- Creating foreign key on [PickerID] in table 'ISPickups'
ALTER TABLE [dbo].[ISPickups]
ADD CONSTRAINT [FK__ISPickup__Picker__5BAD9CC8]
    FOREIGN KEY ([PickerID])
    REFERENCES [dbo].[ISPickers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISPickup__Picker__5BAD9CC8'
CREATE INDEX [IX_FK__ISPickup__Picker__5BAD9CC8]
ON [dbo].[ISPickups]
    ([PickerID]);
GO

-- Creating foreign key on [StudentID] in table 'ISPickups'
ALTER TABLE [dbo].[ISPickups]
ADD CONSTRAINT [FK__ISPickup__Studen__59C55456]
    FOREIGN KEY ([StudentID])
    REFERENCES [dbo].[ISStudents]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISPickup__Studen__59C55456'
CREATE INDEX [IX_FK__ISPickup__Studen__59C55456]
ON [dbo].[ISPickups]
    ([StudentID]);
GO

-- Creating foreign key on [TeacherID] in table 'ISPickups'
ALTER TABLE [dbo].[ISPickups]
ADD CONSTRAINT [FK__ISPickup__Teache__5AB9788F]
    FOREIGN KEY ([TeacherID])
    REFERENCES [dbo].[ISTeachers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISPickup__Teache__5AB9788F'
CREATE INDEX [IX_FK__ISPickup__Teache__5AB9788F]
ON [dbo].[ISPickups]
    ([TeacherID]);
GO

-- Creating foreign key on [SchoolID] in table 'ISStudents'
ALTER TABLE [dbo].[ISStudents]
ADD CONSTRAINT [FK__ISStudent__Schoo__6442E2C9]
    FOREIGN KEY ([SchoolID])
    REFERENCES [dbo].[ISSchools]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISStudent__Schoo__6442E2C9'
CREATE INDEX [IX_FK__ISStudent__Schoo__6442E2C9]
ON [dbo].[ISStudents]
    ([SchoolID]);
GO

-- Creating foreign key on [SchoolID] in table 'ISTeachers'
ALTER TABLE [dbo].[ISTeachers]
ADD CONSTRAINT [FK__ISTeacher__Schoo__46B27FE2]
    FOREIGN KEY ([SchoolID])
    REFERENCES [dbo].[ISSchools]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISTeacher__Schoo__46B27FE2'
CREATE INDEX [IX_FK__ISTeacher__Schoo__46B27FE2]
ON [dbo].[ISTeachers]
    ([SchoolID]);
GO

-- Creating foreign key on [SchoolID] in table 'ISUserActivities'
ALTER TABLE [dbo].[ISUserActivities]
ADD CONSTRAINT [FK__ISUserAct__Schoo__2704CA5F]
    FOREIGN KEY ([SchoolID])
    REFERENCES [dbo].[ISSchools]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISUserAct__Schoo__2704CA5F'
CREATE INDEX [IX_FK__ISUserAct__Schoo__2704CA5F]
ON [dbo].[ISUserActivities]
    ([SchoolID]);
GO

-- Creating foreign key on [ID] in table 'ISSchools'
ALTER TABLE [dbo].[ISSchools]
ADD CONSTRAINT [FK_ISSchool_ISSchool]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[ISSchools]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TypeID] in table 'ISSchools'
ALTER TABLE [dbo].[ISSchools]
ADD CONSTRAINT [FK_ISSchool_ISSchoolType]
    FOREIGN KEY ([TypeID])
    REFERENCES [dbo].[ISSchoolTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISSchool_ISSchoolType'
CREATE INDEX [IX_FK_ISSchool_ISSchoolType]
ON [dbo].[ISSchools]
    ([TypeID]);
GO

-- Creating foreign key on [SchoolID] in table 'ISSchoolInvoices'
ALTER TABLE [dbo].[ISSchoolInvoices]
ADD CONSTRAINT [FK_ISSchoolInvoice_ISSchool]
    FOREIGN KEY ([SchoolID])
    REFERENCES [dbo].[ISSchools]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISSchoolInvoice_ISSchool'
CREATE INDEX [IX_FK_ISSchoolInvoice_ISSchool]
ON [dbo].[ISSchoolInvoices]
    ([SchoolID]);
GO

-- Creating foreign key on [SchoolID] in table 'ISUserRoles'
ALTER TABLE [dbo].[ISUserRoles]
ADD CONSTRAINT [FK_ISUserRole_ISSchool]
    FOREIGN KEY ([SchoolID])
    REFERENCES [dbo].[ISSchools]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISUserRole_ISSchool'
CREATE INDEX [IX_FK_ISUserRole_ISSchool]
ON [dbo].[ISUserRoles]
    ([SchoolID]);
GO

-- Creating foreign key on [StatusID] in table 'ISSchoolInvoices'
ALTER TABLE [dbo].[ISSchoolInvoices]
ADD CONSTRAINT [FK_ISSchoolInvoice_ISTrasectionStatus]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[ISTrasectionStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISSchoolInvoice_ISTrasectionStatus'
CREATE INDEX [IX_FK_ISSchoolInvoice_ISTrasectionStatus]
ON [dbo].[ISSchoolInvoices]
    ([StatusID]);
GO

-- Creating foreign key on [TransactionTypeID] in table 'ISSchoolInvoices'
ALTER TABLE [dbo].[ISSchoolInvoices]
ADD CONSTRAINT [FK_ISSchoolInvoice_ISTrasectionType]
    FOREIGN KEY ([TransactionTypeID])
    REFERENCES [dbo].[ISTrasectionTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISSchoolInvoice_ISTrasectionType'
CREATE INDEX [IX_FK_ISSchoolInvoice_ISTrasectionType]
ON [dbo].[ISSchoolInvoices]
    ([TransactionTypeID]);
GO

-- Creating foreign key on [StatusID] in table 'ISSupports'
ALTER TABLE [dbo].[ISSupports]
ADD CONSTRAINT [FK_ISSupport_ISSupportStatus]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[ISSupportStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISSupport_ISSupportStatus'
CREATE INDEX [IX_FK_ISSupport_ISSupportStatus]
ON [dbo].[ISSupports]
    ([StatusID]);
GO

-- Creating foreign key on [SupportID] in table 'ISTicketMessages'
ALTER TABLE [dbo].[ISTicketMessages]
ADD CONSTRAINT [FK_ISTicketMessage_ISSupport]
    FOREIGN KEY ([SupportID])
    REFERENCES [dbo].[ISSupports]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISTicketMessage_ISSupport'
CREATE INDEX [IX_FK_ISTicketMessage_ISSupport]
ON [dbo].[ISTicketMessages]
    ([SupportID]);
GO

-- Creating foreign key on [RoleID] in table 'ISTeachers'
ALTER TABLE [dbo].[ISTeachers]
ADD CONSTRAINT [FK__ISTeacher__RoleI__6754599E]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[ISUserRoles]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISTeacher__RoleI__6754599E'
CREATE INDEX [IX_FK__ISTeacher__RoleI__6754599E]
ON [dbo].[ISTeachers]
    ([RoleID]);
GO

-- Creating foreign key on [TeacherID] in table 'ISTeacherClassAssignments'
ALTER TABLE [dbo].[ISTeacherClassAssignments]
ADD CONSTRAINT [FK__ISTeacher__Teach__6A30C649]
    FOREIGN KEY ([TeacherID])
    REFERENCES [dbo].[ISTeachers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ISTeacher__Teach__6A30C649'
CREATE INDEX [IX_FK__ISTeacher__Teach__6A30C649]
ON [dbo].[ISTeacherClassAssignments]
    ([TeacherID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
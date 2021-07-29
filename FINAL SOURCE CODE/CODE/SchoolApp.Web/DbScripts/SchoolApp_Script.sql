CREATE TABLE [dbo].[ISRequestDemo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] varchar(150)  NOT NULL,
	[LastName] varchar(150)  NULL,
	[Role] varchar(150)  NOT NULL,
	[SchoolName] varchar(150)  NULL,
	[OfficialEmail] varchar(150)  NULL,
	[OfficialPhoneNumber] varchar(12)  NULL,
	[MobileNumber] varchar(12)  NULL,
    [CreatedDateTime] datetime  NULL,
	CONSTRAINT PK_ISRequestDemo PRIMARY KEY (ID)
);

CREATE TABLE [dbo].[ISRegistration] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BillingAddress] varchar(400)  NULL,
	[SupervisorName] varchar(150)  NULL,
	[SupervisorEmail] varchar(150)  NULL,
	[SchoolNumber] varchar(12)  NULL,
	[MobileNumber] varchar(12)  NULL,
	[SchoolAddress] varchar(400)  NULL,
	[SchoolWebsite] varchar(150)  NULL,
	[SchoolOpeningTime] varchar(10)  NULL,
	[SchoolClosingTime] varchar(10)  NULL,
	[LastMinutesAfterClosing] varchar(10)  NULL,
	[ChargeMinutesAfterClosing] varchar(10)  NULL,
	[ReportMinutesAfterClosing] varchar(10)  NULL,
	[IsNotificationAfterAttendance] BIT  NULL,
	[IsNotificationAfterPickup] BIT  NULL,
	[FilePath] varchar(500) NULL,
    [CreatedDateTime] datetime  NULL,
	CONSTRAINT PK_ISRegistration PRIMARY KEY (ID)
);

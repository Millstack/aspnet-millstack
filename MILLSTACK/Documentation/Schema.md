

### Database: *MillStack*


### User Management
----------------------

# Gender Master
```
CREATE TABLE M_Gender (
	Gender_ID BIGINT PRIMARY KEY,
	GenderName NVARCHAR(1000) NOT NULL,
	GenderNameMr NVARCHAR(1000) NULL,
	GenderCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```



# Level Master
```
CREATE TABLE M_Level (
	Level_ID BIGINT PRIMARY KEY,
	LevelType NVARCHAR(1000) NOT NULL,
	LevelTypeMr NVARCHAR(1000) NULL,
	LevelCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```



# Designation Master
```
CREATE TABLE M_Designation (
	Designation_ID BIGINT PRIMARY KEY,
	Parent_Designation_ID BIGINT NULL,
	Level_ID BIGINT NOT NULL,
	DesignationName NVARCHAR(1000) NOT NULL,
	DesignationNameMr NVARCHAR(1000) NULL,
	DesignationCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```



# Role Master
```
CREATE TABLE M_RoleMaster (
	Role_ID BIGINT PRIMARY KEY,
	RoleName NVARCHAR(1000) NOT NULL,
	RoleNameMr NVARCHAR(1000) NULL,
	RoleCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```




## Table-Valued Parameter (TVP) : needed for iterating over multiple records in single execution
 *example: inserting user assigned roles after the user insertion**
```
-- Drop the existing TVP if it exists
IF EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'RoleIDTableType')
BEGIN
    DROP TYPE dbo.RoleIDTableType;
END
GO

-- Create the updated TVP with an additional SavedBy column
CREATE TYPE dbo.RoleIDTableType AS TABLE
(
    Role_ID BIGINT,
    SavedBy NVARCHAR(1000)
);
GO
```



## User Mater
```
CREATE TABLE Tbl_M_UserMaster (
    User_ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	GUID UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
	Designation_ID BIGINT NOT NULL,
	LoginAttempt INT NULL,
	UserImage NVARCHAR(1000) NULL,
	FirstName NVARCHAR(1000) NOT NULL,
	FirstNameMr NVARCHAR(1000) NULL,
	MiddleName NVARCHAR(1000) NULL,
	MiddleNameMr NVARCHAR(1000) NULL,
	LastName NVARCHAR(1000) NOT NULL,
	LastNameMr NVARCHAR(1000) NULL,
	Gender NVARCHAR(1000) NOT NULL,
	UserPhoneNo VARCHAR(10) UNIQUE NOT NULL CHECK (LEN(UserPhoneNo) = 10 AND UserPhoneNo NOT LIKE '%[^0-9]%'),
	UserEmail VARCHAR(1000) UNIQUE NOT NULL,
	UserAddress NVARCHAR(1000) NULL,
    UserName NVARCHAR(100) UNIQUE NOT NULL,
    UserPassword NVARCHAR(1000) NOT NULL,
	Salt NVARCHAR(1000) NOT NULL, 
	Country_ID VARCHAR(1000) NULL,
	State_ID VARCHAR(1000) NULL,
	Division_ID VARCHAR(MAX) NULL,
	Sub_Division_ID VARCHAR(MAX) NULL,
	District_ID VARCHAR(MAX) NULL,
	Taluka_ID VARCHAR(MAX) NULL,
	City_ID VARCHAR(MAX) NULL,
	Village_ID VARCHAR(MAX) NULL,
	Assembly_ID VARCHAR(MAX) NULL,
	Ward_ID VARCHAR(MAX) NULL,
	Sector_ID VARCHAR(MAX) NULL,
	Society_ID VARCHAR(MAX) NULL,
	ReportingTo BIGINT NULL,
    IsOtpRequired BIT DEFAULT 0 NOT NULL,
    IsActive BIT DEFAULT 1 NOT NULL,
    SavedBy BIGINT NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```


```
temp: user master table with seperate constraints:

CREATE TABLE Tbl_M_UserMaster (
    User_ID BIGINT IDENTITY(1,1) PRIMARY KEY,
    GUID UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    Designation_ID BIGINT NOT NULL,
    LoginAttempt INT NULL,
    UserImage NVARCHAR(1000) NULL,
    FirstName NVARCHAR(1000) NOT NULL,
    FirstNameMr NVARCHAR(1000) NULL,
    MiddleName NVARCHAR(1000) NULL,
    MiddleNameMr NVARCHAR(1000) NULL,
    LastName NVARCHAR(1000) NOT NULL,
    LastNameMr NVARCHAR(1000) NULL,
    Gender NVARCHAR(1000) NOT NULL,
    UserPhoneNo VARCHAR(10) NOT NULL CHECK (LEN(UserPhoneNo) = 10 AND UserPhoneNo NOT LIKE '%[^0-9]%'),
    UserEmail VARCHAR(1000) NOT NULL,
    UserAddress NVARCHAR(1000) NULL,
    UserName NVARCHAR(100) NOT NULL,
    UserPassword NVARCHAR(1000) NOT NULL,
    Salt NVARCHAR(1000) NOT NULL,
    Country_ID VARCHAR(1000) NULL,
    State_ID VARCHAR(1000) NULL,
    Division_ID VARCHAR(MAX) NULL,
    Sub_Division_ID VARCHAR(MAX) NULL,
    District_ID VARCHAR(MAX) NULL,
    Taluka_ID VARCHAR(MAX) NULL,
    City_ID VARCHAR(MAX) NULL,
    Village_ID VARCHAR(MAX) NULL,
    Assembly_ID VARCHAR(MAX) NULL,
    Ward_ID VARCHAR(MAX) NULL,
    Sector_ID VARCHAR(MAX) NULL,
    Society_ID VARCHAR(MAX) NULL,
    ReportingTo BIGINT NULL,
    IsOtpRequired BIT DEFAULT 0 NOT NULL,
    IsActive BIT DEFAULT 1 NOT NULL,
    SavedBy BIGINT NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
    IsDeleted BIT NULL,

    -- Explicitly named unique constraints
    CONSTRAINT UQ_UserPhoneNo UNIQUE (UserPhoneNo),
    CONSTRAINT UQ_UserEmail UNIQUE (UserEmail),
    CONSTRAINT UQ_UserName UNIQUE (UserName)
);

```


# MAP: User Role Master
```
CREATE TABLE Tbl_MAP_UserRole (
	UserRole_ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	User_ID BIGINT NOT NULL,
	Role_ID BIGINT NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```



# Menu Forms
```
CREATE TABLE Tbl_M_MenuForm (
	Menu_ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	Parent_ID BIGINT NOT NULL,
	MenuName VARCHAR(1000) NOT NULL,
	MenuNameMr NVARCHAR(1000) NULL,
	MenuOrder BIGINT NOT NULL,
	MenuURL VARCHAR(1000) NOT NULL,
	MenuIcon VARCHAR(1000) NULL,
    SavedBy VARCHAR(1000) NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```



# MAP: UserRole Menu Forms
```
CREATE TABLE Tbl_MAP_UserRole_MenuForm (
	URFM_ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	UserRole_ID BIGINT NOT NULL,
	Menu_ID BIGINT NOT NULL,
    SavedBy VARCHAR(1000) NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```











### Geographical Masters
----------------------

# Country
```
CREATE TABLE M_Country (
    Country_ID BIGINT PRIMARY KEY,
	CountryName NVARCHAR(1000) NOT NULL,
	CountryNameMr NVARCHAR(1000) NULL,
	CountryCode NVARCHAR(20) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```


# State
```
CREATE TABLE M_State (
    State_ID BIGINT PRIMARY KEY,
	Country_ID BIGINT NOT NULL,
	StateName NVARCHAR(1000) NOT NULL,
	StateNameMr NVARCHAR(1000) NOT NULL,
	StateCode INT NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```


# Division
```
CREATE TABLE M_Division (
    Division_ID BIGINT PRIMARY KEY,
	State_ID BIGINT NOT NULL,
	DivisionName NVARCHAR(1000) NOT NULL,
	DivisionNameMr NVARCHAR(1000) NOT NULL,
	DivisionCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```


# District
```
CREATE TABLE M_District (
	District_ID BIGINT PRIMARY KEY,
	State_ID BIGINT NOT NULL,
    Division_ID BIGINT NOT NULL,
	DistrictName NVARCHAR(1000) NOT NULL,
	DistrictNameMr NVARCHAR(1000) NOT NULL,
	DistrictCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```


# Taluka
```
CREATE TABLE M_Taluka (
	Taluka_ID BIGINT PRIMARY KEY,
	State_ID BIGINT NOT NULL,
    Division_ID BIGINT NOT NULL,
	District_ID BIGINT NOT NULL,
	TalukaName NVARCHAR(1000) NOT NULL,
	TalukaNameMr NVARCHAR(1000) NOT NULL,
	TalukaCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```





-- to add foriegn key constraint in already cerated table
ALTER TABLE Tbl_M_Ward
ADD CONSTRAINT FK_Tbl_M_Ward_Assembly
FOREIGN KEY (Assembly_ID) REFERENCES Tbl_M_Assembly(Assembly_ID);



# Assembly Master
```
CREATE TABLE Tbl_M_Assembly (
	Assembly_ID BIGINT PRIMARY KEY,
	State_ID BIGINT NOT NULL,
	District_ID BIGINT NOT NULL,
	AssemblyName NVARCHAR(1000) NOT NULL,
	AssemblyNameMr NVARCHAR(1000) NOT NULL,
	AssemblyCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```


# Ward Master
```
CREATE TABLE Tbl_M_Ward (
	Ward_ID BIGINT PRIMARY KEY,
	Assembly_ID BIGINT NOT NULL,
	WardName NVARCHAR(1000) NOT NULL,
	WardNameMr NVARCHAR(1000) NULL,
	WardCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL,

	CONSTRAINT FK_Tbl_M_Ward_Assembly FOREIGN KEY (Assembly_ID) REFERENCES Tbl_M_Assembly(Assembly_ID)
);
```


# Sector Master
```
CREATE TABLE Tbl_M_Sector (
	Sector_ID BIGINT PRIMARY KEY,
	Assembly_ID BIGINT NOT NULL,
	Ward_ID BIGINT NOT NULL,
	SectorName NVARCHAR(1000) NOT NULL,
	SectorNameMr NVARCHAR(1000) NULL,
	SectorCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL,

	CONSTRAINT FK_Tbl_M_Sector_Assembly FOREIGN KEY (Assembly_ID) REFERENCES Tbl_M_Assembly(Assembly_ID),
	CONSTRAINT FK_Tbl_M_Sector_Ward FOREIGN KEY (Ward_ID) REFERENCES Tbl_M_Ward (Ward_ID)
);
```


# Society Master
```
CREATE TABLE Tbl_M_Society (
	Society_ID BIGINT PRIMARY KEY,
	Assembly_ID BIGINT NOT NULL,
	Ward_ID BIGINT NOT NULL,
	Sector_ID BIGINT NOT NULL,
	SocietyName NVARCHAR(1000) NOT NULL,
	SocietyNameMr NVARCHAR(1000) NULL,
	SocietyCode NVARCHAR(100) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL,

	CONSTRAINT FK_Tbl_M_Society_Assembly FOREIGN KEY (Assembly_ID) REFERENCES Tbl_M_Assembly(Assembly_ID),
	CONSTRAINT FK_Tbl_M_Society_Ward FOREIGN KEY (Ward_ID) REFERENCES Tbl_M_Ward (Ward_ID),
	CONSTRAINT FK_Tbl_M_Society_Sector FOREIGN KEY (Sector_ID) REFERENCES Tbl_M_Sector (Sector_ID)
);
```





### Customer Masters
----------------------

# Customer Type Master
```
CREATE TABLE Tbl_M_CustomerType (
    CustomerType_ID BIGINT PRIMARY KEY,
	CustomerName NVARCHAR(1000) NOT NULL,
	CustomerNameMr NVARCHAR(1000) NULL,
	CustomerCode NVARCHAR(20) NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
);
```



# Customer Master
```
Create Table Tbl_M_Customer (
	Customer_ID BIGINT PRIMARY KEY IDENTITY (1,1),
	CustomerType_ID BIGINT NOT NULL,
	Gender_ID BIGINT NOT NULL,

	List_No BIGINT NOT NULL,
	Serial_No BIGINT NOT NULL,
	Customer_Name VARCHAR(500) NOT NULL,
	Customer_MobileNo VARCHAR(10) UNIQUE NOT NULL CHECK (LEN(Customer_MobileNo) = 10 AND Customer_MobileNo NOT LIKE '%[^0-9]%'),
	WRN_No VARCHAR(1000) UNIQUE NOT NULL,
	Voting_Booth VARCHAR(100) NOT NULL,
	Voting_Room VARCHAR(100) NOT NULL,

	Assembly_ID BIGINT NOT NULL,
	Ward_ID BIGINT NOT NULL,
	Sector_ID BIGINT NOT NULL,
	Society_ID BIGINT NOT NULL,

	Data_Entry_Mode VARCHAR(100) NOT NULL,
	Customer_Done BIT DEFAULT 0,
	Customer_AR DECIMAL(18,2) NULL, -- Amount Received

	IsApproved BIT NULL,
	IsApprovedBy BIGINT NULL,
	IsVerified BIT NULL,
	IsVerifiedBy BIGINT NULL,

	SavedBy BIGINT NOT NULL,
	SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL,

	CONSTRAINT FK_Tbl_M_Customer_CustomerType FOREIGN KEY (CustomerType_ID) REFERENCES Tbl_M_CustomerType(CustomerType_ID),
	CONSTRAINT FK_Tbl_M_Customer_Gender FOREIGN KEY (Gender_ID) REFERENCES M_Gender (Gender_ID)
);
```

```
IF EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'Customer_TVP')
BEGIN
    DROP TYPE dbo.Customer_TVP;
END
GO

CREATE TYPE Customer_TVP AS TABLE
(
    List_No BIGINT,
    Serial_No BIGINT,
    Customer_Name NVARCHAR(100),
    Customer_MobileNo NVARCHAR(15),
    Gender_ID BIGINT,
    WRN_No NVARCHAR(50),
    CustomerType_ID INT,
    Voting_Booth NVARCHAR(50),
    Voting_Room NVARCHAR(50),
	Assembly_ID BIGINT,
    Ward_ID BIGINT,
    Sector_ID BIGINT,
    Society_ID BIGINT,
    Data_Entry_Mode NVARCHAR(10)
	--SavedBy BIGINT
);
GO
```







```
<ajax:FilteredTextBoxExtender 
ID="FilteredTextBoxExtender1" 
runat="server" 
TargetControlID="Txt_First_Name"
FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" 
ValidChars=". " />
```












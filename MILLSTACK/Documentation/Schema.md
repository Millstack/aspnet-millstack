

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
	Division_ID VARCHAR(1000) NULL,
	Sub_Division_ID VARCHAR(1000) NULL,
	District_ID VARCHAR(1000) NULL,
	Taluka_ID VARCHAR(1000) NULL,
	City_ID VARCHAR(1000) NULL,
	Village_ID VARCHAR(1000) NULL,
	ReportingTo BIGINT NULL,
    IsOtpRequired BIT DEFAULT 0 NOT NULL,
    IsActive BIT DEFAULT 1 NOT NULL,
    SavedBy VARCHAR(1000) NOT NULL,
    SavedOn DATETIME DEFAULT GETDATE() NOT NULL,
	IsDeleted BIT NULL
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


# Assembly
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









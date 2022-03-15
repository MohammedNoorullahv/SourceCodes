USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[KHLI_AttendanceImport]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc KHLI_AttendanceImport

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[KHLI_AttendanceImport]
@mAction				varchar(20)	='SELALL',
@mPKID					int			= NULL ,
@mStaffCode1			Varchar(10)	= NULL ,
@mUnitCode				Varchar(10)	= NULL ,
@mAttendanceDate		Date		= NULL ,
@mFromDate				Date		= NULL ,
@mToDate				Date		= NULL ,

@mID					varchar(50) = NULL,
@mStaffId				varchar(30)	= NULL ,
@mShiftId				int			= NULL ,
@mFirstPunch			datetime	= NULL ,
@mFirstPunchDevice		nvarchar(255)	= NULL ,
@mLastPunch				datetime	= NULL ,
@mLastPunchDevice		nvarchar(255)	= NULL ,
@mLateComingBy			int			= NULL ,
@mEarlyGoingBy			int			= NULL ,
@mEarlyOT				int			= NULL ,
@mLateOT				int			= NULL ,
@mDuration				int			= NULL ,
@mLeave					nvarchar(255)	= NULL ,
@mHoliday				nvarchar(255)	= NULL ,
@mSpecialHoliday		nvarchar(255)	= NULL ,
@mSpecialOff			nvarchar(255)	= NULL ,
@mLogRecords			nvarchar(1000)	= NULL ,
@mDayStatusId			int				= NULL ,
@mWorkStatusId			int				= NULL ,
@mPeriod1Status			nvarchar(50)	= NULL ,
@mPeriod2Status			nvarchar(50)	= NULL ,
@mLeaveReason			nvarchar(250)	= NULL ,
@mCreatedBy				varchar(100)	= NULL ,
@mCreatedDate			datetime		= NULL ,
@mModifiedBy			varchar(100)	= NULL ,
@mModifiedDate			datetime		= NULL ,
@mEnteredOnMachineID	varchar(50)		= NULL ,
@mModuleName			varchar(50)		= NULL ,
@mIsApproved			bit				= NULL ,
@mApprovedBy			varchar(50)		= NULL ,
@mApprovedOn			datetime		= NULL ,
@mRefID					int				= NULL
	
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='SELUNITS'
BEGIN
	Select Distinct(StaffCode1) from eTimeTrackv1.dbo.Staff where name <> '' And StaffCode1 <> '' Order by StaffCode1
END

IF @mAction='LASTDATE'
BEGIN
	--Select * from AHGroup_SSPL.dbo.HR_Attendance_Log1
	Select IsNull(MAX(AttendanceDate),'1900-01-01') As AttendaceDAte from AHGroup_SSPL.dbo.HR_Attendance_Log1 WHERE
	StaffId in ( Select ID from eTimeTrackv1.dbo.Staff Where StaffCode1 = @mStaffCode1)
END

IF @mAction='ATT4AP'
BEGIN
	SELECT * From AHGroup_SSPL.dbo.HR_Attendance_Log1 where UnitCode = @mUnitCode And AttendanceDate = @mAttendanceDate
END

IF @mAction='SELATTEN'
BEGIN
--	Select  * from eTimeTrackv1.dbo.AttendanceLog WHERE AttendanceDate > @mFromDate And AttendanceDate < @mToDate And
--	StaffId in ( Select ID from eTimeTrackv1.dbo.Staff Where StaffCode1 = @mStaffCode1) 
--	Order By AttendanceDate,Id
	
	Select  a.*, 
	b.StaffCode,c.EmpCode,c.EmpFullName

	from eTimeTrackv1.dbo.AttendanceLog a, eTimeTrackv1.dbo.Staff b, AHGroup_SSPL.dbo.Employee c 

	WHERE
	a.StaffId = b.ID And b.StaffCode = c.OldEmpCode AND
	a.AttendanceDate >= @mFromDate And AttendanceDate <= @mToDate And
	b.StaffCode1 = @mStaffCode1
	
	Order By a.AttendanceDate,b.Id
	
END

IF @mAction='INSATTINAP'
BEGIN
	
	Insert Into AHGroup_SSPL.dbo.HR_Attendance_Log1
	VALUES
	(
	@mID,			@mStaffId,				@mAttendanceDate,		@mShiftId,			@mFirstPunch,		@mFirstPunchDevice,
	@mLastPunch,	@mLastPunchDevice,		@mLateComingBy,			@mEarlyGoingBy,		@mEarlyOT,			@mLateOT,
	@mDuration,		@mLeave,				@mHoliday,				@mSpecialHoliday,	@mSpecialOff,		@mLogRecords,
	@mDayStatusId,	@mWorkStatusId,			@mPeriod1Status,		@mPeriod2Status,	@mLeaveReason,		
	@mCreatedBy,
	@mCreatedDate,	@mModifiedBy,			@mModifiedDate,			@mEnteredOnMachineID,					@mModuleName,
	@mIsApproved,	@mApprovedBy,			@mApprovedOn,			@mUnitCode,			@mRefID)
	

END

GO

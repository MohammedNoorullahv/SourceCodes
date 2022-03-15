USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[khli_userlogstatus]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc khli_userlogstatus

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[khli_userlogstatus]
@mAction				varchar(20)	='SELALL',
@mPKId					int			=Null,
@mSystemId				varchar(50) =NULL,
@mSystemName			varchar(50) =NULL,
@mLoginName				varchar(50) =NULL,
@mUserName				varchar(50) =NULL,
@mIPAddress				varchar(50) =NULL,
@mLoginTime				datetime	=NULL,
@mIsCurrentlyLoggedIn	bit			=NULL,
@mLogoutTime			datetime	=NULL,
@mDuration				Varchar(50) =NULL,
@MessageFrom			Varchar(50)	=Null,
@mMsgDate				Datetime	=Null,
@mFrom					Varchar(50)	=Null,
@mFromSystemId			Varchar(50)	=Null,
@mMessage				Varchar(500)=Null,
@mAcknowledgeRequired	Bit			=Null,
@mFKMessageHdr			Int			=Null,
@mMessageTo				Varchar(500)=Null,
@mToSystemId			Varchar(500)=Null,
@mSentTime				Datetime	=Null,
@mAcknowledgeTime		Datetime	=Null,
@mIsSeen				Bit			=Null,
@mTimeofViewing			Datetime	=Null,
@mReplyOf				Int			=Null,
@mFromDate				Datetime	=Null,
@mToDate				Datetime	=Null
	
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

IF @mAction = 'INSERT'
BEGIN
	UPDATE UserLogStatus Set IsCurrentlyLoggedIn = 0 Where LoginName = @mLoginName And IsCurrentlyLoggedIn = 1 
	Delete From MessagesTobeSentTemp Where MessageFrom = @mLoginName
	
	INSERT INTO 
	UserLogStatus
	
	VALUES
	(@mSystemId,			@mSystemName,		@mLoginName,
	@mUserName,				@mIPAddress,		@mLoginTime,
	@mIsCurrentlyLoggedIn,	@mLogoutTime,		@mDuration)
	
END

IF @mAction = 'LOGOUT'
BEGIN
	UPDATE UserLogStatus Set IsCurrentlyLoggedIn = 0, Duration = @mDuration
	
	 Where --LoginName = @mLoginName And IsCurrentlyLoggedIn = 1 
	SystemId = @mSystemId And		SystemName = @mSystemName And 	LoginName = @mLoginName And
	UserName = @mUserName And		IPAddress = @mIPAddress And		LoginTime = @mLoginTime And
	@mIsCurrentlyLoggedIn = 1
	
END



If @mAction = 'SELSYSINFO'
Begin
	Select * from  SystemsInfo Where LoginName = @mLoginName
End

If @mAction='SELACTIVEUSER'
BEGIN
	Select a.*,b.Location from 
	UserLogStatus a, SystemsInfo b
	
	Where
	a.SystemId = b.ID 
	And a.IsCurrentlyLoggedIn = '1'
	And a.SystemId Not In (Select MessageTo From MessagesTobeSentTemp Where MessageFrom = @MessageFrom)
END

If @mAction='SELSELECTUSER'
BEGIN
	Select a.*,b.Location,c.PKID As MessageToID from 
	UserLogStatus a, SystemsInfo b, MessagesTobeSentTemp c 
	
	Where
	a.SystemId = b.ID  And c.MessageTo = b.ID
	And a.IsCurrentlyLoggedIn = '1' 
	And a.SystemId In (Select MessageTo From MessagesTobeSentTemp Where MessageFrom = @MessageFrom)
	And c.PKID In (Select PKID From MessagesTobeSentTemp Where MessageFrom = @MessageFrom)
END

IF @mAction = 'INSERTM2'
BEGIN
	INSERT INTO 
	MessagesTobeSentTemp
	
	VALUES
	(@MessageFrom,			@mSystemId)
END

IF @mAction = 'REMOVEM2'
BEGIN
	Delete From
	MessagesTobeSentTemp
	
	Where
	PKID = @mPKId
END

IF @mAction='INSERTMSG'
BEGIN
	INSERT INTO MessageHdr
	
	VALUES
	(@mMsgDate,		@mFrom,			@mFromSystemId,		@mMessage,		@mAcknowledgeRequired)
END

IF @mAction='SELMSGID'
BEGIN
	Select * From MessageHdr
	
	Where MsgDate = @mMsgDate And [From] = @mFrom And FromSystemId = @mFromSystemId 
END

IF @mAction='INSERTMSGDTL'
BEGIN
	INSERT INTO MessageDtl
	
	VALUES
	(@mFKMessageHdr,		@mMessageTo,		@mToSystemId,		@mAcknowledgeRequired,
	@mSentTime,				@mAcknowledgeTime,	@mIsSeen,			@mTimeofViewing,
	@mReplyOf)
END

IF @mAction='UPDATEMSGDTL'
BEGIN
	Update MessageDtl
	
	Set
	AcknowledgedTime = @mAcknowledgeTime,		IsSeen = @mIsSeen,		TimeofViewing = @mTimeofViewing
	
	Where
	PKID = @mPKID
END

IF @mAction='MESSAGE'
BEGIN
	SELECT a.*,b.[From] As MessageFrom,b.[Message], c.UserName,b.FromSystemId,c.SystemName
	FROM MessageDtl a, MessageHdr b, SystemsInfo c
	Where a.FKMessageHdr = b.PKID And b.FromSystemId = c.ID
	And a.ToSystemId = @mToSystemId 
	--And SentTime = TimeofViewing 
	And a.IsSeen = 0
END

IF @mAction='MESSAGESENTTO'
BEGIN
	SELECT *
	FROM MessageDtl
	Where FKMessageHdr = @mFKMessageHdr
END


If @mAction='SELPENACK'
BEGIN
	Select a.*,b.Location,b.ExtensionNo As Ext, d.Message from 
	UserLogStatus a, SystemsInfo b, MessageDtl c, MessageHdr d
	
	Where
	a.SystemId = b.ID  And a.SystemId = c.ToSystemId
	And a.IsCurrentlyLoggedIn = '1' 
	And c.AcknowledgeRequired = '1' And c.SentTime = c.AcknowledgedTime
	And c.FKMessageHdr = d.PKID And d.[From] = @MessageFrom
END

IF @mAction='SENTHIST'
BEGIN
	Select
	a.PKID As MsgId, a.MsgDate As SentDate, a.[From] As MsgSentFrom,d.UserName As MsgSentBy,
	a.[Message],b.MessageTo,c.UserName,b.AcknowledgeRequired,b.AcknowledgedTime,b.IsSeen,b.TimeofViewing
	
	From
	MessageHdr a, MessageDtl b, SystemsInfo c, SystemsInfo d
	
	Where
	a.PKID = B.FKMessageHdr And b.ToSystemId = c.ID And a.FromSystemId = d.ID And
	a.[From] = @MessageFrom
	And a.MsgDate >= @mFromDate And a.MsgDate <= @mToDate
END

IF @mAction='RECDHIST'
BEGIN
	Select
	a.PKID As MsgId, a.MsgDate As SentDate, a.[From] As MsgSentFrom,d.UserName As MsgSentBy,
	a.[Message],b.MessageTo,c.UserName,b.AcknowledgeRequired,b.AcknowledgedTime,b.IsSeen,b.TimeofViewing
	
	From
	MessageHdr a, MessageDtl b, SystemsInfo c, SystemsInfo d
	
	Where
	a.PKID = B.FKMessageHdr And b.ToSystemId = c.ID And a.FromSystemId = d.ID And
	b.ToSystemId IN (Select ID From SystemsInfo Where LoginName = @MessageFrom)
	--a.[From] = 'ERPAdmin'
	And a.MsgDate >=  @mFromDate  And a.MsgDate <= @mToDate
END
--Select * from SystemsInfo
--Select * from MessagesTobeSentTemp
--Select * from MessageHdr where pkid = '27'
--Select * from MessageDtl Where AcknowledgeRequired = '1' And SentTime = AcknowledgedTime

GO

USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[Op_Login_New]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Op_Login_New]
@mLoginName		Varchar(50)	=Null
AS
BEGIN
	SET NOCOUNT ON;

	Select * from User_GroupDetails
	Where UserId = @mLoginName

END


GO

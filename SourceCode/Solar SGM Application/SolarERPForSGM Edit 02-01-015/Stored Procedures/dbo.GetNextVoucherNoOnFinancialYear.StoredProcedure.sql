USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[GetNextVoucherNoOnFinancialYear]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNextVoucherNoOnFinancialYear]
	-- Add the parameters for the stored procedure here
	@TableName  varchar(255),
	@CompanyName varchar(255),
	@FYear varchar(20),
	 @LastNo Bigint OUTPUT 	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
begin tran
	--Declare @LastNo bigInt;
	SELECT @LastNo= (	Select Top(1) VoucherNo FROM AutoSerial_Gen  WHERE 
							TableName=@TableName AND CompanyCode = @CompanyName AND FYear=@FYear Order By VoucherNo DESC);
	if @LastNo is null
		SELECT @LastNo = 1;
	else
		SELECT @LastNo = @LastNo + 1;
		
	--Insert New record in table
	Insert INTO AutoSerial_Gen (VoucherNo,TableName,FYear,CompanyCode) Values (@LastNo,@TableName,@FYear,@CompanyName);
	SELECT @LastNo
COMMIT Tran 
END

GO

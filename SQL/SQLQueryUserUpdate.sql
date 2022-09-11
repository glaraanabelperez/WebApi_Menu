USE [Menu_Practica]
GO
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 11/9/2022 08:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[User_Update]
	-- Add the parameters for the stored procedure here
	   @UserId int
	  ,@Business_Name nvarchar
      ,@Slogan nvarchar
      ,@User_email nvarchar
      ,@Password nvarchar
      ,@Phone int
      ,@Direction  nvarchar 
      ,@Ig nvarchar
      ,@Facebook nvarchar
      ,@Logo nvarchar
      ,@OrdersWhatsapp tinyint
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	   UPDATE Users SET 
					   [Business_Name] = @Business_Name
					  ,[Slogan] = @Slogan
					  ,[user_email] = @User_email
					  ,[Password] = @Password
					  ,[Phone] =@Phone
					  ,[Direction] =  @Direction
					  ,[Ig] = @Ig
					  ,[Facebook] =  @Facebook
					  ,[Logo] = @Logo
					  ,[OrdersWhatsapp] = @OrdersWhatsapp
			WHERE  [UserId]= @UserId
RETURN 0;
END

USE [Menu_Practica]
GO
/****** Object:  StoredProcedure [dbo].[User_Add]    Script Date: 11/9/2022 08:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[User_Add]
	-- Add the parameters for the stored procedure here
	  @Business_Name nvarchar
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
	INSERT INTO [dbo].[Users]
           ([Business_Name]
           ,[Slogan]
           ,[user_email]
           ,[Password]
           ,[Phone]
           ,[Direction]
           ,[Ig]
           ,[Facebook]
           ,[Logo]
           ,[OrdersWhatsapp])
     VALUES
           (	  
			   @Business_Name 
			  ,@Slogan 
			  ,@User_email 
			  ,@Password 
			  ,@Phone 
			  ,@Direction   
			  ,@Ig 
			  ,@Facebook 
			  ,@Logo 
			  ,@OrdersWhatsapp 
		   )

		   RETURN 0;

END

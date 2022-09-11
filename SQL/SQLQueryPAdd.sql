USE [Menu_Practica]
GO
/****** Object:  StoredProcedure [dbo].[Product_Add]    Script Date: 11/9/2022 08:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Product_Add]
	-- Add the parameters for the stored procedure here
	@UserId int,
	@CategoryId int, 
	@Description varchar,
	@Featured tinyint,
	@NameImage varchar,
	@Price money,
	@Promotion varchar,
	@State tinyint,
	@Title varchar,
	@Subtitle varchar
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Products]
           ([CategoryId_FK]
           ,[UserId_FK]
           ,[State]
           ,[Title]
           ,[Subtitle]
           ,[Description]
           ,[NameImage]
           ,[CreatedOn]
           ,[Price]
           ,[Featured]
           ,[Promotion])
     VALUES
           (@CategoryId
           ,@UserId
           ,@State
           ,@Title
           ,@SubTitle
           ,@Description
           ,@Featured
           ,SYSDATETIME()
           ,@Price
           ,@NameImage
           ,@Promotion)

		   RETURN 0;

END

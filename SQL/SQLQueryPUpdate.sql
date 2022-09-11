USE [Menu_Practica]
GO
/****** Object:  StoredProcedure [dbo].[Product_Update]    Script Date: 11/9/2022 08:12:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Product_Update]
	-- Add the parameters for the stored procedure here
	@ProductId int,
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
	   UPDATE Products SET 
						   [CategoryId_FK] = @CategoryId
						  ,[UserId_FK] = @UserId
						  ,[State] = @State
						  ,[Title] = @Title
						  ,[Subtitle] = @SubTitle
						  ,[Description] = @Description
						  ,[NameImage] = @NameImage
						  ,[CreatedOn] = SYSDATETIME()
						  ,[Price] = @Price
						  ,[Featured] = @Featured
						  ,[Promotion] = @Promotion			
				WHERE  [ProductId]= @ProductId
RETURN 0;

END

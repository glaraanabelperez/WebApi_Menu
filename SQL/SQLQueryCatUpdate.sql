USE [Menu_Practica]
GO
/****** Object:  StoredProcedure [dbo].[Category_Update]    Script Date: 11/9/2022 08:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Category_Update]
	-- Add the parameters for the stored procedure here
	   @UserId int
	  ,@CategoryId int
      ,@Description nvarchar
   	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	   UPDATE Categories SET 
					   [Description] = @Description
			WHERE  [UserId]= @UserId and CategoryId=@CategoryId
RETURN 0;
END

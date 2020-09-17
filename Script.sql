CREATE TABLE [dbo].[Movies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Category] [varchar] (250) NOT NULL,
	[Rating] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Movie_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

---
CREATE PROCEDURE [dbo].[USP_Movies](
 
	@ACTION		CHAR(1)      =	NULL,
	@Id			INT          =	NULL,
	@NAME		VARCHAR(250) =	NULL,
	@Category	VARCHAR(250) =	NULL,
	@Rating		INT           =	NULL

)	
AS
BEGIN
	SET NOCOUNT ON;
		IF(@ACTION='A') -- GET ALL Movies
		BEGIN

			SELECT Id,Name,Category,Rating,CreatedOn From Movies
					
		END
		ELSE IF(@ACTION='D') --DELETE Movie
		BEGIN
			DELETE FROM Movies WHERE Id=@Id
		END
		ELSE IF(@ACTION='E') --CREATE OR UPDATE 
		BEGIN

			IF NOT EXISTS(SELECT 1 FROM Movies WHERE Id=@Id OR Name=@NAME)
			BEGIN

				INSERT INTO Movies(
					Name,Category,Rating,CreatedOn		
					)
					VALUES (
					@NAME,@Category,@Rating,GETDATE()
					)
                    select 'ok' as Result ,'' as Msg
			END
				ELSE
					BEGIN
						UPDATE Movies SET
							Name=@Name,Category=@Category,Rating=@Rating
						
						WHERE	Id=@Id
			 END

			END

			ELSE IF(@ACTION='G') -- GET Movi BY Id
					BEGIN
						SELECT Id,Name,Category,Rating,CreatedOn From Movies
						WHERE	Id=@Id
					END

	END
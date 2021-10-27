Create Database FeedbackDB

use FeedbackDB

CREATE TABLE Users
(
  Id int PRIMARY KEY IDENTITY(1,1),
  Name varchar(50),
  --Role varchar(25)
)

CREATE TABLE Session
(
  Id int  PRIMARY KEY IDENTITY(1,1),
  SessionName varchar(25),
  Date datetime not null,
  Duration datetime not null,
  ConductorId int not null,
  SpeakerId int not null,
  FOREIGN KEY (ConductorId) REFERENCES Users(Id),
  FOREIGN KEY (SpeakerId) REFERENCES Users(Id)
)

ALTER TABLE Session
DROP COLUMN Duration

ALTER TABLE Session
ADD Duration time

ALTER TABLE Session
DROP COLUMN Date

ALTER TABLE Session
ADD Date date

CREATE TABLE FeedbackDetailType
(
  Id int  PRIMARY KEY IDENTITY(1,1),
  Name varchar(50) not null
)

CREATE TABLE Feedback
(
  Id int not null PRIMARY KEY IDENTITY(1,1),
  SessionId int not null,
  Email varchar(25) not null,
  MobileNumber varchar(10) not null,
  Comment varchar(200) ,
  IsInformative bit not null,
  SpeakerRating int not null,
  OverrallRating int not null,
  FOREIGN KEY (SessionId) REFERENCES Session(Id)
)

ALTER TABLE Feedback
ADD Name varchar(50)

CREATE TABLE FeedbackDetail
(
  FeedbackId int not null,
  FdtId int not null,
  FOREIGN KEY (FeedbackId) REFERENCES Feedback(Id),
  FOREIGN KEY (FdtId) REFERENCES FeedbackDetailType(Id)
)

--Stored procedure for creating session
DROP PROCEDURE IF EXISTS Sp_CreateSession
GO
 
Create PROCEDURE Sp_CreateSession
(
   @Name varchar(25),
   @ConductorId int,
   @SpeakerId int,
   @Duration time,
   @Date date
)
AS
BEGIN 
  BEGIN TRY
    BEGIN IF(@Name is not null)
      INSERT INTO Session (SessionName,ConductorId,SpeakerId,Duration,Date) 
	  VALUES (@Name,@ConductorId,@SpeakerId,@Duration,@Date);
	ELSE
	  Return 'Session Name is required'
	END
  END TRY
  BEGIN CATCH
    SELECT ERROR_NUMBER() AS ERRORNO,ERROR_LINE() AS ERRORLINE,ERROR_MESSAGE() AS ERRORMSG,
    ERROR_PROCEDURE() AS ERRORPROCEDURE,ERROR_SEVERITY() AS ERRORSEVERITY
  END CATCH
END
GO

EXEC Sp_CreateSession 'WomenEmpowerment',1,2,'00:10','2021-10-29'

--Stored procedure for getting all the sessions available for today
DROP PROCEDURE IF EXISTS Sp_GetAllSessions
GO

CREATE PROCEDURE Sp_GetAllSessions
AS
BEGIN
  BEGIN TRY

     SELECT S.Id,SessionName,U.Name as ConductorId,U2.Name as SpeakerId,Date,Duration 
	 FROM Session S 
	 join Users U on U.Id = ConductorId 
	 join Users U2 on U2.Id = SpeakerId
	 WHERE Date = CAST(CURRENT_TIMESTAMP AS DATE);

  END TRY
  BEGIN CATCH
    SELECT ERROR_NUMBER() AS ERRORNO,ERROR_LINE() AS ERRORLINE,ERROR_MESSAGE() AS ERRORMSG,
     ERROR_PROCEDURE() AS ERRORPROCEDURE,ERROR_SEVERITY() AS ERRORSEVERITY
  END CATCH
END
GO

--Stored procedure for getting all the multi check control values
DROP PROCEDURE IF EXISTS Sp_GetAllTypes
GO

CREATE PROCEDURE Sp_GetAllTypes
AS
BEGIN
 BEGIN TRY
  SELECT Name FROM FeedbackDetailType;
 END TRY
 BEGIN CATCH

DECLARE @Message varchar(MAX) = ERROR_MESSAGE(),
@Severity int = ERROR_SEVERITY(),
@State smallint = ERROR_STATE()

RAISERROR (@Message, @Severity, @State)

END CATCH;
END
GO

--Stored procedure for getting all Speakers
--DROP PROCEDURE IF EXISTS Sp_GetAllSpeakers
--GO

-------
--create PROCEDURE Sp_GetAllSpeakers
--AS
--BEGIN
--  BEGIN TRY
--    SELECT Name FROM Users WHERE Role = 'Speaker'
--  END TRY
--  BEGIN CATCH
--    SELECT ERROR_NUMBER() AS ERRORNO,ERROR_LINE() AS ERRORLINE,ERROR_MESSAGE() AS ERRORMSG,
--     ERROR_PROCEDURE() AS ERRORPROCEDURE,ERROR_SEVERITY() AS ERRORSEVERITY
--  END CATCH
--END
--GO

----Stored Procedure for getting all Conductors
--DROP PROCEDURE IF EXISTS Sp_GetAllConductors
--GO

--ALTER PROCEDURE Sp_GetAllConductors
--AS
--BEGIN
--  BEGIN TRY
--    SELECT Name FROM Users WHERE Role = 'Conductor';
--  END TRY
--  BEGIN CATCH
--    SELECT ERROR_NUMBER() AS ERRORNO,ERROR_LINE() AS ERRORLINE,ERROR_MESSAGE() AS ERRORMSG,
--     ERROR_PROCEDURE() AS ERRORPROCEDURE,ERROR_SEVERITY() AS ERRORSEVERITY
--  END CATCH
--END
--GO

--Stored procedure for submitting feedback
DROP PROCEDURE IF EXISTS Sp_SubmitFeedback
GO

CREATE PROCEDURE Sp_SubmitFeedback
(
  @SessionId int,
  @Name varchar(50),
  @Email varchar(25),
  @Mobile varchar(10),
  @Comment varchar(200),
  @IsInformative bit,
  @SpeakerRating int,
  @OverrallRating int
)
AS
BEGIN
  BEGIN TRY
    BEGIN IF(@Name is not null and @Email is not null and @Mobile is not null)
	  INSERT INTO Feedback (SessionId,Email,MobileNumber,Comment,IsInformative,SpeakerRating,OverrallRating,Name)
	  VALUES (SCOPE_IDENTITY(),@Email,@Mobile,@Comment,@IsInformative,@SpeakerRating,@OverrallRating,@Name)
	ELSE
	  return 'Name , Email and Mobile number is required'
	END
  END TRY
  BEGIN CATCH

DECLARE @Message varchar(MAX) = ERROR_MESSAGE(),
@Severity int = ERROR_SEVERITY(),
@State smallint = ERROR_STATE()

RAISERROR (@Message, @Severity, @State)

END CATCH;
END
GO

Exec Sp_SubmitFeedback 1,"project review","resh123@gmail.com","8769513467","good",1,5,10










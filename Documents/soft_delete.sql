-- INITIAL SETUP

-- Delete my database if it already exists
ALTER DATABASE "meeenm_homework2" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
IF db_id('meeenm_homework2') IS NOT NULL BEGIN
    USE master
    DROP DATABASE "meeenm_homework2"
END
GO

-- Create my new database
CREATE DATABASE "meeenm_homework2"
GO

-- Use my database
USE "meeenm_homework2"
GO

-- Create basic temporal table structures with system history enabled
CREATE TABLE AppUsers (
    AppUserId       UNIQUEIDENTIFIER    NOT NULL    PRIMARY KEY     DEFAULT NEWSEQUENTIALID(),
    FirstName       VARCHAR(256),
    LastName        VARCHAR(256),
    IsActive        TINYINT,
    LastActive      DATETIME2,
    DateJoined      DATETIME2,
    SysStartTime    DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime      DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.AppUsersHistory))
GO

CREATE TABLE Gifts (
    GiftId          UNIQUEIDENTIFIER    NOT NULL    PRIMARY KEY     DEFAULT NEWSEQUENTIALID(),
    Name            VARCHAR(256)        NOT NULL,
    Description     VARCHAR(1024),
    Image           NVARCHAR(2048),
    Url             NVARCHAR(2048),
    PartneredUrl    NVARCHAR(2048),
    IsPartnered     TINYINT             NOT NULL,
    IsPinned        TINYINT             NOT NULL,
    AppUserId       UNIQUEIDENTIFIER    NOT NULL,
    SysStartTime    DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime      DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.GiftsHistory))
GO

CREATE TABLE Profiles (
    ProfileId       UNIQUEIDENTIFIER    NOT NULL    PRIMARY KEY     DEFAULT NEWSEQUENTIALID(),
    ProfilePicture  NVARCHAR(2048),
    Gender          VARCHAR(256),
    Bio             VARCHAR(512),
    Age             INT,
    IsPrivate       TINYINT,
    AppUserId       UNIQUEIDENTIFIER    NOT NULL    UNIQUE,
    SysStartTime    DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime      DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.ProfilesHistory))
GO

-- Add foreign key constraints (one-to-many, one-to-one)
ALTER TABLE Gifts ADD CONSTRAINT FK_AppUserGift FOREIGN KEY (AppUserId) REFERENCES AppUsers(AppUserId) ON DELETE CASCADE
ALTER TABLE Profiles ADD CONSTRAINT FK_AppUserProfile FOREIGN KEY (AppUserId) REFERENCES AppUsers(AppUserId) ON DELETE CASCADE

-- Insert data to the tables (with references)
INSERT INTO AppUsers (FirstName, LastName, DateJoined) VALUES ('Chris', 'Harms', GETDATE())
INSERT INTO AppUsers (FirstName, LastName, DateJoined) VALUES ('Mari', 'Poppins', GETDATE())
INSERT INTO Gifts (Name, IsPartnered, IsPinned, AppUserId) VALUES ('Black 15inch laptop bag', 0, 0, (SELECT AppUserId FROM AppUsers WHERE FirstName='Chris' AND LastName='Harms'))
INSERT INTO Gifts (Name, IsPartnered, IsPinned, AppUserId) VALUES ('New electric guitar', 0, 0, (SELECT AppUserId FROM AppUsers WHERE FirstName='Chris' AND LastName='Harms'))
INSERT INTO Gifts (Name, IsPartnered, IsPinned, AppUserId) VALUES ('Red roses', 0, 0, (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins'))
INSERT INTO Gifts (Name, IsPartnered, IsPinned, AppUserId) VALUES ('Test', 0, 0, (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins'))
INSERT INTO Profiles (Age, IsPrivate, AppUserId) VALUES (40, 1, (SELECT AppUserId FROM AppUsers WHERE FirstName='Chris' AND LastName='Harms'));
INSERT INTO Profiles (Age, IsPrivate, AppUserId) VALUES (20, 0, (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins'));
-- Test the uniqueness of AppUserId in Profiles to ensure 1:0-1 relationship. If the following line gives a "Violation of UNIQUE KEY constraint" error it's all good
-- INSERT INTO Profiles (Age, IsPrivate, AppUserId) VALUES (50, 1, (SELECT AppUserId FROM AppUsers WHERE FirstName='Chris' AND LastName='Harms'));

-- Check initial data
SELECT 'Initial data';
SELECT * FROM AppUsers
SELECT * FROM Gifts
SELECT * FROM Profiles


-- TESTING SOFT UPDATE / DELETE IN SINGLE TABLE


-- Modify data (to test versioning in single table).
UPDATE Profiles SET Age=21 where AppUserId like (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins')
SELECT 'Mari age changed 20->21'
SELECT * FROM Profiles
SELECT 'History of Mari ages'
SELECT * FROM Profiles FOR SYSTEM_TIME ALL WHERE AppUserId LIKE (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins') ORDER BY ProfileId, SysStartTime Asc

-- Delete data (to test versioning in single table).
DELETE FROM Profiles WHERE Age like 21
SELECT 'Mari profile deleted'
SELECT * FROM Profiles
SELECT 'History of Mari profiles'
SELECT * FROM Profiles FOR SYSTEM_TIME ALL WHERE AppUserId LIKE (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins') ORDER BY ProfileId, SysStartTime Asc

-- Add deleted data back (to test versioning in single table).
INSERT INTO Profiles (Age, IsPrivate, AppUserId) VALUES (21, 1, (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins'))
SELECT 'Mari profile added back'
SELECT * FROM Profiles
SELECT 'History of Mari profiles'
SELECT * FROM Profiles FOR SYSTEM_TIME ALL WHERE AppUserId LIKE (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins') ORDER BY ProfileId, SysStartTime Asc

-- Test the change/delete on Gifts table as well (to test versioning in one table)
UPDATE Gifts SET Description='Dark red!' WHERE Name LIKE 'Red roses'
DELETE FROM Gifts WHERE Name like 'Test'
SELECT 'Updated Gifts table'
SELECT * FROM Gifts

-- Get data at some specific date in time (show no changes that were before or after)
SELECT * FROM dbo.Profiles FOR SYSTEM_TIME AS OF '2020-03-08 16:15:53.6186957' ORDER BY ProfileId, SysStartTime Desc
-- Get data during some specific period in time
SELECT * FROM dbo.Profiles FOR SYSTEM_TIME BETWEEN '2020-03-08 16:15:53.6062784' AND '2020-03-08 16:15:53.6186957' ORDER BY ProfileId, SysStartTime Desc


-- CHECK THE WHOLE CHANGE HISTORY IN TABLES


-- Profile table history
SELECT 'Profile table active rows'
SELECT * FROM dbo.Profiles ORDER BY ProfileId, SysStartTime Desc
SELECT 'Profile table with history rows'
SELECT * FROM dbo.Profiles FOR SYSTEM_TIME ALL ORDER BY ProfileId, SysStartTime Desc
SELECT 'ProfilesHistory table itself'
SELECT * FROM dbo.ProfilesHistory ORDER BY ProfileId, SysStartTime Desc
-- Gift table history
SELECT 'Gift table active rows'
SELECT * FROM dbo.Gifts ORDER BY GiftId, SysStartTime Desc
SELECT 'Gift table with history rows'
SELECT * FROM dbo.Gifts FOR SYSTEM_TIME ALL ORDER BY GiftId, SysStartTime Desc
SELECT 'GiftsHistory table itself'
SELECT * FROM dbo.GiftsHistory  ORDER BY GiftId, SysStartTime Desc


-- TESTING SOFT UPDATE / DELETE IN 1:M RELATIONSHIP


-- Modify data (to test versioning in 1:m table)
SELECT 'AppUsers and their Gifts'
SELECT AppUsers.FirstName, Gifts.Name FROM Gifts INNER JOIN AppUsers ON AppUsers.AppUserId = Gifts.AppUserId 

UPDATE Gifts SET Name='Guitar' WHERE Name LIKE 'New electric guitar'
SELECT 'AppUsers and changed Gift'
SELECT AppUsers.FirstName, Gifts.Name FROM Gifts INNER JOIN AppUsers ON AppUsers.AppUserId = Gifts.AppUserId 

SELECT 'AppUsers and Gifts history'
SELECT AppUsers.FirstName, Gifts.name, Gifts.SysStartTime, Gifts.SysEndTime
FROM Gifts
FOR SYSTEM_TIME ALL
LEFT JOIN AppUsers ON Gifts.AppUserId = AppUsers.AppUserId

-- Delete data (to test versioning in 1:m table)
SELECT 'AppUsers and their Gifts'
SELECT AppUsers.FirstName, Gifts.Name FROM Gifts INNER JOIN AppUsers ON AppUsers.AppUserId = Gifts.AppUserId 

-- Delete a gift
DELETE FROM Gifts WHERE Name like 'Black 15inch laptop bag';
SELECT 'AppUsers and deleted Gift'
SELECT AppUsers.FirstName, Gifts.Name FROM Gifts INNER JOIN AppUsers ON AppUsers.AppUserId = Gifts.AppUserId 

-- Delete a user (cascade)
DELETE FROM AppUsers WHERE FirstName LIKE 'Mari'
SELECT 'Existing AppUsers'
SELECT * FROM AppUsers
SELECT 'Existing Gifts'
SELECT * FROM Gifts

SELECT 'Existing AppUsers Gifts'
SELECT AppUsers.FirstName, Gifts.name, Gifts.SysStartTime, Gifts.SysEndTime
FROM Gifts
LEFT JOIN AppUsers ON Gifts.AppUserId = AppUsers.AppUserId

SELECT 'AppUsers history'
SELECT * FROM AppUsersHistory
SELECT 'Gifts history'
SELECT * FROM GiftsHistory

SELECT 'Deleted AppUser Gifts history'
SELECT AppUsersHistory.FirstName, GiftsHistory.Name, GiftsHistory.SysStartTime, GiftsHistory.SysEndTime
FROM GiftsHistory
RIGHT JOIN AppUsersHistory ON GiftsHistory.AppUserId = AppUsersHistory.AppUserId


-- TESTING SOFT UPDATE / DELETE IN 1:0-1 RELATIONSHIP


-- Add deleted user and their data back (for testing)
INSERT INTO AppUsers (FirstName, LastName, DateJoined) VALUES ('Mari', 'Poppins', GETDATE())
INSERT INTO Gifts (Name, IsPartnered, IsPinned, AppUserId) VALUES ('Red roses', 0, 0, (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins'))
INSERT INTO Gifts (Name, IsPartnered, IsPinned, AppUserId) VALUES ('Test', 0, 0, (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins'))
INSERT INTO Profiles (Age, IsPrivate, AppUserId) VALUES (20, 0, (SELECT AppUserId FROM AppUsers WHERE FirstName='Mari' AND LastName='Poppins'));

-- Modify data (to test versioning in 1:0-1 table)
SELECT 'AppUsers and their Age'
SELECT AppUsers.FirstName, Profiles.Age FROM Profiles INNER JOIN AppUsers ON AppUsers.AppUserId = Profiles.AppUserId 

-- Change data in Profile (child) table
UPDATE Profiles SET Age='45' WHERE AppUserId LIKE (SELECT AppUserId FROM AppUsers WHERE FirstName='Chris' AND LastName='Harms')
SELECT 'AppUsers and changed Age'
SELECT AppUsers.FirstName, Profiles.Age FROM Profiles INNER JOIN AppUsers ON AppUsers.AppUserId = Profiles.AppUserId 

SELECT 'AppUsers age change history'
SELECT AppUsers.FirstName, Profiles.Age, Profiles.SysStartTime, Profiles.SysEndTime
FROM Profiles
FOR SYSTEM_TIME ALL
LEFT JOIN AppUsers ON Profiles.AppUserId = AppUsers.AppUserId
WHERE AppUsers.FirstName LIKE 'Chris'

-- Change data in AppUsers (parent) table
UPDATE AppUsers SET FirstName='Mart' WHERE AppUsers.FirstName LIKE 'Chris'
SELECT 'AppUsers with new name'
SELECT * FROM AppUsers

SELECT 'AppUsers name change history'
SELECT AppUsersHistory.FirstName, AppUsers.FirstName, AppUsers.SysStartTime, AppUsers.SysEndTime
FROM AppUsersHistory
LEFT JOIN AppUsers ON AppUsers.AppUserId = AppUsersHistory.AppUserId
WHERE AppUsers.FirstName IS NOT NULL

-- Delete data (to test versioning in 1:0-1 table)

-- Delete Profile (child)
DELETE FROM Profiles WHERE AppUserId LIKE (SELECT AppUserId FROM AppUsers WHERE FirstName='Mart' AND LastName='Harms')
SELECT 'Existing profiles'
SELECT * FROM Profiles
SELECT 'History of profiles'
SELECT * FROM ProfilesHistory

-- Delete AppUser (parent, cascade)
DELETE FROM AppUsers WHERE AppUserId LIKE (SELECT AppUserId FROM AppUsers WHERE FirstName='Mart' AND LastName='Harms')
SELECT 'Existing users'
SELECT * FROM AppUsers
SELECT 'History of users'
SELECT * FROM AppUsersHistory
SELECT 'Existing profiles'
SELECT * FROM Profiles
SELECT 'History of profiles'
SELECT * FROM ProfilesHistory

SELECT 'Existing AppUser Profiles'
SELECT AppUsers.FirstName, Profiles.Age, Profiles.SysStartTime, Profiles.SysEndTime
FROM Profiles
RIGHT JOIN AppUsers ON AppUsers.AppUserId = Profiles.AppUserId

SELECT 'Deleted AppUser Profiles'
SELECT AppUsersHistory.FirstName, ProfilesHistory.Age, ProfilesHistory.SysStartTime, ProfilesHistory.SysEndTime
FROM ProfilesHistory
RIGHT JOIN AppUsersHistory ON ProfilesHistory.AppUserId = AppUsersHistory.AppUserId

-- Add user/profile back
INSERT INTO AppUsers (FirstName, LastName, DateJoined) VALUES ('Chris', 'Harms', GETDATE())
INSERT INTO Profiles (Age, IsPrivate, AppUserId) VALUES (40, 1, (SELECT AppUserId FROM AppUsers WHERE FirstName='Chris' AND LastName='Harms'));
SELECT 'User and profile added back'
SELECT * FROM AppUsers
SELECT * FROM Profiles




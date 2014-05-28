
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/26/2014 16:56:07
-- Generated from EDMX file: D:\Documentos\Projects\devenv\Projects\MyVanity\MyVanity\Source\MyVanity\MyVanity.Domain\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyVanity];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SentMessages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_SentMessages];
GO
IF OBJECT_ID(N'[dbo].[FK_Inbox]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Inbox];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientUserProcedure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProcedures] DROP CONSTRAINT [FK_PatientUserProcedure];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcedureUserProcedure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProcedures] DROP CONSTRAINT [FK_ProcedureUserProcedure];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageAttachments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents_MessageAttachment] DROP CONSTRAINT [FK_MessageAttachments];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientDocuments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents_PatientDocument] DROP CONSTRAINT [FK_PatientDocuments];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProcedureAppointment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT [FK_UserProcedureAppointment];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcedureCategoryProcedure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Procedures] DROP CONSTRAINT [FK_ProcedureCategoryProcedure];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentCategoryDocumentSubcategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentSubcategories] DROP CONSTRAINT [FK_DocumentCategoryDocumentSubcategory];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentCategoryDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_DocumentCategoryDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentSubcategoryDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_DocumentSubcategoryDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcedureTypeProcedure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Procedures] DROP CONSTRAINT [FK_ProcedureTypeProcedure];
GO
IF OBJECT_ID(N'[dbo].[FK_AgentPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Patient] DROP CONSTRAINT [FK_AgentPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorUserProcedure_Doctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DoctorUserProcedure] DROP CONSTRAINT [FK_DoctorUserProcedure_Doctor];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorUserProcedure_UserProcedure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DoctorUserProcedure] DROP CONSTRAINT [FK_DoctorUserProcedure_UserProcedure];
GO
IF OBJECT_ID(N'[dbo].[FK_AgentUserProcedure_Agent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AgentUserProcedure] DROP CONSTRAINT [FK_AgentUserProcedure_Agent];
GO
IF OBJECT_ID(N'[dbo].[FK_AgentUserProcedure_UserProcedure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AgentUserProcedure] DROP CONSTRAINT [FK_AgentUserProcedure_UserProcedure];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProcedureSharedDocument_UserProcedure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProcedureSharedDocument] DROP CONSTRAINT [FK_UserProcedureSharedDocument_UserProcedure];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProcedureSharedDocument_SharedDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProcedureSharedDocument] DROP CONSTRAINT [FK_UserProcedureSharedDocument_SharedDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProcedureUserProcedurePatientDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents_UserProcedurePatientDocument] DROP CONSTRAINT [FK_UserProcedureUserProcedurePatientDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientUserProcedurePatientDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents_UserProcedurePatientDocument] DROP CONSTRAINT [FK_PatientUserProcedurePatientDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_AdminReport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reports] DROP CONSTRAINT [FK_AdminReport];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProcedureUserProcedureConsentSign]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProcedureConsentSign] DROP CONSTRAINT [FK_UserProcedureUserProcedureConsentSign];
GO
IF OBJECT_ID(N'[dbo].[FK_ConsentFormUserProcedureConsentSign]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProcedureConsentSign] DROP CONSTRAINT [FK_ConsentFormUserProcedureConsentSign];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_MessageMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProcedurePlace]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProcedures] DROP CONSTRAINT [FK_UserProcedurePlace];
GO
IF OBJECT_ID(N'[dbo].[FK_AppointmentPlace]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT [FK_AppointmentPlace];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Patient] DROP CONSTRAINT [FK_Patient_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageAttachment_inherits_Document]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents_MessageAttachment] DROP CONSTRAINT [FK_MessageAttachment_inherits_Document];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientDocument_inherits_Document]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents_PatientDocument] DROP CONSTRAINT [FK_PatientDocument_inherits_Document];
GO
IF OBJECT_ID(N'[dbo].[FK_Agent_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Agent] DROP CONSTRAINT [FK_Agent_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_SharedDocument_inherits_Document]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents_SharedDocument] DROP CONSTRAINT [FK_SharedDocument_inherits_Document];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProcedurePatientDocument_inherits_Document]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents_UserProcedurePatientDocument] DROP CONSTRAINT [FK_UserProcedurePatientDocument_inherits_Document];
GO
IF OBJECT_ID(N'[dbo].[FK_Admin_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Admin] DROP CONSTRAINT [FK_Admin_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[Procedures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Procedures];
GO
IF OBJECT_ID(N'[dbo].[UserProcedures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProcedures];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[Appointments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Appointments];
GO
IF OBJECT_ID(N'[dbo].[ProcedureCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcedureCategories];
GO
IF OBJECT_ID(N'[dbo].[DocumentCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentCategories];
GO
IF OBJECT_ID(N'[dbo].[DocumentSubcategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentSubcategories];
GO
IF OBJECT_ID(N'[dbo].[ProcedureTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcedureTypes];
GO
IF OBJECT_ID(N'[dbo].[Doctors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctors];
GO
IF OBJECT_ID(N'[dbo].[SystemConfigurations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemConfigurations];
GO
IF OBJECT_ID(N'[dbo].[ConsentForms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsentForms];
GO
IF OBJECT_ID(N'[dbo].[Reports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reports];
GO
IF OBJECT_ID(N'[dbo].[UserProcedureConsentSign]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProcedureConsentSign];
GO
IF OBJECT_ID(N'[dbo].[Places]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Places];
GO
IF OBJECT_ID(N'[dbo].[Users_Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Patient];
GO
IF OBJECT_ID(N'[dbo].[Documents_MessageAttachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents_MessageAttachment];
GO
IF OBJECT_ID(N'[dbo].[Documents_PatientDocument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents_PatientDocument];
GO
IF OBJECT_ID(N'[dbo].[Users_Agent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Agent];
GO
IF OBJECT_ID(N'[dbo].[Documents_SharedDocument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents_SharedDocument];
GO
IF OBJECT_ID(N'[dbo].[Documents_UserProcedurePatientDocument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents_UserProcedurePatientDocument];
GO
IF OBJECT_ID(N'[dbo].[Users_Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Admin];
GO
IF OBJECT_ID(N'[dbo].[DoctorUserProcedure]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DoctorUserProcedure];
GO
IF OBJECT_ID(N'[dbo].[AgentUserProcedure]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AgentUserProcedure];
GO
IF OBJECT_ID(N'[dbo].[UserProcedureSharedDocument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProcedureSharedDocument];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Subject] nvarchar(max)  NOT NULL,
    [Body] nvarchar(max)  NULL,
    [ToId] int  NOT NULL,
    [FromId] int  NOT NULL,
    [IsRead] bit  NOT NULL,
    [Date] datetime  NOT NULL,
    [MessageMessage_Message1_Id] int  NULL
);
GO

-- Creating table 'Procedures'
CREATE TABLE [dbo].[Procedures] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [TypeId] int  NOT NULL,
    [RegularPrice] float  NULL,
    [SalePrice] float  NULL,
    [PicPath] nvarchar(max)  NULL
);
GO

-- Creating table 'UserProcedures'
CREATE TABLE [dbo].[UserProcedures] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PatientId] int  NOT NULL,
    [ProcedureId] int  NOT NULL,
    [PlaceId] int  NOT NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [Censured] bit  NOT NULL,
    [CategoryId] int  NOT NULL,
    [SubcategoryId] int  NULL,
    [ContentType] int  NULL,
    [Description] nvarchar(max)  NULL,
    [RealName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Appointments'
CREATE TABLE [dbo].[Appointments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserProcedureId] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Status] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [PlaceId] int  NOT NULL
);
GO

-- Creating table 'ProcedureCategories'
CREATE TABLE [dbo].[ProcedureCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DocumentCategories'
CREATE TABLE [dbo].[DocumentCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DocumentSubcategories'
CREATE TABLE [dbo].[DocumentSubcategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProcedureTypes'
CREATE TABLE [dbo].[ProcedureTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonDetails_FirstName] nvarchar(max)  NOT NULL,
    [PersonDetails_LastName] nvarchar(max)  NOT NULL,
    [PersonDetails_MiddleName] nvarchar(max)  NULL,
    [PersonDetails_Sex] int  NOT NULL,
    [PersonDetails_DOB] datetime  NULL,
    [ProcedureId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NULL,
    [PicPath] nvarchar(max)  NULL
);
GO

-- Creating table 'SystemConfigurations'
CREATE TABLE [dbo].[SystemConfigurations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ConsentForms'
CREATE TABLE [dbo].[ConsentForms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [FilePath] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Reports'
CREATE TABLE [dbo].[Reports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Trigger] int  NOT NULL,
    [AdminId] int  NOT NULL
);
GO

-- Creating table 'UserProcedureConsentSign'
CREATE TABLE [dbo].[UserProcedureConsentSign] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserProcedureId] int  NOT NULL,
    [ConsentFormId] int  NOT NULL,
    [Signed] bit  NOT NULL
);
GO

-- Creating table 'Places'
CREATE TABLE [dbo].[Places] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users_Patient'
CREATE TABLE [dbo].[Users_Patient] (
    [Profile_FirstName] nvarchar(max)  NOT NULL,
    [Profile_LastName] nvarchar(max)  NOT NULL,
    [Profile_MiddleName] nvarchar(max)  NULL,
    [Profile_Sex] int  NOT NULL,
    [Profile_DOB] datetime  NULL,
    [Contact_Phone_Home] nvarchar(max)  NULL,
    [Contact_Phone_Work] nvarchar(max)  NULL,
    [Contact_Phone_Mobile] nvarchar(max)  NULL,
    [Contact_Social_Facebook] nvarchar(max)  NULL,
    [Contact_Social_Twitter] nvarchar(max)  NULL,
    [Contact_Address] nvarchar(max)  NOT NULL,
    [Contact_Address1] nvarchar(max)  NULL,
    [Contact_ZipCode] nvarchar(max)  NULL,
    [AgentId] int  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PicPath] nvarchar(max)  NULL,
    [PatientNumber] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Documents_MessageAttachment'
CREATE TABLE [dbo].[Documents_MessageAttachment] (
    [MessageId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Documents_PatientDocument'
CREATE TABLE [dbo].[Documents_PatientDocument] (
    [PatientId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Users_Agent'
CREATE TABLE [dbo].[Users_Agent] (
    [PersonDetails_FirstName] nvarchar(max)  NOT NULL,
    [PersonDetails_LastName] nvarchar(max)  NOT NULL,
    [PersonDetails_MiddleName] nvarchar(max)  NULL,
    [PersonDetails_Sex] int  NOT NULL,
    [PersonDetails_DOB] datetime  NULL,
    [Type] int  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PicPath] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Documents_SharedDocument'
CREATE TABLE [dbo].[Documents_SharedDocument] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'Documents_UserProcedurePatientDocument'
CREATE TABLE [dbo].[Documents_UserProcedurePatientDocument] (
    [UserProcedureId] int  NOT NULL,
    [PatientId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Users_Admin'
CREATE TABLE [dbo].[Users_Admin] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'DoctorUserProcedure'
CREATE TABLE [dbo].[DoctorUserProcedure] (
    [Doctors_Id] int  NOT NULL,
    [UserProcedures_Id] int  NOT NULL
);
GO

-- Creating table 'AgentUserProcedure'
CREATE TABLE [dbo].[AgentUserProcedure] (
    [Agents_Id] int  NOT NULL,
    [UserProcedures_Id] int  NOT NULL
);
GO

-- Creating table 'UserProcedureSharedDocument'
CREATE TABLE [dbo].[UserProcedureSharedDocument] (
    [UserProcedures_Id] int  NOT NULL,
    [SharedDocuments_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Procedures'
ALTER TABLE [dbo].[Procedures]
ADD CONSTRAINT [PK_Procedures]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserProcedures'
ALTER TABLE [dbo].[UserProcedures]
ADD CONSTRAINT [PK_UserProcedures]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [PK_Appointments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProcedureCategories'
ALTER TABLE [dbo].[ProcedureCategories]
ADD CONSTRAINT [PK_ProcedureCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentCategories'
ALTER TABLE [dbo].[DocumentCategories]
ADD CONSTRAINT [PK_DocumentCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentSubcategories'
ALTER TABLE [dbo].[DocumentSubcategories]
ADD CONSTRAINT [PK_DocumentSubcategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProcedureTypes'
ALTER TABLE [dbo].[ProcedureTypes]
ADD CONSTRAINT [PK_ProcedureTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [PK_Doctors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SystemConfigurations'
ALTER TABLE [dbo].[SystemConfigurations]
ADD CONSTRAINT [PK_SystemConfigurations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConsentForms'
ALTER TABLE [dbo].[ConsentForms]
ADD CONSTRAINT [PK_ConsentForms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [PK_Reports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserProcedureConsentSign'
ALTER TABLE [dbo].[UserProcedureConsentSign]
ADD CONSTRAINT [PK_UserProcedureConsentSign]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Places'
ALTER TABLE [dbo].[Places]
ADD CONSTRAINT [PK_Places]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users_Patient'
ALTER TABLE [dbo].[Users_Patient]
ADD CONSTRAINT [PK_Users_Patient]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents_MessageAttachment'
ALTER TABLE [dbo].[Documents_MessageAttachment]
ADD CONSTRAINT [PK_Documents_MessageAttachment]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents_PatientDocument'
ALTER TABLE [dbo].[Documents_PatientDocument]
ADD CONSTRAINT [PK_Documents_PatientDocument]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users_Agent'
ALTER TABLE [dbo].[Users_Agent]
ADD CONSTRAINT [PK_Users_Agent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents_SharedDocument'
ALTER TABLE [dbo].[Documents_SharedDocument]
ADD CONSTRAINT [PK_Documents_SharedDocument]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents_UserProcedurePatientDocument'
ALTER TABLE [dbo].[Documents_UserProcedurePatientDocument]
ADD CONSTRAINT [PK_Documents_UserProcedurePatientDocument]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users_Admin'
ALTER TABLE [dbo].[Users_Admin]
ADD CONSTRAINT [PK_Users_Admin]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Doctors_Id], [UserProcedures_Id] in table 'DoctorUserProcedure'
ALTER TABLE [dbo].[DoctorUserProcedure]
ADD CONSTRAINT [PK_DoctorUserProcedure]
    PRIMARY KEY CLUSTERED ([Doctors_Id], [UserProcedures_Id] ASC);
GO

-- Creating primary key on [Agents_Id], [UserProcedures_Id] in table 'AgentUserProcedure'
ALTER TABLE [dbo].[AgentUserProcedure]
ADD CONSTRAINT [PK_AgentUserProcedure]
    PRIMARY KEY CLUSTERED ([Agents_Id], [UserProcedures_Id] ASC);
GO

-- Creating primary key on [UserProcedures_Id], [SharedDocuments_Id] in table 'UserProcedureSharedDocument'
ALTER TABLE [dbo].[UserProcedureSharedDocument]
ADD CONSTRAINT [PK_UserProcedureSharedDocument]
    PRIMARY KEY CLUSTERED ([UserProcedures_Id], [SharedDocuments_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FromId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_SentMessages]
    FOREIGN KEY ([FromId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SentMessages'
CREATE INDEX [IX_FK_SentMessages]
ON [dbo].[Messages]
    ([FromId]);
GO

-- Creating foreign key on [ToId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Inbox]
    FOREIGN KEY ([ToId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Inbox'
CREATE INDEX [IX_FK_Inbox]
ON [dbo].[Messages]
    ([ToId]);
GO

-- Creating foreign key on [PatientId] in table 'UserProcedures'
ALTER TABLE [dbo].[UserProcedures]
ADD CONSTRAINT [FK_PatientUserProcedure]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Users_Patient]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientUserProcedure'
CREATE INDEX [IX_FK_PatientUserProcedure]
ON [dbo].[UserProcedures]
    ([PatientId]);
GO

-- Creating foreign key on [ProcedureId] in table 'UserProcedures'
ALTER TABLE [dbo].[UserProcedures]
ADD CONSTRAINT [FK_ProcedureUserProcedure]
    FOREIGN KEY ([ProcedureId])
    REFERENCES [dbo].[Procedures]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcedureUserProcedure'
CREATE INDEX [IX_FK_ProcedureUserProcedure]
ON [dbo].[UserProcedures]
    ([ProcedureId]);
GO

-- Creating foreign key on [MessageId] in table 'Documents_MessageAttachment'
ALTER TABLE [dbo].[Documents_MessageAttachment]
ADD CONSTRAINT [FK_MessageAttachments]
    FOREIGN KEY ([MessageId])
    REFERENCES [dbo].[Messages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageAttachments'
CREATE INDEX [IX_FK_MessageAttachments]
ON [dbo].[Documents_MessageAttachment]
    ([MessageId]);
GO

-- Creating foreign key on [PatientId] in table 'Documents_PatientDocument'
ALTER TABLE [dbo].[Documents_PatientDocument]
ADD CONSTRAINT [FK_PatientDocuments]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Users_Patient]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientDocuments'
CREATE INDEX [IX_FK_PatientDocuments]
ON [dbo].[Documents_PatientDocument]
    ([PatientId]);
GO

-- Creating foreign key on [UserProcedureId] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [FK_UserProcedureAppointment]
    FOREIGN KEY ([UserProcedureId])
    REFERENCES [dbo].[UserProcedures]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProcedureAppointment'
CREATE INDEX [IX_FK_UserProcedureAppointment]
ON [dbo].[Appointments]
    ([UserProcedureId]);
GO

-- Creating foreign key on [CategoryId] in table 'Procedures'
ALTER TABLE [dbo].[Procedures]
ADD CONSTRAINT [FK_ProcedureCategoryProcedure]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[ProcedureCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcedureCategoryProcedure'
CREATE INDEX [IX_FK_ProcedureCategoryProcedure]
ON [dbo].[Procedures]
    ([CategoryId]);
GO

-- Creating foreign key on [CategoryId] in table 'DocumentSubcategories'
ALTER TABLE [dbo].[DocumentSubcategories]
ADD CONSTRAINT [FK_DocumentCategoryDocumentSubcategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[DocumentCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentCategoryDocumentSubcategory'
CREATE INDEX [IX_FK_DocumentCategoryDocumentSubcategory]
ON [dbo].[DocumentSubcategories]
    ([CategoryId]);
GO

-- Creating foreign key on [CategoryId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_DocumentCategoryDocument]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[DocumentCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentCategoryDocument'
CREATE INDEX [IX_FK_DocumentCategoryDocument]
ON [dbo].[Documents]
    ([CategoryId]);
GO

-- Creating foreign key on [SubcategoryId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_DocumentSubcategoryDocument]
    FOREIGN KEY ([SubcategoryId])
    REFERENCES [dbo].[DocumentSubcategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentSubcategoryDocument'
CREATE INDEX [IX_FK_DocumentSubcategoryDocument]
ON [dbo].[Documents]
    ([SubcategoryId]);
GO

-- Creating foreign key on [TypeId] in table 'Procedures'
ALTER TABLE [dbo].[Procedures]
ADD CONSTRAINT [FK_ProcedureTypeProcedure]
    FOREIGN KEY ([TypeId])
    REFERENCES [dbo].[ProcedureTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcedureTypeProcedure'
CREATE INDEX [IX_FK_ProcedureTypeProcedure]
ON [dbo].[Procedures]
    ([TypeId]);
GO

-- Creating foreign key on [AgentId] in table 'Users_Patient'
ALTER TABLE [dbo].[Users_Patient]
ADD CONSTRAINT [FK_AgentPatient]
    FOREIGN KEY ([AgentId])
    REFERENCES [dbo].[Users_Agent]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AgentPatient'
CREATE INDEX [IX_FK_AgentPatient]
ON [dbo].[Users_Patient]
    ([AgentId]);
GO

-- Creating foreign key on [Doctors_Id] in table 'DoctorUserProcedure'
ALTER TABLE [dbo].[DoctorUserProcedure]
ADD CONSTRAINT [FK_DoctorUserProcedure_Doctor]
    FOREIGN KEY ([Doctors_Id])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserProcedures_Id] in table 'DoctorUserProcedure'
ALTER TABLE [dbo].[DoctorUserProcedure]
ADD CONSTRAINT [FK_DoctorUserProcedure_UserProcedure]
    FOREIGN KEY ([UserProcedures_Id])
    REFERENCES [dbo].[UserProcedures]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorUserProcedure_UserProcedure'
CREATE INDEX [IX_FK_DoctorUserProcedure_UserProcedure]
ON [dbo].[DoctorUserProcedure]
    ([UserProcedures_Id]);
GO

-- Creating foreign key on [Agents_Id] in table 'AgentUserProcedure'
ALTER TABLE [dbo].[AgentUserProcedure]
ADD CONSTRAINT [FK_AgentUserProcedure_Agent]
    FOREIGN KEY ([Agents_Id])
    REFERENCES [dbo].[Users_Agent]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserProcedures_Id] in table 'AgentUserProcedure'
ALTER TABLE [dbo].[AgentUserProcedure]
ADD CONSTRAINT [FK_AgentUserProcedure_UserProcedure]
    FOREIGN KEY ([UserProcedures_Id])
    REFERENCES [dbo].[UserProcedures]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AgentUserProcedure_UserProcedure'
CREATE INDEX [IX_FK_AgentUserProcedure_UserProcedure]
ON [dbo].[AgentUserProcedure]
    ([UserProcedures_Id]);
GO

-- Creating foreign key on [UserProcedures_Id] in table 'UserProcedureSharedDocument'
ALTER TABLE [dbo].[UserProcedureSharedDocument]
ADD CONSTRAINT [FK_UserProcedureSharedDocument_UserProcedure]
    FOREIGN KEY ([UserProcedures_Id])
    REFERENCES [dbo].[UserProcedures]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SharedDocuments_Id] in table 'UserProcedureSharedDocument'
ALTER TABLE [dbo].[UserProcedureSharedDocument]
ADD CONSTRAINT [FK_UserProcedureSharedDocument_SharedDocument]
    FOREIGN KEY ([SharedDocuments_Id])
    REFERENCES [dbo].[Documents_SharedDocument]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProcedureSharedDocument_SharedDocument'
CREATE INDEX [IX_FK_UserProcedureSharedDocument_SharedDocument]
ON [dbo].[UserProcedureSharedDocument]
    ([SharedDocuments_Id]);
GO

-- Creating foreign key on [UserProcedureId] in table 'Documents_UserProcedurePatientDocument'
ALTER TABLE [dbo].[Documents_UserProcedurePatientDocument]
ADD CONSTRAINT [FK_UserProcedureUserProcedurePatientDocument]
    FOREIGN KEY ([UserProcedureId])
    REFERENCES [dbo].[UserProcedures]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProcedureUserProcedurePatientDocument'
CREATE INDEX [IX_FK_UserProcedureUserProcedurePatientDocument]
ON [dbo].[Documents_UserProcedurePatientDocument]
    ([UserProcedureId]);
GO

-- Creating foreign key on [PatientId] in table 'Documents_UserProcedurePatientDocument'
ALTER TABLE [dbo].[Documents_UserProcedurePatientDocument]
ADD CONSTRAINT [FK_PatientUserProcedurePatientDocument]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Users_Patient]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientUserProcedurePatientDocument'
CREATE INDEX [IX_FK_PatientUserProcedurePatientDocument]
ON [dbo].[Documents_UserProcedurePatientDocument]
    ([PatientId]);
GO

-- Creating foreign key on [AdminId] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [FK_AdminReport]
    FOREIGN KEY ([AdminId])
    REFERENCES [dbo].[Users_Admin]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AdminReport'
CREATE INDEX [IX_FK_AdminReport]
ON [dbo].[Reports]
    ([AdminId]);
GO

-- Creating foreign key on [UserProcedureId] in table 'UserProcedureConsentSign'
ALTER TABLE [dbo].[UserProcedureConsentSign]
ADD CONSTRAINT [FK_UserProcedureUserProcedureConsentSign]
    FOREIGN KEY ([UserProcedureId])
    REFERENCES [dbo].[UserProcedures]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProcedureUserProcedureConsentSign'
CREATE INDEX [IX_FK_UserProcedureUserProcedureConsentSign]
ON [dbo].[UserProcedureConsentSign]
    ([UserProcedureId]);
GO

-- Creating foreign key on [ConsentFormId] in table 'UserProcedureConsentSign'
ALTER TABLE [dbo].[UserProcedureConsentSign]
ADD CONSTRAINT [FK_ConsentFormUserProcedureConsentSign]
    FOREIGN KEY ([ConsentFormId])
    REFERENCES [dbo].[ConsentForms]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConsentFormUserProcedureConsentSign'
CREATE INDEX [IX_FK_ConsentFormUserProcedureConsentSign]
ON [dbo].[UserProcedureConsentSign]
    ([ConsentFormId]);
GO

-- Creating foreign key on [MessageMessage_Message1_Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessageMessage]
    FOREIGN KEY ([MessageMessage_Message1_Id])
    REFERENCES [dbo].[Messages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageMessage'
CREATE INDEX [IX_FK_MessageMessage]
ON [dbo].[Messages]
    ([MessageMessage_Message1_Id]);
GO

-- Creating foreign key on [PlaceId] in table 'UserProcedures'
ALTER TABLE [dbo].[UserProcedures]
ADD CONSTRAINT [FK_UserProcedurePlace]
    FOREIGN KEY ([PlaceId])
    REFERENCES [dbo].[Places]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProcedurePlace'
CREATE INDEX [IX_FK_UserProcedurePlace]
ON [dbo].[UserProcedures]
    ([PlaceId]);
GO

-- Creating foreign key on [PlaceId] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [FK_AppointmentPlace]
    FOREIGN KEY ([PlaceId])
    REFERENCES [dbo].[Places]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AppointmentPlace'
CREATE INDEX [IX_FK_AppointmentPlace]
ON [dbo].[Appointments]
    ([PlaceId]);
GO

-- Creating foreign key on [Id] in table 'Users_Patient'
ALTER TABLE [dbo].[Users_Patient]
ADD CONSTRAINT [FK_Patient_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Documents_MessageAttachment'
ALTER TABLE [dbo].[Documents_MessageAttachment]
ADD CONSTRAINT [FK_MessageAttachment_inherits_Document]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Documents]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Documents_PatientDocument'
ALTER TABLE [dbo].[Documents_PatientDocument]
ADD CONSTRAINT [FK_PatientDocument_inherits_Document]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Documents]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Users_Agent'
ALTER TABLE [dbo].[Users_Agent]
ADD CONSTRAINT [FK_Agent_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Documents_SharedDocument'
ALTER TABLE [dbo].[Documents_SharedDocument]
ADD CONSTRAINT [FK_SharedDocument_inherits_Document]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Documents]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Documents_UserProcedurePatientDocument'
ALTER TABLE [dbo].[Documents_UserProcedurePatientDocument]
ADD CONSTRAINT [FK_UserProcedurePatientDocument_inherits_Document]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Documents]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Users_Admin'
ALTER TABLE [dbo].[Users_Admin]
ADD CONSTRAINT [FK_Admin_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
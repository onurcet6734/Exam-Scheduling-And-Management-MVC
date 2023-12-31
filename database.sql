USE [ESandMSDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8.06.2023 17:29:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Classes]    Script Date: 8.06.2023 17:29:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Year] [int] NOT NULL,
	[Semester] [nvarchar](max) NOT NULL,
	[NumberOfStudent] [int] NOT NULL,
 CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Exams]    Script Date: 8.06.2023 17:29:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Exams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Halls]    Script Date: 8.06.2023 17:29:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Halls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[NumberOfSeats] [int] NOT NULL,
 CONSTRAINT [PK_Halls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Logins]    Script Date: 8.06.2023 17:29:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Roles] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Logins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Schedulings]    Script Date: 8.06.2023 17:29:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedulings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaperName] [nvarchar](max) NOT NULL,
	[ExamDate] [datetime2](7) NOT NULL,
	[ExamTime] [time](7) NOT NULL,
	[Duration] [int] NOT NULL,
	[HallId] [int] NOT NULL,
	[ExamId] [int] NOT NULL,
	[StudentId] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Schedulings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 8.06.2023 17:29:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Surname] [nvarchar](max) NOT NULL,
	[SchoolNumber] [nvarchar](max) NOT NULL,
	[LoginId] [int] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230509093346_test', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230530160241_mig_2', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230530210521_mig_3', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230530213034_mig_4', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230530213433_mig_4', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230530213626_mig_5', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230601141300_mig_6', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230601142322_mig_7', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602130535_mig_8', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602141556_mig_9', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602141838_mig_10', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602142141_mig_10', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602142303_mig_10', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602143132_mig_10', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602144013_mig_10', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602144449_mig_10', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602202441_mig_6', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602203045_mig_6', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230602203346_mig_6', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230605203027_mig_6', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230606232929_mig_7', N'7.0.3')
SET IDENTITY_INSERT [dbo].[Classes] ON 

INSERT [dbo].[Classes] ([Id], [Name], [Year], [Semester], [NumberOfStudent]) VALUES (16, N'Computer Enginnering', 4, N'Fall', 60)
INSERT [dbo].[Classes] ([Id], [Name], [Year], [Semester], [NumberOfStudent]) VALUES (18, N'Machine Enginnering', 4, N'Fall', 75)
SET IDENTITY_INSERT [dbo].[Classes] OFF
SET IDENTITY_INSERT [dbo].[Exams] ON 

INSERT [dbo].[Exams] ([Id], [ClassId], [Name]) VALUES (9, 16, N'Engineering Project')
INSERT [dbo].[Exams] ([Id], [ClassId], [Name]) VALUES (10, 16, N'Special Topics')
INSERT [dbo].[Exams] ([Id], [ClassId], [Name]) VALUES (13, 16, N'Data Mining')
INSERT [dbo].[Exams] ([Id], [ClassId], [Name]) VALUES (15, 16, N'Embedded Systems')
SET IDENTITY_INSERT [dbo].[Exams] OFF
SET IDENTITY_INSERT [dbo].[Halls] ON 

INSERT [dbo].[Halls] ([Id], [Name], [NumberOfSeats]) VALUES (1, N'T-243', 55)
INSERT [dbo].[Halls] ([Id], [Name], [NumberOfSeats]) VALUES (2, N'T-456', 54)
INSERT [dbo].[Halls] ([Id], [Name], [NumberOfSeats]) VALUES (3, N'T-356', 53)
INSERT [dbo].[Halls] ([Id], [Name], [NumberOfSeats]) VALUES (4, N'D-2356', 52)
SET IDENTITY_INSERT [dbo].[Halls] OFF
SET IDENTITY_INSERT [dbo].[Logins] ON 

INSERT [dbo].[Logins] ([Id], [Username], [Password], [Roles]) VALUES (1, N'admin1', N'1234', N'A')
INSERT [dbo].[Logins] ([Id], [Username], [Password], [Roles]) VALUES (3, N'onurcetrefil', N'1234', N'S')
SET IDENTITY_INSERT [dbo].[Logins] OFF
SET IDENTITY_INSERT [dbo].[Schedulings] ON 

INSERT [dbo].[Schedulings] ([Id], [PaperName], [ExamDate], [ExamTime], [Duration], [HallId], [ExamId], [StudentId]) VALUES (39, N'Special Topics', CAST(N'2023-06-08 00:00:00.0000000' AS DateTime2), CAST(N'14:00:00' AS Time), 60, 2, 10, 18)
INSERT [dbo].[Schedulings] ([Id], [PaperName], [ExamDate], [ExamTime], [Duration], [HallId], [ExamId], [StudentId]) VALUES (40, N'Engineering Project	', CAST(N'2023-06-08 00:00:00.0000000' AS DateTime2), CAST(N'11:00:00' AS Time), 60, 3, 9, 18)
INSERT [dbo].[Schedulings] ([Id], [PaperName], [ExamDate], [ExamTime], [Duration], [HallId], [ExamId], [StudentId]) VALUES (41, N'Data Mining', CAST(N'2023-06-09 00:00:00.0000000' AS DateTime2), CAST(N'12:00:00' AS Time), 60, 4, 13, 18)
INSERT [dbo].[Schedulings] ([Id], [PaperName], [ExamDate], [ExamTime], [Duration], [HallId], [ExamId], [StudentId]) VALUES (45, N'Embedded Systems', CAST(N'2023-06-10 00:00:00.0000000' AS DateTime2), CAST(N'12:30:00' AS Time), 45, 1, 15, 18)
SET IDENTITY_INSERT [dbo].[Schedulings] OFF
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([Id], [ClassId], [Name], [Surname], [SchoolNumber], [LoginId]) VALUES (18, 16, N'Onur ', N'Cetrefil', N'B2105.010153', 3)
SET IDENTITY_INSERT [dbo].[Students] OFF
ALTER TABLE [dbo].[Exams]  WITH CHECK ADD  CONSTRAINT [FK_Exams_Classes_ClassId] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Classes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Exams] CHECK CONSTRAINT [FK_Exams_Classes_ClassId]
GO
ALTER TABLE [dbo].[Schedulings]  WITH CHECK ADD  CONSTRAINT [FK_Schedulings_Exams_ExamId] FOREIGN KEY([ExamId])
REFERENCES [dbo].[Exams] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedulings] CHECK CONSTRAINT [FK_Schedulings_Exams_ExamId]
GO
ALTER TABLE [dbo].[Schedulings]  WITH CHECK ADD  CONSTRAINT [FK_Schedulings_Halls_HallId] FOREIGN KEY([HallId])
REFERENCES [dbo].[Halls] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedulings] CHECK CONSTRAINT [FK_Schedulings_Halls_HallId]
GO
ALTER TABLE [dbo].[Schedulings]  WITH CHECK ADD  CONSTRAINT [FK_Schedulings_Students_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[Schedulings] CHECK CONSTRAINT [FK_Schedulings_Students_StudentId]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Classes_ClassId] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Classes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Classes_ClassId]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Logins_LoginId] FOREIGN KEY([LoginId])
REFERENCES [dbo].[Logins] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Logins_LoginId]
GO

USE [Autos]
GO

/****** Object:  Table [dbo].[Poliza]    Script Date: 24/05/2022 10:10:28 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Poliza](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroPoliza] [int] NOT NULL,
	[NombreCliente] [varchar](max) NOT NULL,
	[IdentificacionCliente] [varchar](50) NOT NULL,
	[FechaNacimientoCliente] [date] NOT NULL,
	[FechaPoliza] [date] NOT NULL,
	[CoberturaPoliza] [varchar](50) NOT NULL,
	[ValorPoliza] [decimal](18, 0) NOT NULL,
	[NombrePlanPoliza] [varchar](50) NOT NULL,
	[CiudadCliente] [varchar](50) NOT NULL,
	[DireccionCliente] [varchar](50) NOT NULL,
	[PlacaAutomotor] [varchar](50) NOT NULL,
	[ModeloAutomotor] [varchar](50) NOT NULL,
	[TieneInspeccion] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK_Poliza] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Poliza] ADD  CONSTRAINT [DF_Poliza_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO



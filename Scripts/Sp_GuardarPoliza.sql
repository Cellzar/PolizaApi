USE [Autos]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GuardarPoliza]    Script Date: 24/05/2022 10:11:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Sp_GuardarPoliza] 
	@NumeroPoliza int,
	@NombreCliente varchar(60),
	@IdentificacionCliente varchar(60),
	@FechaNacimientoCliente date,
	@FechaPoliza date,
	@CoberturaPoliza varchar(60),
	@ValorPoliza decimal(18, 0),
	@NombrePlanPoliza varchar(60),
	@CiudadCliente varchar(60),
	@DireccionCliente varchar(60),
	@PlacaAutomotor varchar(60),
	@ModeloAutomotor varchar(60),
	@TieneInspeccion bit
AS
BEGIN

    
	IF EXISTS(SELECT * FROM Poliza WHERE IdentificacionCliente = @IdentificacionCliente AND CAST(FechaPoliza AS DATE) >= CAST(GETDATE() AS DATE))
	BEGIN

		SELECT 'TIENE POLIZA PENDIENTE' AS IdPoliza 
	END

	ELSE
	BEGIN
		INSERT INTO Poliza VALUES(@NumeroPoliza,
		@NombreCliente,
		@IdentificacionCliente,
		@FechaNacimientoCliente,
		@FechaPoliza,
		@CoberturaPoliza,
		@ValorPoliza,
		@NombrePlanPoliza,
		@CiudadCliente,
		@DireccionCliente,
		@PlacaAutomotor,
		@ModeloAutomotor,
		@TieneInspeccion, GETDATE())

		SELECT SCOPE_IDENTITY() AS IdPoliza 

	END
END

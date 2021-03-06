USE [Autos]
GO
/****** Object:  StoredProcedure [dbo].[Sp_ConsultarPoliza]    Script Date: 24/05/2022 10:11:05 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Sp_ConsultarPoliza]
	@NumeroPoliza int = null,
	@PlacaAutomotor varchar(60) = null,
	@tipo varchar(10) = null,
	@fechaInicial date = null,
	@fechaFinal date = null
AS
BEGIN
	
	IF(@tipo = 'FECHAS')
	BEGIN
		SELECT Id, NumeroPoliza,
		NombreCliente,
		IdentificacionCliente,
		FechaNacimientoCliente,
		FechaPoliza,
		CoberturaPoliza,
		ValorPoliza,
		NombrePlanPoliza,
		CiudadCliente,
		DireccionCliente,
		PlacaAutomotor,
		ModeloAutomotor,
		TieneInspeccion FROM Poliza where cast(FechaPoliza as date) between cast(@fechaInicial as date) and cast(@fechaFinal as date)
	END

	ELSE IF (@tipo = 'FECHAS')
	BEGIN
	SELECT Id, NumeroPoliza,
		NombreCliente,
		IdentificacionCliente,
		FechaNacimientoCliente,
		FechaPoliza,
		CoberturaPoliza,
		ValorPoliza,
		NombrePlanPoliza,
		CiudadCliente,
		DireccionCliente,
		PlacaAutomotor,
		ModeloAutomotor,
		TieneInspeccion FROM Poliza
	END

	ELSE 
	BEGIN
		SELECT Id, NumeroPoliza,
		NombreCliente,
		IdentificacionCliente,
		FechaNacimientoCliente,
		FechaPoliza,
		CoberturaPoliza,
		ValorPoliza,
		NombrePlanPoliza,
		CiudadCliente,
		DireccionCliente,
		PlacaAutomotor,
		ModeloAutomotor,
		TieneInspeccion FROM Poliza WHERE NumeroPoliza = @NumeroPoliza
		or PlacaAutomotor = @PlacaAutomotor
	END

	
END


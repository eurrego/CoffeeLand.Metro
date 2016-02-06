use DBFinca
go

create PROCEDURE InsertarDetalleCompra
	(@dtDetalleCompra DetalleCompra READONLY)
AS
    
	
	INSERT INTO Compra_Insumo
           (idCompra
           ,idInsumo,Cantidad,Precio)
        SELECT idCompra, idInsumo,Cantidad,Precio FROM  @dtDetalleCompra


		UPDATE P
		SET P.CantidadExistente = P.CantidadExistente + DC.cantidad, 
		P.PrecioPromedio= (ISNULL((P.PrecioPromedio * P.CantidadExistente),0) + CONVERT(int,DC.Subtotal))/(P.CantidadExistente + DC.cantidad)
		FROM Insumo AS P
		INNER JOIN @dtDetalleCompra AS DC
		ON P.idInsumo = DC.idInsumo
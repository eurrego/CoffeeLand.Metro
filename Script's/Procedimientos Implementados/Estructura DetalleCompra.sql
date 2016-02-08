USE DBFinca
GO

-- Create the data type
CREATE TYPE DetalleCompra AS TABLE 
(
	Nombre varchar(45), 
	Cantidad varchar(10), 
	Precio varchar(10), 
    Subtotal varchar(20),
	idInsumo varchar(20),
	idCompra varchar(20)
)
GO


  
  
  create procedure ComprasProveedor
  (@nit varchar(45))
 as
	  begin
		  select c.idCompra,sum(d.Cantidad*d.Precio) as total,isnull(sum(d.Cantidad*d.Precio)-
			(select SUM(a.Valor) from AbonoCompra a where a.idCompra = c.idCompra),sum(d.Cantidad*d.Precio)) as adeuda
			,c.Fecha,c.NumeroFactura from Compra c
		 
		  inner join Compra_Insumo d
		  on c.idCompra = d.idCompra
		 
		  where c.NitProveedor = @nit and c.EstadoCuenta = 'D'
		  group by c.idCompra,c.Fecha,c.NumeroFactura
	 end
  
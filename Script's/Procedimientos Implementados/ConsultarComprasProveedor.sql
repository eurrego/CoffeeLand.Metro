

  
  
  create procedure ComprasProveedor
  (@nit varchar(45))
 as
	  begin
		    select c.idCompra,isnull(sum(d.Cantidad*d.Precio),Cb.Precio) as total,isnull(isnull(sum(d.Cantidad*d.Precio),Cb.Precio)-
			(select SUM(a.Valor) from AbonoCompra a where a.idCompra = c.idCompra),isnull(sum(d.Cantidad*d.Precio),Cb.Precio)) as adeuda
			,c.Fecha,c.NumeroFactura from Compra c
		 
		  left join Compra_Insumo d
		  on c.idCompra = d.idCompra
		  left join CostoBeneficio CB
		  on c.idCompra = Cb.idCompra
		 
		  where c.NitProveedor = @nit and c.EstadoCuenta = 'D'
		  group by c.idCompra,c.Fecha,c.NumeroFactura,cb.Precio
	 end
  
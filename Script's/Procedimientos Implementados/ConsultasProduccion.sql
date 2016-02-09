create procedure Consultasproduccion
 as
	  begin
		 
		  select convert (decimal(10,2),(SUM (t1.CantidadRestante)/125)) as Cargas from (
		  select  isnull((p.Cantidad)-
			(select SUM(mp.Cantidad) from MovimientoProduccion mp  where mp.idProduccion = p.idProduccion),(p.Cantidad)) as CantidadRestante
			 from Produccion p
		  where p.EstadoProduccion = 'NV')t1
		  
		  
	 end
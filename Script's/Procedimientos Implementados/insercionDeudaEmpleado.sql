create proc insercionDeudaEmpleado
(
@DocumentoPersona varchar(15), @valor money, @fecha date, @descripcion varchar(150)
)

as
begin

set nocount on
declare @mensaje varchar(50)


			insert into DeudaPersona( DocumentoPersona,Valor,Fecha, Descripcion)
			values (@DocumentoPersona,@valor,@fecha,@descripcion)
			set @mensaje = 'Registro exitoso'
	

select @mensaje as Mensaje

end
create procedure ModificarFinca
(
@nombreFinca varchar(25),
@Propietario varchar(45),
@idMunicipio int,
@Vereda varchar(50),
@telefono varchar(10),
@hectareas varchar(5)
)


as

begin

	update Finca
	set
	NombreFinca = @nombreFinca,
	Propietario = @Propietario,
	idMunicipio = @idMunicipio,
	Vereda = @Vereda,
	Telefono = @telefono,
	Hectareas = @hectareas
	
end
-------------------------------------------------------Persona--------------------------------------


create proc gestionPersona
(
@nombrePersona varchar(50),@genero char(1),@telefono varchar(10),@fechaNacimiento date, @documentoPerosna int, @opc int,
@idTipoDocumento tinyint , @idTipoContrato tinyint
)

as
begin

set nocount on
declare @mensaje varchar(50)

if(@opc=1)
	 begin
		if ((select count(DocumentoPersona) from Persona where DocumentoPersona = @documentoPerosna) = 0)

			begin
				insert into Persona (idTipoDocumento, DocumentoPersona,idTipoContratoPersona,NombrePersona, Genero,Telefono,FechaNacimineto)
				values (@idTipoDocumento,@documentoPerosna,@idTipoContrato,@nombrePersona,@genero,@telefono,@fechaNacimiento)
				set @mensaje = 'Registro exitoso'
			end
		else
			begin
				set @mensaje = 'Existe un Empleado con este documento'
			end
	 end

else if(@opc=2)

		begin
		update Persona
			set
			NombrePersona=@nombrePersona,
			Genero=@genero,
			Telefono=@telefono,
			FechaNacimineto=@fechaNacimiento,
			idTipoContratoPersona=@idTipoContrato
			where DocumentoPersona = @documentoPerosna
			set @mensaje = 'Actualización exitosa!'

		end
else if (@opc = 3)
		
		begin
			update Persona
			set
			EstadoPersona = 'I'
			where DocumentoPersona = @documentoPerosna

			set @mensaje = 'Inhabilitación exitosa!'
		end
		
	select @mensaje  as Mensaje


end


insert into TipoDocumento ( NombreTipoDocumento) values ('CC')
insert into TipoDocumento ( NombreTipoDocumento) values ('RC')
insert into TipoDocumento ( NombreTipoDocumento) values ('TI')

insert into TipoContratoPersona( NombreTipoContratoPersona) values ('Temporal')
insert into TipoContratoPersona( NombreTipoContratoPersona) values ('Permanente')
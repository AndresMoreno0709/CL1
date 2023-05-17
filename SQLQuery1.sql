CREATE Proc usp_ClienteListar
as
Begin
	Select * from Ventas.clientes
End

Create Proc usp_PaisListar
as
Begin
	Select * from Ventas.paises
End
go

create or alter procedure ups_listarCliente
@idPais varchar(5),
@nomCliente varchar(40)
as
Begin
select*from [Ventas].[clientes] where idpais = @idPais and NomCliente = @nomCliente
End
go



--PREGUNTA 2
create procedure usp_listarEmpelado
as
Begin
select*from [RRHH].[empleados] 
End
go


create procedure usp_listarCargo
as
Begin
select*from[RRHH].[Cargos]
End
go



create or alter procedure usp_insertar_empleado
@idempleado int,
@nomempleado varchar(50),
@apeEmpleado varchar(50),
@fecNac datetime,
@dirEmpleado varchar(60),
@iddistrito int,
@fonoEmpleado varchar(15),
@idcargo int,
@fecContrata datetime
as
declare @num int= CAST((select top 1 idEmpleado from [RRHH].[empleados] order by idEmpleado desc)+1 AS int)
merge RRHH.empleados as target
using(select @idempleado,@apeEmpleado,@nomempleado,@fecNac,@dirempleado,@iddistrito,@fonoempleado,@idcargo ,@fecContrata)
as source(IdEmpleado,ApeEmpleado,NomEmpleado,FecNac,DirEmpleado,idDistrito,fonoEmpleado,idCargo,FecContrata)
on target.IdEmpleado=source.IdEmpleado
when matched then
	update set	target.apeEmpleado=source.ApeEmpleado,
				target.nomempleado=source.NomEmpleado,
				target.FecNac=source.FecNac,
				target.dirempleado=source.DirEmpleado,
				target.idDistrito=source.idDistrito,
				target.fonoempleado=source.fonoEmpleado,
				target.idcargo=source.idCargo,
				target.FecContrata=source.FecContrata
when not matched then 
	insert values(@num,source.ApeEmpleado,source.NomEmpleado,source.FecNac,source.DirEmpleado,source.idDistrito,source.fonoEmpleado,source.idCargo,source.FecContrata);
go

create procedure usp_eliminar
@idEmpelado int
as
Begin
delete from [RRHH].[empleados] where IdEmpleado=@idEmpelado
End
go


create procedure usp_listar_distrito
as
Begin
select*from [RRHH].[Distritos]
End
go

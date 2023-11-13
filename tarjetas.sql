-- Crea la base de datos "tarjetas"
CREATE DATABASE tarjetas;

-- Cambia al contexto de la base de datos "tarjetas"
USE tarjetas;

--DROP DATABASE tarjetas;

-- Crea la tabla "interes"
CREATE TABLE interes (
	idInteres INT IDENTITY(1, 1) PRIMARY KEY,
	nombre VARCHAR(50) NOT NULL,
	interes FLOAT NOT NULL,
    interesMinimo FLOAT NOT NULL,
    descripcion VARCHAR(250) NOT NULL,
    fechaIngreso DATETIME NOT NULL,
	fechaUp DATETIME,
	estado INT NOT NULL /*1 = activo; 0 = inhabilitado*/
);

-- Crear la tabla "tipoTarjeta"
CREATE TABLE tipoTarjeta (
    idTipo INT IDENTITY(1, 1) PRIMARY KEY,
    idInteres INT FOREIGN KEY REFERENCES interes(idInteres),
    tipo VARCHAR(50) NOT NULL,
	montoMaximo FLOAT NOT NULL,
    descripcion VARCHAR(250),
    fechaIngreso DATETIME NOT NULL,
	fechaUp DATETIME,
	estado INT NOT NULL /*1 = activo; 0 = inhabilitado*/
);

-- Crea la tabla "clientes"
CREATE TABLE clientes (
    idCliente INT IDENTITY(1, 1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(52) NOT NULL,
	sexo INT NOT NULL, /*1 = hombre; 0=mujer*/
	fechaNacimiento DATE NOT NULL,
    dui VARCHAR(10) NOT NULL,
	direccion VARCHAR(250) NOT NULL,
	telefono VARCHAR(9) NOT NULL,
	email varchar(100) NOT NULL,
	estado INT NOT NULL /*1 = activo; 0 = inactivo*/
);

-- Crear la tabla "tarjeta"
CREATE TABLE tarjeta (
    idTarjeta INT IDENTITY(1, 1) PRIMARY KEY,
    idCliente INT FOREIGN KEY REFERENCES clientes(idCliente),
    idTipo INT FOREIGN KEY REFERENCES tipoTarjeta(idTipo),
	numeroTarjeta VARCHAR(19) NOT NULL,
	fechaExpira varchar(7) NOT NULL,
    saldoActual FLOAT NOT NULL,
    saldoDisponible FLOAT NOT NULL,
	fechaIngreso DATETIME NOT NULL,
	fechaUp DATETIME,
	estado INT NOT NULL /*1 = activo; 0 = inhabilitado*/
);

-- Crear la tabla "movimientos"
CREATE TABLE movimientos (
    idMovimiento INT IDENTITY(1, 1) PRIMARY KEY,
    idTarjeta INT FOREIGN KEY REFERENCES tarjeta(idTarjeta),
    monto FLOAT NOT NULL,
    fechaCompra DATE NOT NULL,
    fechaIngreso DATETIME NOT NULL,
    descripcion VARCHAR(250),
	activo INT /*1 = activo; 0 = cancelado*/,
	tipoMovimiento INT /*1 = compra; 2 = pago*/
);

--PROCEDIMIENTOS ALMACENADOS DE LA TABLA CLIENTES
create procedure insertaCliente
@nombre VARCHAR(50),
@apellido VARCHAR(52),
@sexo INT,
@fechaNacimiento DATE,
@dui VARCHAR(10),
@direccion VARCHAR(250),
@telefono VARCHAR(9),
@email VARCHAR(100)
as 
begin
	-- Inserta los valores en la tabla "interes"
	insert into clientes (nombre, apellido, sexo, fechaNacimiento, dui, direccion, telefono, email, estado)
	values(@nombre,@apellido,@sexo,@fechaNacimiento,@dui,@direccion,@telefono,@email,1);
end;

	-- Inserta los valores en la tabla "clientes"

create procedure mostrarCliente
@estado INT
as
begin
	if @estado > 1
	begin
		select clientes.*,COUNT(tarjeta.idTarjeta) as cantidad from clientes
		left join tarjeta on tarjeta.idCliente=clientes.idCliente and tarjeta.estado = 1
		GROUP BY clientes.idCliente,clientes.nombre,clientes.apellido, clientes.sexo,
		clientes.fechaNacimiento, clientes.dui, clientes.direccion, clientes.telefono,
		clientes.email,clientes.estado;
	end
	else
	begin
		select clientes.*,COUNT(tarjeta.idTarjeta) as cantidad from clientes
		inner join tarjeta on tarjeta.idCliente=clientes.idCliente and tarjeta.estado = 1
		where clientes.estado=@estado
		GROUP BY clientes.idCliente,clientes.nombre,clientes.apellido, clientes.sexo,
		clientes.fechaNacimiento, clientes.dui, clientes.direccion, clientes.telefono,
		clientes.email,clientes.estado;
	end
end;

create procedure datosCliente
@idCliente INT
as
begin
	select * from clientes where idCliente=@idCliente;
end;

create procedure editarCliente
@idCliente INT,
@nombre VARCHAR(50),
@apellido VARCHAR(52),
@sexo INT,
@fechaNacimiento DATE,
@dui VARCHAR(10),
@direccion VARCHAR(250),
@telefono VARCHAR(9),
@email VARCHAR(100)
as 
begin
	update clientes
	set nombre = @nombre,apellido = @apellido,sexo = @sexo,fechaNacimiento = @fechaNacimiento,dui = @dui,direccion = @direccion,
        telefono = @telefono,email = @email
	where idCliente=@idCliente;
end;

create procedure eliminarCliente
@idCliente INT
as 
begin
	delete from clientes where idCliente=@idCliente;
end;

select * from clientes;
--delete from clientes where idCliente = 6;

--PROCEDIMIENTOS ALMACENADOS DE LA TABLA INTERES
create procedure mostrarInteres
@estado INT
as
begin
	if @estado > 1
	begin
		select * from interes;
	end
	else
	begin
		select * from interes where estado=@estado;
	end
end;

create procedure datosInteres
@idInteres INT
as
begin
	select * from interes where idInteres=@idInteres;
end;

create procedure insertaInteres
@nombre VARCHAR(250),
@interes FLOAT,
@interesMinimo FLOAT,
@descripcion VARCHAR(250)
as
begin
	DECLARE @fechaIngreso VARCHAR(250);
	--se captura la fecha actual
	SET @fechaIngreso = CONVERT(VARCHAR(250), GETDATE(), 120);
	-- Inserta los valores en la tabla "interes"
	insert into interes (nombre, interes, interesMinimo, descripcion, fechaIngreso,estado)
	values(@nombre, @interes, @interesMinimo, @descripcion, @fechaIngreso,1);
end;

--exec insertaInteres @nombre="Prueba", @interes = 0.15, @interesMinimo = 0.1, @descripcion = "Es una prueba";

create procedure editaInteres
@idInteres INT,
@nombre VARCHAR(250),
@interes FLOAT,
@interesMinimo FLOAT,
@descripcion VARCHAR(250)
as
begin
	DECLARE @fechaUp VARCHAR(250);
	--se captura la fecha actual
	SET @fechaUp = CONVERT(VARCHAR(250), GETDATE(), 120);
	update interes
	set nombre=@nombre,interes=@interes,interesMinimo=@interesMinimo,descripcion=@descripcion,fechaUp=@fechaUp
	where idInteres=@idInteres;
end;

create procedure inhabilitarInteres
@idInteres INT
as
begin
	DECLARE @fechaUp VARCHAR(250);
	--se captura la fecha actual
	SET @fechaUp = CONVERT(VARCHAR(250), GETDATE(), 120);
	update interes
	set fechaUp=@fechaUp,estado=0
	where idInteres=@idInteres;
end;

--PROCEDIMIENTOS ALMACENADOS DE LA TABLA TIPOTARJETA
create procedure mostrarTipoTarjeta
as
begin
	select tipoTarjeta.*,interes.nombre,interes.interes,interes.interesMinimo from tipoTarjeta
	inner join interes on interes.idInteres=tipoTarjeta.idInteres;
end;

create procedure insertaTipoTarjeta
@idInteres INT,
@tipo VARCHAR(50),
@montoMaximo FLOAT,
@descripcion VARCHAR(250)
as
begin
	DECLARE @fechaIngreso VARCHAR(250);
	--se captura la fecha actual
	SET @fechaIngreso = CONVERT(VARCHAR(250), GETDATE(), 120);
	-- Inserta los valores en la tabla "interes"
	insert into tipoTarjeta (idInteres, tipo, montoMaximo, descripcion, fechaIngreso,estado)
	values(@idInteres, @tipo, @montoMaximo, @descripcion, @fechaIngreso,1);
end;

--exec insertaTipoTarjeta @idInteres=1, @tipo = "Normal", @montoMaximo = 1500, @descripcion = "Es una prueba";

create procedure datosTipoTarjeta
@idTipo INT
as
begin
	select tipoTarjeta.*,interes.nombre from tipoTarjeta
	inner join interes on interes.idInteres=tipoTarjeta.idInteres
	where tipoTarjeta.idTipo = @idTipo;
end;

create procedure editarTipoTarjeta
@idTipo INT,
@idInteres INT,
@tipo VARCHAR(50),
@montoMaximo FLOAT,
@descripcion VARCHAR(250)
as
begin

	update tipoTarjeta
	set idInteres=@idInteres,tipo=@tipo,montoMaximo=@montoMaximo,descripcion=@descripcion
	where idTipo=@idTipo;
end;

create procedure inhabilitarTipoTarjeta
@idTipo INT
as
begin
	DECLARE @fechaUp VARCHAR(250);
	--se captura la fecha actual
	SET @fechaUp = CONVERT(VARCHAR(250), GETDATE(), 120);

	update tipoTarjeta
	set fechaUp=@fechaUp,estado=0
	where idTipo=@idTipo;
end;

--PROCEDIMIENTOS ALMACENADOS DE LA TABLA TARJETA
create procedure verificaNumTarjeta
@numeroTarjeta varchar(19)
as
begin
	select COUNT(*) as cantidad from tarjeta where numeroTarjeta=@numeroTarjeta;
end;

create procedure insertaTarjeta
@idCliente INT,
@idTipo INT,
@numeroTarjeta varchar(19),
@fechaExpira varchar(19),
@saldoDisponible FLOAT
as
begin
	DECLARE @fechaIngreso VARCHAR(250);
	--se captura la fecha actual
	SET @fechaIngreso = CONVERT(VARCHAR(250), GETDATE(), 120);
	-- Inserta los valores en la tabla "interes"
	insert into tarjeta (idCliente, idTipo, numeroTarjeta, fechaExpira, saldoActual, saldoDisponible, fechaIngreso, estado)
	values(@idCliente, @idTipo, @numeroTarjeta, @fechaExpira, 0, @saldoDisponible, @fechaIngreso,1);
end;

create procedure detalleTarjeta
@idCliente int,
@numeroTarjeta varchar(19)
as
begin
	if @numeroTarjeta is null or @numeroTarjeta = ''
	begin
		select clientes.*, tarjeta.numeroTarjeta,tarjeta.fechaExpira,tipoTarjeta.tipo, 
		interes.interes,interes.interesMinimo,tipoTarjeta.montoMaximo from clientes
		inner join tarjeta on tarjeta.idCliente=clientes.idCliente
		inner join tipoTarjeta on tipoTarjeta.idTipo=tarjeta.idTipo
		inner join interes on interes.idInteres=tipoTarjeta.idInteres
		where clientes.idCliente = @idCliente;
	end
	else
	begin
		select clientes.*, tarjeta.numeroTarjeta,tarjeta.fechaExpira,tipoTarjeta.tipo, 
		interes.interes,interes.interesMinimo,tipoTarjeta.montoMaximo from clientes
		inner join tarjeta on tarjeta.idCliente=clientes.idCliente
		inner join tipoTarjeta on tipoTarjeta.idTipo=tarjeta.idTipo
		inner join interes on interes.idInteres=tipoTarjeta.idInteres
		where tarjeta.numeroTarjeta = @numeroTarjeta;	
	end
end;

create procedure mostrarTarjetas
as
begin
	select tarjeta.idTarjeta,tarjeta.idCliente,tarjeta.numeroTarjeta,tarjeta.fechaExpira,tarjeta.saldoActual,tarjeta.saldoDisponible,tipoTarjeta.tipo from tarjeta
	inner join tipoTarjeta on tipoTarjeta.idTipo=tarjeta.idTipo
	where tarjeta.estado = 1;
end;

create procedure datosTarjeta
@idTarjeta INT
as
begin
	select tarjeta.*,tipoTarjeta.montoMaximo, interes.interes from tarjeta 
	inner join tipoTarjeta on tipoTarjeta.idTipo=tarjeta.idTipo
	inner join interes on interes.idInteres=tipoTarjeta.idInteres
	where idTarjeta = @idTarjeta;
end;

create procedure upSaldoTarjeta
@idTarjeta INT,
@saldoActual FLOAT,
@saldoDisponible FLOAT
as
begin
	update tarjeta
	set saldoActual=@saldoActual, saldoDisponible=@saldoDisponible where idTarjeta=@idTarjeta;
end;

create procedure datosCuenta
@idCliente INT
as
begin
	select clientes.idCliente,clientes.nombre,clientes.apellido,tarjeta.idTarjeta,tarjeta.numeroTarjeta,tarjeta.fechaExpira,tarjeta.saldoActual,tarjeta.saldoDisponible,
	tipoTarjeta.montoMaximo,interes.interes,interes.interesMinimo from clientes
	inner join tarjeta on tarjeta.idCliente=clientes.idCliente
	inner join tipoTarjeta on tipoTarjeta.idTipo=tarjeta.idTipo
	inner join interes on interes.idInteres=tipoTarjeta.idTipo
	where clientes.idCliente = 1;
end;

--PROCEDIMIENTOS ALMACENADOS DE LA TABLA MOVIMIENTOS
create procedure mostrarMovimiento
@idTarjeta INT,
@activo INT
as
begin
    if @activo > 1
    begin
        select * from movimientos where idTarjeta=@idTarjeta;
    end
    else
    begin
        select * from movimientos where activo=@activo and idTarjeta=@idTarjeta;
    end
end;

create procedure insertaMovimiento
@idTarjeta INT,
@monto FLOAT,
@fechaCompra DATE,
@descripcion VARCHAR(250),
@tipoMovimiento INT
as
begin
	DECLARE @fechaIngreso VARCHAR(250);
	--se captura la fecha actual
	SET @fechaIngreso = CONVERT(VARCHAR(250), GETDATE(), 120);
	-- Inserta los valores en la tabla "movimientos"
	insert into movimientos (idTarjeta, monto, fechaCompra, fechaIngreso, descripcion,activo,tipoMovimiento)
	values(@idTarjeta, @monto, @fechaCompra, @fechaIngreso, @descripcion,1,@tipoMovimiento);
end;

create procedure historialMovimiento
@idTarjeta INT,
@inicioMes DATE,
@finMes DATE
as
begin
	select tarjeta.numeroTarjeta, movimientos.monto, movimientos.fechaCompra, movimientos.descripcion, movimientos.tipoMovimiento from tarjeta
	inner join movimientos on movimientos.idTarjeta=tarjeta.idTarjeta
	where movimientos.idTarjeta = @idTarjeta and movimientos.fechaCompra >= @inicioMes and movimientos.fechaCompra <= @finMes
	order by movimientos.fechaCompra desc;
end;

create procedure historialCompras
@idTarjeta INT,
@inicioMes DATE,
@finMes DATE
as
begin
	select tarjeta.numeroTarjeta, movimientos.monto, movimientos.fechaCompra, movimientos.descripcion, movimientos.tipoMovimiento from tarjeta
	inner join movimientos on movimientos.idTarjeta=tarjeta.idTarjeta
	where movimientos.idTarjeta = @idTarjeta and movimientos.fechaCompra >= @inicioMes and movimientos.fechaCompra <= @finMes and movimientos.tipoMovimiento = 1
	order by movimientos.fechaCompra desc;
end;

create procedure totalCompras
@idTarjeta INT,
@inicioMes DATE,
@finMes DATE
as
begin
	select SUM(monto) as total from movimientos
	where movimientos.idTarjeta = @idTarjeta and movimientos.fechaCompra >= @inicioMes and movimientos.fechaCompra <= @finMes and movimientos.tipoMovimiento = 1;
end;

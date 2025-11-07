EXEC sp_rename 'cuentas_banco', 'OrigenFuentes';
Select * from OrigenFuentes
Drop table origen
Select * from transaccion
delete OrigenFuentes
DBCC CHECKIDENT ('OrigenFuentes', RESEED, 1);
GO
SELECT 
    name
FROM 
    sys.key_constraints
WHERE 
    parent_object_id = OBJECT_ID('OrigenFuentes') 
    AND type = 'PK';
GO
EXEC sp_rename 
    'OrigenFuentes.Id_cuentaBanco',
    'Id_Origen',  
    'COLUMN';
GO
SELECT 
    name
FROM 
    sys.foreign_keys
WHERE 
    parent_object_id = OBJECT_ID('transaccion')
GO
ALTER TABLE transaccion
DROP CONSTRAINT FK_transacciid_or_73501C2F;
GO
Alter table transaccion
drop column id_origen
GO
ALTER TABLE transaccion
ADD Id_Origen INT NOT NULL;
GO
ALTER TABLE transaccion
ADD CONSTRAINT FK_transaccion_OrigenFuentes
FOREIGN KEY (Id_Origen)
REFERENCES OrigenFuentes(Id_Origen);
GO
select * from OrigenFuentes
delete OrigenFuentes
GO
DBCC CHECKIDENT ('OrigenFuentes', RESEED, 0);
GO
Insert into OrigenFuentes(Nombre,saldo,tasa_interes)
values('Caja Chica',0,0),
('BANCO DE OCCIDENTE',5000,2),
('BANPAIS',3000,2)

alter table certificado_deposito
add Parroquia_ID int;
go
ALTER TABLE certificado_deposito
ADD CONSTRAINT FK_transaccion_ParroquiaID
FOREIGN KEY (Parroquia_ID)
REFERENCES parroquia(Parroquia_ID);
--
ALTER PROCEDURE IngresarIngresos

    @fecha_transaccion DATETIME,
    @descripcion NVARCHAR(255),
    @monto_historico DECIMAL(18, 2),
    @Numero_de_Referencia NVARCHAR(50),
    @Usuario_id INT, 
    @Id_Origen INT,
    @Nombre nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @cod_tipo INT = 1;
    DECLARE @id_modulo INT = 2;
    DECLARE @id_tarea INT = 1;

    INSERT INTO transaccion(fecha_transaccion,descripcion,monto_historico,cod_tipo,
        Usuario_id,id_modulo,id_tarea,Numero_de_Referencia,Id_Origen,NombreCuenta)
    VALUES
    (@fecha_transaccion,@descripcion,@monto_historico,@cod_tipo,@Usuario_id,@id_modulo,
    @id_tarea,@Numero_de_Referencia,@Id_Origen,@Nombre);
    
END
--

ALTER PROCEDURE sp_mostrarCuentas
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Nombre, 
        saldo,
        tasa_interes
    FROM 
        OrigenFuentes
    WHERE 
        Id_Origen <> 1 -- 
    ORDER BY 
        Nombre;
END
--
alter PROCEDURE SP_ObtenerFuentesDeFondos
AS
BEGIN
    SET NOCOUNT ON;
    
    
    SELECT
        Id_Origen AS ID,
        Nombre AS NombreOrigen
    FROM 
        OrigenFuentes 
    
    ORDER BY ID; 
END
--

CREATE PROCEDURE SP_ObtenerUsuarioId
    @NombreUsuario NVARCHAR(50) 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        Usuario_id 
    FROM 
        Usuario 
    WHERE 
        usuario = @NombreUsuario;
END

go
select * from tareas_periodica
insert into tareas_periodica(nombre_tarea,descripcion,fecha_alerta,rol_id,alarma_activa)
values('Ingresos periodicos', 'debe realizar ingresos', '2025-11-15', 2, 'alarma')

--
alter PROCEDURE SP_ObtenerFuentesDeFondos
AS
BEGIN
    SET NOCOUNT ON;
    
    
    SELECT
        Id_Origen AS ID,
        CONCAT(Nombre, ' - Lps. ', saldo) AS NombreOrigen
    FROM 
        OrigenFuentes 
    
    ORDER BY ID; 
END
--
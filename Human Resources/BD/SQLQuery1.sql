/*APLICACIÓN DE GESTIÓN DE RECURSOS HUMANOS.

MÓDULO DE MANTENIMIENTOS
Mantenimiento de empleados
(Id, Código Empleado, Nombre, Apellido, Teléfono, Departamento, Cargo, Fecha ingreso, Salario, Estatus (Activo/Inactivo))
Mantenimiento de departamentos
(Id, Código Departamento, Nombre, Encargado)
Mantenimiento de cargos
(Id, Código Cargo, Cargo)

MÓDULO DE PROCESOS
*Cálculo de Nómina: *
Calcular el monto total de la nómina, sumando el salario de los empleados activos y presentando el total, para que sea visto y validado por el contador. Almacenar en una tabla NOMINAS la siguiente información:
(Id, Año, Mes, Monto Total)

Salida de empleados
(Empleado, Tipo salida (Renuncia, Despido, Desahucio), Motivo, Fecha Salida) La salida de un empleado, es inactivarlo.

Vacaciones:
(Desde, Hasta, Correspondiente a: (año), Comentarios)

Permisos
(Desde, Hasta, Comentarios)


Licencias
(Desde, Hasta, Motivo, Comentarios)

MÓDULO DE INFORMES (EN ESTE MÓDULO SE ELEGIRÁN EL TIPO DE INFORME A GENERAR PARA PRESENTAR LISTAS DE DATOS EN VISTAS)
Nóminas:
 (Con opción de buscar todas las nóminas, o una en particular)
Empleados Activos
 (con opción de buscar todos, buscar por nombre y buscar por departamento)
Empleados inactivos
 (Los que han salido de la empresa)
Departamentos
Cargos
Entradas de empleados por mes
Salida de empleados por mes
Vacaciones por año
Permisos*/

Create table Departament(

Id Int not null Constraint pk_departament primary key Identity(1,1),
Codigo Int not null unique,
Departamento Varchar(50) not null unique,
Encargado Varchar(100) not null
);

Create table Position(

Id Int not null Constraint pk_position primary key Identity(1,1),
Codigo Int not null unique,
Cargo Varchar(50) unique
);

Create table Employee(

Id Int not null Constraint pk_employee primary key Identity(1,1),
Code Int not null unique,
Name Varchar(100) not null,
Lastname Varchar(100) not null,
Telephone Varchar(15) not null,
Admission DateTime not null,
Salary Decimal(13,2) not null,
Status Char(8)Check(Status in('Active', 'Inactive')),
Departament Int not null ,
Position Int not null ,
Constraint fk_Departament foreign key(Departament) References Departament(Id),
Constraint fk_Position foreign key(Position) References Position(Id)
);

Create table Nomina(

Id Int not null Constraint pk_nomina primary key Identity(1,1),
Año Varchar(4) not null,
Mes Varchar(20) not null,
MontoTotal decimal(13,2) not null
);

Create table Departure(

Id Int not null Constraint pk_salida primary key Identity(1,1),
Employee Int not null,
Tipo Char(9)Check(Tipo in('Renuncia', 'Despido', 'Desahucio')),
Motivo Varchar(5000) not null,
Fecha DateTime not null,
Constraint fk_Departure foreign key(Employee) References Employee(Id)
);

Create table Holidays(

Id Int not null Constraint pk_holiday primary key Identity(1,1),
Employee Int not null,
Desde DateTime not null,
Hasta DateTime not null,
Correspondiente Varchar(4) not null,
Comentarios Varchar(400),
Constraint fk_Holidays foreign key(Employee) References Employee(Id)
);

Create Table Permission(

Id Int not null Constraint pk_permission primary key Identity(1,1),
Employee Int not null,
Desde DateTime not null,
Hasta DateTime not null,
Comentarios Varchar(400)
Constraint fk_permission foreign key(Employee) References Employee(Id)
);

Create Table License(

Id Int not null Constraint pk_license primary key Identity(1,1),
Employee Int not null,
Desde DateTime not null,
Hasta DateTime not null,
Motivo Varchar(500) not null,
Comentarios Varchar(400),
Constraint fk_Licence foreign key(Employee) References Employee(Id)
);

drop table departure
drop table Employee;
drop table Position;
drop table Departament;
drop table Nomina;
drop table Departure;
delete from Nomina;

select * from Employee;
select * from Position;
select * from Departament;
select * from Nomina;
select * from Tipo;
select * from Permission;
select * from License;
select * from Departure;

Insert into Employee Values(001, ' Miguel', 'Martinez', '8294784489', getDate(), 1000.00, 'Active', 3, 4)
Insert into Position Values(001, 'Jefe')
Insert into Departament Values(001, 'Seguridad', 'Jose Miguel')
Insert into Nomina Values(2019, 6, 1000000)
Insert into Tipo Values('Desahucio')
Insert into Departure Values(3, 'Renuncia', 'Araca Vaca', GetDate())

Create proc CalculoNomina /*Proc Calculo Nomina*/
as
begin
select sum(Salary) as 'Monto Total' from Employee where Status = 'Active';
end

drop proc CalculoNomina;

exec CalculoNomina

Create proc EmpleadosActivos/*Proc Empleados Activos*/
as
begin
Select * from Employee
end

Create proc EmpleadosInactivos/*Proc Empleados Inactivos*/
as
begin
Select * from Employee Where Status = 'Inactive'
end

Create proc DepartamentCreated/*Proc Departament Created*/
as
begin
Select * from Departament;
end

Create proc PositiontCreated/*Proc Positiont Created*/
as
begin
Select * from Position;
end

CREATE TRIGGER Salidas on Departure/*Inactivar un Empleado Ingresado a Departure*/
After Insert 
as
begin
declare @id Int
  Set @id = (Select Employee from Inserted);

  Update Employee Set Status = 'Inactive' Where Id = @id;
end

CREATE TRIGGER Activados on Departure/*Activar un Empleado Borrado de Departure*/
After Delete 
as
begin
declare @id Int
  Set @id = (Select Employee from Deleted);

  Update Employee Set Status = 'Active' Where Id = @id;
end

Drop Trigger Activados

CREATE TRIGGER MontoTotal on Nomina 
After Insert
as
begin
 Declare @id Int;
 Set @id = (Select Id from Inserted);
 Declare @Total Decimal(13,2);

 Set @Total = (Select Sum(Salary) From Employee Where Status = 'Active');
 Update Nomina set MontoTotal = @Total Where Id = @id;

end


/*Pruebas*/
/*Select Employee.Code, Employee.Name, Employee.Lastname, Employee.Telephone, Employee.Admission, Employee.Salary, Employee.Status, Departament.Departamento, Employee.Position
from Departament 
Inner Join Employee on Departament.Id = Employee.Id

Select Employee.Code, Employee.Name, Employee.Lastname, Employee.Telephone, Employee.Admission, Employee.Salary, Employee.Status, Departament.Departamento, Employee.Position
from Employee 
Inner Join Departament on Employee.Id = Departament.Id Inner Join Position on Id.Employee = Position.Id

select Departament.Departamento from Departament;*/


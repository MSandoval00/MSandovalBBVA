CREATE DATABASE MSandovalBBVA

CREATE TABLE Productos(
IdProducto INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL, 
Precio INT NOT NULL,
TipoProducto CHAR(1)  NOT NULL
)

INSERT INTO Productos(
Nombre,Precio,TipoProducto)
VALUES('Bebidas',270,'A'),
('Galletas',340,'B'),
('Electronicos', 390,'C')


CREATE PROCEDURE ProductoGetAll
AS
SELECT 
IdProducto,
Nombre,
Precio,
TipoProducto
FROM Productos
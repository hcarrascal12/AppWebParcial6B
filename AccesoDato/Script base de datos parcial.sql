CREATE DATABASE bdbiblioteca6B;
USE bdbiblioteca6B;
/****** CREACIÓN DE TABLAS ******/
GO
/****** TABLA DE AUTORES ******/
CREATE TABLE autores(
	id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	descripcion text NOT NULL,
	estado varchar(1) NULL
);
GO
/****** TABLA DE CATEGORIAS ******/
CREATE TABLE categorias(
	id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	descripcion text NOT NULL,
	estado varchar(1) NOT NULL
);
GO
/****** TABLA DE EDICIONES DE LIBRO ******/
CREATE TABLE ediciones(
	id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	descripcion text NOT NULL,
	estado varchar(1) NULL
)
GO
/****** TABLA DE EDITORIALES DE LIBROS ******/
CREATE TABLE editorial(
	id int IDENTITY(1,1) NOT NULL,
	descripcion text NOT NULL,
	estado varchar(1) NOT NULL
)
GO
/****** TABLA DE LECTORES ******/
CREATE TABLE lectores(
	id int primary key IDENTITY(1,1) NOT NULL,
	n_ide varchar(15) NOT NULL,
	nombre varchar(100) NOT NULL,
	apellido varchar(100) NOT NULL,
	email varchar(100) NOT NULL,
	tel varchar(13) NOT NULL,
	estado varchar(1) NOT NULL
)
GO
/****** TABLA DE LIBROS ******/

CREATE TABLE libro(
	id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	isbn varchar(30) NOT NULL,
	nombre text NOT NULL,
	idautor int foreign key references autores(Id) NOT NULL,
	idEdicion int foreign key references ediciones(Id) NOT NULL,
	idEditorial int foreign key references editorial(Id) NOT NULL,
	idCategoria int foreign key references categorias(Id) NOT NULL,
	ESTADO varchar(1) NOT NULL,
	disponible varchar(1) NULL,
);
GO
/****** TABLA DE PRESTAMO DE LIBROS ******/
CREATE TABLE PRESTAMO(
	id int primary key IDENTITY(1,1) NOT NULL,
	IdLector int foreign key references lectores(Id) NULL,
	IdLibro int foreign key references libro(Id) NULL,
	FechaDevolucion datetime NULL,
	FechaConfirmacionDevolucion datetime NULL,
	EstadoEntregado varchar(100) NULL,
	EstadoRecibido varchar(100) NULL,
	EstadoPrestamo varchar(1) default 'A' NULL,
	FechaPrestamo datetime default getdate() NULL,
);
GO
/****** TABLA DE ROLES DE USUARIO ******/
CREATE TABLE tipo_usuario(
	id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	nombre varchar(30) NOT NULL,
);
/****** TABLA DE USUARIOS ******/
GO
CREATE TABLE usuarios(
	id int primary key IDENTITY(1,1) NOT NULL,
	nombre varchar(100) NOT NULL,
	apellido varchar(100) NOT NULL,
	email varchar(100) NOT NULL,
	password text NOT NULL,
	idTipoUsuario int foreign key references tipo_usuario(Id) NOT NULL,
	estado varchar(1) NOT NULL,
	username varchar(100) NOT NULL,
);

/****** VALORES POR DEFECTO ******/
GO
/****** USUARIO POR DEFECTO ******/
INSERT INTO usuarios(nombre, apellido, email, password, idTipoUsuario, estado, username) VALUES('Hasley', 'Carrascal', 'hasley1999@hotmail.com', 'o4iqTJvxB4l3jP6Zpw2INIq/jKCahidQc6AzgzL6G9U/7kCVXT9fQZbxlNZChDw2', 1, 'A', 'hasley.carrascal');
GO
/****** EDICIONES DE LIBRO POR DEFECTO ******/
INSERT INTO ediciones (descripcion, estado) VALUES ('PRIMERA EDICION', 'A');
INSERT INTO ediciones (descripcion, estado) VALUES ('SEGUNDA EDICION', 'A');
INSERT INTO ediciones (descripcion, estado) VALUES ('TERCERA EDICION', 'A');
INSERT INTO ediciones (descripcion, estado) VALUES ('CUARTA EDICION', 'A');
GO
/****** ROLES DE USUARIOS POR DEFECTO ******/
INSERT INTO tipo_usuario (descripcion) VALUES ('ADMINISTRADOR');
INSERT INTO tipo_usuario (descripcion) VALUES ('EMPLEADO');
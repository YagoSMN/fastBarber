CREATE TABLE FB_Cliente (
	Id			INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Cpf			DECIMAL(11, 0) NOT NULL,
	Nome		NVARCHAR(20) NOT NULL,
	SNome		NVARCHAR(10) NOT NULL,
	DataNasc	Date NOT NULL,
	Tel			NVARCHAR(20) NOT NULL,
	Email		NVARCHAR(50),
);

CREATE TABLE FB_Barber(
	Id			INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Nome		NVARCHAR(20) NOT NULL,
	Email		NVARCHAR(30) NOT NULL,
	Senha		NVARCHAR(12) NOT NULL
);

CREATE TABLE FB_HorariosMarc (
	Id				INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Id_Cliente		INT FOREIGN KEY REFERENCES FB_Cliente(Id) NOT NULL,
	StatusCorte		INT NOT NULL,
	BarberId		INT FOREIGN KEY REFERENCES FB_Barber(Id) NOT NULL,
	DataCorte		DATETIME NOT NULL,
	TempoCorte		Time,
	DatCancelamento DateTime,
	DatFinalizado	DateTime
);

CREATE TABLE FB_HorarioFunc(
	Id							INT PRIMARY KEY NOT NULL,
	Nome_Dia					NVARCHAR(13) NOT NULL,
	Horario_Inicio				Time NOT NULL,
	Horario_AlmocoInicio		Time NOT NULL,
	Horario_AlmocoFim			Time NOT NULL,
	Horario_Fim					Time NOT NULL,
	Ind_Ativo					char NOT NULL,
)

INSERT INTO FB_Barber(Nome, Email, Senha) VALUES ('yago', 'yagohenriquest@gmail.com', '123456789123');

INSERT INTO FB_Cliente (Cpf, Nome, SNome, DataNasc, Tel, Email)VALUES	('96375249082', 'Pedro', 'Maldoso', '2003-02-10', '(16) 99234-4123', 'pedro@maldosoprod.com'),
									('96360112060', 'Kauan', 'Flash', '1999-10-05', '(16) 99223-2533', 'roger.guedes@smn.com.br')

INSERT INTO FB_HorarioFunc(Id, Nome_Dia, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES (1, 'Segunda-Feira', '08:00:00', '13:00:00','14:30:00','21:30:00', 'S')
INSERT INTO FB_HorarioFunc(Id, Nome_Dia, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES (2, 'Terça-Feira', '08:00:00', '13:00:00','14:30:00','21:30:00', 'S')
INSERT INTO FB_HorarioFunc(Id, Nome_Dia, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES (3, 'Quarta-Feira', '08:00:00', '13:00:00','14:30:00','21:30:00', 'S')
INSERT INTO FB_HorarioFunc(Id, Nome_Dia, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES (4, 'Quinta-Feira', '08:00:00', '13:00:00','14:30:00','21:30:00', 'S')
INSERT INTO FB_HorarioFunc(Id, Nome_Dia, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES (5, 'Sexta-Feira', '08:00:00', '13:00:00','14:30:00','21:30:00', 'S')
INSERT INTO FB_HorarioFunc(Id, Nome_Dia, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES (6, 'Sábado', '08:00:00', '13:00:00','14:30:00','21:30:00', 'S')
INSERT INTO FB_HorarioFunc(Id, Nome_Dia, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES (0, 'Domingo', '08:00:00', '13:00:00','14:30:00','21:30:00', 'S')
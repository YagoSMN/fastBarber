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
	TempoCorte		NVARCHAR(8)
);

CREATE TABLE FB_HorarioFunc(
	Id							INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Nome_Semana					NVARCHAR(13) NOT NULL,
	Horario_Inicio				Time NOT NULL,
	Horario_AlmocoInicio		Time NOT NULL,
	Horario_AlmocoFim			Time NOT NULL,
	Horario_Fim					Time NOT NULL,
	Ind_Ativo						Bit NOT NULL,
)

INSERT INTO FB_Barber(Nome, Email, Senha) VALUES ('yago', 'yagohenriquest@gmail.com', '123456789123');
INSERT INTO FB_HorarioFunc(Nome_Semana, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES ('Segunda-Feira', '00:00:00', '00:00:00','00:00:00','00:00:00', 1)
INSERT INTO FB_HorarioFunc(Nome_Semana, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES ('Terça-Feira', '00:00:00', '00:00:00','00:00:00','00:00:00', 1)
INSERT INTO FB_HorarioFunc(Nome_Semana, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES ('Quarta-Feira', '00:00:00', '00:00:00','00:00:00','00:00:00', 1)
INSERT INTO FB_HorarioFunc(Nome_Semana, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES ('Quinta-Feira', '00:00:00', '00:00:00','00:00:00','00:00:00', 1)
INSERT INTO FB_HorarioFunc(Nome_Semana, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES ('Sexta-Feira', '00:00:00', '00:00:00','00:00:00','00:00:00', 1)
INSERT INTO FB_HorarioFunc(Nome_Semana, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES ('Sábado', '00:00:00', '00:00:00','00:00:00','00:00:00', 1)
INSERT INTO FB_HorarioFunc(Nome_Semana, Horario_Inicio, Horario_AlmocoInicio, Horario_AlmocoFim, Horario_Fim, Ind_Ativo) VALUES ('Domingo', '01:00:00', '00:00:00','00:00:00','00:00:00', 1)

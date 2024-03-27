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
	TempoCorte		DateTime
);

INSERT INTO FB_Barber(Nome, Email, Senha) VALUES ('yago', 'yagohenriquest@gmail.com', '123456789123');
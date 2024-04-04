CREATE PROCEDURE [dbo].[FBSP_MostraHorariosMarc]
	AS
	/*
	Documentação
	Objetivo..........: Buscar os cortes marcados para o dia atual
	Autor.............: SMN - Yago S.
	Data..............: 16/05/2020
	Ex................: EXEC [dbo].[FBSP_MostraHorariosMarc]
	*/
	BEGIN 
		DECLARE @DataAtual AS DATE
		SET @DataAtual = GETDATE() -- Obtém a data atual

		SELECT	hm.Id,
				hm.Id_Cliente,
				c.CPF,
				c.Nome,
				c.SNome,
				c.Email,
				c.DataNasc,
				c.Tel,
				hm.DataCorte,
				hm.StatusCorte
			FROM [dbo].[FB_HorariosMarc] hm
				INNER JOIN [dbo].[FB_Cliente] c
					ON c.Id = hm.Id_Cliente 
			WHERE CAST(hm.DataCorte AS DATE) = @DataAtual -- Compara apenas o dia da data de corte
			ORDER BY hm.DataCorte ASC
	END

CREATE PROCEDURE [dbo].[FBSP_AddOrDenyCostumer]
	@Cpf	DECIMAL(11, 0),
	@Nome	NVARCHAR(20),
	@SNome	NVARCHAR(10),
	@DataNasc	Date,
	@Tel		NVARCHAR(20),
	@Email		NVARCHAR(50),
	@Result NVARCHAR(100) OUTPUT -- Added an OUTPUT parameter for result message

AS
	/*
	Documentação
	Objetivo..........: Identificar se o cliente existe ou criá-lo
	Autor.............: SMN - Yago S.
	Data..............: 16/05/2020
	Ex................: EXEC [dbo].[FBSP_AddOrDenyCostumer]
	*/
	BEGIN 
		DECLARE @ErrorMessage NVARCHAR(100);

		SELECT @ErrorMessage = 'Cadastro inválido, Cliente já existe!'
		FROM [dbo].[FB_Cliente] c
		WHERE c.CPF = @Cpf;
			
		IF @@ROWCOUNT = 0
		BEGIN
			INSERT INTO FB_Cliente VALUES (@Cpf, @Nome, @SNome, @DataNasc, @Tel, @Email);
			SET @Result = 'Sucesso, Cliente cadastrado!';
		END
		ELSE
		BEGIN
			RAISERROR(@ErrorMessage, 16, 1);
		END;
	END;

CREATE PROCEDURE [dbo].[FBSP_BuscaOcupadoData]
    @DataMarcacao NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DataCorte DATE;

    -- Converter a string de data para o tipo DATE
    SET @DataCorte = CONVERT(DATE, @DataMarcacao, 103);

    -- Selecionar os cortes marcados na data fornecida
    SELECT	hm.Id,
			hm.DataCorte
    FROM [dbo].[FB_HorariosMarc] hm -- Substitua pelo nome real da sua tabela de cortes
    WHERE CONVERT(DATE, DataCorte, 103) = @DataCorte AND StatusCorte IN (2);
END;

CREATE PROCEDURE [dbo].[FBSP_BuscarCorteCliente]
    @Cpf DECIMAL(11, 0)
AS
BEGIN
    DECLARE @DataAtual DATE = GETDATE();
    DECLARE @DataLimiteSuperior DATETIME = DATEADD(DAY, 7, @DataAtual);

    
    IF NOT EXISTS (SELECT 1 FROM FB_Cliente WHERE CPF = @Cpf)
    BEGIN
        RAISERROR('CPF não cadastrado!', 16, 1);
        RETURN; 
    END

    SELECT	hm.Id,
            hm.Id_Cliente,
            hm.StatusCorte,
            hm.BarberId,
            hm.DataCorte,
            hm.TempoCorte	
    FROM FB_HorariosMarc hm
    INNER JOIN FB_Cliente c ON c.Id = hm.Id_Cliente
    WHERE c.CPF = @Cpf
        AND hm.StatusCorte = 2
        AND DataCorte BETWEEN @DataAtual AND @DataLimiteSuperior
        AND NOT EXISTS (
            SELECT 1
            FROM FB_HorariosMarc hm2
            WHERE hm2.Id_Cliente = hm.Id_Cliente
                AND hm2.DataCorte = hm.DataCorte
                AND hm2.StatusCorte = 2
                AND hm2.Id <> hm.Id
        );
END;

CREATE PROCEDURE [dbo].[FBSP_DesmarcarHorario]
    @Id INT
AS
/*
    Documentação
    Objetivo..........: Desmarcar horário cliente
    Autor.............: Yago
    Data..............: 12/11/2023
    Ex................: EXEC [dbo].[FBSP_ExistsCostumer] 20
    */
BEGIN
    UPDATE FB_HorariosMarc SET StatusCorte = 3 WHERE Id = @Id;
END;

CREATE PROCEDURE [dbo].[FBSP_ExistsCostumer]
    @Cpf DECIMAL(11, 0)
AS
/*
    Documentação
    Objetivo..........: Verificar se o cliente existe na tabela FB_Cliente
    Autor.............: [Seu Nome]
    Data..............: [Data Atual]
    Ex................: EXEC [dbo].[FBSP_CheckIfClientExists] 12345678901
    */
BEGIN
    DECLARE @ClientExists INT;

    -- Inicializar a variável
    SET @ClientExists = 0;

    -- Verificar se o CPF existe na tabela FB_Cliente
    IF EXISTS (SELECT 1 FROM FB_Cliente WHERE CPF = @Cpf)
    BEGIN
        -- Cliente existe
        SET @ClientExists = 1;
    END

    -- Retornar o resultado
    SELECT @ClientExists AS Result;
END;

CREATE PROCEDURE [dbo].[FBSP_MarcarHorario]
    @Cpf DECIMAL(11,0),
	@DataCorte NVARCHAR(20)
AS
BEGIN
	DECLARE @Id_Cliente INT;
	SELECT @Id_Cliente = Id FROM FB_Cliente WHERE CPF = @Cpf;

    INSERT INTO FB_HorariosMarc (Id_Cliente, StatusCorte, BarberId, DataCorte)
    VALUES (@Id_Cliente, 2, 1, CONVERT(DATETIME, @DataCorte, 103));
END;

ALTER PROCEDURE [dbo].[FBSP_SelCliente]
	@Id INT
	AS
	/*
	Documentação
	Objetivo..........: Buscar o cliente por id
	Autor.............: SMN - Yago S.
	Data..............: 16/05/2020
	Ex................: EXEC [dbo].[FBSP_SelCliente] 4
	*/
	BEGIN 

		SELECT TOP 1	hm.Id,
						hm.Id_Cliente,
						c.CPF,
						c.Nome,
						c.SNome,
						hm.BarberId,
						b.Nome AS BarberName,
						c.Email,
						c.DataNasc,
						c.Tel,
						hm.DataCorte,
						hm.StatusCorte,
						hm.TempoCorte
					FROM [dbo].[FB_HorariosMarc] hm
					INNER JOIN [dbo].[FB_Cliente] c ON c.Id = hm.Id_Cliente
					INNER JOIN [dbo].[FB_Barber] b ON hm.BarberId = b.Id
					WHERE hm.Id = @Id 
					ORDER BY hm.DataCorte ASC

	END

CREATE PROCEDURE [dbo].[FBSP_finalizarHorario]
	@Id			INT,
	@TempoCorte NVARCHAR(8)

	AS
	/*
	Documentação
	Objetivo..........: finalizar o corte como concluido.
	Autor.............: SMN - Yago S.
	Data..............: 16/05/2020
	Ex................: EXEC [dbo].[FBSP_finalizarHorario] 4
	*/
	BEGIN 
		UPDATE FB_HorariosMarc SET StatusCorte = 1, TempoCorte = @TempoCorte WHERE Id = @Id
	END

CREATE PROCEDURE [dbo].[FBSP_BuscaDiaSemana]
	@Id			INT

	AS
	/*
	Documentação
	Objetivo..........: buscar info do dia da semana.
	Autor.............: SMN - Yago S.
	Data..............: 16/05/2020
	Ex................: EXEC [dbo].[FBSP_BuscaDiaSemana] 2
	*/
	BEGIN 
		SELECT *
			FROM [dbo].[FB_HorarioFunc] hf
			WHERE hf.Id = @Id
	END
CREATE PROCEDURE [dbo].[FBSP_AddOrDenyCostumer]
	@Cpf	DECIMAL(11, 0),
	@Nome	NVARCHAR(20),
	@SNome	NVARCHAR(10),
	@DataNasc	Date,
	@Tel		NVARCHAR(11),
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


CREATE PROCEDURE FBSP_ExistsCostumer
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


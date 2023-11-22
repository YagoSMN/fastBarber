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


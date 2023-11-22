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
				c.Sobrenome,
				c.Email,
				c.DataNasc,
				c.Tel,
				hm.DataCorte,
				hm.StatusCorte
			FROM [dbo].[FB_HorariosMarc] hm
				INNER JOIN [dbo].[FB_Cliente] c
					ON c.Id = hm.Id_Cliente 
			WHERE CAST(hm.DataCorte AS DATE) = @DataAtual -- Compara apenas o dia da data de corte
	END


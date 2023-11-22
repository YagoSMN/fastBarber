CREATE PROCEDURE FBSP_BuscarCorteCliente
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
                AND hm2.Id <> hm.Id -- 
        );
END;


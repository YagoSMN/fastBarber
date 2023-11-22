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


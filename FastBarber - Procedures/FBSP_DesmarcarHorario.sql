CREATE PROCEDURE FBSP_DesmarcarHorario
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



USE corporativo
GO

CREATE OR ALTER PROCEDURE PROC_LISTAR_CLIENTES
AS
BEGIN
    SELECT
        Idclientes,
        Documento,
        Nome,
        Sexo,
        Email,
        Telefone,
        UF
    FROM Clientes
END
GO
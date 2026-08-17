USE corporativo
GO

CREATE OR ALTER PROCEDURE PROC_GET_CLIENTES
(
    @Idclientes INT
)
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
    WHERE Idclientes = @Idclientes
END
GO
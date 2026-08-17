USE corporativo
GO

ALTER PROCEDURE PROC_UPDATE_CLIENTES(
    @IdClientes INT,
    @Documento VARCHAR(15),
    @Nome VARCHAR(50),
    @Sexo VARCHAR(1),
    @Email VARCHAR(100),
    @Telefone VARCHAR(11),
    @UF VARCHAR(2)
)
AS
BEGIN
    UPDATE Clientes 
    SET Documento = @Documento, Nome = @Nome, Sexo = @Sexo, Email = @Email, Telefone = @Telefone, UF = @UF 
    WHERE IdClientes = @IdClientes
END
GO

USE corporativo
GO

CREATE OR ALTER PROCEDURE PRO_INSERIR_CLIENTES
(
    @Idclientes INT,
    @Documento VARCHAR(15),
    @Nome VARCHAR(50),
    @Sexo VARCHAR(1),
    @Email VARCHAR(100),
    @Telefone VARCHAR(11),
    @UF VARCHAR(2)
)
AS
BEGIN
    INSERT INTO Clientes
        (Idclientes, Documento, Nome, Sexo, Email, Telefone, UF)
    VALUES
        (@Idclientes, @Documento, @Nome, @Sexo, @Email, @Telefone, @UF)
END
GO
USE corporativo
GO
CREATE PROCEDURE PROC_DELETAR_CLIENTES(
	@Idclientes INT
)

AS 

BEGIN
	DELETE FROM Clientes WHERE Idclientes = @Idclientes
END
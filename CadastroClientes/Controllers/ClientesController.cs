using CadastroClientes.Models;
using CadastroClientes.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        //Instância de IConfiguration para carregar o appsettings.json em mémoria
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        [HttpPost("Salvar")]
        public object Salvar([FromBody] Clientes cliente)
        {
            try
            {
                var appConfig = new AppConnection(configuration);
                ClientesRepository clientes = new ClientesRepository(appConfig);

                var retorno = clientes.GetClient(cliente.Idclientes);

                if (retorno != null)      
                {
                    clientes.Atualizar(cliente);
                }
                else                       
                {
                    clientes.Salvar(cliente);
                }
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }

        [HttpPost("Alterar")]
        public object Alterar([FromBody] Clientes cliente)
        {
            try
            {
                var appConfig = new AppConnection(configuration);

                ClientesRepository clientesRepo = new ClientesRepository(appConfig);

                clientesRepo.Atualizar(cliente);

                return true;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpGet("Listar")]
        public object Listar()
        {
            List<Clientes> listaCli = null;
            try
            {
                var appConfig = new AppConnection(configuration);

                ClientesRepository clientesRepo = new ClientesRepository(appConfig);
                listaCli = clientesRepo.Listar();

            }
            catch (Exception ex)
            {
               
            }
            return listaCli;
        }

        [HttpDelete("Deletar")]
        public object Deletar(int Idclientes)
        {
            try
            {
                var appConfig = new AppConnection(configuration);


                ClientesRepository clientesRepo = new ClientesRepository(appConfig);
                bool retornoDelete = clientesRepo.Deletar(Idclientes);

                return retornoDelete;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetClient")]

        public object GetCliente(int Idclientes)
        {
            try
            {
                var appConfig = new AppConnection(configuration);

                ClientesRepository cliente = new ClientesRepository(appConfig);
                var retorno = cliente.GetClient(Idclientes);
                return retorno;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}

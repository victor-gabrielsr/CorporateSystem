using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace CadastroClientes.Models.Repository
{
    public class ClientesRepository
    {
        private AppConnection _appConfig;


        public ClientesRepository(AppConnection appconfig)
        {
            _appConfig = appconfig;
        }


        public void Salvar(Clientes clientes)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("PRO_INSERIR_CLIENTES", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Documento", clientes.Documento);
                        cmd.Parameters.AddWithValue("@Nome", clientes.Nome);
                        cmd.Parameters.AddWithValue("@Sexo", clientes.Sexo);
                        cmd.Parameters.AddWithValue("@Email", clientes.Email);
                        cmd.Parameters.AddWithValue("@Telefone", clientes.Telefone);
                        cmd.Parameters.AddWithValue("@UF", clientes.UF);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            
            }
        }

        public void Atualizar(Clientes clientes)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("PROC_UPDATE_CLIENTES", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Idclientes", clientes.Idclientes);
                        cmd.Parameters.AddWithValue("@Documento", clientes.Documento);
                        cmd.Parameters.AddWithValue("@Nome", clientes.Nome);
                        cmd.Parameters.AddWithValue("@Sexo", clientes.Sexo);
                        cmd.Parameters.AddWithValue("@Email", clientes.Email);
                        cmd.Parameters.AddWithValue("@Telefone", clientes.Telefone);
                        cmd.Parameters.AddWithValue("@UF", clientes.UF);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }



        public List<Clientes> Listar()
        {
            List<Clientes> retorno = new List<Clientes>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("PROC_LISTAR_CLIENTES", connection))
                    {
                        //Pega as informações da lista do bando de dados e faz uma planilha do exel
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //Ira ler todas as linhas da tabela de clientes
                            while (reader.Read())
                            {
                                Clientes cliente = new Clientes();

                                cliente.Idclientes = Convert.ToInt32(reader["Idclientes"].ToString());
                                cliente.Email = reader["Email"].ToString();
                                cliente.Telefone = reader["Telefone"].ToString();
                                cliente.UF = reader["UF"].ToString();
                                cliente.Documento = reader["Documento"].ToString();
                                cliente.Sexo = reader["Sexo"].ToString();
                                cliente.Nome = reader["Nome"].ToString();

                                retorno.Add(cliente);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return retorno;
        }

        public bool Deletar(int Idclientes)
        {
            bool retorno = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("PROC_DELETAR_CLIENTES", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Idclientes", Idclientes);

                        int linhas = cmd.ExecuteNonQuery();
                        if (linhas > 0)
                        {
                            retorno = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return retorno;
        }

        public Clientes? GetClient(int Idclientes)
        {
            Clientes cliente = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("PROC_GET_CLIENTES", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Idclientes", Idclientes);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cliente = new Clientes();
                                cliente.Idclientes = Convert.ToInt32(reader["Idclientes"].ToString());
                                cliente.Email = reader["Email"].ToString();
                                cliente.Telefone = reader["Telefone"].ToString();
                                cliente.UF = reader["UF"].ToString();
                                cliente.Documento = reader["Documento"].ToString();
                                cliente.Sexo = reader["Sexo"].ToString();
                                cliente.Nome = reader["Nome"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return cliente;
        }
    }
}
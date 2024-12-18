using CadastroDeContatosVS;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Tutorial.DbConnection;

namespace CadastroDeContatosVS
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AtualizaTabela();
            }
        }
        public static List<Contato> ListContatoBancoDados = new List<Contato>();

        bool LinhaEditando;
        public int GerarId()
        {
            return ListContatoBancoDados.Count > 0 ? ListContatoBancoDados.Max(c => c.Id) + 1 : 1;
        }
       
        public void PercorrerBanco()
        {
            DbConn dbHnd = new DbConn();

            try
            {
                dbHnd.OpenConnection();

                DbDataReader dbReader = dbHnd.ExecuteReader("SELECT ID, Nome, Email, DataNascimento, CPF, Cidade, Estado, Endereço FROM Clientes");
                //uma consulta é executada para selecionar os campos,o resultado é armazenado em dbReader.
                while (dbReader.Read())//
                {
                    //while percorre cada registro retornado pela consulta ,para cada registro os valores dos campos são lidos.
                    int codigo = int.Parse(dbReader["ID"].ToString());
                    string nome = dbReader["Nome"].ToString();
                    string email = dbReader["Email"].ToString();
                    string dataNascimento = dbReader["DataNascimento"].ToString();
                    string CPF = dbReader["CPF"].ToString();
                    string cidade = dbReader["Cidade"].ToString();
                    string estado = dbReader["Estado"].ToString();
                    string endereco = dbReader["Endereço"].ToString();

                        bool novoContato = !ListContatoBancoDados.Any(c => c.Id == codigo);
                        //ANY verifica se algum elemento no banco de dados atende a uma condição específica.
                        // ! está invertendo o resultado do método Any
                        //verifica se nenhum dos contatos na lista ListContatoBancoDados tem um Id igual ao valor de codigo.

                        if (novoContato)
                        {
                            Contato contatoBanco = new Contato()
                            {
                                Id = codigo,
                                Nome = nome,
                                Email = email,
                                DataDeNascimento = dataNascimento,
                                Cpf = CPF,
                                Cidade = cidade,
                                Estado = estado,
                                Endereco = endereco
                            };
                            ListContatoBancoDados.Add(contatoBanco);
                        }
                    }
                
                AtualizaTabela();

                dbReader.Dispose();//Libera a conexão para ser utilizada em outros comandos. 

                dbHnd.CloseConnection();
            }
            finally

            {
                dbHnd.Dispose();
            }
        }
        protected void Cadastrar_Click(object sender, EventArgs e)
        {
            if (NomeTxt.Text == "" || EmailTxt.Text == "" || DataTxt.Text == "" || CpfTxt.Text == "")
            {
                return;
            }
            if (Session["LinhaEditando"] == null)
            {
                Session["LinhaEditando"] = false;
            }

            LinhaEditando = (bool)Session["LinhaEditando"];//Session usado para armazenar variáveis de sessão.
           
            //Session["armLista"] = ListContatoBancoDados; //para armazenar a lista depois de colocar algo nela.
            //ListContatoBancoDados = Session["armLista"] as List<Contato>;
            //antes de colocar algo na lista, trago ela da ultima atualizaçao "session".
            if (LinhaEditando)
                {
                    int id = (int)Session["IdContatoEditado"];
                    Contato contato = ListContatoBancoDados.FirstOrDefault(c => c.Id == id);
                    //busca o primeiro contato na lista ListContatoBancoDados cujo Id seja igual ao valor de id.

                    if (contato != null)
                    {
                        contato.Nome = NomeTxt.Text;
                        contato.Email = EmailTxt.Text;
                        contato.DataDeNascimento = DataTxt.Text;
                        contato.Cpf = CpfTxt.Text;
                        contato.Cidade = CidadeTxt.Text;
                        contato.Estado = EstadoTxt.Text;
                        contato.Endereco = EnderecoTxt.Text;
                    }

                    DbConn dbHnd = new DbConn();

                    try
                    {
                        dbHnd.OpenConnection();

                        dbHnd.AddParameter("Nome", NomeTxt.Text);
                        dbHnd.AddParameter("Email", EmailTxt.Text);
                        dbHnd.AddParameter("DataNascimento", DataTxt.Text);
                        dbHnd.AddParameter("CPF", CpfTxt.Text);
                        dbHnd.AddParameter("Cidade", CidadeTxt.Text);
                        dbHnd.AddParameter("Estado", EstadoTxt.Text);
                        dbHnd.AddParameter("Endereço", EnderecoTxt.Text);

                        // @ retorna parâmetro

                        dbHnd.ExecuteNonQuery("UPDATE Clientes SET Nome=@Nome, Email=@Email, DataNascimento=@DataNascimento, CPF=@CPF, Cidade=@Cidade, Estado=@Estado, Endereço=@Endereço WHERE ID = " + id);

                        dbHnd.CloseConnection();
                    }

                    finally
                    {
                        dbHnd.Dispose();
                    }
                
                    AtualizaTabela();
                
                Session["LinhaEditando"] = false;
                
            }
            else
            {
                    DbConn dbHnd = new DbConn();

                    try
                    {
                        dbHnd.OpenConnection();

                        dbHnd.AddParameter("Nome", NomeTxt.Text);
                        dbHnd.AddParameter("Email", EmailTxt.Text);
                        dbHnd.AddParameter("DataNascimento", DataTxt.Text);
                        dbHnd.AddParameter("CPF", CpfTxt.Text);
                        dbHnd.AddParameter("Cidade", CidadeTxt.Text);
                        dbHnd.AddParameter("Estado", EstadoTxt.Text);
                        dbHnd.AddParameter("Endereço", EnderecoTxt.Text);

                        dbHnd.ExecuteNonQuery("INSERT INTO Clientes (Nome,Email,DataNascimento,CPF,Cidade,Estado,Endereço)values(@Nome , @Email , @DataNascimento , @CPF , @Cidade , @Estado , @Endereço)");

                        dbHnd.CloseConnection();

                    }
                    finally

                    {
                        PercorrerBanco();
                        dbHnd.Dispose();

                    }
            }
            Limpar_Click(sender, e);
        }
        private void AtualizaTabela()
        {
            repeater.DataSource = ListContatoBancoDados;//coleção/fonte de dados
            repeater.DataBind();//ela atualiza o repeater para exibir os dados contidos em ListContatoBancoDados.
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;//obtenção do botão clicado.
            int id = Convert.ToInt32(botao.CommandArgument);//conversão do argumento do comando.
            Contato contato = ListContatoBancoDados.FirstOrDefault(c => c.Id == id);
            //Esta é uma expressão lambda que define a condição para o método FirstOrDefault.

            //Se um contato com o Id correspondente for encontrado, contato será esse objeto Contato,
            //caso não for encontrado, contato será null.
            if (contato != null)
            {
                NomeTxt.Text = contato.Nome;
                EmailTxt.Text = contato.Email;
                DataTxt.Text = contato.DataDeNascimento;
                CpfTxt.Text = contato.Cpf;
                CidadeTxt.Text = contato.Cidade;
                EstadoTxt.Text = contato.Estado;
                EnderecoTxt.Text = contato.Endereco;
            }
            
                LinhaEditando = true;
            
            Session["LinhaEditando"] = LinhaEditando;
           
            Session["IdContatoEditado"] = id;

        }

        protected void Limpar_Click(object sender, EventArgs e)
        {
            NomeTxt.Text = string.Empty;
            EmailTxt.Text = string.Empty;
            DataTxt.Text = string.Empty;
            CpfTxt.Text = string.Empty;
            CidadeTxt.Text = string.Empty;
            EstadoTxt.Text = string.Empty;
            EnderecoTxt.Text = string.Empty;
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;
            int id = Convert.ToInt32(botao.CommandArgument);
            Contato contato = ListContatoBancoDados.FirstOrDefault(c => c.Id == id);
            if (contato != null)
            {
                ListContatoBancoDados.Remove(contato);
            }
            AtualizaTabela();

            DbConn dbHnd = new DbConn();

            try
            {
                dbHnd.OpenConnection();

                dbHnd.ExecuteNonQuery("DELETE FROM Clientes WHERE ID = " + id);

                dbHnd.CloseConnection();
            }
            finally
            {
                dbHnd.Dispose();
            }
            Limpar_Click(sender, e);
            Session["LinhaEditando"] = false;
        }
    }
}

//Repeater é um controle de servidor que permite repetir uma lista.
//'<%# Eval("Id") %>' Passar o ID de um usuário específico para o evento de clique no servidor.
//O CommandArgument é usado para armazenar esse ID.
//<%# ... %>: Esta é a sintaxe de data binding.
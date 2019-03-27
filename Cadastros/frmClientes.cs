using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Cadastros
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SalvarCliente(string nome, string estadocivil)
        {
            StreamWriter arquivo;
            string caminho = "C:\\Banco De Dados\\Cadastro Clientes e Funcionarios\\Clientes.txt";
            arquivo = File.AppendText(caminho);
            arquivo.WriteLine();
            arquivo.WriteLine("Cadastro de Clientes");
            arquivo.WriteLine("Nome: " + nome);
            arquivo.WriteLine("Telefone: " + maskTelefone.Text);
            arquivo.WriteLine("Estado Civil: " + estadocivil);
            arquivo.WriteLine("Endereço: " + txtRua.Text + " Numero: "+txtNumero.Text);
            arquivo.WriteLine("Bairro: " + txtBairro.Text);
            arquivo.WriteLine("Cidade: " + txtCidade.Text);
            arquivo.WriteLine("Estado: " + txtEstado.Text);
            arquivo.WriteLine("Dia do Cadastro: " + data1.Text);
            arquivo.WriteLine("______________________________________");
            arquivo.WriteLine();
            arquivo.Close();
            MessageBox.Show("Cliente Cadastrado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void LimparCliente()
        {
            txtNome.Clear();
            maskTelefone.Clear();
            txtRua.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtEstado.Clear();
            txtNumero.Clear();
            txtCep.Clear();
            rdbCasado.Checked = false;
            rdbSolteiro.Checked = false;
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            data1.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome, telefone;
            nome = txtNome.Text;
            telefone = maskTelefone.Text;
            SalvarCliente(nome, telefone);

            //Cria um botão com dialogo para continuar , S/N
            DialogResult dialogResult = MessageBox.Show("Deseja cadastrar outro cliente?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //Cria um IF para o botao acima , se caso SIM , ele returna para o FORM e o lIMPA os campos , caso NÃO ele o FECHA !
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                LimparCliente();
                return;
            }

            //Caso NÃO ele o FECHA !
            else
            {
                Application.Exit();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string caminho = "C:\\Banco De Dados\\Cadastro Clientes e Funcionarios\\clientes.txt";
            System.Diagnostics.Process.Start("NOTEPAD", caminho);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            {

                try
                {
                    var webService = new CORREIOS.AtendeClienteClient();
                    var resposta = webService.consultaCEP(txtCep.Text);
                    txtRua.Text += resposta.end;
                    txtBairro.Text += resposta.bairro;
                    txtCidade.Text += resposta.cidade;
                    txtEstado.Text += resposta.uf;
                    MessageBox.Show("Cadastro gerado com sucesso, Informe apenas o número");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Esse CEP não foi encontrado");
                }
            }
        }
    }
}


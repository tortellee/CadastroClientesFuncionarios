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
    public partial class frmFuncionarios : Form
    {
        public frmFuncionarios()
        {
            InitializeComponent();
        }

        private void SalvarFuncionario(string nome, string estadocivil)
        {
            StreamWriter arquivo;
            string caminho = "C:\\Banco De Dados\\Cadastro Clientes e Funcionarios\\Funcionarios.txt";
            arquivo = File.AppendText(caminho);
            arquivo.WriteLine();
            arquivo.WriteLine("Cadastro de Funcionários");
            arquivo.WriteLine("Nome: "+ nome);
            arquivo.WriteLine("Telefone: " + maskTelefone.Text);
            arquivo.WriteLine("Estado Civil: "+ estadocivil);
            arquivo.WriteLine("Endereço: " + txtRua.Text + " Numero: " + txtNumero.Text);
            arquivo.WriteLine("Bairro: " + txtBairro.Text);
            arquivo.WriteLine("Cidade: " + txtCidade.Text);
            arquivo.WriteLine("Estado: " + txtEstado.Text);
            arquivo.WriteLine("Dia do Cadastro: " + data2.Text);
            arquivo.WriteLine("______________________________________");
            arquivo.WriteLine();
            arquivo.Close();
            MessageBox.Show("Funcionário Cadastrado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LimparFuncionario()
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

        private void frmFuncionarios_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            data2.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome, estadoCivil;
            nome = txtNome.Text;
            if (rdbCasado.Checked == true)
            {
                estadoCivil = "Casado(a)";
            }
            else if (rdbSolteiro.Checked == true)
            {
                estadoCivil = "Solteiro(a)";
            }
            else
            {
                MessageBox.Show("Escolha um estado civil");
                return;
            }
            SalvarFuncionario(nome, estadoCivil);


            //Cria um botão com dialogo para continuar , S/N
            DialogResult dialogResult = MessageBox.Show("Deseja cadastrar outro funcionário?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //Cria um IF para o botao acima , se caso SIM , ele returna para o FORM e o lIMPA os campos , caso NÃO ele o FECHA !
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                LimparFuncionario();
                return;
            }

            //Caso NÃO ele o FECHA !
            else
            {
                Application.Exit();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string caminho = "C:\\Banco De Dados\\Cadastro Clientes e Funcionarios\\funcionarios.txt";
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

        private void txtcep_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
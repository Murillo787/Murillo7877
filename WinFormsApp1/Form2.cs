using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public partial class FormCadastro : System.Windows.Forms.Form
    {
        public FormCadastro()
        {
            InitializeComponent();
            this.AcceptButton = btnSalvar;
        }
        private void btnSalvar_Click_1(object sender, EventArgs e) // Ação do botão salvar
        {
            

        }

        private void btnCancelar_Click_1(object sender, EventArgs e) // ação do botão cancelar
        {
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProduto.Text))
            {
                MessageBox.Show("Informe o nome do produto.");
                return;
            }

            if (numQuantidade.Value <= 0)
            {
                MessageBox.Show("Informe a quantidade.");
                return;
            }

            try
            {
                using (var conexao = Database.GetConnection())
                {
                    conexao.Open();
                    string query = "INSERT INTO itens (produto, quantidade) VALUES (@produto, @quantidade)";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@produto", txtProduto.Text.Trim());
                    cmd.Parameters.AddWithValue("@quantidade", Convert.ToInt32(numQuantidade.Value));
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Item cadastrado com sucesso!");
                this.DialogResult = DialogResult.OK; // sinaliza sucesso para quem abriu o formulário
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

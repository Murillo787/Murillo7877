using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using MySqlConnector;

namespace WinFormsApp1
{
    public partial class FormAtualizar : System.Windows.Forms.Form
    {
        private int _id;
        public FormAtualizar(int id)
        {
            InitializeComponent();
            _id = id;
            this.AcceptButton = btnSalvar;
            CarregarItem();
            _id = id;
        }

        public FormAtualizar()
        {
        }

        private void CarregarItem()
        {
            try
            {
                using (var conexao = Database.GetConnection())
                {
                    conexao.Open();
                    string query = "SELECT produto, quantidade, comprado FROM itens WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@id", _id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtProduto.Text = reader["produto"].ToString();
                            numQuantidade.Value = Convert.ToDecimal(reader["quantidade"]);
                            chkComprado.Checked = Convert.ToBoolean(reader["comprado"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar item: " + ex.Message);
            }
        }

        private void btnSalvar_Click_1(object sender, EventArgs e) //Ação do botão salvar
        {
            try
            {
                using (var conexao = Database.GetConnection())
                {
                    conexao.Open();
                    string query = "UPDATE itens SET produto=@produto, quantidade=@quantidade, comprado=@comprado WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@produto", txtProduto.Text.Trim());
                    cmd.Parameters.AddWithValue("@quantidade", Convert.ToInt32(numQuantidade.Value));
                    cmd.Parameters.AddWithValue("@comprado", chkComprado.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@id", _id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Item atualizado!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar: " + ex.Message);
            }
        }

        private void FormAtualizar_Load(object sender, EventArgs e)
        {

        }
    }
}

using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormListagem : System.Windows.Forms.Form
    {



        public FormListagem()
        {
            InitializeComponent();
            CarregarDados();
        }
        private void CarregarDados()
        {
            try
            {
                using (var conexao = Database.GetConnection())
                {
                    conexao.Open();
                    string query = "SELECT id, produto, quantidade, comprado FROM itens";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conexao);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvItens.DataSource = dt;

                    if (dgvItens.Columns.Contains("id")) dgvItens.Columns["id"].Visible = false;
                    if (dgvItens.Columns.Contains("produto")) dgvItens.Columns["produto"].HeaderText = "Produto";
                    if (dgvItens.Columns.Contains("quantidade")) dgvItens.Columns["quantidade"].HeaderText = "Quantidade";
                    if (dgvItens.Columns.Contains("comprado")) dgvItens.Columns["comprado"].HeaderText = "Comprado";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }
        private void btnCadastrar_Click(object sender, EventArgs e) // ação ao clicar no botão cadastrar
        {

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            if (dgvItens.SelectedRows.Count == 0) { MessageBox.Show("Selecione um item para editar."); return; }

            int id = Convert.ToInt32(dgvItens.SelectedRows[0].Cells["id"].Value);
            using (var frm = new FormAtualizar(id))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarDados(); // atualiza depois da edição
                }
            }
        }



        private void btnApagar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            using (var frm = new FormCadastro())
            {
                // ShowDialog abre a janela e pausa aqui até ela fechar (modal)
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarDados(); // atualiza a lista se o cadastro tiver sido concluído
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvItens.SelectedRows.Count == 0) { MessageBox.Show("Selecione um item para editar."); return; }

            int id = Convert.ToInt32(dgvItens.SelectedRows[0].Cells["id"].Value);
            using (var frm = new FormAtualizar(id))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarDados(); // atualiza depois da edição
                }
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dgvItens.SelectedRows.Count == 0) { MessageBox.Show("Selecione um item para apagar."); return; }

            int id = Convert.ToInt32(dgvItens.SelectedRows[0].Cells["id"].Value);
            if (MessageBox.Show("Confirma exclusão?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var conexao = Database.GetConnection())
                    {
                        conexao.Open();
                        string query = "DELETE FROM itens WHERE id=@id";
                        MySqlCommand cmd = new MySqlCommand(query, conexao);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    CarregarDados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao apagar: " + ex.Message);
                }
            }
        }
    }
}

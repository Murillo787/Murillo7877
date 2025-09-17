using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            // selecione o nome da tela que deseja exibir exemplo FormCadastro

            FormCadastro cadastro = new FormCadastro();

            cadastro.Show(); // Abre em uma nova janela

            // cadastro.ShowDialog(); // Se quiser abrir como modal 
        }

        private void btnListagem_Click(object sender, EventArgs e)
        {
            FormListagem listagem = new FormListagem();

            listagem.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }
    }
}

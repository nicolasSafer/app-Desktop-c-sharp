using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3;

namespace ProjetoSenai.FormsAdm
{
    public partial class Tela_Cadas_Evento : Form
    {
        conexao con = new conexao();
        public Tela_Cadas_Evento()
        {
            InitializeComponent();
        }

        private void cadas_sala_Click(object sender, EventArgs e)
        {

            MySqlConnection conexao = con.getconexao();
            conexao.Open();
            string inserir1 = "insert into tb_evento(nome_evento,local_evento,data_evento,horario_evento,descricao_evento) values " + " (@nome_evento,@local_evento,@data_evento,@horario_evento,@descricao_evento)";
            MySqlCommand comando = new MySqlCommand(inserir1, conexao);
            comando.Parameters.AddWithValue("@nome_evento", box_nome_evento.Text);
            comando.Parameters.AddWithValue("@local_evento", local_evento.Text);
            comando.Parameters.AddWithValue("@data_evento", Convert.ToInt32(data_evento.Text));
            comando.Parameters.AddWithValue("@horario_evento", Convert.ToInt32(horario_evento.Text));
            comando.Parameters.AddWithValue("@descricao_evento", descricao_evento.Text);
            comando.CommandType = CommandType.Text;
            comando.ExecuteReader();
            MessageBox.Show("Foi");
            conexao.Close();







        }
    }
}

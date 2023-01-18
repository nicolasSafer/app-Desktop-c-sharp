﻿using MySql.Data.MySqlClient;
using ProjetoSenai.FormsAdm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3;

namespace ProjetoSenai
{
    public partial class TelaFeedback : Form
    {
        conexao con = new conexao();
        public string id_on;
        Thread f2;

        public TelaFeedback(string id)
        {
            id_on = id;
            InitializeComponent();
            MySqlConnection conexao = con.getconexao();
            conexao.Open();
            String sql;
            sql = "select * from tb_feedback_software";
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            MySqlDataAdapter dados = new MySqlDataAdapter(comando);
            DataTable dtUsuario = new DataTable();
            dados.Fill(dtUsuario);
            dataGridView1.DataSource = dtUsuario;

            conexao.Close();


        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
           
            MySqlConnection conexao = con.getconexao();
            conexao.Open();
            String sql;
            sql = "select * from tb_feedback_software";
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            MySqlDataAdapter dados = new MySqlDataAdapter(comando);
            DataTable dtUsuario = new DataTable();
            dados.Fill(dtUsuario);
            dataGridView1.DataSource = dtUsuario;

            conexao.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormsAdm.tela_cadas_feed prox = new FormsAdm.tela_cadas_feed();
            prox.ShowDialog();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            MySqlConnection conexao = con.getconexao();
            conexao.Open();
            string sql = "select tb_feedback_software.id_feedback from tb_feedback_software where id_feedback = @id_sala";
            MySqlCommand comando_2 = new MySqlCommand(sql, conexao);
            comando_2.Parameters.Clear();
            comando_2.Parameters.AddWithValue("@id_sala", box_pesquisa.Text);
            comando_2.CommandType = CommandType.Text;
            MySqlDataReader dr;
            dr = comando_2.ExecuteReader();
            dr.Read();
            try
            {
                if (dr.GetString(0) == box_pesquisa.Text)
                {
                    id_on = dr.GetString(0);
                    // Tela_att_Sala prox = new Tela_att_Sala(id_on);
                    //prox.ShowDialog();
                  //  this.Close();
                    f2 = new Thread(abrirjan2);
                    f2.SetApartmentState(ApartmentState.STA);
                    f2.Start(id_on);
                }
                else
                {
                    MessageBox.Show("Não existe");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não existe" + ex);
            }
            void abrirjan2(object obj)
            {
                Application.Run(new tela_cadas_feed(id_on));
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            var resposta = DialogResult;
            resposta = MessageBox.Show("Deseja realmente deletar a sala ?", "CUIDADO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta == DialogResult.Yes)
            {
                MySqlConnection conexao = con.getconexao();

                conexao.Open();
                string delet = "delete from tb_feedback_software where id_feedback = '" + box_pesquisa.Text + "'";
                MySqlCommand comando_3 = new MySqlCommand(delet, conexao);
                comando_3.CommandType = CommandType.Text;

                comando_3.ExecuteReader();

                conexao.Close();

                MessageBox.Show("deletado");

            }
            else
            {
                MessageBox.Show("Cancelado");
            }
        }
    }
}

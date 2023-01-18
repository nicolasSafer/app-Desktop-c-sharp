﻿using MaterialSkin.Properties;
using ProjetoSenai.FormsVis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoSenai
{
    public partial class TelaVisitante : Form
    {
        bool sidebarExpand;
        private Form activeForm;
        Thread nt;

        public TelaVisitante()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
        }

        //comando para a movimentação da tela
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        //evento para conseguir mover o software
        private void BarraSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //fechar o software
        private void Fechar_Click_1(object sender, EventArgs e)
        {
        Application.Exit();
        }

        //maximar e minimizar tela
        private void Janelas_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        //minimizar o software
        private void Minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Comando para abrir e fechar barra lateral
        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                //se a barra lateral estiver aberta, minimiza
                PainelLateral.Width -= 10;
                if (PainelLateral.Width == PainelLateral.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                //se a barra lateral estiver fechada, maximiza
                PainelLateral.Width += 10;
                if (PainelLateral.Width == PainelLateral.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }
        //final menu

        //conseguir abrir forms em um painel
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.PainelGeral.Controls.Add(childForm);
            this.PainelGeral.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void BtnInicio_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
        }

        private void BtnMapa_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TelaLoc());
        }

        private void BtnEvento_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TelaEventovis());
        }

        private void BtnFeedback_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TelaFeedback2());
        }

        private void BtnContato_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TelaContato());
        }

        private void BtnInfo_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TelaInfo());
        }
        //final das telas

        //sair do visitante
        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            nt = new Thread(AbrirJan);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void AbrirJan(object obj)
        {
            Application.Run(new TelaLogin());
        }
    }    
}

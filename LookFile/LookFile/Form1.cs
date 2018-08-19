using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

using Mail;

namespace LookFile
{
    public partial class Form1 : Form
    {
        Mail.Mail email = new Mail.Mail();

        public Form1()
        {
            InitializeComponent();
        }

        //Monitorar
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                loadXml();
            }
            catch (Exception)
            {
                MessageBox.Show("Arquivo de Configuração nao encontrado \r\n Ou contate o fornecedor: \r\n Bruno Vieira Roberti: 970146532");
            }

            try
            {
                fileSystemWatcher1.Path = textBox1.Text;

                fileSystemWatcher1.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.CreationTime;

                fileSystemWatcher1.EnableRaisingEvents = true;

                fileSystemWatcher1.IncludeSubdirectories = true;

                CheckForIllegalCrossThreadCalls = false;

                WaitForChangedResult wcr = fileSystemWatcher1.WaitForChanged(WatcherChangeTypes.All, 10000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifique a pasta monitorada / " + ex.Message + "Ou contate o fornecedor: \r\n Bruno Vieira Roberti: 970146532");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            ExecutarEventoSystem(sender, e);
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            ExecutarEventoSystem(sender, e);
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            ExecutarEventoSystem(sender, e);
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            ExecutarEventoRenamed(sender, e);
        }

        public void ExecutarEventoRenamed(object sender, RenamedEventArgs e)
        {
            if (chkRenomear.Checked)
            {
                StringBuilder texto = new StringBuilder();

                txtNotificacoes.Text = string.Empty;

                txtNotificacoes.Text += String.Format("Alteração: {0} {1}", e.FullPath, Environment.NewLine);
                txtNotificacoes.Text += String.Format("Nome: {0} {1}", e.Name, Environment.NewLine);
                txtNotificacoes.Text += String.Format("Evento: {0} {1}", e.ChangeType, Environment.NewLine);
                txtNotificacoes.Text += String.Format("----------------------- {0}", Environment.NewLine);

                texto.Append(String.Format("Alteração: {0} {1}", e.FullPath, Environment.NewLine));
                texto.Append(String.Format("Nome: {0} {1}", e.Name, Environment.NewLine));
                texto.Append(String.Format("Evento: {0} {1}", e.ChangeType, Environment.NewLine));
                texto.Append(String.Format("----------------------- {0}", Environment.NewLine));


                email.Enviar(txtNotificacoes.Text, e.Name, textBox1.Text);
            }            
        }

        public void ExecutarEventoSystem(object sender, FileSystemEventArgs e)
        {
            if (chkCriar.Checked)
            {
                StringBuilder texto = new StringBuilder();

                txtNotificacoes.Text = string.Empty;

                txtNotificacoes.Text += String.Format("Alteração: {0} {1}", e.FullPath, Environment.NewLine);
                txtNotificacoes.Text += String.Format("Nome: {0} {1}", e.Name, Environment.NewLine);
                txtNotificacoes.Text += String.Format("Evento: {0} {1}", e.ChangeType, Environment.NewLine);
                txtNotificacoes.Text += String.Format("----------------------- {0}", Environment.NewLine);

                texto.Append(String.Format("Alteração: {0} {1}", e.FullPath, Environment.NewLine));
                texto.Append(String.Format("Nome: {0} {1}", e.Name, Environment.NewLine));
                texto.Append(String.Format("Evento: {0} {1}", e.ChangeType, Environment.NewLine));
                texto.Append(String.Format("----------------------- {0}", Environment.NewLine));


                email.Enviar(txtNotificacoes.Text, e.Name, textBox1.Text);
            }            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(2014,01,01);

            if (DateTime.Now >= dt)
            {
                MessageBox.Show("Prazo excedido \r\n contate o fornecedor: \r\n Bruno Vieira Roberti: 970146532");
                this.Close();
            }

            notifyIcon1.Visible = true;
            notifyIcon1.Text = "LookFile";
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void exibirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enviarParaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cadastros frmCad = new Cadastros(this.email);

            frmCad.Show();
        }

        private void enviadoDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cadastros frmCad = new Cadastros(this.email);

            frmCad.Show();
        }

        private void sMTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cadastros frmCad = new Cadastros(this.email);

            frmCad.Show();
        }

        private void loadXml()
        {
            //Cria uma instância de um documento XML
            XmlDocument oXML = new XmlDocument();

            //Define o caminho do arquivo XML 
            string ArquivoXML = System.IO.Path.Combine(Environment.CurrentDirectory, "Emails.xml");
            //carrega o arquivo XML
            oXML.Load(ArquivoXML);

            email._smtp = oXML.SelectSingleNode("Email").ChildNodes[0].InnerText;

            email._mailDe = oXML.SelectSingleNode("Email").ChildNodes[1].InnerText;

            //Lê o filho de um Nó Pai específico 
            if (!string.IsNullOrEmpty(oXML.SelectSingleNode("Email").ChildNodes[2].InnerText))
            {
                email._mailPara.Add(oXML.SelectSingleNode("Email").ChildNodes[2].InnerText);
            }
            
            if (!string.IsNullOrEmpty(oXML.SelectSingleNode("Email").ChildNodes[3].InnerText))
            {
                email._mailPara.Add(oXML.SelectSingleNode("Email").ChildNodes[3].InnerText);
            }

            if (!string.IsNullOrEmpty(oXML.SelectSingleNode("Email").ChildNodes[4].InnerText))
            {
                email._mailPara.Add(oXML.SelectSingleNode("Email").ChildNodes[4].InnerText);
            }

            if (!string.IsNullOrEmpty(oXML.SelectSingleNode("Email").ChildNodes[5].InnerText))
            {
                email._mailPara.Add(oXML.SelectSingleNode("Email").ChildNodes[5].InnerText);
            }

            if (!string.IsNullOrEmpty(oXML.SelectSingleNode("Email").ChildNodes[6].InnerText))
            {
                email._mailPara.Add(oXML.SelectSingleNode("Email").ChildNodes[6].InnerText);
            }

            if (!string.IsNullOrEmpty(oXML.SelectSingleNode("Email").ChildNodes[7].InnerText))
            {
                email._senha = oXML.SelectSingleNode("Email").ChildNodes[7].InnerText;
            }

            if (!string.IsNullOrEmpty(oXML.SelectSingleNode("Email").ChildNodes[8].InnerText))
            {
                email._login = oXML.SelectSingleNode("Email").ChildNodes[8].InnerText;
            }

            if (!string.IsNullOrEmpty(oXML.SelectSingleNode("Email").ChildNodes[8].InnerText))
            {
                email._Porta = Convert.ToInt32(oXML.SelectSingleNode("Email").ChildNodes[9].InnerText);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cadastros frmCad = new Cadastros(this.email);

            frmCad.Show();
        }
    }
}

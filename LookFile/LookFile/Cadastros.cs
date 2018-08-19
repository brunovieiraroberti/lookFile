using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Mail;

namespace LookFile
{
    public partial class Cadastros : Form
    {
        public Mail.Mail objEmail = null;
        
        public Cadastros(Mail.Mail email)
        {
            InitializeComponent();

            objEmail = email;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(System.IO.Path.Combine(Environment.CurrentDirectory, "Emails.xml"), null);

                //inicia o documento xml
                writer.WriteStartDocument();
                //escreve o elmento raiz
                writer.WriteStartElement("Email");
                //Escreve os sub-elementos
                writer.WriteElementString("Smtp", textBox1.Text);
                writer.WriteElementString("De", textBox2.Text);
                writer.WriteElementString("Para", textBox3.Text);
                writer.WriteElementString("Para", textBox4.Text);
                writer.WriteElementString("Para", textBox5.Text);
                writer.WriteElementString("Para", textBox6.Text);
                writer.WriteElementString("Para", textBox7.Text);
                writer.WriteElementString("Senha", txtSenha.Text);
                writer.WriteElementString("Login", txtLogin.Text);
                writer.WriteElementString("Porta", txtPorta.Text);
                // encerra o elemento raiz
                writer.WriteEndElement();                

                //Escreve o XML para o arquivo e fecha o objeto escritor
                writer.Close();
                MessageBox.Show("Salvo com Sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }

        private void Cadastros_Load(object sender, EventArgs e)
        {
            //Cria uma instância de um documento XML
            XmlDocument oXML = new XmlDocument();

            //Define o caminho do arquivo XML 
            string ArquivoXML = System.IO.Path.Combine(Environment.CurrentDirectory, "Emails.xml");
            //carrega o arquivo XML
            oXML.Load(ArquivoXML);

            textBox1.Text = oXML.SelectSingleNode("Email").ChildNodes[0].InnerText;

            textBox2.Text = oXML.SelectSingleNode("Email").ChildNodes[1].InnerText;

            //Lê o filho de um Nó Pai específico 
            textBox3.Text = oXML.SelectSingleNode("Email").ChildNodes[2].InnerText;
            textBox4.Text = oXML.SelectSingleNode("Email").ChildNodes[3].InnerText;
            textBox5.Text = oXML.SelectSingleNode("Email").ChildNodes[4].InnerText;
            textBox6.Text = oXML.SelectSingleNode("Email").ChildNodes[5].InnerText;
            textBox7.Text = oXML.SelectSingleNode("Email").ChildNodes[6].InnerText;

            txtSenha.Text = oXML.SelectSingleNode("Email").ChildNodes[7].InnerText;

            txtLogin.Text = oXML.SelectSingleNode("Email").ChildNodes[8].InnerText;

            txtPorta.Text = oXML.SelectSingleNode("Email").ChildNodes[9].InnerText;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

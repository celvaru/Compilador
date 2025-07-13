using AnalizadorLexico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class Form1 : Form
    {
        List<Simbolo> tablaSimbolos = new List<Simbolo>();
        private Lexico _lexico = new Lexico();
        private Sintactico _sintactico = new Sintactico();
        public Form1()
        {
            InitializeComponent();
            erroresText.Padding = new Padding(20);
        }

        private void compilarBtn_Click(object sender, EventArgs e)
        {
            string errores = "";
            tablaSimbolos.Clear();
            tablaSimbolos = _lexico.Analizar(codigoText.Text);
            if (tablaSimbolos.Count <= 0)
                MessageBox.Show("No se encontraron simbolos", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                bool resultado = _sintactico.Analizar(tablaSimbolos);
                if (!resultado)
                {
                    foreach (var i in _sintactico.Errors)
                    {
                        errores += "\r\n" + i;
                    }

                }
                else
                {
                    errores = "No se encontraron errores";
                }
                
                erroresText.Text = errores;
            }

            erroresText.Text = errores;
        }

        private void simbolosBtn_Click(object sender, EventArgs e)
        {
            erroresText.Text = "";
            string errores = "";
            tablaSimbolos.Clear();
            tablaSimbolos = _lexico.Analizar(codigoText.Text);
            foreach (var item in tablaSimbolos)
            {
                errores = errores + "\r\n" + ($"\t{item.Lexema}\t{item.Token}\t{item.Linea}\t{item.Columna}");
            }

            erroresText.Text = errores;
        }

        private void limpiarBtn_Click(object sender, EventArgs e)
        {
            codigoText.Text = "";
            erroresText.Text = "";
        }
    }
}

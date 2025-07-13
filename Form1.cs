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
        public Form1()
        {
            InitializeComponent();
        }

        private void compilarBtn_Click(object sender, EventArgs e)
        {
            tablaSimbolos.Clear();
            tablaSimbolos = _lexico.Analizar(codigoText.Text);
            foreach (var item in tablaSimbolos)
            {
                erroresText.Text = ($"{item.Lexema}\t{item.Token}\t{item.Linea}\t{item.Columna}");
            }
        }
    }
}

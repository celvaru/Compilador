using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    internal class Lexico
    {
        
        public List<Simbolo> TablaSimbolos;
        public List<string> Errores;

        public Lexico()
        {
            TablaSimbolos = new List<Simbolo>();
            Errores = new List<string>();
        }

        Dictionary<string, string> Expresiones = new Dictionary<string, string>()
        {
            {"PalabraReservada", @"\b(prog|si|sino|cuando|para|lee|imp|salto)\b" },
            {"TipoDato", @"\b(ent|dec|sim)\b" },
            {"Comentario", @"//.*"},
            {"Concatenar", @"<<"},
            {"Cadena", @"""[^""]*"""},
            {"Caracter", @"'[^']'"},
            {"Leer", @">>" },
            {"Entero", @"[+-]?\d+"},
            {"Decimal", @"[+-]?(\d+\.\d*|\.\d+)([eE][+-]?\d+)?"},
            {"Variable", @"\b(_|[a-zA-Z])(_|[a-zA-Z]|\d)*\b"},
            {"OperadorAritmetico", @"\b[+\-*/%]\b"},
            {"OperadorRelacional", @"==|!=|<=|>=|<|>"},
            {"Asignacion", @"="},
            {"Aumentar", @"\+\+|--"},
            {"ParentesisAbierto", @"\("},
            {"ParentesisCerrado", @"\)"},
            {"LlaveAbierta", @"\{"},
            {"LlaveCerrada", @"\}"},
            {"PuntoyComa", @";"},
            {"Coma", @","}
        };
        public List<Simbolo> Analizar(string sourceCode)
        {
            Errores = new List<string>();
            TablaSimbolos = new List<Simbolo>();

            string[] lines = sourceCode.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            string pattern = string.Join("|", Expresiones.Values);
            Regex regex = new Regex(pattern);

            int lineNumber = 1;
            foreach (string line in lines)
            {
                int currentIndex = 0;
 
                MatchCollection matches = regex.Matches(line);

                foreach (Match match in matches)
                {
                    string value = match.Value;
                    int matchStartIndex = match.Index;

                    if (currentIndex < matchStartIndex)
                    {
                        string unrecognized = line.Substring(currentIndex, matchStartIndex - currentIndex);
                        if (!string.IsNullOrWhiteSpace(unrecognized))
                        {
                            Errores.Add($"Error ({lineNumber}:{currentIndex + 1}): {unrecognized} -> No se reconce la cadena");
                        }
                    }

                    string tokenName = GetToken(value);
                    if (tokenName != "Comentario")
                    {
                        TablaSimbolos.Add(new Simbolo
                        {
                            Lexema = value,
                            Token = tokenName,
                            Linea = lineNumber,
                            Columna = match.Index + 1
                        });
                    }
                  
                    currentIndex = match.Index + match.Length;
                }

                if (currentIndex < line.Length)
                {
                    string unrecognized = line.Substring(currentIndex);
                    if (!string.IsNullOrWhiteSpace(unrecognized))
                    {
                        Errores.Add($"Error ({lineNumber}:{currentIndex + 1}): {unrecognized} -> No se reconce el simbolo");
                    }
                }

                lineNumber++;
            }

            return TablaSimbolos;
        }

        private string GetToken(string value)
        {
            string tokenName = Expresiones.FirstOrDefault(x => Regex.IsMatch(value, x.Value)).Key;
            return tokenName;
        }
    }
}

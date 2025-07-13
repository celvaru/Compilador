using AnalizadorLexico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    internal class Sintactico
    {
        public List<string> Errors;


        public bool Analizar(List<Simbolo> tablaSimbolos)
        {
            Errors = new List<string>();

            int index = 0;
            return Estructura(tablaSimbolos, ref index) && index == (tablaSimbolos.Count - 1);
        }
        private bool Estructura(List<Simbolo> tabla, ref int index)
        {
            if (index < 0 || index >= tabla.Count)
            {
                Errors.Add("Error: Índice fuera de rango");
                return false;
            }

            if (tabla[index].Lexema != "prog")
            {
                Errors.Add($"Error: Se esperaba 'prog'");
                return false;
            }
            index++;

            if (index >= tabla.Count || tabla[index].Token != "ParentesisAbierto")
            {
                Errors.Add($"Error: Se esperaba '('");
                return false;
            }
            index++;

            if (index >= tabla.Count || tabla[index].Token != "ParentesisCerrado")
            {
                Errors.Add($"Error: Se esperaba ')'");
                return false;
            }
            index++;

            if (index >= tabla.Count || tabla[index].Token != "LlaveAbierta")
            {
                Errors.Add($"Error: Se esperaba {{ ");
                return false;
            }
            index++;

            if (!Programa(tabla, ref index))
            {
                return false;
            }

            if (index >= tabla.Count || tabla[index].Token != "LlaveCerrada")
            {
                Errors.Add($"Error: Se esperaba }}");
                return false;
            }

            return true;
        }
        private bool Programa(List<Simbolo> ts, ref int index)
        {
            if (index < 0 || index >= ts.Count) return false;

            if (Sentencias(ts, ref index))
            {
                if (index >= ts.Count) return false;

                if (ts[index].Token != "LlaveCerrada")
                {
                    return Programa(ts, ref index);
                }
                return true;
            }
            return false;
        }
        private bool Sentencias(List<Simbolo> ts, ref int index)
        {
            int startIndex = index;

            if (Declaracion(ts, ref index))
            {
                return true;
            }
            index = startIndex;

            if (Asignacion(ts, ref index))
            {
                return true;
            }
            index = startIndex;

            if (Lectura(ts, ref index))
            {
                return true;
            }
            index = startIndex;

            if (Escritura(ts, ref index))
            {
                return true;
            }
            index = startIndex;
            if (Si(ts, ref index))
            {
                return true;
            }
            index = startIndex;
            if (Cuando(ts, ref index))
            {
                return true;
            }
            index = startIndex;
            if (Para(ts, ref index))
            {
                return true;
            }
            index = startIndex;

            Errors.Add($"Error ({ts[index].Linea}): Se esperaba una sentencia");
            return false;

        }

        private bool Declaracion(List<Simbolo> ts, ref int index)
        {
            if (index < 0 || index >= ts.Count) return false;
            int startIndex = index;

            if (ts[index].Token != "TipoDato") return false;
            index++;
            if (index >= ts.Count) return false;

            if (ts[index].Token != "Variable")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba nombre de variable después de tipo");
                index = startIndex;
                return false;
            }
            index++;
            if (index >= ts.Count) return false;

            if (ts[index].Lexema == "=")
            {
                index++;
                if (index >= ts.Count) return false;

                if (ts[index].Token != "Entero" && ts[index].Token != "Variable")
                {
                    Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba valor numérico o variable después de '='");
                    index = startIndex;
                    return false;
                }
                index++;
                if (index >= ts.Count) return false;
            }

            while (index < ts.Count && ts[index].Lexema == ",")
            {
                index++;
                if (index >= ts.Count) return false;

                if (ts[index].Token != "Variable")
                {
                    Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba nombre de variable después de coma");
                    index = startIndex;
                    return false;
                }
                index++;
                if (index >= ts.Count) return false;

                if (ts[index].Lexema == "=")
                {
                    index++;
                    if (index >= ts.Count) return false;

                    if (ts[index].Token != "Entero" && ts[index].Token != "Variable")
                    {
                        Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba valor numérico o variable después de '='");
                        index = startIndex;
                        return false;
                    }
                    index++;
                    if (index >= ts.Count) return false;
                }
            }

            if (index >= ts.Count || ts[index].Lexema != ";")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba ';'");
                index = startIndex;
                return false;
            }
            index++;

            return true;
        }
        private bool Asignacion(List<Simbolo> ts, ref int index)
        {
            int startIndex = index;

            if (index < 0 || index >= ts.Count) return false;

            if (ts[index].Token != "Variable")
            {
                return false;
            }
            index++;
            if (index >= ts.Count) return false;

            if (ts[index].Lexema != "=")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba '=' ");
                index = startIndex;
                return false;
            }
            string operador = ts[index].Lexema;
            index++;
            if (index >= ts.Count) return false;

            if (ts[index].Token != "Variable" && ts[index].Token != "Entero")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba variable o número");
                index = startIndex;
                return false;
            }
            index++;

            if (index >= ts.Count || ts[index].Lexema != ";")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba ';'");
                index = startIndex;
                return false;
            }
            index++;

            return true;
        }
        private bool Lectura(List<Simbolo> ts, ref int index)
        {
            int startIndex = index;

            if (index < 0 || index >= ts.Count)
                return false;

            if (ts[index].Lexema != "lee")
            {
                return false;
            }
            index++;
            if (index >= ts.Count) return false;


            if (ts[index].Lexema != ">>")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba '>>'");
                index = startIndex;
                return false;
            }
            index++;
            if (index >= ts.Count) return false;

            if (ts[index].Token != "Variable")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba variable");
                index = startIndex;
                return false;
            }
            index++;
            if (index >= ts.Count) return false;

            if (ts[index].Lexema != ";")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba ';'");
                index = startIndex;
                return false;
            }
            index++;

            return true;
        }
        private bool Escritura(List<Simbolo> ts, ref int index)
        {
            int startIndex = index;

            if (index < 0 || index >= ts.Count)
                return false;

            if (ts[index].Lexema != "imp")
            {
                return false;
            }
            index++;
            if (index >= ts.Count) return false;

            if (ts[index].Lexema != "<<")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba '<<'");
                index = startIndex;
                return false;
            }
            index++;
            if (index >= ts.Count)
                return false;

            if (!(ts[index].Token == "Cadena" ||
                  ts[index].Token == "Variable" ||
                  ts[index].Token == "Entero" ||
                  ts[index].Lexema == "salto"))
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba texto, variable, número o 'salto'");
                index = startIndex;
                return false;
            }
            index++;

            while (index < ts.Count && ts[index].Lexema == "<<")
            {
                index++;
                if (index >= ts.Count) return false;

                if (!(ts[index].Token == "Cadena" ||
                      ts[index].Token == "Variable" ||
                      ts[index].Token == "Entero" ||
                      ts[index].Lexema == "salto"))
                {
                    Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba texto, variable, número o 'salto'");
                    index = startIndex;
                    return false;
                }
                index++;
            }

            if (index >= ts.Count || ts[index].Lexema != ";")
            {
                Errors.Add($"Error ({ts[startIndex].Linea}): Se esperaba ';'");
                index = startIndex;
                return false;
            }
            index++;

            return true;
        }
        private bool Si(List<Simbolo> ts, ref int index)
        {
            if (ts[index].Lexema != "si")
                return false;
            index++;

            if (!ValidarCondicion(ts, ref index))
                return false;

            if (ts[index].Lexema != "{")
                return false;
            index++;

            while (ts[index].Lexema != "}")
            {
                index++;
                if (index >= ts.Count)
                    return false;
            }
            index++;

            if (index < ts.Count && ts[index].Lexema == "sino")
            {
                index++;
                if (ts[index].Lexema != "{")
                    return false;
                index++;

                while (ts[index].Lexema != "}")
                {
                    index++;
                    if (index >= ts.Count)
                        return false;
                }
                index++;
            }

            return true;
        }
        private bool Cuando(List<Simbolo> ts, ref int index)
        {
            if (ts[index].Lexema != "cuando") return false;
            index++;

            if (!ValidarCondicion(ts, ref index)) return false;

            if (ts[index].Lexema != "{") return false;
            index++;

            while (ts[index].Lexema != "}")
            {
                index++;
                if (index >= ts.Count) return false;
            }
            index++;

            return true;
        }
        private bool Para(List<Simbolo> ts, ref int index)
        {
            if (ts[index].Lexema != "para")
                return false;
            index++;

            if (ts[index].Lexema != "(")
                return false;
            index++;

            if (ts[index].Token == "TipoDato")
            {
                index++;
                if (ts[index].Token != "Variable")
                    return false;
                index++;
                if (ts[index].Lexema == "=")
                {
                    index++;
                    if (ts[index].Token != "Entero" && ts[index].Token != "Variable")
                        return false;
                    index++;
                }
            }
            else if (ts[index].Token == "Variable") 
            {
                index++;
                if (ts[index].Lexema != "=")
                    return false;
                index++;
                if (ts[index].Token != "Entero" && ts[index].Token != "Variable")
                    return false;
                index++;
            }
            else
            {
                return false;
            }

            if (ts[index].Lexema != ";")
                return false;
            index++;

            if (!ValidarCondicion(ts, ref index))
                return false;
            if (ts[index].Lexema != ";")
                return false;
            index++;

            if (ts[index].Token != "Variable")
                return false;
            index++;
            if (ts[index].Lexema != "++" && ts[index].Lexema != "--" && ts[index].Lexema != "+=" && ts[index].Lexema != "-=")
                return false;
            index++;

            if (ts[index].Lexema != ")")
                return false;
            index++;

            if (ts[index].Lexema != "{")
                return false;
            index++;

            while (index < ts.Count && ts[index].Lexema != "}")
            {
                if (!Sentencias(ts, ref index))
                    return false;
            }

            if (index >= ts.Count)
                return false;
            index++;

            return true;
        }
        private bool ValidarCondicion(List<Simbolo> ts, ref int index)
        {
            if (ts[index].Lexema != "(")
                return false;
            index++;

            if (ts[index].Token != "Variable" && ts[index].Token != "Entero")
                return false;
            index++;

            if (ts[index].Token != "OperadorRelacional")
                return false;
            index++;

            if (ts[index].Token != "Variable" && ts[index].Token != "Entero")
                return false;
            index++;

            if (ts[index].Lexema != ")")
                return false;
            index++;

            return true;
        }
    }

    }

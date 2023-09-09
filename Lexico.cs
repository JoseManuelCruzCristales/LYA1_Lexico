using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Metadata;

namespace LYA1_Lexico
{
    public class Lexico : Token, IDisposable
    {
        private StreamReader archivo;
        private StreamWriter log;
        public Lexico()
        {
            archivo = new StreamReader("prueba.cpp");
            log = new StreamWriter("prueba.log");
        }
        public Lexico(string nombre)
        {
            archivo = new StreamReader(nombre);
            log = new StreamWriter("prueba.log");
        }
        public void Dispose()
        {
            archivo.Close();
            log.Close();
        }
        public void nextToken()
        {
            char c;
            string buffer = "";
            while (char.IsWhiteSpace(c = (char)archivo.Read()))
            {
            }
            buffer += c;
            if (char.IsLetter(c))
            {
                setClasificacion(Tipos.Identificador);
                while (char.IsLetterOrDigit(c = (char)archivo.Peek()))
                {
                    buffer += c;
                    archivo.Read();
                }
            }
            else if (char.IsDigit(c))
            {
                setClasificacion(Tipos.Numero);
                while (char.IsDigit(c = (char)archivo.Peek()))
                {
                    buffer += c;
                    archivo.Read();
                }
            }
            else if (c == '=')
            {
                setClasificacion(Tipos.Asignacion);
                c = (char)archivo.Peek();
                if (c == '=')
                {

                    setClasificacion(Tipos.OperadorRelacional);
                    buffer += c;
                    archivo.Read();
                }
            }
            else if (c == ';')
            {
                setClasificacion(Tipos.FinSentencia);
            }
            else if (c == '{')
            {
                setClasificacion(Tipos.Inicio);
            }
            else if (c == '}')
            {
                setClasificacion(Tipos.Fin);
            }
            //Operdaorlogico

            else if (c == '&')
            {
                setClasificacion(Tipos.Caracter);
                c = (char)archivo.Peek();
                if (c == '&')
                {

                    setClasificacion(Tipos.OperadorLogico);
                    buffer += c;
                    archivo.Read();
                }
            }
            else if (c == '!')
            {
                setClasificacion(Tipos.OperadorLogico);
                c = (char)archivo.Peek();
                if (c == '=')
                {

                    setClasificacion(Tipos.OperadorRelacional);
                    buffer += c;
                    archivo.Read();
                }
            }

            else if (c == '|')
            {
                setClasificacion(Tipos.Caracter);
                c = (char)archivo.Peek();
                if (c == '|')
                {

                    setClasificacion(Tipos.OperadorLogico);
                    buffer += c;
                    archivo.Read();
                }
            }

            else if (c == '>' || c == '<')
            {
                setClasificacion(Tipos.OperadorCompararcion);
                c = (char)archivo.Peek();
                if (c == '=')
                {

                    setClasificacion(Tipos.OperadorCompararcion);
                    buffer += c;
                    archivo.Read();
                }
            }



            //operadortermino

            else if (c == '+')
            {
                setClasificacion(Tipos.OperadorTermino);
                c = (char)archivo.Peek();
                if (c == '+' || c == '-' || c == '=')
                {

                    setClasificacion(Tipos.IncrementoTermino);
                    buffer += c;
                    archivo.Read();
                }
            }
            else if (c == '-')
            {
                setClasificacion(Tipos.OperadorTermino);
                c = (char)archivo.Peek();
                if (c == '+' || c == '-' || c == '=')
                {

                    setClasificacion(Tipos.IncrementoTermino);
                    buffer += c;
                    archivo.Read();
                }
            }
            //operador factor
            else if (c == '*' || c == '/' || c == '%')
            {
                setClasificacion(Tipos.OperadorFactor);
                c = (char)archivo.Peek();
                if (c == '=')
                {

                    setClasificacion(Tipos.IncrementoFactor);
                    buffer += c;
                    archivo.Read();
                }
            }
            else
            {
                setClasificacion(Tipos.Caracter);
            }
            setContenido(buffer);
            log.WriteLine(getContenido() + " = " + getClasificacion());
        }


        public bool FinArchivo()
        {

            return archivo.EndOfStream;
        
        }
    }
}
    
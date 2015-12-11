using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoffeeLand
{
    public class Validator : System.ComponentModel.IDataErrorInfo
    {

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private string valor;

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        private DateTime fecha ;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private int selected;

        public int Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public string Error
        {
             
            get { return null; }
        }


        public string this[string name]
        {
            get
            {
                string result = null;
            
                Regex numeros = new Regex("^[0-9]*$");

                switch (name)
                {
                    case "Nombre":
                        if (string.IsNullOrEmpty(nombre))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                                if (nombre.Length > 20)
                                {
                                    result = "El nombre debe ser menor que 20 caracteres.";
                                }
                        }
                    break;
                    case "Descripcion":
                        if (string.IsNullOrEmpty(descripcion))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                                if (descripcion.Length > 45)
                                {
                                    result = "La descripción debe ser menor que 45 caracteres.";
                                }
                        }
                        break;
                    case "Valor":
                        if (string.IsNullOrEmpty(valor))
                        {
                            result = "El campo es obligatorio.";
                        }
                        else
                        {
                            if (!numeros.IsMatch(valor))
                            {
                                result = "El campo solo acepta números";
                            }
                            else
                            {
                                if (valor.Length > 10)
                                {
                                    result = "El valor debe ser menor que 10 caracteres.";
                                }
                            }
                           
                        }
                        break;

                    case "Fecha":
                        if (Fecha == null)
                        {
                            result = "El campo es obligatorio.";
                        }
                       
                        break;
                    case "Selected":
                        if (selected == 0)
                        {
                            result = "Debe seleccionar un valor";
                        }

                        break;
                  

                    default:
                        break;
                }

                return result;
            }
        }
    }
}

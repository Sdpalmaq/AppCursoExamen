using System;
using System.Collections.Generic;
using System.Text;

namespace AppCursosExamen.Models
{
    public class Curso
    {
        public int id_curso { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string tutor { get; set; }
    }
}

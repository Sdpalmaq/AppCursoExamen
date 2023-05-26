using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCursosExamen.Paginas.Curso
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class nuevoCurso : ContentPage
    {
       
        public nuevoCurso()
        {
            InitializeComponent();
        }

        Models.Curso _curso;

        public nuevoCurso(Models.Curso cursos)
        {
            InitializeComponent();
            Title = "Editar Curso";
            _curso = cursos;
            id.Text = cursos.id_curso.ToString();
            nombreCurso.Text = cursos.nombre;
            descripcionCurso.Text= cursos.descripcion;
            Tutor.Text = cursos.tutor;
            nombreCurso.Focus();

        }

        // private string Url = "http://192.168.1.7:3000";

        private string Url = "https://moviles.fly.dev";

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string id_curso = id.Text;
            string nombre = nombreCurso.Text;
            string descriscion = descripcionCurso.Text;
            string tutor = Tutor.Text;

            //Comprobar que los campos no estan vacios

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(descriscion) || string.IsNullOrEmpty(tutor))
            {
                await DisplayAlert("Error", "Ingrese los datos correspondientes", "Aceptar");
                return;

            } 
            else if (_curso !=null)
            {
                using (var wc = new WebClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });

                    wc.Headers.Add("Content-Type", "application/json");
                    var datos = new Models.Curso
                    {
                        id_curso = int.Parse(id_curso),
                        nombre = nombre,
                        descripcion = descriscion,
                        tutor = tutor
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(datos);
                    wc.UploadString(Url + "/prueba/curso/" + id.Text, "PUT", json);
                    await DisplayAlert("Exito", "El curso se ha editado con exito", "Aceptar");

                    await Navigation.PopAsync();


                }

            }
            else {

                using (var wc = new WebClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });

                    wc.Headers.Add("Content-Type", "application/json");
                    var datos = new Models.Curso
                    {

                        nombre = nombre,
                        descripcion = descriscion,
                        tutor = tutor
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(datos);
                    await wc.UploadStringTaskAsync(Url + "/prueba/curso", "POST", json);

                    await DisplayAlert("Exito", "El curso ha sido agregado con exito", "Aceptar");

                    await Navigation.PopAsync();

                }
            }

        }
    }
}
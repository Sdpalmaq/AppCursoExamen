using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Security;

namespace AppCursosExamen.Paginas.Curso
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
       // private string Url = "http://192.168.1.7:3000";

        private string Url = "https://moviles.fly.dev";
        private Models.Curso[] Cursos { get; set; }
        public Inicio()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await httpClient.GetStringAsync(Url + "/prueba/curso");
                    var cursos = JsonConvert.DeserializeObject<Models.Curso[]>(response);

                    if (cursos != null && cursos.Length > 0)
                    {
                        myCollectionView.ItemsSource = cursos;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción o error de manera adecuada
                Console.WriteLine("Error al cargar los cursos: " + ex.Message);
            }
        }



        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Paginas.Curso.nuevoCurso());
        }

        async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var curso = item.CommandParameter as Models.Curso;
            await Navigation.PushAsync(new Paginas.Curso.nuevoCurso(curso));
        }

        async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var curso = item.CommandParameter as Models.Curso;
            var resultado = await DisplayAlert("Eliminar", $"Esta seguro de elimnar {curso.nombre}", "Si","No");

            if (resultado)
            {
                using (var wc = new WebClient())
                {

                    wc.Headers.Add("Content-Type", "application/json");

                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });


                    wc.UploadString(Url + "/prueba/curso/"+curso.id_curso.ToString(), "DELETE","");


                    OnAppearing();

                }
            }
        }
    }
}
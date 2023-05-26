using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCursosExamen
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AppCursosExamen.Paginas.Curso.Inicio());// MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

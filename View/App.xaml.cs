using System.Configuration;
using System.Data;
using System.Windows;
using View.Model.Services;
using View.ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}

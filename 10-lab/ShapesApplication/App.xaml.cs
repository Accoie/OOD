using System.Windows;
using ShapesApplication.Controllers;

namespace ShapesApplication
{
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            WindowView mainWindow = new WindowView( new DocumentController() );
            mainWindow.Show();
        }
    }
}
using Windows.UI.Xaml.Controls;

namespace MyUWPLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            this.InitializeComponent();

            //var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("MyUWPLib/Resources");
            //var test = resourceLoader.GetString("Test");
        }
    }
}

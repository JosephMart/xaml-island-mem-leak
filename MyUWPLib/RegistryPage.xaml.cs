using Windows.UI.Xaml.Controls;

namespace MyUWPLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistryPage : Page
    {
        public RegistryPage()
        {
            this.InitializeComponent();
            try
            {
                regVal.Text = "Git Version: " + Helpers.Registry.PReadRegistry(@"SOFTWARE\\GitForWindows", "CurrentVersion").ToString();
                // regVal.Text = "Windows Product Key: " + Helpers.Registry.PReadRegistry(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductId").ToString();
            }
            catch
            {

            }
        }
    }
}

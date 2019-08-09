using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyUWPLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CredManPage : Page
    {
        public CredManPage()
        {
            this.InitializeComponent();
        }

        private void Button_CredAdd(object sender, RoutedEventArgs e)
        {
            Helpers.CredMan.AddDomainUserCredential(credTarget.Text, credUser.Text, credPwd.Text);
            SavedMessage.Visibility = Visibility.Visible;
        }
    }
}

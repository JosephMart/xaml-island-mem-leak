using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyUWPLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilePage : Page
    {
        public FilePage()
        {
            this.InitializeComponent();
        }
        public delegate string OpenFileHandler();

        public static OpenFileHandler OpenFolderSelect;

        private void Button_SelectFolder(object sender, RoutedEventArgs e)
        {
            pathText.Text = Helpers.File.OpenFileDialog() ?? pathText.Text;
        }

        private void Button_SaveFile(object sender, RoutedEventArgs e)
        {
            string path = pathText.Text;
            FileInfo fInfo = new FileInfo(path);

            if (fInfo.Exists)
            {
                InfoMsg.Text = "File already exists at: " + path;
                return;
            }

            try
            {
                Helpers.File.PCreateFile(path, fileText.Text);
                InfoMsg.Text = "File saved to: " + path;
            }
            catch (Exception exp)
            {
                InfoMsg.Text = exp.Message;
            }
        }
    }
}

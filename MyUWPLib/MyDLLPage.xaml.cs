using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyUWPLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyDLLPage : Page
    {
        public MyDLLPage()
        {
            this.InitializeComponent();
        }

        private void Button_Evaluate(object sender, RoutedEventArgs e)
        {
            result.Text = Helpers.MyDLL.fib(int.Parse(nInput.Text)).ToString();
        }
    }
}

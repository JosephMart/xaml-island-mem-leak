using System;
using Windows.UI.Xaml.Controls;

namespace MyUWPLib
{
    public sealed partial class MainPage : UserControl
    {
        public MainPage()
        {
            this.InitializeComponent();

            contentFrame.Navigate(typeof(WelcomePage), null);
        }

        public delegate void FormHandler();

        public FormHandler NewWinForm;

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(SettingsPage));
                return;
            }

            Type pageType = null;
            NavigationViewItem navViewItem = args.InvokedItemContainer as NavigationViewItem;
            if (navViewItem != null)
            {

                switch (navViewItem.Tag)
                {
                    case "WelcomePage":
                        pageType = typeof(WelcomePage);
                        break;
                    case "FilePage":
                        pageType = typeof(FilePage);
                        break;
                    case "RegistryPage":
                        pageType = typeof(RegistryPage);
                        break;
                    case "CredManPage":
                        pageType = typeof(CredManPage);
                        break;
                    case "MyDLLPage":
                        pageType = typeof(MyDLLPage);
                        break;
                    case "NewPage":
                        NewWinForm();
                        return;
                }
                if (pageType != null)
                {
                    contentFrame.Navigate(pageType);
                }
            }

        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (contentFrame.CanGoBack)
                contentFrame.GoBack();
        }
    }
}

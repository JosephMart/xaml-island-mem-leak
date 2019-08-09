using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1(bool isMainWindow = false)
        {
            InitializeComponent();

            this.windowsXamlHost1.InitialTypeName = "MyUWPLib.MainPage";

            this.isMainWindow = isMainWindow;
        }

        bool isMainWindow;

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            if (this.windowsXamlHost1.Child == null)
                return;

            MyUWPLib.MainPage mainPage = (MyUWPLib.MainPage)this.windowsXamlHost1.Child;
            mainPage.NewWinForm = NewWinForm;
        }

        public void NewWinForm()
        {
            Form1 form = new Form1();
            form.Show();
        }
    }
}

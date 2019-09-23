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
            tonsMemory = new byte[1024 * 1024 * 100];
            //if (isMainWindow)
            //    this.MemLeakTest();
        }

        bool isMainWindow;
        byte[] tonsMemory;

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // Set callback in XamlIsland to open new Form1
            if (global::MyUWPLib.MainPage.NewWinForm == null)
            {
                global::MyUWPLib.MainPage.NewWinForm = NewWinForm;
            }
        }

        public async void MemLeakTest()
        {
            int delay = 1000;
            int iterations = 100;

            for (int i = 0; i < iterations; ++i)
            {
                var form = new Form1();
                form.Show();
                await Task.Delay(delay);
                form.Close();
                GC.Collect();
            }
        }

        static public void NewWinForm()
        {
            Form1 form = new Form1();
            form.Show();
        }
    }
}

﻿using System;
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
            if (isMainWindow)
                this.MemLeakTest();
        }

        bool isMainWindow;

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

        public void NewWinForm()
        {
            Form1 form = new Form1();
            form.Show();
        }
    }
}

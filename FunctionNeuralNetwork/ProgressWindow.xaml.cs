using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace FunctionNeuralNetwork
{
    /// <summary>
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        public ProgressWindow()
        {
            InitializeComponent();
            this.Closing += ProgressWindow_Closing;
        }

        public void AllowClosing()
        {
            this.Closing -= ProgressWindow_Closing;
        }

        private void ProgressWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        public void ProgressChanged(int percentage)
        {
            goPB.Value = percentage;
        }
    }
}

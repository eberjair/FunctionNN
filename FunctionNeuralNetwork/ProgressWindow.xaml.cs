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
        BackgroundWorker Worker;

        public ProgressWindow(BackgroundWorker worker)
        {
            InitializeComponent();
            this.Closing += ProgressWindow_Closing;
            Worker = worker;
            worker.ProgressChanged += Worker_ProgressChanged;
        }

        public void AllowClosing()
        {
            this.Closing -= ProgressWindow_Closing;
        }

        private void ProgressWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            goPB.Value = e.ProgressPercentage;
        }
    }
}

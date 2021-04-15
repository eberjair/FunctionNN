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
    /// Interaction logic for RefreshWindow.xaml
    /// </summary>
    public partial class RefreshWindow : Window
    {
        public RefreshWindow()
        {
            InitializeComponent();
            this.Closing += RefreshWindow_Closing;
        }

        public void AllowClosing()
        {
            this.Closing -= RefreshWindow_Closing;
        }

        private void RefreshWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

    }
}

using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace webptool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Select_File_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.InitialDirectory = Application.;
            ofd.Title = "请选择要打开的文件";
            ofd.Multiselect = true;
            ofd.Filter = "图片文件(png,jpg)|*.png;*.jpg";
            ofd.RestoreDirectory = true;
            if ((bool)ofd.ShowDialog())
            {
                //选择了文件
                String[] files = ofd.FileNames;
                foreach (String file in files)
                {
                    Console.Out.WriteLine(file.ToString());
                    MessageBox.Show(file.ToString());
                }
            }
            else
            {
                //取消

            }






        }
    }
}

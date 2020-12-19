using Microsoft.Win32;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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
            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Title = "请选择要打开的文件";
            ofd.Multiselect = true;
            ofd.Filter = "图片文件(png,jpg)|*.png;*.jpg";
            ofd.RestoreDirectory = true;
            if ((bool)ofd.ShowDialog())
            {
                //选择了文件
                String[] files = ofd.FileNames;
                ImageFile[] f = new ImageFile[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    f[i] = new ImageFile(files[i]);
                }
                Array.Sort(f);
                AddListItem(f);

            }
            else
            {
                //取消

            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddListItem(ImageFile[] files)
        {
            this.file_list.Items.Clear();
            foreach (ImageFile file in files)
            {
                ListViewItem item = new ListViewItem();
                item.Content = file.file;
                item.Padding = new Thickness(5, 5, 5, 5);

                this.file_list.Items.Add(item);

            }
        }

        public class ImageFile : IComparable<ImageFile>
        {

            private static readonly Regex regex = new Regex(@"[^\d]+");

            public String file;

            public ImageFile(string file)
            {
                this.file = file;
            }

            public int CompareTo([AllowNull] ImageFile other)
            {
                try
                {
                    int a = Int32.Parse(regex.Replace(file, ""));
                    int b = Int32.Parse(regex.Replace(other.file, ""));
                    return a - b;
                }
                catch (Exception e)
                {
                    return 0;
                }

            }
        }
    }
}

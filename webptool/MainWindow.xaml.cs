using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
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

        private ImageFile[] imageFiles;
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
                imageFiles = new ImageFile[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    imageFiles[i] = new ImageFile(files[i]);
                }
                Array.Sort(imageFiles);
                AddListItem(imageFiles);

            }
            else
            {
                //取消

            }

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

        private void Save_File_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder cmd = new StringBuilder();
            cmd.Append("-loop 0 -lossy -q ");
            //压缩质量
            cmd.Append(90);

            foreach(ImageFile file in imageFiles)
            {
                cmd.Append(" \"");
                cmd.Append(file.file);
                cmd.Append("\"");

                //帧率
                cmd.Append(" -d ");
                cmd.Append(24);
            }


            //输出
            cmd.Append(" -o output.webp");

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo("img2webp.exe",cmd.ToString());
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}

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

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsStop { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private static void FinishHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Finish!");
        }

        private static void StartHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Start!");
        }

        private void GenerateFileNode(object sender, FileEventArgs e)
        {
            var treeViewItem = new TreeViewItem
            {
                Tag = e.FileItem,
                Header = e.FileItem.Name
            };
            e.IsStop = IsStop;
            tv.Items.Add(treeViewItem);
        }

        private void GenerateDirectoryNode(object sender, DirectoryEventArgs e)
        {
            var treeViewItem = new TreeViewItem
            {
                Tag = e.DirectoryItem,
                Header = e.DirectoryItem.Name
            };
            e.IsStop = IsStop;
            tv.Items.Add(treeViewItem);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            IsStop = true;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var fileSystemVisitor = new FileSystemVisitor();

            fileSystemVisitor.FileFinded += GenerateFileNode;
            fileSystemVisitor.FilteredFileFinded += GenerateFileNode;
            fileSystemVisitor.DirectoryFinded += GenerateDirectoryNode;
            fileSystemVisitor.FilteredDirectoryFinded += GenerateDirectoryNode;
            fileSystemVisitor.Start += StartHandler;
            fileSystemVisitor.Finish += FinishHandler;

            fileSystemVisitor.ExploreDirectory(@"C:\Users\Aibek_Shulembekov\Downloads", SearchTextBox?.Text ?? "");
        }
    }
}

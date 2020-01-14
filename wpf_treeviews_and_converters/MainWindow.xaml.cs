using System;
using System.Collections.Generic;
using System.IO;
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

namespace wpf_treeviews_and_converters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion


        #region On Loaded
        /// <summary>
        /// Quand l'application ouvre la première fois
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // Create a new item for it
                var item = new TreeViewItem()
                {
                    // Set the header
                    Header = drive,
                    // And the full path
                    Tag = drive
                };


                item.Items.Add(null);

                // Listen out for item being expanded

                item.Expanded += Folder_Expanded;

                // Add it to the main treeview
                FolderView.Items.Add(item);
            }
        }

        #endregion

        #region folder_expanded
        /// <summary>
        /// When a folder is expanded find the folders and files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region initial checks
            var item = (TreeViewItem)sender;

            // If the item only contains the dummy data

            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }

            // Clear dummy data
            item.Items.Clear();

            #endregion

            #region Get folders

            var fullPath = (string)item.Tag;

            // Create a blank list for directories
            var directories = new List<string>();

            // Try and get directories from the folder
            // ignoring any issues doing so.. bad practice
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            // Foreach directory...
            directories.ForEach(directoryPath =>
            {
                // Create directory item
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath
                };

                

                // Dummy item to test expansion
                subItem.Items.Add(null);

                subItem.Expanded += Folder_Expanded;


                // Add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion

            #region Get files

            // Create a blank list for directories
            var files = new List<string>();

            // Try and get the files from the folder
            // ignoring any issues doing so.. bad practice
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            // Foreach directory...
            files.ForEach(filePath =>
            {
                // Create file item
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath),
                    Tag = filePath
                };

                // Add this item to the parent
                item.Items.Add(subItem);
            });
            #endregion

        }

        #endregion

        #region Helpers


        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="directoryPath">The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // C:\something\a folder
            // c:\something\a\file.png
            // afile.png


            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Taking in account unix style folder separator
            var normalizedPath = path.Replace('/', '\\');

            // Find the last backslash of the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            // Return the name after the last backslash
            return path.Substring(lastIndex + 1);
        }

        #endregion



    }
}

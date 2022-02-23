using System;
using System.Linq;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using GameEngineApp.Core;

namespace GameEngineApp.LayoutWindows
{
    /// <summary>
    /// Interaction logic for ProjectFilesWindow.xaml
    /// </summary>
    public partial class ProjectFilesWindow : UserControl
	{
        public static Action<string> FolderChanged;
		public ProjectFilesWindow()
		{
			InitializeComponent();
            foreach (var item in Directory.GetDirectories(
                Path.GetFullPath(Navigation.GetAssetsFolder() + "\\..")))
            {
                TreeViewItem folder = new TreeViewItem();
                
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;

                Image image = new Image();
                image.Source = new BitmapImage
                    (new Uri("pack://application:,,,/GameEngineApp;component/Images/folder.png"));
                image.Width = 16;
                image.Height = 16;

                Label lbl = new Label();
                lbl.Content = item.Split('\\').Last();
                lbl.Foreground = new SolidColorBrush(Colors.AliceBlue);

                stack.Children.Add(image);
                stack.Children.Add(lbl);

                folder.Header = stack;
                FilesView.Items.Add(folder);
                folder.Tag = item;

                if (Directory.GetDirectories(item).Length != 0)
                {
                    AdjustTreeView(item, folder);
                }
            }
            FilesView.SelectedItemChanged += SelectedFolderChanged;
        }
        public string GetFullPath(TreeViewItem node)
        {
            return "";
        }
        private void SelectedFolderChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
            FolderChanged?.Invoke(((TreeViewItem)FilesView.SelectedItem).Tag.ToString());
		}

		private void AdjustTreeView(string path, TreeViewItem treeViewItem)
		{
            foreach (var item in Directory.GetDirectories(path))
            {
                TreeViewItem folder = new TreeViewItem();

                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;

                Image image = new Image();
                image.Source = new BitmapImage
                    (new Uri("pack://application:,,,/GameEngineApp;component/Images/folder.png"));
                image.Width = 16;
                image.Height = 16;

                Label lbl = new Label();
                lbl.Content = item.Split('\\').Last();
                lbl.Foreground = new SolidColorBrush(Colors.AliceBlue);

                stack.Children.Add(image);
                stack.Children.Add(lbl);

                folder.Header = stack;
                folder.Tag = item;
                treeViewItem.Items.Add(folder);
                if (Directory.GetDirectories(item).Length != 0)
                {
                    AdjustTreeView(item, folder);
                }
            }
        }
        private void ExpandAll(ItemsControl items, bool expand)
        {
            foreach (object obj in items.Items)
            {
                ItemsControl childControl = items.ItemContainerGenerator.ContainerFromItem(obj) as ItemsControl;
                if (childControl != null)
                {
                    ExpandAll(childControl, expand);
                }
                TreeViewItem item = childControl as TreeViewItem;
                if (item != null)
                    item.IsExpanded = expand;
            }
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            foreach (object item in FilesView.Items)
            {
                TreeViewItem treeItem = FilesView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeItem != null)
                    ExpandAll(treeItem, true);
                treeItem.IsExpanded = true;
            }
        }

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
            foreach (object item in FilesView.Items)
            {
                TreeViewItem treeItem = FilesView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeItem != null)
                    ExpandAll(treeItem, false);
                treeItem.IsExpanded = false;
            }
        }
	}
}

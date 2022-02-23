
using System.Windows;
using System.Windows.Forms;

namespace GameEngineApp
{
	/// <summary>
	/// Interaction logic for NewProjectDialog.xaml
	/// </summary>
	public partial class NewProjectDialog : Window
	{
		public string ProjectName
		{
			get => projectName.Text;
		}
		public string Path
		{
			get => projectPath.Text;
		}
		public NewProjectDialog()
		{
			
			InitializeComponent();
		}

		private void AcceptCreate(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void SelectFolder(object sender, RoutedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					projectPath.Text = dialog.SelectedPath;
				}
			}
		}
	}
}

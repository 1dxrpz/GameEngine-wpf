using MonoGame.WpfCore;
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

namespace GameEngineApp.LayoutWindows
{
	/// <summary>
	/// Interaction logic for PropertiesWindow.xaml
	/// </summary>
	public partial class PropertiesWindow : UserControl
	{
		public PropertiesWindow()
		{
			WorkObject.ObjectSelected += PropertiesChange;
			InitializeComponent();
		}

		private void PropertiesChange(WorkObject obj)
		{
			pos_x.Text = obj.Position.X.ToString();
			pos_y.Text = obj.Position.Y.ToString();
		}
	}
}

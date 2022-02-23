using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineApp.Core
{
	public class Navigation
	{
		public static string AssetsProject = "AssetsProject";
		public static string GameProject = "";
		public static string MainDirectory = "";
		public static string GetAssetsFolder()
		{
			return Path.GetFullPath($@"{MainDirectory}\\{AssetsProject}\\Assets");
		}
	}
}

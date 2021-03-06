using GameEngineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineApp.Services
{
	public class ProjectNavigation : IProjectNavigation
	{
		public static string MainDirectory = string.Empty;
		public static string CoreProject = string.Empty;
		public static string AssetsProject = string.Empty;

		public string GetCurrentDirectory()
		{
			return Directory.GetCurrentDirectory();
		}
	}
}

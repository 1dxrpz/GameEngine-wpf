
using System;
using System.Diagnostics;

namespace AppDebug
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");


			Process cmd = new Process();
			cmd.StartInfo.FileName = "cmd.exe";
			cmd.StartInfo.RedirectStandardInput = true;
			cmd.StartInfo.RedirectStandardOutput = true;
			cmd.StartInfo.CreateNoWindow = true;
			cmd.StartInfo.UseShellExecute = false;
			cmd.Start();

			cmd.StandardInput.WriteLine($@"dotnet build C:\Users\HP\source\repos\GameEngineApp\TestApp\TestApp.csproj");
			cmd.StandardInput.Flush();
			cmd.StandardInput.Close();
			cmd.WaitForExit();
			cmd.Exited += CloseCnsole;
			//Console.WriteLine(cmd.StandardOutput.ReadToEnd());
		}

		private static void CloseCnsole(object sender, EventArgs e)
		{
			
		}
	}
}

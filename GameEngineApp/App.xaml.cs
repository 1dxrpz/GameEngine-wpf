using GameEngineApp.Core;
using GameEngineApp.Services;
using GameEngineApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace GameEngineApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly IHost _host;

		public App()
		{
			//ProjectNavigation.MainDirectory = Path.GetFullPath($@"{Directory.GetCurrentDirectory()}/../../..");
			Navigation.MainDirectory = Path.GetFullPath($@"{Directory.GetCurrentDirectory()}/../../..");
			// Path to assets & game projects
			
			if (!Directory.Exists("Layouts"))
			{
				Directory.CreateDirectory("Layouts");
			}
			if (!Directory.Exists("ProjectsInfo"))
			{
				Directory.CreateDirectory("ProjectsInfo");
			}
			_host = Host.CreateDefaultBuilder()
				.ConfigureServices((context, services) =>
				{
					ConfigureServices(services);
				})
				.Build();
		}
		private void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<MainWindow>();
			services.AddSingleton<StartWindow>();
			services.AddSingleton<IProjectNavigation, ProjectNavigation>();
		}
		protected override async void OnStartup(StartupEventArgs e)
		{
			await _host.StartAsync();
			var StartWindow = _host.Services.GetRequiredService<MainWindow>();
			StartWindow.Show();
			base.OnStartup(e);
		}
		protected override async void OnExit(ExitEventArgs e)
		{
			using (_host)
			{
				await _host.StopAsync();
			}
			base.OnExit(e);
		}

	}
}

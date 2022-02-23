namespace TestApp
{
	public class ScriptManager
	{
		public void InitializeScripts()
		{
			var test = ProjectNavigation.MainDirectory;
			//foreach (var item in Directory.GetFiles($"../../../../AssetsProject/Assets"))
			//{
			//	var filename = item.Split('\\').Last().Split('.').First();
			//	Type type = Type.GetType($"TestApp.Scripts.{filename}");
			//	object[] parameters = new object[] { };
			//	dynamic obj = Activator.CreateInstance(type, parameters) as IDxScript;
			//	//IDxScript script = (IDxScript)Convert.ChangeType(obj, type);
			//}
		}
		private void FindScripts()
		{

		}
	}
}
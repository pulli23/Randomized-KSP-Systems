using System.IO;

namespace RandomizedSystems.Persistence
{
	public static class SeedTracker
	{
		private static string cfgFile = "";

		public static void Jump ()
		{
			if (string.IsNullOrEmpty (cfgFile))
			{
				FindConfig ();
			}
			File.WriteAllText (cfgFile, Hyperdrive.seedString);
		}

		public static string LastSeed ()
		{
			if (string.IsNullOrEmpty (cfgFile))
			{
				FindConfig ();
			}
			return File.ReadAllText (cfgFile);
		}

		private static void FindConfig ()
		{
			try
			{
			string appPath = KSPUtil.ApplicationRootPath;
			string addonFolder = "";
			while (!string.IsNullOrEmpty(appPath) && addonFolder == "")
			{
				if (Directory.Exists (appPath))
				{
					string[] allDirectories = Directory.GetDirectories (appPath);
					foreach (string directory in allDirectories)
					{
						if (Path.GetFileName (directory).ToLower () == "gamedata")
						{
							addonFolder = directory;
						}
					}
				}
				if (addonFolder == "")
				{
					// Shorten the path name
					appPath = Path.GetDirectoryName (appPath);
				}
			}
			addonFolder = Path.Combine (addonFolder, "RandomizedSystems");
			cfgFile = Path.Combine (addonFolder, "lastseed.seed");
			if (!File.Exists (cfgFile))
			{
				File.Create (cfgFile);
			}
			}
			catch(IOException e)
			{
				Debugger.LogError ("Could not load config file! Error: "+e.Source+", stack trace: "+e.StackTrace".");
			}
		}
	}
}

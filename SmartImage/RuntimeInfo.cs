#region

#nullable enable
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using CommandLine;
using SimpleCore;
using Newtonsoft.Json.Linq;
using RestSharp;
using SimpleCore.Utilities;
using SmartImage.Model;
using SmartImage.Searching;

// ReSharper disable MemberCanBePrivate.Global

#endregion


namespace SmartImage
{
	public static class RuntimeInfo
	{
		public const string NAME = "SmartImage";

		public const string NAME_EXE = "SmartImage.exe";

		public const string NAME_CFG = "smartimage.cfg";

		public const string Author = "Read Stanton";

		public const string Readme = "https://github.com/Decimation/SmartImage/blob/master/README.md";

		public const string REG_SUBKEY = @"SOFTWARE\SmartImage";

		public const string REG_SHELL = @"HKEY_CLASSES_ROOT\*\shell\SmartImage\";

		public const string REG_SHELL_CMD = @"HKEY_CLASSES_ROOT\*\shell\SmartImage\command";

		public static string AppFolder {
			get {
				string folder = Path.GetDirectoryName(ExeLocation);
				Debug.Assert(folder != null);
				return folder;
			}
		}

		/// <summary>
		/// User config and arguments
		/// </summary>
		public static SearchConfig Config { get;  set; } = null!;

		public static string ConfigLocation {
			get { return Path.Combine(AppFolder, NAME_CFG); }
		}

		public static bool IsExeInAppFolder => File.Exists(Path.Combine(AppFolder, NAME_EXE));

		/// <summary>
		///     Null if executable is not in path.
		/// </summary>
		public static string ExeLocation => FindExecutableLocation(NAME_EXE);

		public static bool IsContextMenuAdded {
			get {
				string cmdStr = String.Format(@"reg query {0}", REG_SHELL_CMD);
				var    cmd    = Cli.Shell(cmdStr, true);

				string[] stdOut = Cli.ReadAllLines(cmd.StandardOutput);

				// todo
				if (stdOut.Any(s => s.Contains(NAME))) {
					return true;
				}

				string[] stdErr = Cli.ReadAllLines(cmd.StandardError);

				if (stdErr.Any(s => s.Contains("ERROR"))) {
					return false;
				}


				throw new InvalidOperationException();
			}
		}

		public static bool IsAppFolderInPath => ExplorerSystem.IsFolderInPath(AppFolder);

		public static string BundleExtractFolder =>
			Environment.GetEnvironmentVariable("DOTNET_BUNDLE_EXTRACT_BASE_DIR", EnvironmentVariableTarget.User)!;

		internal static void Setup()
		{
			// todo
			if (!IsAppFolderInPath) {
				CliParse.PathCommand.Add();
			}
		}


		private static string FindExecutableLocation(string exe)
		{
			string path = ExplorerSystem.FindExectableInPath(exe);

			if (path == null) {
				string cd = Environment.CurrentDirectory;

				if (Try(cd, exe, out path)) {
					return path;
				}


				// SPECIAL CASE: app folder is not in path, continuing past here causes a stack overflow
				// todo


				/*if (Try(AppFolder, exe, out path)) {
					return path;
				}*/
			}

			static bool Try(string folder, string exeStr, out string folderExe)
			{
				string folderExeFull = Path.Combine(folder, exeStr);
				bool   inFolder      = File.Exists(folderExeFull);

				folderExe = folderExeFull;
				return inFolder;
			}

			return path;
		}

		// todo
		private static string FindExecutableLocationOld(string exe, params string[] searchDirs)
		{
			string path = ExplorerSystem.FindExectableInPath(exe);

			if (path == null) {
				foreach (string dir in searchDirs) {
					if (SearchFolder(exe, dir, out var s)) {
						return s;
					}
				}
			}

			return path;
		}

		// todo
		private static bool SearchFolder(string exe, string folder, out string path)
		{
			path = Path.Combine(folder, exe);

			bool inFolder = File.Exists(path);

			if (inFolder) {
				return true;
			}
			else {
				return false;
			}
		}
	}
}
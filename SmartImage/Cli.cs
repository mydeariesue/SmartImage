using System;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using SmartImage.Model;

namespace SmartImage
{
	public static class Cli
	{
		public delegate void RunCommand(string[] args);

		private const string Readme = "https://github.com/Decimation/SmartImage/blob/master/README.md";

		private const string STRING_FORMAT_ARG = "msg";

		private const char HEAVY_BALLOT_X   = '\u2718';
		private const char HEAVY_CHECK_MARK = '\u2714';

		public const char MUL_SIGN = '\u00D7';
		public const char RAD_SIGN = '\u221A';

		private const char GT = '>';


		private static readonly CliCommand SetImgurAuth = new CliCommand
		{
			Parameter   = "--set-imgur-auth",
			Syntax      = "<consumer id> <consumer secret>",
			Description = "Sets up Imgur API authentication",
			Action = args =>
			{
				var newId     = args[1];
				var newSecret = args[2];

				Console.WriteLine("New client ID and secret: ({0}, {1})", newId, newSecret);

				Config.ImgurAuth = (newId, newSecret);
			}
		};
		
		private static readonly CliCommand SetSauceNaoAuth = new CliCommand
		{
			Parameter   = "--set-saucenao-auth",
			Syntax      = "<api key>",
			Description = "Sets up SauceNao API authentication",
			Action = args =>
			{
				var newKey     = args[1];

				Console.WriteLine("New API key: {0}", newKey);

				Config.SauceNaoAuth = newKey;
			}
		};

		private static readonly CliCommand SetSearchEngines = new CliCommand
		{
			Parameter   = "--search-engines",
			Syntax      = "<search engines>",
			Description = "Sets the search engines to utilize when searching; delimited by commas",
			Action = args =>
			{
				var newOptions = args[1];

				Console.WriteLine("Engines: {0}", newOptions);

				Config.SearchEngines = Enum.Parse<SearchEngines>(newOptions);
			}
		};

		private static readonly CliCommand SetPriorityEngines = new CliCommand
		{
			Parameter = "--priority-engines",
			Syntax    = "<search engines>",
			Description = "Sets the search engines whose results to automatically " +
			              "open in the browser when search is complete; delimited by commas",
			Action = args =>
			{
				var newOptions = args[1];

				Console.WriteLine("Priority engines: {0}", newOptions);

				Config.PriorityEngines = Enum.Parse<SearchEngines>(newOptions);
			}
		};

		private static readonly CliCommand ContextMenu = new CliCommand
		{
			Parameter   = "--ctx-menu",
			Syntax      = null,
			Description = "Installs the context menu integration",
			Action = args =>
			{
				Config.AddToContextMenu();

				Console.WriteLine("Added to context menu!");
			}
		};

		private static readonly CliCommand Reset = new CliCommand()
		{
			Parameter   = "--reset",
			Syntax      = null,
			Description = "Resets configuration to defaults",
			Action = args =>
			{
				Config.Reset();

				Console.WriteLine("Config reset");
			}
		};

		public static readonly CliCommand[] AllCommands =
		{
			SetImgurAuth, SetSauceNaoAuth, SetSearchEngines, ContextMenu, SetPriorityEngines, Reset
		};

		public static void WriteHelp()
		{
			Console.WriteLine("Available commands:\n");

			foreach (var command in AllCommands) {
				Console.WriteLine(command);
				Console.WriteLine();
			}

			Console.WriteLine("See readme: {0}", Readme);
		}

		public static CliCommand ReadCommand(string s)
		{
			var cmd = AllCommands.FirstOrDefault(cliCmd => cliCmd.Parameter == s);

			return cmd;
		}

		public static void Init()
		{
			Console.Title          = Config.NAME;
			Console.OutputEncoding = Encoding.Unicode;
			Console.Clear();
		}

		public static void WithColor(ConsoleColor color, Action func)
		{
			var oldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			func();
			Console.ForegroundColor = oldColor;
		}

		[StringFormatMethod(STRING_FORMAT_ARG)]
		public static void WriteError(string msg, params object[] args)
		{
			WithColor(ConsoleColor.Red, () =>
			{
				//
				Console.WriteLine("{0} {1}", MUL_SIGN, string.Format(msg, args));
			});
		}

		[StringFormatMethod(STRING_FORMAT_ARG)]
		public static void WriteInfo(string msg, params object[] args)
		{
			WithColor(ConsoleColor.White, () =>
			{
				//
				Console.WriteLine("{0} {1}", GT, string.Format(msg, args));
			});
		}

		[StringFormatMethod(STRING_FORMAT_ARG)]
		public static void WriteSuccess(string msg, params object[] args)
		{
			WithColor(ConsoleColor.Green, () =>
			{
				//
				Console.WriteLine("{0} {1}", RAD_SIGN, string.Format(msg, args));
			});
		}
	}
}
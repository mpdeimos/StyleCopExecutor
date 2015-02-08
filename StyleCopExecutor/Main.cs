using System;
using System.Collections.Generic;

namespace StyleCopExecutor
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string settingsFile = null;
			string outputFile = null;
			string workingDirectory = null;
			List<string> files = new List<string> ();
			
			string mode = null;
			foreach (string _arg in args) {
				var arg = _arg.Trim ();
				if (arg.StartsWith ("-")) {
					mode = arg;
					continue;
				}
				
				switch (mode) {
				case "-o":
					outputFile = arg;
					break;
				case "-s":
					settingsFile = arg;
					break;
				case "-w":
					workingDirectory = arg;
					break;
				case "-f":
					files.Add (arg);
					break;
				case "-l":
					files.AddRange (System.IO.File.ReadLines (arg));
					break;
				}
			}
			
			StyleCopConsole console = new StyleCopConsole (
				settingsFile,
				false,
				outputFile,
				null,
				true);
		
			StyleCop.CodeProject project = new StyleCop.CodeProject (
				0,
				System.IO.Directory.GetCurrentDirectory (),
				new StyleCop.Configuration (null));
			
			files.RemoveAll (file => System.String.IsNullOrEmpty (file));
			
			foreach (string file in files) {
				var filepath = file.Trim ();
				if (workingDirectory != null) {
					filepath = System.IO.Path.Combine (workingDirectory, file);
				}
				console.Core.Environment.AddSourceCode (project, filepath, null);
			}

//			console.OutputGenerated += OnOutputGenerated;
//			console.ViolationEncountered += OnViolationEncountered;
			console.Start (new[] { project }, true);
//			console.OutputGenerated -= OnOutputGenerated;
//			console.ViolationEncountered -= OnViolationEncountered;
		}
		
		/// <summary>
		/// Handles generated output.
		/// </summary>
		private static void OnOutputGenerated (
			object sender,
			StyleCop.OutputEventArgs e)
		{
			//Console.WriteLine (e.Output);
		}

		/// <summary>
		/// Handles encountered violations.
		/// </summary>
		private static void OnViolationEncountered (
			object sender,
			StyleCop.ViolationEventArgs e)
		{
			Console.WriteLine (
				"{0}: {1}",
				e.Violation.Rule.CheckId,
				e.Message);
		}
	}
}
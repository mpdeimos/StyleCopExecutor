using System;

namespace StyleCopExecutor
{
	public class ZipFileEnvironment: StyleCop.FileBasedEnvironment
	{
		private System.Collections.Generic.Dictionary<string, ZipFile> zipFiles = new System.Collections.Generic.Dictionary<string, ZipFile> ();

		public ZipFileEnvironment ()
		{
			String[] parts = file.Split ('!');
			if (parts.Length == 2) {
				string zip = parts [0];
				string entry = parts [1];
				if (!zipFiles.ContainsKey (zip)) {
					zipFiles [zip] = ZipFile.Read (zip);
				}
				
				return new ZipCodeFile (zip, entry, file, project, parser, context);
			}
			
			return new CodeFile (file, project, parser, configurations);
		}
	}
}


using System;
using Ionic.Zip;
using StyleCop;
using System.Collections.Generic;

namespace StyleCopExecutor
{
	public class CodeFileFactory
	{
		private System.Collections.Generic.Dictionary<string, ZipFile> zipFiles = new System.Collections.Generic.Dictionary<string, ZipFile> ();
		private static CodeFileFactory instance;
		
		private CodeFileFactory ()
		{
			
		}
		
		public static CodeFileFactory Instance ()
		{
			if (instance == null) {
				instance = new CodeFileFactory ();
			}
			return instance;
		}
		
		/*public CodeFile GetCodeFile (string file, CodeProject project)
		{
			IEnumerable<Configuration> configurations = null;
			
            // Get the parsers for this file based on its extension.
			string extension = Path.GetExtension (file);
			if (extension != null && extension.Length > 0) {
                // Remove the leading dot and convert the extension to uppercase.
				extension = extension.Substring (1).ToUpperInvariant ();

				ICollection<SourceParser> parserList = this.GetParsersForFileType (extension);
				if (parserList != null) {
                    // Create SourceCode objects representing this file, for each parser.
					foreach (SourceParser parser in parserList) {
                        // Create and return a SourceCode for this file.
						SourceCode source = this.CreateCodeFile (path, project, parser, context);
						project.AddSourceCode (source);
						added = true;
					}
				}
			}
		}
		
		private CodeFile CreateCodeFile (string file, CodeProject project, SourceParser parser, object context)
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
		}*/
		
		public ZipFile GetCachedZipFile (string zipFile)
		{
			lock(zipFiles)
			{
				if (!zipFiles.ContainsKey (zipFile)) {
					zipFiles [zipFile] = ZipFile.Read (zipFile);
				}
			}
			return zipFiles [zipFile];
		}
	}
}


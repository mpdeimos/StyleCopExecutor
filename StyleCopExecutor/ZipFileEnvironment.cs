using System;
using Ionic.Zip;
using StyleCop;

namespace StyleCopExecutor
{
	public class ZipFileEnvironment: StyleCop.FileBasedEnvironment
	{
		//private System.Collections.Generic.Dictionary<string, ZipFile> zipFiles = new System.Collections.Generic.Dictionary<string, ZipFile> ();

		public ZipFileEnvironment ()
		{
		}
		
		public override bool AddSourceCode (CodeProject project, string path, object context)
		{
			Console.WriteLine(" === add source code " + path);
			var ext = System.IO.Path.GetExtension(path).Substring(1).ToUpper();
			Console.WriteLine (" === with extension " + ext);
			var parserList = this.GetParsersForFileType(ext);
			return base.AddSourceCode(project, path, context);
		}
		
	    protected override CodeFile CreateCodeFile(string path, CodeProject project, SourceParser parser, object context)
        {
			Console.WriteLine(" === create code file " + path);
            Param.Ignore(path, project, parser, context);
        
			String[] parts = path.Split ('!');
			if (parts.Length == 2) {
				
				string zip = parts [0];
				string entry = parts [1];
				Console.WriteLine("... as zip file " + zip + " with entry " + entry);
				/*if (!zipFiles.ContainsKey (zip)) {
					zipFiles [zip] = ZipFile.Read (zip);
				}*/
				
				return new ZipCodeFile (zip, entry, path, project, parser);
			}
			
			Console.WriteLine("... as normal file");
			return new CodeFile (path, project, parser);
		}
	}
}


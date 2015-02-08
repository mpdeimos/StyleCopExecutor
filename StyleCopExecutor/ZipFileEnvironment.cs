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
			var ext = System.IO.Path.GetExtension(path).Substring(1).ToUpper();
			var parserList = this.GetParsersForFileType(ext);
			return base.AddSourceCode(project, path, context);
		}
		
	    protected override CodeFile CreateCodeFile(string path, CodeProject project, SourceParser parser, object context)
        {
            Param.Ignore(path, project, parser, context);
        
			String[] parts = path.Split ('!');
			if (parts.Length == 2) {
				
				string zip = parts [0];
				string entry = parts [1];
				/*if (!zipFiles.ContainsKey (zip)) {
					zipFiles [zip] = ZipFile.Read (zip);
				}*/
				
				return new ZipCodeFile (zip, entry, path, project, parser);
			}
			return new CodeFile (path, project, parser);
		}
	}
}


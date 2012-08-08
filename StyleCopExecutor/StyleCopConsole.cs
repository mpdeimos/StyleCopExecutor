using System;
using System.Collections.Generic;

namespace StyleCopExecutor
{
	public class StyleCopConsole: StyleCop.StyleCopConsole
	{
	   public StyleCopConsole(
            string settings,
            bool writeResultsCache,
            string outputFile,
            ICollection<string> addInPaths,
            bool loadFromDefaultPath) 
            : base(settings, writeResultsCache, outputFile, addInPaths, loadFromDefaultPath, null)
        {
            // empty
        }
		
		protected override void InitCore ()
		{
			this.Core = new StyleCop.StyleCopCore (new ZipFileEnvironment(), null);
			base.InitCore ();
		}
	}
}


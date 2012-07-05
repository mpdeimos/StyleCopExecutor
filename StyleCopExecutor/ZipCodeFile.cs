using System;
using StyleCop;
using Ionic.Zip;
using System.Collections.Generic;

namespace StyleCopExecutor
{
	public class ZipCodeFile : CodeFile
	{
		private string zip;
		private string zipEntryName;
		
		public ZipCodeFile (string zip, string zipEntryName, string path, CodeProject project, SourceParser parser, IEnumerable<Configuration> configurations)
            : base(path, project, parser, configurations)
		{
			this.zip = zip;
			this.zipEntryName = zipEntryName;
		}
		
		/// <summary>
		///   Gets a value indicating whether the source code document currently exists and is accessible.
		/// </summary>
		public override bool Exists {
			get {
				ZipFile zipFile = CodeFileFactory.Instance ().GetCachedZipFile (zip);
				return !string.IsNullOrEmpty (this.Path) && zipFile.ContainsEntry (this.zipEntryName);
			}
		}
		
		public override System.IO.TextReader Read ()
		{
			ZipFile zipFile = CodeFileFactory.Instance ().GetCachedZipFile (zip);
			ZipEntry entry = zipFile [zipEntryName];
			return new System.IO.StreamReader (entry.OpenReader ());
		}
	}
}


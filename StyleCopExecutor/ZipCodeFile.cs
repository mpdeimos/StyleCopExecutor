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
		
		public ZipCodeFile (string zip, string zipEntryName, string path, CodeProject project, SourceParser parser)
            : base(path, project, parser)
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
			
			var memoryStream = new System.IO.MemoryStream();
			lock(zipFile)
			{
				ZipEntry entry = zipFile [zipEntryName];
				entry.Extract(memoryStream);
			}
			memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
			return new System.IO.StreamReader (memoryStream);
		}
	}
}


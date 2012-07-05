using System;
using Ionic.Zip;

namespace PlayGround
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			String[] parts = args [0].Split ('!');
			using (ZipFile zip = ZipFile.Read(parts [0])) {
				ZipEntry entry = zip [parts [1]];
				using (var stream = new System.IO.StreamReader(entry.OpenReader())) {
					while (stream.Peek() > 0) {
						Console.WriteLine (stream.ReadLine ());
					}
				}
			}
		}
	}
}

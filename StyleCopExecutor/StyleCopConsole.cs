using System;

namespace StyleCopExecutor
{
	public class StyleCopConsole: StyleCop.StyleCopConsole
	{
		protected override void InitCore ()
		{
			this.Core = new StyleCop.StyleCopCore (null, null);
			base.InitCore ();
		}
	}
}


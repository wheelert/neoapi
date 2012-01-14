using System;
using Gtk;

namespace neoapi
{
	public partial class Audio_Files : Gtk.Window
	{
		public Audio_Files () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}


using System;

namespace neoapi
{
	public partial class audio_files : Gtk.Window
	{
		public audio_files () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}


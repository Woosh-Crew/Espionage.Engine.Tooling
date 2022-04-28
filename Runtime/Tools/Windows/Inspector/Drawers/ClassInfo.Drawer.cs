using Espionage.Engine;
using ImGuiNET;
using UnityEngine;

namespace Espionage.Engine.Tools
{
	[Target( typeof( Library ) )]
	internal class ClassInfoDrawer : Inspector.Drawer<Library>
	{
		protected override bool OnLayout( object instance, in Library value, out Library change )
		{
			if ( ImGui.Selectable( "Library" ) )
			{
				Engine.Modules.Get<Diagnostics>().Selection = value;
			}

			ImGui.SameLine();
			ImGui.TextColored( Color.gray, $" [{value.Name}, {value.Group}]" );

			change = null;
			return false;
		}
	}
}

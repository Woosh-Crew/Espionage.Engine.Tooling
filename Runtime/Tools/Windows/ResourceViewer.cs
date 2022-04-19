using Espionage.Engine.Resources;
using ImGuiNET;

namespace Espionage.Engine.Tools
{
	public class ResourceViewer : Window
	{
		public override void OnLayout()
		{
			foreach ( var resource in Resource.Database )
			{
				if ( ImGui.Selectable( $"{resource.Identifier}" ) )
				{
					Service.Selection = resource;
				}
			}
		}
	}
}

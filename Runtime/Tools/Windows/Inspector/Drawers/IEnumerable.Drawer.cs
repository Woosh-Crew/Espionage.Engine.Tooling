using System.Collections;
using System.Collections.Generic;
using Espionage.Engine;
using ImGuiNET;
using UnityEngine;

namespace Espionage.Engine.Tools
{
	[Target( typeof( IEnumerable ) )]
	public class IEnumerableDrawer : Inspector.Drawer<IEnumerable>
	{
		protected override bool OnLayout( object instance, in IEnumerable value, out IEnumerable change )
		{
			var tree = ImGui.TreeNode( Property?.Name ?? Type.Name );
			ImGui.SameLine();
			ImGui.TextColored( Color.gray, "[Readonly]" );

			if ( tree )
			{
				var index = 0;

				try
				{
					foreach ( var item in value )
					{
						index++;

						if ( item == null )
						{
							ImGui.Text( "Null" );
							continue;
						}

						// Normal Drawer
						ImGui.BeginGroup();
						{
							ImGui.PushID( (Property?.Name ?? Type.Name) + index );

							Inspector.PropertyGUI( item.GetType(), Property, instance, item, out _ );

							ImGui.PopID();
						}
						ImGui.EndGroup();

						if ( ImGui.IsItemHovered() && !string.IsNullOrWhiteSpace( Property?.Help ) )
						{
							ImGui.SetTooltip( Property.Help );
						}
					}
				}
				finally
				{
					if ( index == 0 )
					{
						ImGui.Text( "Empty Collection" );
					}
				}
				
				if ( ImGui.Selectable( "Inspect Collection" ) )
				{
					Engine.Modules.Get<Diagnostics>().Selection = value;
				}

				ImGui.TreePop();
			}

			change = null;
			return false;
		}
	}
}

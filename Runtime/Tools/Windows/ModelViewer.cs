using Espionage.Engine.Resources;
using ImGuiNET;
using UnityEngine;

namespace Espionage.Engine.Tools
{
	public class ModelViewer : Window
	{
		private string _input = "";
		private Entity _preview;

		public override void OnLayout()
		{
			if ( ImGui.InputTextWithHint( string.Empty, "Model Path...", ref _input, 160, ImGuiInputTextFlags.EnterReturnsTrue ) )
			{
				Send();
			}
		}

		void Send()
		{
			_preview ??= Entity.Create<Entity>();
			
			// Load Model
			_preview.Visuals.Model = Assets.Load<Model>( _input );
			_preview.Position = Vector3.up * 150;

			Window.Show<Viewport>().Inspecting = _preview;
		}
	}
}

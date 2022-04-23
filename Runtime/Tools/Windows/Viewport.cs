using Espionage.Engine.ImGUI;
using ImGuiNET;
using UnityEngine;

namespace Espionage.Engine.Tools
{
	public class Viewport : Window
	{
		public Camera Camera { get; }
		public RenderTexture Texture { get; set; }
		public Entity Inspecting { get; set; }

		public Viewport()
		{
			Camera = new GameObject( "Viewport" ).AddComponent<Camera>();
			Texture = new( 512, 512, 16, RenderTextureFormat.ARGB32 );

			Camera.targetTexture = Texture;
			Camera.transform.position = Vector3.up * 2 + Vector3.back * 2;
		}

		internal override bool Layout()
		{
			if ( !Service.Enabled )
			{
				return false;
			}

			var delete = true;

			ImGui.SetNextWindowSize( new( 512, 512 ), ImGuiCond.Once );

			ImGui.PushStyleVar( ImGuiStyleVar.WindowPadding, Vector2.zero );
			if ( ImGui.Begin( ClassInfo.Title, ref delete, Flags ) )
			{
				OnLayout();
			}

			ImGui.End();
			ImGui.PopStyleVar();

			return !delete;
		}

		public override void OnLayout()
		{
			var windowWidth = (int)ImGui.GetWindowWidth();
			var windowHeight = (int)ImGui.GetWindowHeight() - 36;

			if ( Texture.width != windowWidth || Texture.height != windowHeight )
			{
				Camera.targetTexture.Release();
				Texture = new( windowWidth, windowHeight, 16, RenderTextureFormat.ARGB32 );
				Camera.targetTexture = Texture;
			}

			ImGui.Image( UImGuiUtility.GetTextureId( Texture ), new( windowWidth, windowHeight ) );

			if ( Inspecting == null || Inspecting.Visuals.Model == null )
			{
				return;
			}

			Camera.transform.position = Camera.transform.position.WithY( Inspecting.Visuals.Bounds.center.y );
			Camera.transform.LookAt( Inspecting.Visuals.Bounds.center );
			Camera.transform.RotateAround( Inspecting.Visuals.Bounds.center, Vector3.up, 20 * Time.deltaTime );
		}
	}
}

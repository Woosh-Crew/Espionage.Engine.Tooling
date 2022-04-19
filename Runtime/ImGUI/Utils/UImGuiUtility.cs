using ImGuiNET;
using System;
using Espionage.Engine.ImGUI.Texture;
using UnityEngine;
using UTexture = UnityEngine.Texture;

namespace Espionage.Engine.ImGUI
{
	public static class UImGuiUtility
	{
		public static IntPtr GetTextureId( UTexture texture )
		{
			return Context?.TextureManager.GetTextureId( texture ) ?? IntPtr.Zero;
		}

		internal static SpriteInfo GetSpriteInfo( Sprite sprite )
		{
			return Context?.TextureManager.GetSpriteInfo( sprite ) ?? null;
		}

		internal static Context Context;

		internal static unsafe Context CreateContext()
		{
			return new()
			{
				ImGuiContext = ImGui.CreateContext(),
				TextureManager = new()
			};
		}

		internal static void DestroyContext( Context context )
		{
			ImGui.DestroyContext( context.ImGuiContext );
		}

		internal static void SetCurrentContext( Context context )
		{
			Context = context;
			ImGui.SetCurrentContext( context?.ImGuiContext ?? IntPtr.Zero );
		}
	}
}

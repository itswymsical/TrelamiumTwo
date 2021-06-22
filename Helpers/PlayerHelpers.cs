using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Terraria;

namespace TrelamiumTwo.Helpers
{
    internal static partial class Helper
    {
		public static bool JustPressed(this Player player, Keys key) => Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);

		public static bool IsUnderwater(this Player player) => Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);

		public static bool IsUnderground(this Player player) => player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight;

		public static bool HasInInventory(this Player player, params int[] items) => player.inventory.Any(item => items.Contains(item.type));

		public static bool ZoneAnyPillar(this Player player) => player.ZoneTowerStardust || player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula;

		public static bool ZoneForest(this Player player) =>
			!player.ZoneJungle
			&& !player.ZoneDungeon
			&& !player.ZoneCorrupt
			&& !player.ZoneCrimson
			&& !player.ZoneHoly
			&& !player.ZoneSnow
			&& !player.ZoneUndergroundDesert
			&& !player.ZoneGlowshroom
			&& !player.ZoneMeteor
			&& !player.ZoneBeach
			&& !player.ZoneDesert
			&& player.ZoneOverworldHeight;

		public static void AddAllCrit(this Player player, int crit)
		{
			player.meleeCrit += crit;
			player.rangedCrit += crit;
			player.magicCrit += crit;
			player.thrownCrit += crit;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="player"></param>
		/// <param name="intensitiy">The intensity of the screen shake.</param>
		/// <param name="minValue">the minimum value that is required for screen shakes to take effect and the value that the shake ends at. Defaults to .15f.</param>
		/// <param name="multiplier">Multiplies the intensity of the shake. Defaults to .95f.</param>
		public static void ApplyScreenShake(this Entity entity, float intensitiy = 1f, float minValue = .15f, float multiplier = .95f)
        {
			if (intensitiy > minValue)
			{
				var shake = new Vector2(Main.rand.NextFloat(-intensitiy, intensitiy), Main.rand.NextFloat(-intensitiy, intensitiy));

				Main.screenPosition += shake;

				intensitiy *= multiplier;
			}
		}
	}
}

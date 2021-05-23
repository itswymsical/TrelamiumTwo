using System.Linq;
using Terraria;

namespace TrelamiumTwo.Utilities.Extensions
{
    internal static class PlayerExtensions
    {
		public static bool IsUnderwater(this Player player) => Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);

		public static bool HasInInventory(this Player player, params int[] items) => player.inventory.Any(item => items.Contains(item.type));

		public static bool ZoneAnyPillar(this Player player) => player.ZoneTowerStardust || player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula;

		public static bool ZoneForest(this Player player)
		{
			return !player.ZoneJungle
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
		}
	}
}

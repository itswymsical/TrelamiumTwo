#region Using directives

using Terraria;

#endregion

namespace TrelamiumTwo.Common.Extensions
{
	internal static class PlayerExtensions
	{
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

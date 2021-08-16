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
			player.GetCritChance(DamageClass.Melee) += crit;
			player.GetCritChance(DamageClass.Ranged) += crit;
			player.GetCritChance(DamageClass.Magic) += crit;
			player.GetCritChance(DamageClass.Throwing) += crit;
		}
	}
}

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Content.Projectiles.Summon;

namespace TrelamiumTwo.Content.Buffs.Minion
{
	public class FloralSpiritBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Floral Spirit");
			Description.SetDefault("The Floral Spirit will protect you");

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			bool notSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<FloralSpiritGuardian>()] <= 0 && Main.LocalPlayer.GetModPlayer<ArmorSetPlayer>().EverbloomSet;
			if (notSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<FloralSpiritGuardian>(), 0, 0f, player.whoAmI);
			}
		}
	}
}

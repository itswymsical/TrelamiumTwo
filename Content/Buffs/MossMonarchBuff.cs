using Trelamium2.Content.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;
using Trelamium2.Common.Players;
using System;

namespace Trelamium2.Content.Buffs
{
	public class MossMonarchBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Moss Monarch");
			Description.SetDefault("The Moss Monarch, a hornet with a Vortex Beater!");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<TrelamiumPlayer>().mossMonarch = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<MossMonarchProjectile>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<MossMonarchProjectile>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
    }
}
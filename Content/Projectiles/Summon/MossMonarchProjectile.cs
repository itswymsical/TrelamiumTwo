using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Projectiles.Summon
{
	public class MossMonarchProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moss Monarch");
		}
		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 54;

			projectile.penetrate = -1;
			projectile.timeLeft *= 5;

			projectile.friendly = 
				projectile.ignoreWater = 
				projectile.minion = true;

			projectile.tileCollide = false;

			projectile.minionSlots = 1f;
		}
        public override bool MinionContactDamage() => false;
        public override void AI()
		{
			/* TODO: sig
			* Fly & Target NPCs
			* Focus Fire on NPCs, Rapid fire Vortex Beater
			* Occasionally Fire Vortex Beater Rockets
			*/
			Player player = Main.player[projectile.owner];
			MinionPlayer minionPlayer = player.GetModPlayer<MinionPlayer>();
			player.AddBuff(ModContent.BuffType<Buffs.Minion.MossMonarchBuff>(), 3600);
			if (player.dead)
			{
				minionPlayer.mossMonarch = false;
			}
			if (minionPlayer.mossMonarch)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}
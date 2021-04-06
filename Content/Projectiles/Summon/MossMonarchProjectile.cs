using Terraria;
using Terraria.ModLoader;

namespace Trelamium2.Content.Projectiles.Summon
{
	public class MossMonarchProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moss Monarch");
			// Projectile Frame Count
		}
		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 54;
			projectile.penetrate = -1;
			projectile.timeLeft *= 5;

			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;

			projectile.minion = true;
			projectile.minionSlots = 1f;
		}
        public override bool MinionContactDamage() => false;
        public override void AI()
		{
			// Fly & Target NPCs
			// Focus Fire on NPCs, Rapid fire Vortex Beater
			// Occasionally Fire Vortex Beater Rockets
			var modPlayer = Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>();
			Player player = Main.player[projectile.owner];
			player.AddBuff(ModContent.BuffType<Buffs.Minion.MossMonarchBuff>(), 3600);
			if (player.dead)
			{
				modPlayer.mossMonarch = false;
			}
			if (modPlayer.mossMonarch)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}
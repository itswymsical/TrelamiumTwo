using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Trelamium2.Content.Projectiles.Summon
{
	public class PholiotaMinionProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pholiota Mushroom");
			Main.projFrames[projectile.type] = 8;
		}
		public override void SetDefaults()
		{
			projectile.width = 42;
			projectile.height = 44;
			projectile.penetrate = -1;
			projectile.timeLeft *= 5;
			aiType = 266;
			projectile.aiStyle = 26;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.minion = true;
			projectile.minionSlots = 1f;
		}
		public override bool MinionContactDamage()
		{
			return true;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.penetrate == 0)
			{
				projectile.Kill();
			}
			return false;
		}
		public override void AI()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>();
			Player player = Main.player[projectile.owner];
			player.AddBuff(BuffType<Content.Buffs.Minion.PholiotaMinionBuff>(), 3600);

			if (player.dead)
			{
				modPlayer.pholiotaMinion = false;
			}
			if (modPlayer.pholiotaMinion)
			{
				projectile.timeLeft = 2;
			}
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				projectile.frameCounter = 0;
				num = projectile.frame + 1;
				projectile.frame = num;
				if (num >= 2)
				{
					projectile.frame = 0;
				}
			}
		}
	}
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Projectiles;
using TrelamiumTwo.Core;
using TrelamiumTwo.Core.Mechanics.Trails;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
	public class ToadstoolClusterclot : TomahawkProjectile
	{
		public override string Texture => Assets.Items.Fungore + "ToadstoolClusterclot";

		public override void SetStaticDefaults() => base.SetStaticDefaults();
		

		public override void SetDefaults()
		{
			projectile.width = projectile.height = 20;

			base.SetDefaults();
		}
        public override void Kill(int timeLeft)
		{
			int num281 = 22;
			for (int num282 = 0; num282 < num281; num282++)
			{
				int num283 = Dust.NewDust(projectile.Center, 0, 0, ModContent.DustType<Dusts.Mushroom>(), 0f, 0f, 0, default, 0.5f);
				Dust dust = Main.dust[num283];
				dust.velocity *= 1.6f;
				Dust dust25 = Main.dust[num283];
				dust25.velocity.Y = 1f;
				Main.dust[num283].position = Vector2.Lerp(Main.dust[num283].position, projectile.Center, 0.5f);
			}
			for (int i = 0; i < 2; ++i)
			{
				Projectile.NewProjectile(projectile.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 4f,
					ModContent.ProjectileType<Toadstool>(), (int)(projectile.damage * 0.5f), 0.5f, projectile.owner);
			}
		}
	}
}
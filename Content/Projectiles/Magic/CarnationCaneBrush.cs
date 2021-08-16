using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;


namespace TrelamiumTwo.Content.Projectiles.Magic
{
	internal class CarnationCaneBrush : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Magic + "CarnationCaneBrush";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bush");
		}
		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 28;

			Projectile.penetrate = 3;
			Projectile.timeLeft = 300;

			Projectile.friendly = true;
			// projectile.tileCollide = false;
			// projectile.ignoreWater = false;
		}

		public override bool PreAI()
		{
			if (Projectile.ai[0] == 0)
			{
				DustEffect();

				Projectile.position.Y += 2;

				Projectile.ai[0] = 1;
			}
			return (false);
		}

		public override void Kill(int timeLeft) => DustEffect();

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)=> Projectile.DrawProjectileCentered(spriteBatch, lightColor);

		private void DustEffect()
		{
			for (int i = 0; i < 10; ++i)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.BloomRose>(), 0, 0, 100);
			}
		}
	}
}

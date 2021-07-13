using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;


namespace TrelamiumTwo.Content.Projectiles.Magic
{
	internal sealed class CarnationCaneBrush : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brush");
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 28;

			projectile.penetrate = 3;
			projectile.timeLeft = 300;

			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
		}

		public override bool PreAI()
		{
			if (projectile.ai[0] == 0)
			{
				DustEffect();

				projectile.position.Y += 2;

				projectile.ai[0] = 1;
			}

			return (false);
		}

		public override void Kill(int timeLeft) => DustEffect();

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)=> projectile.DrawProjectileCentered(spriteBatch, lightColor);

		private void DustEffect()
		{
			for (int i = 0; i < 10; ++i)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.BloomRose>(), 0, 0, 100);
			}
		}
	}
}

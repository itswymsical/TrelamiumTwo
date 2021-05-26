using TrelamiumTwo.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Dusts
{
	public class BloomRose : ModDust
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			return mod.Properties.Autoload;
		}

		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;

			dust.velocity *= 0.5f;
			dust.velocity.Y -= 0.1f;

			dust.alpha = 10;
			dust.fadeIn = 1f;
			dust.scale = Main.rand.NextFloat(0.2f, 0.4f);

			dust.frame.X = 0;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.1f;

			dust.velocity.X += Main.windSpeed / 20f;
			dust.velocity.X = MathHelper.Clamp(dust.velocity.X, -1f, 1f);

			dust.velocity.Y -= 0.01f;
			dust.velocity.Y = MathHelper.Clamp(dust.velocity.Y, 0f, -0.25f);

			if (dust.velocity.Y < 0.25f)
				dust.velocity.Y = -0.25f;

			if (dust.fadeIn > dust.scale)
			{
				dust.scale += 0.005f;
				dust.alpha -= 5;
			}
			else
			{
				dust.fadeIn = 0f;

				dust.scale -= 0.005f;
				dust.alpha += 5;

				if (dust.scale < 0.1f)
					dust.active = false;
			}

			return false;
		}
	}
}
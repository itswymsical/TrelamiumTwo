using TrelamiumTwo.Core;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Dusts
{
	public class PinkPetal : ModDust
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = Assets.Dusts + "PinkPetal";
			return Mod.Properties.Autoload;
		}

		public override void OnSpawn(Dust dust)
		{
			// dust.noGravity = false;

			dust.velocity *= 0.5f;
			dust.velocity.Y -= 0.1f;

			dust.scale = Main.rand.NextFloat(0.6f, 0.8f);

			dust.frame.X = 0;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.1f;

			dust.scale -= 0.02f;

			if (dust.scale < 0.2f)
				// dust.active = false;

			return false;
		}
	}
}
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Dusts
{
	public class Earthen : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = false;

			dust.velocity *= 0.6f;

			dust.scale = Main.rand.NextFloat(0.8f, 1f);
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.1f;

			dust.scale -= 0.02f;

			if (dust.scale < 0.2f)
				dust.active = false;

			return false;
		}
	}
}
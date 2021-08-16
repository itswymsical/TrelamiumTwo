using Terraria;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Dusts
{
	public class Dustilite : ModDust
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = Assets.Dusts + "Dustilite";
			return Mod.Properties.Autoload;
		}
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.velocity *= -0.5f;
			dust.velocity.Y -= 0.1f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.1f;
			dust.scale -= 0.02f;
			if (dust.scale < 0.5f)
			{
				// dust.active = false;
			}

			return (false);
		}
	}
}

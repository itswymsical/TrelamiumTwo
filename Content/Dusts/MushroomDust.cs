using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Dusts
{
	public class MushroomDust : ModDust
	{
		public override void OnSpawn(Dust dust) 
		{
			dust.noGravity = true;
			dust.noLight = true;
			dust.velocity *= 0.3f;
		}

		public override bool Update(Dust dust) 
		{
			dust.position += dust.velocity;
			dust.scale -= 0.01f;
			if (dust.scale < 0.10f)
				dust.active = false;
			else 
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), dust.color.R / 255f * 0.5f, dust.color.G / 255f * 0.5f, dust.color.B / 255f * 0.5f);
			return false;
		}
	}
}
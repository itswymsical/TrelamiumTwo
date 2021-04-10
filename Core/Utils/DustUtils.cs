using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TrelamiumTwo.Core.Utils
{
	public static class DustUtils // Eldrazi Code imported from EH
	{
		public static void SpawnDustCloud(Vector2 position, int width, int height, int type, int amount = 10, float speedX = 0, float speedY = 0, int alpha = 0, Color c = default, float scale = 1f)
		{
			for (int i = 0; i < amount; ++i)
			{
				Dust.NewDust(position, width, height, type, speedX, speedY, alpha, c, scale);
			}
		}

		public static void SpawnDustFromTexture(Vector2 position, int dustType, float size, string imagePath, bool noGravity = true, float rot = 0.34f, float velocityModifier = 1f)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				float rotation = Main.rand.NextFloat(-rot, rot);
				Texture2D texture = ModContent.GetTexture(imagePath);
				int[] pixelData = new int[texture.Width * texture.Height];
				
				texture.GetData(pixelData);

				for (int i = 0; i < texture.Width; i += 2)
				{
					for (int j = 0; j < texture.Height; j += 2)
					{
						if (pixelData[j * texture.Width + i] != 0)
						{
							Vector2 dustPosition = new Vector2(i - texture.Width / 2, j - texture.Height / 2) * size;
							Dust.NewDustPerfect(position, dustType, dustPosition.RotatedBy(rotation) * velocityModifier).noGravity = noGravity;
						}
					}
				}
			}
		}
	}
}

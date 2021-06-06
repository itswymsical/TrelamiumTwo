using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Dusts;

namespace TrelamiumTwo.Content.Tiles.DruidsGarden.Trees
{
	public class AlderwoodTree : ModTree
	{
		private Mod Mod => ModLoader.GetMod("TrelamiumTwo");

		public override int CreateDust() {
			return ModContent.DustType<Wood>();
		}

		public override int GrowthFXGore() {
			return Mod.GetGoreSlot("Content/Gores/AlderwoodTreeFX");
		}

		public override int DropWood() {
			return ModContent.ItemType<Items.Placeable.AlluviumOre>();
		}

		public override Texture2D GetTexture() {
			return Mod.GetTexture("Content/Tiles/DruidsGarden/Trees/AlderwoodTree");
		}

		public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset) 
		{
			frameWidth = 124;
			frameHeight = 96;
			xOffsetLeft = 50;
			yOffset = 2;
			return Mod.GetTexture("Content/Tiles/DruidsGarden/Trees/AlderwoodTree_Tops");
		}

		public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
		{
			return Mod.GetTexture("Content/Tiles/DruidsGarden/Trees/AlderwoodTree_Branches");
		}
	}
} 
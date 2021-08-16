using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class Alderwood : ModItem
	{
		public override string Texture => Assets.PlaceholderTexture;
        public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.maxStack = 999;
			Item.value = 0;
			Item.rare = ItemRarityID.White;
		}
    }
}

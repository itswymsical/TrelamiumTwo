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
			item.width = item.height = 20;
			item.maxStack = 999;
			item.value = 0;
			item.rare = ItemRarityID.White;
		}
    }
}

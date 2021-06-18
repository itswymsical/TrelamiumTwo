using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class BloomRose : ModItem
	{
		public override string Texture => Assets.Items.Materials + "BloomRose";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Withers away in the night");
        public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 2);
			item.rare = ItemRarityID.White;
		}
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
           if (!Main.dayTime)
            {
				for (int i = 0; i < 16; i++)
				{
					var velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
					Dust.NewDustDirect(item.Center, 0, 0, ModContent.DustType<Dusts.BloomRose>(), velocity.X, velocity.Y);
				}
				item.TurnToAir();
            }
        }
    }
}

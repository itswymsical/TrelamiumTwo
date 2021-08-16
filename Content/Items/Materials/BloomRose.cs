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
			Item.width = Item.height = 20;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(copper: 2);
			Item.rare = ItemRarityID.Blue;
		}
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
           if (!Main.dayTime)
            {
				for (int i = 0; i < 16; i++)
				{
					var velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
					Dust.NewDustDirect(Item.Center, 0, 0, ModContent.DustType<Dusts.BloomRose>(), velocity.X, velocity.Y);
				}
				Item.TurnToAir();
            }
        }
    }
}

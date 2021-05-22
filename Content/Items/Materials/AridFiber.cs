using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class AridFiber : TrelamiumItem
	{
		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.buyPrice(silver: 50);

			item.material = true;
		}
	}
}

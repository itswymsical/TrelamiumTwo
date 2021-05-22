using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class SandwormTooth : TrelamiumItem
	{
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 30;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.buyPrice(silver: 50);

			item.material = true;
		}
	}
}

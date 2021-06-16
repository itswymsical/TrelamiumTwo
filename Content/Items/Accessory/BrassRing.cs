using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class BrassRing : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 22;
			item.rare = ItemRarityID.Blue;
			item.defense = 1;
			item.accessory = true;
		}
	}
}

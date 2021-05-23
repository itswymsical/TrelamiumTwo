using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TrelamiumTwo.Content.Items.Materials
{
	public class AFunny : TrelamiumItem
	{
		public override void SetStaticDefaults() 
			=> DisplayName.SetDefault("Chris turns blue and fucking dies");
        public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 0, 2, 50);
		}
	}
}

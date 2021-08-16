using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class Microlith : ModItem
	{
		public override string Texture => Assets.Items.Accessory + "Microlith";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Microlith");
			Tooltip.SetDefault("Grants the player the chance to gain more ore from mining");
		}
		public override void SetDefaults()
		{
			Item.value = Item.sellPrice(gold: 2);
			Item.rare = ItemRarityID.Blue;
			Item.width = 24;
			Item.height = 30;
			Item.accessory = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<Common.Players.TrelamiumPlayer>().microlith = true;
        }
    }
}
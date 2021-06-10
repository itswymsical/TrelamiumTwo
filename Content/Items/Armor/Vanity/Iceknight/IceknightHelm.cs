using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Vanity.Iceknight
{
	[AutoloadEquip(EquipType.Head)]
	public class IceknightHelm : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Ice Knight's Mask");
            Tooltip.SetDefault("'Kingdom Krashers'");
		}

		public override void SetDefaults()
		{
            item.height = item.width = 20;
            item.rare = ItemRarityID.Blue;
            item.vanity = true;          
		}
    }
}
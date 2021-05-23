using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Vanity.Peepo
{
	[AutoloadEquip(EquipType.Head)]
	public class SigniasMask : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Signia's Mask");
            Tooltip.SetDefault("'Great for impersonating developers!'");
		}

		public override void SetDefaults()
		{
            item.height = 24;
            item.width = 20;
            item.value = 0;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;          
		}
        public override bool IsVanitySet(int head, int body, int legs)
        {
            return body == ModContent.ItemType<SigniasShirt>() && legs == ModContent.ItemType<SigniasLegs>();
        }
        public override void UpdateVanitySet(Player player)
        {
            if (player.name.Contains("sig"))
            {
                player.Yoraiz0rEye();
            }
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Trelamium2.Content.Items.Materials;

namespace Trelamium2.Content.Items.Weapons.Melee
{
    public class MossMonarchStaff : TrelamiumItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Moss Monarch Staff");
        public override void SetStaticDefaults() => Tooltip.SetDefault("Summons Moss Hornet to fight for you" +
            "\nHoly! He's an inferno!");
        public override void SetDefaults()
        {
            item.summon = true;
            item.noMelee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 25;
            item.useTime = 25;
            item.damage = 19;
            item.crit = 3;

            item.knockBack = 1.75f;

            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(gold: 10);
        }
    }
}
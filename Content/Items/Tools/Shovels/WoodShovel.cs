using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class WoodShovel : ShovelItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Wood Shovel");
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 2;
            item.useTime = 22;
            item.useAnimation = 22;

            diggingPower(28);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;

            item.value = Item.sellPrice(copper: 25);
            item.useTurn = true;

            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
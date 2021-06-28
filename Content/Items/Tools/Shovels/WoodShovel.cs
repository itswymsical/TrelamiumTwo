using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Items;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class WoodShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "WoodShovel";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Breaks a cluster of blocks");
        public override void SetDefaults()
        {
            DiggingPower(30);

            item.melee = true;
            item.damage = 2;
            item.useTime = item.useAnimation = 16;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(copper: 33);

            item.useStyle = ItemUseStyleID.SwingThrow;
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
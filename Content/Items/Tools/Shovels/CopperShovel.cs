using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Items;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class CopperShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "CopperShovel";
        public override void SetDefaults()
        {
            DiggingPower(32);

            item.melee = true;
            item.damage = 2;
            item.useTime = item.useAnimation = 19;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(copper: 33);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.CopperBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
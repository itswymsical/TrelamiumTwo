using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class ArchaicTrovel : ShovelItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Archaic Trovel");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 6;
            item.useTime = 22;
            item.useAnimation = 22;

            DiggingPower(45);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;

            item.value = Item.sellPrice(silver: 5);
            item.useTurn = true;

            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.Fossilite>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
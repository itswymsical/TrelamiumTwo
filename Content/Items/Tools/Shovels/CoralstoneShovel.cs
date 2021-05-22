using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class CoralstoneShovel : ShovelItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Coralstone Shovel");
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 6;
            item.useTime = 26;
            item.useAnimation = 26;
            item.rare = ItemRarityID.White;
            DiggingPower(45);

            item.autoReuse = true;
            item.useTurn = true;

            item.value = Item.sellPrice(silver: 5);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.Coralstone>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
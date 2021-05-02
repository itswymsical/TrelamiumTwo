using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Tools.Coralstone
{
    public class CoralstonePickaxe : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Coralstone Pickaxe");

        public override void SafeSetDefaults()
        {
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.useTime = 26;
            item.useAnimation = 26;
            item.damage = 10;
            item.rare = ItemRarityID.White;
            item.knockBack = 2f;
            item.pick = 45;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.Coralstone>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

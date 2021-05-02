using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Tools.Coralstone
{
    public class CoralstoneHamaxe : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Coralstone Hamaxe");

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
            item.axe = 20;
            item.hammer = 50;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.Coralstone>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

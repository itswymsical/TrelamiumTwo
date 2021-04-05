using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Trelamium2.Content.Items.Weapons.Sets.Primeval
{
    public class PrimordialSands : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Primordial Sands");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.damage = 9;
            item.crit = 1;
            item.rare = ItemRarityID.White;
            item.useAnimation = 25;
            item.useTime = 25;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4.5f;
            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;
            item.value = Item.buyPrice(0, 0, 50, 0);
            item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Fossilite>(), 6);
            recipe.AddIngredient(ItemID.Amber, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
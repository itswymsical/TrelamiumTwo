using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Trelamium2.Content.Items.Weapons.Sets.Primeval
{
    public class PrimevalStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Primeval Staff");
            Tooltip.SetDefault("Summons a desert wisp to fight for you");
        }
        public override void SetDefaults()
        {
            item.damage = 8;
            item.crit = 2;
            item.rare = ItemRarityID.White;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4.5f;
            item.ranged = true;
            item.noMelee = true;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet;
            item.value = Item.buyPrice(0, 0, 30, 0);
            item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Fossilite>(), 8);
            recipe.AddIngredient(ItemID.Amber, 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
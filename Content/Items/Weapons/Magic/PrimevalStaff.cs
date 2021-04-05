using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Trelamium2.Content.Items.Materials;

namespace Trelamium2.Content.Items.Weapons.Magic
{
    public class PrimevalStaff : TrelamiumItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Primeval Staff");
            Tooltip.SetDefault("Summons a desert wisp to fight for you");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.noMelee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.damage = 8;
            item.crit = 2;
            item.useAnimation = 30;
            item.useTime = 30;

            item.knockBack = 4.5f;

            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet; // shootSpeed?

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.White;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(silver: 30);
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Fossilite>(), 8);
            recipe.AddIngredient(ItemID.Amber, 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
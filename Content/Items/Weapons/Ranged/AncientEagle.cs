using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Trelamium2.Content.Items.Materials;

namespace Trelamium2.Content.Items.Weapons.Ranged
{
    public class AncientEagle : TrelamiumItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Ancient Eagle");

        public override void SafeSetDefaults()
        {
            item.ranged = true;
            item.noMelee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 30;
            item.useTime = 30;
            item.damage = 7;
            item.crit = 2;

            item.knockBack = 4.5f;

            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet;

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.rare = ItemRarityID.White;

            item.value = Item.buyPrice(silver: 30);
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Fossilite>(), 6);
            recipe.AddIngredient(ItemID.Amber, 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
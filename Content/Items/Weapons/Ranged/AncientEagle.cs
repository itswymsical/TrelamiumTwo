using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Materials;

namespace TrelamiumTwo.Content.Items.Weapons.Ranged
{
    public class AncientEagle : TrelamiumItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Ancient Eagle");

        public override void SafeSetDefaults()
        {
            item.ranged = item.noMelee = true;
            
            item.autoReuse = true;

            item.useAnimation = 30;
            item.useTime = 30;
            item.damage = 7;
            item.crit = 2;

            item.knockBack = 2.5f;

            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 4f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item11;
            item.rare = ItemRarityID.White;

            item.value = Item.buyPrice(silver: 30);
        }
        public override Vector2? HoldoutOffset() => new Vector2(-6, -4);
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
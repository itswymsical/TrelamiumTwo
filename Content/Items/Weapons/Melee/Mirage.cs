using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class Mirage : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Mirage");

        public override void SafeSetDefaults()
        {
            item.noUseGraphic = item.channel = item.melee = item.useTurn = item.autoReuse = true;

            item.useAnimation = 25;
            item.useTime = 25;
            item.damage = 15;
            item.crit = 2;

            item.knockBack = 2.5f;

            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<Projectiles.Melee.MirageProjectile>();
            item.shootSpeed = 7f;
            item.value = Item.buyPrice(silver: 2);
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Granite, 12);
            recipe.AddIngredient(ItemID.Sapphire, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
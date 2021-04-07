
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Trelamium2.Content.Projectiles.Ranged;

namespace Trelamium2.Content.Items.Weapons.Ranged
{
    public class DemoniteKunai : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Soulrender");

        public override void SetDefaults()
        {
            item.shootSpeed = 18f;

            item.damage = 8;
            item.knockBack = 2f;

            item.width = 26;
            item.height = 13;
            item.useTime = item.useAnimation = 17;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;

            item.shoot = ProjectileID.PurificationPowder;

            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(silver: 3);
            item.shoot = ModContent.ProjectileType<DemoniteKunaiProjectile>();
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

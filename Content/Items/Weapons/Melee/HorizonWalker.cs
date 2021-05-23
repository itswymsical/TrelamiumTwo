using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Materials;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class HorizonWalker : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Horizon Walker");

        public override void SafeSetDefaults()
        {
            item.noUseGraphic = 
                item.channel = 
                item.melee = 
                item.useTurn = 
                item.autoReuse = true;

            item.useAnimation =
                item.useTime = 25;
            item.damage = 15;
            item.crit = 2;

            item.knockBack = 2.5f;

            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<Projectiles.Melee.HorizonWalkerProjectile>();
            item.shootSpeed = 7f;
            item.value = Item.buyPrice(silver: 2);
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AzumuthBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
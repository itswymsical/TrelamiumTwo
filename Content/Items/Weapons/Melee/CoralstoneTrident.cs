using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Projectiles.Melee;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class CoralstoneTrident : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Coralstone Trident");
        

        public override void SetDefaults()
        {
            item.damage = 12;
            item.width = item.height = 60;
            item.useTime = item.useAnimation = 26;
            item.shootSpeed = 3.5f;
            item.melee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 3.25f;
            item.rare = ItemRarityID.White;
            item.value = Item.sellPrice(copper: 50);
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<CoralstoneTridentProjectile>();
        }

        public override bool CanUseItem(Player player)
           => player.ownedProjectileCounts[item.shoot] < 1;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.Coralstone>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

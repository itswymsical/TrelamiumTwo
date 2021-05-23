using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Content.Projectiles.Melee;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class DustiliteJavelin : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Dustilite Javelin");
        

        public override void SetDefaults()
        {
            item.damage = 22;
            item.width = item.height = 54;
            item.useTime = item.useAnimation = 26;
            item.shootSpeed = 3.5f;
            item.melee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 3f;
            item.rare = ItemRarityID.White;
            item.value = Item.sellPrice(copper: 50);
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<DustiliteJavelinProjectile>();
        }

        public override bool CanUseItem(Player player)
           => player.ownedProjectileCounts[item.shoot] < 1;

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<DustiliteCrystal>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

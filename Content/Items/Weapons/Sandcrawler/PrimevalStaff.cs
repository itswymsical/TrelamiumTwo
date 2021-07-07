using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Sandcrawler
{
    public class PrimevalStaff : ModItem
    {
        public override string Texture => Assets.Weapons.Sandcrawler + "PrimevalStaff";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Casts a burst of sandballs");
        public override void SetDefaults()
        {
            item.damage = 7;
            item.crit = 2;
            item.mana = 8;
            item.knockBack = 2.5f;

            item.useAnimation = item.useTime = 40;
            item.width = item.height = 52;

            Item.staff[item.type] = 
                item.magic = 
                item.noMelee = 
                item.autoReuse = true;

            item.useTurn = false;

            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item9;

            item.shoot = ModContent.ProjectileType<Projectiles.Magic.SandBall>();
            item.shootSpeed = 6.7f;

            item.value = Item.buyPrice(silver: 80);
        }  
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 80f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            for (int i = 0; i < 4; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(12));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed *= scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage / 2, knockBack, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AntlionMandible, 2);
            recipe.AddIngredient(ModContent.ItemType<Materials.AntlionChitin>(), 7);
            recipe.AddIngredient(ModContent.ItemType<Materials.SandcrawlerShell>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
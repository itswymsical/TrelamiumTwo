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
            Item.damage = 7;
            Item.crit = 2;
            Item.mana = 8;
            Item.knockBack = 2.5f;

            Item.useAnimation = Item.useTime = 40;
            Item.width = Item.height = 52;

            Item.staff[Item.type] = 
                Item.DamageType = 
                // item.noMelee = 
                item.autoReuse = true;

            // item.useTurn = false;

            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item9;

            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.SandBall>();
            Item.shootSpeed = 6.7f;

            Item.value = Item.buyPrice(silver: 80);
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
            CreateRecipe(1).AddIngredient(ItemID.AntlionMandible, 2).AddIngredient(ModContent.ItemType<Materials.AntlionChitin>(), 7).AddIngredient(ModContent.ItemType<Materials.SandcrawlerShell>(), 2).AddTile(TileID.Anvils).Register();
        }
    }
}
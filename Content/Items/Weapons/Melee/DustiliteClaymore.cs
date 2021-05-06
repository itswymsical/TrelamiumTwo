using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Materials;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class DustiliteClaymore : TrelamiumItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dustilite Claymore");
            Tooltip.SetDefault("Creates shadows of claymores on enemy hit");
        }

        public override void SafeSetDefaults()
        {
            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 28;
            item.useTime = 28;
            item.damage = 15;
            item.crit = 2;

            item.knockBack = 2.5f;

            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;

            item.value = Item.buyPrice(silver: 80);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.life > 0 && Main.rand.NextBool(3))
            {
                float length = 250f;
                Vector2 projectilePos = target.Center + (Main.rand.NextFloat() * MathHelper.TwoPi).ToRotationVector2() * length;
                Vector2 projectileVelocity = Vector2.Normalize(target.Center - projectilePos) * 15f;

                Projectile.NewProjectile(projectilePos, projectileVelocity, ModContent.ProjectileType<Projectiles.Melee.DustiliteClaymore_Proj>(), damage, knockBack, player.whoAmI, length);
            }
        }
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
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Content.Projectiles.Magic;

namespace TrelamiumTwo.Content.Items.Weapons.Magic
{
    public class RukhsBlessing : TrelamiumItem
    {
        public override void SetStaticDefaults()
            => DisplayName.SetDefault("Rukh's Blessing");
        
        public override void SafeSetDefaults()
        {
            item.damage = 11;
            item.knockBack = 3.5f;

            item.ranged = 
                item.noMelee =
                item.autoReuse = true;

            item.useTime = item.useAnimation = 20;
            item.width = item.height = 28;

            item.shootSpeed = 4f;
            item.shoot = ModContent.ProjectileType<RukhsBlessingProjectile>();

            item.rare = ItemRarityID.White;
            item.UseSound = SoundID.DD2_PhantomPhoenixShot;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(copper: 32);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float minDist = 20f;
            float maxDist = 60f;
            float randomRot = Main.rand.NextFloat() * MathHelper.TwoPi;

            Vector2 spawnPos = position + randomRot.ToRotationVector2() * MathHelper.Lerp(minDist, maxDist, Main.rand.NextFloat());

            for (int i = 0; i < 50; ++i)
            {
                spawnPos = position + randomRot.ToRotationVector2() * MathHelper.Lerp(minDist, maxDist, Main.rand.NextFloat());
                if (Collision.CanHit(position, 0, 0, spawnPos + (spawnPos - position).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
                {
                    break;
                }
                randomRot = Main.rand.NextFloat() * MathHelper.TwoPi;
            }
            Projectile.NewProjectile(spawnPos, Vector2.Zero, type, damage, knockBack, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);

            return false;
        }
    }
}
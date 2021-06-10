using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
    public class TalonArrowProjectile : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Talon Arrow");
        public override string Texture => "Terraria/Projectile_" + ProjectileID.WoodenArrowFriendly; // Placeholder Texture
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 32;
            projectile.timeLeft = 500;

            projectile.arrow = true;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.aiStyle = 1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.position, Vector2.Zero, ProjectileID.SandnadoFriendly, projectile.damage / 4, projectile.knockBack, projectile.owner);
        }
    }
}
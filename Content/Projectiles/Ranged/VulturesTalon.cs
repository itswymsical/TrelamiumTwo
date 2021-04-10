
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.Projectiles.Ranged
{
    public class VulturesTalon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Talon");
        }

        public override string Texture => "Terraria/Projectile_" + ProjectileID.JestersArrow;

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.aiStyle = 1;
            projectile.penetrate = 3;
            projectile.arrow = true;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();

            if (projectile.velocity.Y >= 0 && projectile.oldVelocity.Y < 0)
            {
                projectile.velocity.Y = 16f;
                projectile.velocity.X = 0f;
            }
        }
    }
}

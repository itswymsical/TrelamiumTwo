using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
    public class LightningProjectile : ModProjectile
    {
        private float directionTimer = 15;
        private Vector2 initialVel;
        private bool setVariables = false;
        public override void SetStaticDefaults() => DisplayName.SetDefault("Lighting");
        public override string Texture => "Terraria/Projectile_" + ProjectileID.None;
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.timeLeft = 300;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 100;
        }
        public override void AI()
        {
            directionTimer--;
            if (!setVariables)
            {
                initialVel = projectile.velocity;
                setVariables = true;
            }

            if (directionTimer <= 0)
            {
                projectile.velocity = initialVel.RotatedByRandom(MathHelper.ToRadians(15));
                directionTimer = 15;
            }

            projectile.rotation = projectile.velocity.ToRotation();

            for (int i = 0; i < 2; i++)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Dust.NewDustPerfect(position, 15, new Vector2(0f, 0f), 200, new Color(0, 167, 255), 1f);
                dust.noGravity = true;
                dust.fadeIn = 1f;
            }
        }

    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
    public class LightningProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.timeLeft = 200;
            projectile.extraUpdates = 1;
            projectile.magic = true;
        }
        public override void AI()
        {
            int num;
            for (int k = 0; k < 1; k = num + 1)
            {
                float x = projectile.velocity.X / 4f * k;
                float y = projectile.velocity.Y / 4f * k;
                int Index = Dust.NewDust(projectile.position, projectile.width, projectile.height, 156, 0f, 0f, 0, default(Color), 1.6f);
                Main.dust[Index].position = projectile.Center - new Vector2(x, y);
                Dust dust = Main.dust[Index];
                dust.velocity *= 0f;
                dust.noGravity = true;
                dust.fadeIn = 0.75f;
                Main.dust[Index].scale = 0.75f;
                num = k;
            }

            int radians = 16;
            Vector2 perturbedSpeed = projectile.velocity.RotatedByRandom(MathHelper.ToRadians(radians));

            if (radians >= 16)
                radians = -24;
            
            if (radians <= -16)
                radians = 16;
            
            float scale = 1f - (Main.rand.NextFloat() * .3f);
            projectile.velocity *= perturbedSpeed;
            projectile.velocity *= projectile.DirectionTo(Main.MouseWorld) * radians;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(2, projectile.position, 10);
            return true;
        }
    }
}
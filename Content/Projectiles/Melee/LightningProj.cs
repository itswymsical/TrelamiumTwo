using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
    public class LightningProj : ModProjectile
    {

        public override void SetStaticDefaults() => DisplayName.SetDefault("Lighting Projectile");
		public override string Texture => "Terraria/Projectile_" + ProjectileID.None;
		public override void SetDefaults()
        {
			projectile.width = projectile.height = 4;
			projectile.tileCollide = projectile.melee = true;
			projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 500;
            projectile.alpha = 255;
            projectile.penetrate = -1;
        }

        int radians = 16;
        
        public override void AI()
        {
            
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 projectilePosition = projectile.position;
                    projectilePosition -= projectile.velocity * ((float)i * 0.28f);
                    projectile.alpha = 255;
                    int dust = Dust.NewDust(projectilePosition, 1, 1, DustID.WitherLightning, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = (float)Main.rand.Next(45, 55) * 0.011f;
                    Main.dust[dust].velocity *= 0.15f;
                }
            }


            projectile.ai[0] += 1;

            if (projectile.ai[0] >= 8)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(radians));
                if (radians >= 16)
                {
                    radians = -24;
                }
                if (radians <= -16)
                {
                    radians = 16;
                }
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                projectile.velocity.Y = perturbedSpeed.Y;
                projectile.velocity.X = perturbedSpeed.X;
                projectile.ai[0] = 0;
            }
        }
    }
}

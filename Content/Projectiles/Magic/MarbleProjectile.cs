using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
    public class MarbleProjectile : ModProjectile
    {
        
        public override void SetStaticDefaults() => DisplayName.SetDefault("Marble Projectile");

        public override string Texture => "Terraria/Projectile_" + ProjectileID.None;

        public override void SetDefaults()
        {
            projectile.width = projectile.height = 16;

            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.magic = true;
        }
        

        int Timer = 0;
        public override void AI()
        {
            if (projectile.scale <= 3) // this scales the projectiles util it scales the projectile until its 3 times the original projectile
            {
                projectile.scale *= 1.03f;
                projectile.width = (int)(projectile.scale * 20); // make the width hitbox bigger
                projectile.height = (int)(projectile.scale * 24);  // makes the height hitbox bigger
            }
            var dust = Dust.NewDustDirect(projectile.position, 1, 1, 159, 0, 0, 100, Color.White, 2f);
            dust.noGravity = true;
            dust.velocity *= 0;
            dust.scale = 0.97f;

            float t = (float)Main.time * 0.1f;
            dust.position = projectile.Center + new Vector2((float)Math.Sin(2 * t) / 2f, -(float)Math.Cos(2 * t) / 3f) * 100f;

            
        }
    }
}

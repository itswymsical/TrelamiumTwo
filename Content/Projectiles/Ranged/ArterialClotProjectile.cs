using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
    public class ArterialClotProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            
            projectile.timeLeft = 200;

            projectile.penetrate = 3;
            projectile.aiStyle = -1;
            projectile.width = 40;
            projectile.height = 28;

            projectile.friendly = projectile.ranged = projectile.tileCollide = true;
            
        }
        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;
            projectile.velocity.X *= 0.994f; 
            projectile.velocity.Y += 0.15f;

            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Blood);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Blood);
            Main.dust[dust].scale = 1.23f;
            Main.PlaySound(SoundID.NPCKilled, -1, -1, 12, 0.45f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 220);
        }
    }
}

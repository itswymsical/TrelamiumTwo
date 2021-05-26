using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
    public class HorizonWalkerProjectile : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Horizon Walker Projectile");
   
        public override void SetDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 3.5f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 300f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 13f;

            projectile.extraUpdates = 0;
            projectile.width = projectile.height = 18;
            
            projectile.aiStyle = 99;
            projectile.friendly = projectile.melee = true;
            projectile.penetrate = -1;
            
            projectile.scale = 1f;
        }

        int OnHit = 0;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {           
            OnHit++;
            if(OnHit == 5)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-5, 12) * .25f, Main.rand.Next(-6, 10) * .25f, ModContent.ProjectileType<LightningProj>(), (int)(projectile.damage * .5f), 0, projectile.owner);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-2, 9) * .25f, Main.rand.Next(-3, 5) * .25f, ModContent.ProjectileType<LightningProj>(), (int)(projectile.damage * .5f), 0, projectile.owner);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-1, 8) * .25f, Main.rand.Next(-5, 10) * .25f, ModContent.ProjectileType<LightningProj>(), (int)(projectile.damage * .5f), 0, projectile.owner);
                Main.PlaySound(SoundID.Item93, -1, -1);
                for (double i = 0; i < 6.28; i += 0.1)
                {
                    Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.WitherLightning, new Vector2((float)Math.Sin(i) * 1.3f, (float)Math.Cos(i)) * 1.4f);
                    dust.noGravity = true;
                }

                OnHit = 0;
            }

        }
    }
}

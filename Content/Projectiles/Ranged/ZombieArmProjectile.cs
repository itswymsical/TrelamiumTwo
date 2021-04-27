using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
    public class ZombieArmProjectile : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Zombie Arm");

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.penetrate = 2;
            projectile.aiStyle = -1;
            projectile.timeLeft = 260;
            projectile.ranged = true;
            projectile.friendly = true;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = height = 10;
            return true;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return projHitbox.Intersects(targetHitbox);
        }
        public bool isStickingToTarget
        {
            get { return projectile.ai[0] == 1f; }
            set { projectile.ai[0] = value ? 1f : 0f; }
        }
        public float targetWhoAmI
        {
            get { return projectile.ai[1]; }
            set { projectile.ai[1] = value; }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
        ref int hitDirection)
        {
            isStickingToTarget = true;
            targetWhoAmI = (float)target.whoAmI;
            projectile.velocity =
                (target.Center - projectile.Center) *
                0.55f;
            projectile.netUpdate = true;
            int maxStickingJavelins = 3;
            projectile.damage = 1;
            Point[] stickingJavelins = new Point[maxStickingJavelins];
            int javelinIndex = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != projectile.whoAmI
                    && currentProjectile.active
                    && currentProjectile.owner == Main.myPlayer
                    && currentProjectile.type == projectile.type
                    && currentProjectile.ai[0] == 1f
                    && currentProjectile.ai[1] == (float)target.whoAmI
                )
                {
                    stickingJavelins[javelinIndex++] =
                        new Point(i, currentProjectile.timeLeft);
                    if (javelinIndex >= stickingJavelins.Length
                    )
                    {
                        break;
                    }
                }
            }
            if (javelinIndex >= stickingJavelins.Length)
            {
                int oldJavelinIndex = 0;
                for (int i = 1; i < stickingJavelins.Length; i++)
                {
                    if (stickingJavelins[i].Y < stickingJavelins[oldJavelinIndex].Y)
                    {
                        oldJavelinIndex = i;
                    }
                }
                Main.projectile[stickingJavelins[oldJavelinIndex].X].Kill();
            }
        }
        private const float maxTicks = 45f;
        private const int alphaReduction = 25;
        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= alphaReduction;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (!isStickingToTarget)
            {
                targetWhoAmI += 1f;
                if (targetWhoAmI >= maxTicks)
                {
                    float velXmult = 0.98f;
                    float
                        velYmult = 0.35f;
                    targetWhoAmI = maxTicks;
                    projectile.velocity.X = projectile.velocity.X * velXmult;
                    projectile.velocity.Y = projectile.velocity.Y + velYmult;
                }
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            }
            if (isStickingToTarget)
            {
                projectile.ignoreWater = true;
                projectile.tileCollide = false;
                int aiFactor = 15;
                bool killProj = false;
                bool hitEffect = false;
                projectile.localAI[0] += 1f;
                hitEffect = projectile.localAI[0] % 30f == 0f;
                int projTargetIndex = (int)targetWhoAmI;
                if (projectile.localAI[0] >= (float)(60 * aiFactor)
                    || (projTargetIndex < 0 || projTargetIndex >= 200))
                {
                    killProj = true;
                }
                else if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage)
                {
                    projectile.Center = Main.npc[projTargetIndex].Center - projectile.velocity * 2f;
                    projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
                    if (hitEffect)
                    {
                        Main.npc[projTargetIndex].HitEffect(0, 1.0);
                    }
                }
                else
                {
                    killProj = true;
                }

                if (killProj)
                {
                    projectile.Kill();
                }
            }
        }
       
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCHit8, -1, -1);
            Main.PlaySound(SoundID.NPCDeath11, -1, -1);
            int num281 = 22;
            for (int num282 = 0; num282 < num281; num282++)
            {
                int num283 = Dust.NewDust(projectile.Center, 0, 0, 5, 0f, 0f, 0, default(Color), 0.5f);
                Dust dust = Main.dust[num283];
                dust.velocity *= 1.6f;
                Dust dust25 = Main.dust[num283];
                dust25.velocity.Y = dust25.velocity.Y - 1f;
                Main.dust[num283].position = Vector2.Lerp(Main.dust[num283].position, projectile.Center, 0.5f);
            }
            if (Main.myPlayer == projectile.owner)
            {
                int num116 = Main.rand.Next(3, 6);
                for (int num117 = 0; num117 < num116; num117++)
                {
                    Vector2 vector8 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    while (vector8.X == 0f && vector8.Y == 0f)
                    {
                        vector8 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    }
                    vector8.Normalize();
                    vector8 *= (float)Main.rand.Next(70, 101) * 0.1f;
                }
            }
        }
    }
}
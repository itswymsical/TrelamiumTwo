using Terraria;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.NPCs.Boss.Fungore.Projectiles
{
    public class FungoreSlam : ModProjectile
    {
        public override string Texture => Assets.NPCs.Fungore + "FungoreSlam";
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.width = 244;
            projectile.height = 208;
            projectile.hostile = true;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }

            if (projectile.frame > 5)
            {
                projectile.Kill();
                projectile.frame = 0;
            }
        }
    }
}
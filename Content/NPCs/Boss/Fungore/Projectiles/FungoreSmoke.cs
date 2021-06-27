using Terraria;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.NPCs.Boss.Fungore.Projectiles
{
    public class FungoreSmoke : ModProjectile
    {
        public override string Texture => Assets.NPCs.Fungore + "FungoreSmoke";
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 172;
            projectile.height = 104;
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

            if (projectile.frame > 3)
            {
                projectile.Kill();
                projectile.frame = 0;
            }
        }
    }
}
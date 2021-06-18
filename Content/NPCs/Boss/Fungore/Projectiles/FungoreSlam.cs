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
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 172;
            projectile.height = 104;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 80;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frame > 4)
            {
                projectile.frame = 0;
            }
        }
    }
}
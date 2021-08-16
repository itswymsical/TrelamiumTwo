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
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 244;
            Projectile.height = 208;
            Projectile.hostile = true;
            // projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }

            if (Projectile.frame > 5)
            {
                Projectile.Kill();
                Projectile.frame = 0;
            }
        }
    }
}
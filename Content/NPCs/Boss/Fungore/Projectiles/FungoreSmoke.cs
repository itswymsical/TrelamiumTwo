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
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 172;
            Projectile.height = 104;
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

            if (Projectile.frame > 3)
            {
                Projectile.Kill();
                Projectile.frame = 0;
            }
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
    public class DemoniteKunaiProjectile : ModProjectile
    {
        public override string Texture => "TrelamiumTwo/Content/Items/Weapons/Ranged/DemoniteKunai";
        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.timeLeft = 600;

            projectile.penetrate = 3;
            projectile.aiStyle = 1;
            projectile.width = 26;
            projectile.height = 13;

            projectile.friendly = true;
            projectile.ranged = true;
        }

        private bool Rotation = false;

        public override void AI()
        {
            projectile.velocity.X *= 0.994f;  projectile.velocity.Y += 0.55f;
            

            if (!Rotation){
                projectile.rotation = projectile.DirectionTo(Main.MouseWorld).ToRotation() - MathHelper.PiOver2;
                Rotation = true;
            }

            if (Main.rand.Next(5) == 0){
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PurpleCrystalShard);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PurpleCrystalShard);
            Main.dust[dust].scale = 1.23f;
            Main.PlaySound(SoundID.Dig, projectile.position);
        }


    }
}

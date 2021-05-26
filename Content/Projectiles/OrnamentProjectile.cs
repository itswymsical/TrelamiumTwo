using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Projectiles
{
    public class OrnamentProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gemstone");
            Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.friendly = true;
            projectile.aiStyle = 1;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[0] == 1)
            {
                projectile.frame = Main.rand.Next(0, 5);    
            }

        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Staff_3"), 0.75f, 0.75f);
            Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
            float num = (float)Main.rand.Next(7, 13);
            Vector2 value = new Vector2(2.1f, 2f);
            Color newColor = Main.DiscoColor;
            newColor.A = 255;
            for (float num2 = 0f; num2 < num; num2 += 1f)
            {
                int num3 = Dust.NewDust(projectile.Center, 0, 0, 267, 0f, 0f, 0, newColor, 1f);
                Main.dust[num3].position = projectile.Center;
                Main.dust[num3].velocity = spinningpoint.RotatedBy((double)(6.28318548f * num2 / num), default(Vector2)) * value * (0.8f + Main.rand.NextFloat() * 0.4f);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].scale = 2f;
                Main.dust[num3].fadeIn = Main.rand.NextFloat() * 2f;
                if (num3 != 6000)
                {
                    Dust expr_141 = Dust.CloneDust(num3);
                    expr_141.scale /= 2f;
                    expr_141.fadeIn /= 2f;
                    expr_141.color = new Color(255, 255, 255, 255);
                }
            }
        }
    }
}
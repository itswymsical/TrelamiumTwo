using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Trelamium2.Content.Dusts
{
    public class LuminescentDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;

            dust.scale = Main.rand.NextFloat(1f, 1.2f);
            dust.velocity.Y = -Main.rand.NextFloat(-2f, 2f);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X / 2f;

            dust.velocity *= 0.9f;
            dust.scale *= 0.9f;

            if (dust.scale < 0.1f)
                dust.active = false;

            Lighting.AddLight(dust.position, new Vector3(0f, 0.4f, 0.6f) * dust.scale);

            return false;
        }
    }
}
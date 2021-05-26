using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Common.Projectiles
{
    public class TrelamiumGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            ArmorSetPlayer armorSetPlayer = Main.LocalPlayer.GetModPlayer<ArmorSetPlayer>();
            if (armorSetPlayer.frostbarkSet && projectile.melee)
            {
                if (Main.rand.Next(12) == 0)
                    target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 140));
            }
        }
    }
}
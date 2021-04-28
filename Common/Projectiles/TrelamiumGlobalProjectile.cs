#region Using Directives
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
#endregion

namespace TrelamiumTwo.Common.Projectiles
{
    public class TrelamiumGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Players.TrelamiumPlayer tp = Main.LocalPlayer.GetModPlayer<Players.TrelamiumPlayer>();
            if (tp.frostbarkBonus && projectile.melee)
            {
                if (Main.rand.Next(12) == 0)
                    target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 140));
            }
        }
    }
}
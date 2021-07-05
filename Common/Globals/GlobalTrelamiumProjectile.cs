using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Common.Globals
{
    public class GlobalTrelamiumProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            ArmorSetPlayer armorSetPlayer = Main.LocalPlayer.GetModPlayer<ArmorSetPlayer>();
            TrelamiumPlayer trelamiumPlayer = Main.LocalPlayer.GetModPlayer<TrelamiumPlayer>();
            if (armorSetPlayer.vikingSet && projectile.melee)
            {
                if (Main.rand.Next(12) == 0)
                    target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 140));
            }
            if (trelamiumPlayer.scarabIdol)
            {
                if (projectile.minion)
                {
                    if (Main.rand.Next(12) == 0)
                        target.AddBuff(BuffID.PotionSickness, Main.rand.Next(60, 140));
                }
            }
        }
    }
}
using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Players
{
	public class BuffPlayer : ModPlayer
	{
        public bool gleamingNectar;

        public bool solarAura;

        public override void ResetEffects()
		{
            gleamingNectar = false;
            solarAura = false;
		}
        public override void UpdateBadLifeRegen()
        {
            if (gleamingNectar)
            {
                player.lifeRegen += 4;
                player.manaRegen += 4;
            }
            if (solarAura)
            {
                if (player.lavaWet)
                    player.statDefense %= 10;
            }
        }
    }
}

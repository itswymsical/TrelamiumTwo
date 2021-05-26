using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Players
{
	public class MinionPlayer : ModPlayer
	{
		public bool Avalanche;

		public bool BloomBulb;

		public bool floralSpirit;

		public bool mossMonarch;

		public bool pholiotaMinion;
		public override void ResetEffects()
		{
			Avalanche = false;

			BloomBulb = false;

			floralSpirit = false;

			mossMonarch = false;

			pholiotaMinion = false;
		}
	}
}

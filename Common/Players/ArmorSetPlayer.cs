using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Players
{
	public class ArmorSetPlayer : ModPlayer
	{
		public bool WildWarriorSet;

		public bool desertRogueSet;
		public bool EverbloomSet;

		public bool frostbarkSet;

		public bool FrostbiteSet;

		public bool kindledSet;
		public override void ResetEffects()
		{
			WildWarriorSet = false;

			EverbloomSet = false;

			desertRogueSet = false;

			frostbarkSet = false;

			FrostbiteSet = false;

			kindledSet = false;
		}
	}
}

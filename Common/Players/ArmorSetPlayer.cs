using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Players
{
	public class ArmorSetPlayer : ModPlayer
	{
		public bool AntlionSet;

		public bool WildWarriorSet;

		public bool desertRogueSet;

		public bool EverbloomSet;

		public bool vikingSet;

		public bool kindledSet;

        public override void ResetEffects()
		{
			AntlionSet = false;

			WildWarriorSet = false;

			EverbloomSet = false;

			desertRogueSet = false;

			vikingSet = false;

			kindledSet = false;
		}
	}
}

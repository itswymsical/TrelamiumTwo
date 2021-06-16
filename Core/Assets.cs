namespace TrelamiumTwo.Core
{
	/// <summary>
	/// Contains directories for all asset paths, inspired by Starlight River's system.
	/// </summary>
	internal static class Assets
	{
		public const string Directory = "TrelamiumTwo/Assets/";

		public const string PlaceholderTexture = Directory + "PlaceholderTexture";

		public const string Icon = "TrelamiumTwo/icon";

		public const string Dusts = Directory + "Dusts/";

		internal class Items
		{
			public const string Directory = Assets.Directory + "Items/";

			public const string Armor = Directory + "Armor/";

			public const string Materials = Directory + "Materials/";

			public const string Tools = Directory + "Tools/";
            #region Boss
            public const string Boss = Directory + "Boss/";
			public const string Fungore = Boss + "Fungore/";
            #endregion

            #region Weapons
            public const string Weapons = Directory + "Weapons/";
			public const string Nut = Weapons + "Nut/";
			public const string Viking = Weapons + "Viking/";
			public const string Magic = Weapons + "Magic/";
			public const string Summoner = Weapons + "Summoner/";
            #endregion
        }

        internal class NPCs
		{
			public const string Directory = Assets.Directory + "NPCs/";
            #region Boss
            public const string Boss = Directory + "Boss/";
			public const string Fungore = Boss + "Fungore/";
            #endregion

            public const string Critters = Directory + "Critters/";

			public const string Enemies = Directory + "Enemies/";
		}

		internal class Projectiles
		{
			public const string Directory = Assets.Directory + "Projectiles/";

			public const string Typeless = Directory + "Typeless/";

			public const string Melee = Directory + "Melee/";
			public const string Ranged = Directory + "Ranged/";
			public const string Magic = Directory + "Magic/";
			public const string Minions = Directory + "Minions/";
		}

		internal class Tiles
		{
			public const string Directory = Assets.Directory + "Tiles/";

			public const string Ambience = Directory + "Ambience/";

			public const string Bars = Directory + "Bars/";

			public const string Ores = Directory + "Ores/";

			public const string Forest = Directory + "Forest/";
		}
	}
}

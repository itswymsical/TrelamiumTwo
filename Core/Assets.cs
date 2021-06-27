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

			public const string Misc = Directory + "Misc/";

			public const string Accessory = Directory + "Accessory/";

			public const string Materials = Directory + "Materials/";

			public const string Tools = Directory + "Tools/";

			public const string Fish = Directory + "Fish/";

			public const string Shovels = Tools + "Shovels/";

            #region Armor
            public const string Armor = Directory + "Armor/";

			public const string Antlion = Armor + "Antlion/";

			public const string Everbloom = Armor + "Everbloom/";

			public const string VikingArmor = Armor + "Viking/";

			#endregion

			#region Vanity
			public const string Vanity = Directory + "Vanity/";

			public const string Peepo = Vanity + "Peepo/";

			#endregion

			#region Boss
			public const string Boss = Directory + "Boss/";

			public const string Fungore = Boss + "Fungore/";
            #endregion

            #region Weapons
            public const string Weapons = Directory + "Weapons/";

			public const string BloomRose = Weapons + "BloomRose/";

			public const string AntlionW = Weapons + "Antlion/";

			public const string Nut = Weapons + "Nut/";

			public const string Viking = Weapons + "Viking/";

			public const string Melee = Weapons + "Melee/";

			public const string Magic = Weapons + "Magic/";

			public const string Summoner = Weapons + "Summoner/";
			#endregion
		}
		internal class Buffs
		{
			public const string Directory = Assets.Directory + "Buffs/";

			public const string Minions = Directory + "Minions/";

			public const string Potions = Directory + "Potions/";

			public const string Debuffs = Directory + "Debuffs/";

		}
		internal class NPCs
		{
			public const string Directory = Assets.Directory + "NPCs/";

            public const string Boss = Directory + "Boss/";

			public const string Fungore = Boss + "Fungore/";

            public const string Critters = Directory + "Critters/";

			public const string Enemies = Directory + "Enemies/";

			public const string Forest = Enemies + "Forest/";
		}
		internal class Projectiles
		{
			public const string Directory = Assets.Directory + "Projectiles/";

			public const string Typeless = Directory + "Typeless/";

			public const string Melee = Directory + "Melee/";

			public const string Ranged = Directory + "Ranged/";

			public const string Magic = Directory + "Magic/";

			public const string Summon = Directory + "Summon/";
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

namespace TrelamiumTwo
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

			public const string Consumable = Directory + "Consumable/";

			public const string Materials = Directory + "Materials/";

			public const string Tools = Directory + "Tools/";

			public const string Fish = Directory + "Fish/";

			public const string Shovels = Tools + "Shovels/";

			#region Boss
			public const string Boss = Directory + "Boss/";

			public const string Fungore = Boss + "Fungore/";
            #endregion
		}
		internal class Armor
		{
			public const string Directory = Assets.Directory + "Items/Armor/";

			public const string BloomRose = Directory + "BloomRose/";

			public const string Sandcrawler = Directory + "Sandcrawler/";

			public const string Wildlife = Directory + "Wildlife/";

			public const string Viking = Directory + "Viking/";
		}
		internal class Vanity
		{
			public const string Directory = Assets.Directory + "Items/Vanity/";

			public const string Peepo = Directory + "Peepo/";
		}

		internal class Weapons
		{
			public const string Directory = Assets.Directory + "Items/Weapons/";

			public const string BloomRose = Directory + "BloomRose/";

			public const string Sandcrawler = Directory + "Sandcrawler/";

			public const string Brass = Directory + "Brass/";

			public const string Nut = Directory + "Nut/";

			public const string Viking = Directory + "Viking/";

			public const string Melee = Directory + "Melee/";

			public const string Magic = Directory + "Magic/";

			public const string Summon = Directory + "Summon/";
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

			public const string Desert = Enemies + "Desert/";

			public const string Underground = Enemies + "Underground/";
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

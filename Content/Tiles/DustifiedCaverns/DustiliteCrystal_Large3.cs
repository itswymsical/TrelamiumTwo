#region Using directives

using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

#endregion

namespace TrelamiumTwo.Content.Tiles.DustifiedCaverns
{
	public sealed class DustiliteCrystal_Large3 : DustiliteCrystal_Base
	{
		public override void SafeSetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 18 };
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 0);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 2);
			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);
		}
	}

	// TODO: Eldrazi - remove temp item.
	internal sealed class DustiliteCrystal3_Item : ModItem
	{
		public override string Texture => "TrelamiumTwo/Content/Tiles/DustifiedCaverns/DustiliteCrystal_Large1";

		public override void SetDefaults()
		{
			item.width = item.height = 16;
			item.maxStack = 999;

			item.useTime = 10;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;

			item.createTile = ModContent.TileType<DustiliteCrystal_Large3>();
		}
	}
}

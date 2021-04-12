#region Using directives

using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

#endregion

namespace TrelamiumTwo.Content.Tiles.DustifiedCaverns
{
	public sealed class DustiliteCrystal_Medium2 : DustiliteCrystal_Base
	{
		public override void SafeSetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 3, 0);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);
		}
	}

	// TODO: Eldrazi - remove temp items.
	internal sealed class DustiliteCrystal_Medium2_Item : ModItem
	{
		public override string Texture => "TrelamiumTwo/Content/Tiles/DustifiedCaverns/DustiliteCrystal_Medium2";

		public override void SetDefaults()
		{
			item.width = item.height = 16;
			item.maxStack = 999;

			item.useTime = 10;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;

			item.createTile = ModContent.TileType<DustiliteCrystal_Medium2>();
		}
	}
}

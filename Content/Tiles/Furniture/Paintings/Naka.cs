using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TrelamiumTwo.Content.Tiles.Furniture.Paintings
{
	public class NakaTile : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = Assets.Tiles.Paintings + "NakaTile";

			return Mod.Properties.Autoload;
		}
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);

			TileObjectData.newTile.AnchorBottom = default;
			TileObjectData.newTile.AnchorTop = default;
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.addTile(Type);

			disableSmartCursor = true;
			dustType -= 1;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Painting");
			AddMapEntry(new Color(150, 150, 150), name);
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<Naka>());
	}
	public class Naka : ModItem
	{
		public override string Texture => Assets.Tiles.Paintings + "Naka";
		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.White;
			Item.maxStack = 999;

			Item.width = Item.height = 24;
			Item.useAnimation = Item.useTime = 18;

			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn =
				Item.autoReuse =
				Item.consumable = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.createTile = ModContent.TileType<NakaTile>();
		}
	}
}
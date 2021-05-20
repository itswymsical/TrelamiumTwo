using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

using Microsoft.Xna.Framework;

using TrelamiumTwo.Core.Loaders;
using TrelamiumTwo.Content.UI.ArchaeologistUI;

namespace TrelamiumTwo.Content.Tiles.Stationary
{
	public sealed class ArchaeologistsWorkshopTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileFrameImportant[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<ArchaeologistsWorkshopTileEntity>().Hook_AfterPlacement, -1, 0, true);
			TileObjectData.addTile(Type);
			
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Archeologist's Workshop");
			AddMapEntry(new Color(230, 111, 85), name);
			
			dustType = DustID.t_LivingWood;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
			=> num = fail ? 1 : 3; 

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, ModContent.ItemType<Items.Placeable.ArcheologistsWorkshop>());
			ModContent.GetInstance<ArchaeologistsWorkshopTileEntity>().Kill(i, j);
		}

		public override bool HasSmartInteract()
			=> true;

		public override bool NewRightClick(int i, int j)
		{
			if (UILoader.GetUIState<ArchaeologistsWorkshopUI>().Visible)
			{
				UILoader.GetUIState<ArchaeologistsWorkshopUI>().Deactivate();
			}
			else
			{
				Main.playerInventory = true;
				UILoader.GetUIState<ArchaeologistsWorkshopUI>().Activate();
			}

			return (true);
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = ModContent.ItemType<Items.Placeable.ArcheologistsWorkshop>();
		}
	}
}

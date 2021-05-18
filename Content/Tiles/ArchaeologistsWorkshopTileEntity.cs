using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using TrelamiumTwo.Core.Loaders;
using TrelamiumTwo.Content.UI.ArchaeologistUI;

namespace TrelamiumTwo.Content.Tiles
{
	public sealed class ArchaeologistsWorkshopTileEntity : ModTileEntity
	{
		public override void Update()
		{
			Vector2 pos = new Vector2(Position.ToWorldCoordinates().X + 16, Position.ToWorldCoordinates().Y);
			Player player = Main.LocalPlayer;
			if ((player.Distance(pos) >= 104 && player.Distance(pos) <= 107) || !Main.playerInventory) 
			{
				UILoader.GetUIState<ArchaeologistsWorkshopUI>().Deactivate();
			}
		}

		public override bool ValidTile(int i, int j)
			=> Main.tile[i, j].active() && Main.tile[i, j].type == ModContent.TileType<ArchaeologistsWorkshopTile>();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
				NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
				return -1;
			}

			return Place(i, j);
		}
	}
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using TrelamiumTwo.Content.Items.Materials;

namespace TrelamiumTwo.Common.Tiles
{
	public class TrelamiumTile : GlobalTile
	{
		private const float nutSpawnChance = 0.25f;
		private const float leafSpawnChance = 0.1f;

		public override void RandomUpdate(int i, int j, int type)
		{
			if (Main.tile[i, j].nactive())
			{
				if ((type == TileID.Grass || type == TileID.HallowedGrass) && Main.rand.Next(62) == 0)
				{
					TryBloomRose(i, j);
				}
			}
		}

		public override void FloorVisuals(int type, Player player)
		{
			player.GetModPlayer<Players.TrelamiumPlayer>().onSand =
				(TileID.Sets.Conversion.Sand[type] || TileID.Sets.Conversion.Sandstone[type] || TileID.Sets.Conversion.HardenedSand[type]);
		}

		private void TryBloomRose(int genX, int genY)
		{
			int scatteredFlowerRadiusCheck = 5;
			int scatteredFlowerType = ModContent.TileType<Content.Tiles.Ambience.BloomRose>();

			if (genX < 95 || genX > Main.maxTilesX - 95 || genY < 95 || genY > Main.worldSurface)
			{
				return;
			}

			Tile topTile = Framing.GetTileSafely(genX, genY - 1);
			if (topTile.active() && topTile.type != scatteredFlowerType)
			{
				return;
			}

			int minX = Utils.Clamp(genX - scatteredFlowerRadiusCheck, 1, Main.maxTilesX - 2);
			int maxX = Utils.Clamp(genX + scatteredFlowerRadiusCheck, 1, Main.maxTilesX - 2);

			int minY = Utils.Clamp(genY - scatteredFlowerRadiusCheck, 1, Main.maxTilesY - 2);
			int maxY = Utils.Clamp(genY + scatteredFlowerRadiusCheck, 1, Main.maxTilesY - 2);

			for (int x = minX; x < maxX; ++x)
			{
				for (int y = minY; y < maxY; ++y)
				{
					Tile t = Framing.GetTileSafely(x, y);

					if (t.active() && t.type == scatteredFlowerType)
					{
						return;
					}
				}
			}

			WorldGen.PlaceTile(genX, genY - 1, scatteredFlowerType, true);
		}

		public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (!effectOnly)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient && type == TileID.Trees)
				{
					TrySpawnLeaf(i, j);
					TrySpawnNut(i, j, fail);
				}
			}
		}

		private void TrySpawnNut(int x, int y, bool fail)
		{
			if (fail || Main.rand.NextFloat() > nutSpawnChance)
			{
				return;
			}
			while (y > 10 && Main.tile[x, y].active() && Main.tile[x, y].type == TileID.Trees)
			{
				y--;
			}
			y++;
			if (!IsTileALeafyTreeTop(x, y) || Collision.SolidTiles(x - 2, x + 2, y - 2, y + 2))
			{
				return;
			}

			Item.NewItem(new Vector2(x * 16, y * 16), ModContent.ItemType<Nut>());
		}

		private void TrySpawnLeaf(int x, int y)
		{
			if (Main.rand.NextFloat() > leafSpawnChance)
			{
				return;
			}
			while (y > 10 && Main.tile[x, y].active() && Main.tile[x, y].type == TileID.Trees)
			{
				y--;
			}
			y++;

			if (!IsTileALeafyTreeTop(x, y) || Collision.SolidTiles(x - 2, x + 2, y - 2, y + 2))
			{
				return;
			}

			int velocityXDir = Main.rand.Next(2) * 2 - 1;
			int projectileType = ModContent.ProjectileType<Content.Projectiles.Typeless.FallingLeaf>();
			Projectile.NewProjectile(x * 16, y * 16, Main.rand.NextFloat(2f) * velocityXDir, 0f, projectileType, 0, 0f, Player.FindClosest(new Vector2(x * 16, y * 16), 16, 16));
		}

		private bool IsTileALeafyTreeTop(int i, int j)
		{
			Tile tileSafely = Framing.GetTileSafely(i, j);
			if (tileSafely.active() && tileSafely.type == TileID.Trees)
			{
				if (tileSafely.frameX == 22 && tileSafely.frameY >= 198 && tileSafely.frameY <= 242)
				{
					return true;
				}
			}
			return false;
		}
	}
}

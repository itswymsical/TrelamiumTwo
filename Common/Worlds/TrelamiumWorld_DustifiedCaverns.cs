#region Using directives

using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

using Microsoft.Xna.Framework;

using TrelamiumTwo.Content.Tiles.DustifiedCaverns;

#endregion

namespace TrelamiumTwo.Common.Worlds
{
	public sealed partial class TrelamiumWorld : ModWorld
	{
		private readonly int largeCrystalAmount = 5;
		private readonly int mediumCrystalAmount = 25;

		private void ModifyWorldGenTasks_DustifiedCaverns(List<GenPass> tasks, ref float totalWeight)
		{
			int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
			if (shiniesIndex != -1)
			{
				tasks.Insert(shiniesIndex + 1, new PassLegacy("TrelamiumTwo:DustifiedCaverns_Crystals", GenPass_DustifiedCaverns_Crystals));
			}
		}

		private void GenPass_DustifiedCaverns_Crystals(GenerationProgress progress)
		{
			progress.Message = "Growing desert crystals...";

			int genTries = 50;
			int genAmount = largeCrystalAmount + mediumCrystalAmount;

			int minXGen = WorldGen.UndergroundDesertLocation.X;
			int maxXGen = WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width;

			int minYGen = WorldGen.UndergroundDesertLocation.Y;
			int genHeight = WorldGen.UndergroundDesertLocation.Height;

			for (int i = 0; i <= genAmount; ++i)
			{
				int currentTries = genTries;

				while (currentTries >= 0)
				{
					// Generate a large crystal.
					if (i <= largeCrystalAmount)
					{
						if (TryGenerateLargeCrystal(minXGen, maxXGen, minYGen, minYGen + genHeight))
						{
							break;
						}
					}
					// Generate a medium crystal.
					else
					{
						if (TryGenerateMediumCrystal(minXGen, maxXGen, minYGen, minYGen + genHeight))
						{
							break;
						}
					}

					currentTries--;
				}
			}
		}

		private bool TryGenerateLargeCrystal(int minX, int maxX, int minY, int maxY)
		{
			int[] largeCrystalTypes = {
				ModContent.TileType<DustiliteCrystal_Large1>(),
				ModContent.TileType<DustiliteCrystal_Large2>(),
				ModContent.TileType<DustiliteCrystal_Large3>()
			};
			Point origin = new Point(WorldGen.genRand.Next(minX, maxX + 1), minY);

			return TryPlaceDirectionalTile(origin, maxY - minY, WorldGen.genRand.Next(largeCrystalTypes));
		}

		private bool TryGenerateMediumCrystal(int minX, int maxX, int minY, int maxY)
		{
			int[] mediumCrystalTypes = { 
				ModContent.TileType<DustiliteCrystal_Medium1>(),
				ModContent.TileType<DustiliteCrystal_Medium2>(),
				ModContent.TileType<DustiliteCrystal_Medium3>()
			};
			Point origin = new Point(WorldGen.genRand.Next(minX, maxX + 1), minY);

			return TryPlaceDirectionalTile(origin, maxY - minY, WorldGen.genRand.Next(mediumCrystalTypes));
		}

		private bool TryPlaceDirectionalTile(Point origin, int searchHeight, int type)
		{
			int alternate = Main.rand.Next(2);
			TileObjectData tileData = TileObjectData.GetTileData(type, 0, alternate);

			int solidX = tileData.AnchorBottom.checkStart;
			int solidWidth = tileData.AnchorBottom.tileCount;

			bool searchSuccessful = WorldUtils.Find(origin, Searches.Chain(new Searches.Down(searchHeight), new GenCondition[]
			{
				new Core.TrelamiumConditions.IsEmpty().AreaAnd(tileData.Width, tileData.Height),
				new Core.TrelamiumConditions.OffsetIsSolid(solidX, tileData.Height).AreaAnd(solidWidth, 1)
			}), out Point resultPoint);

			if (searchSuccessful)
			{
				WorldGen.PlaceObject(resultPoint.X, resultPoint.Y, type, false, 0, 0, -1, alternate * 2 - 1);

				return (true);
			}

			return (false);
		}

		/* TODO: Eldrazi
		 * Uncomment and modify to test different world gen features.
		 * 
		public override void PostUpdate()
		{
			if (JustPressed(Microsoft.Xna.Framework.Input.Keys.D1))
			{
				int genX = (int)Main.MouseWorld.X / 16;

				int genY = (int)Main.MouseWorld.Y / 16;

				Point point = new Point(genX, genY);
				WorldUtils.Gen(point, new Shapes.Tail(10, new Vector2(10, -10)), new Actions.SetTile(Terraria.ID.TileID.RubyGemspark));
			}
		}
		public static bool JustPressed(Microsoft.Xna.Framework.Input.Keys key)
		{
			return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
		}*/
	}
}

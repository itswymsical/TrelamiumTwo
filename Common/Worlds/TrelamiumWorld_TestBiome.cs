using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Common.Worlds
{
    public partial class TrelamiumWorld : ModWorld
    {
        private void ModifyWorldGenTasks_TestBiome(List<GenPass> tasks)
        {
            int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Grass Wall"));
            if (num != -1)
            {
                tasks.Insert(num + 1, new PassLegacy("DG Tree Generation", delegate (GenerationProgress progress)
                {
                    progress.Message = "Planting Druid's Garden Tree...";
                    Point origin = new Point(Main.spawnTileX - 40, Main.spawnTileY);
                    WorldUtils.Gen(origin, new Shapes.Circle(120, 60), new Actions.SetTile(TileID.Dirt));
                    WorldUtils.Gen(origin, new Shapes.Circle(9, 5), new Actions.Clear());
                }));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Common.Worlds
{
    public class DruidsGarden : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int DruidsGardenIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Stalac"));
            if (DruidsGardenIndex != -1)
            {
                tasks.Insert(DruidsGardenIndex + 1, new PassLegacy("DG Tree Generation", delegate (GenerationProgress progress)
                {
                    progress.Message = "Planting Druid's Garden Tree...";
				}));
            }
        }
        public int Raycast(int x, int y)
        {
            if (x < 2 || x > Main.maxTilesX - 2)
            {
                Mod.Logger.Error("X is dead.");
                return 0;
            }
            if (y < 2 || y > Main.maxTilesY - 2)
            {
                Mod.Logger.Error("Y is not alive");
                return 0;
            }
            while (!Main.tile[x, y].active())
            {
                y++;
            }
            return y;
        }
    }
}
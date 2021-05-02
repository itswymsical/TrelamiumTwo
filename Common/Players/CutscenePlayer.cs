using TrelamiumTwo.Core.Abstracts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core.Loaders;
using TrelamiumTwo.Common.Cutscenes;

namespace TrelamiumTwo.Common.Players
{
	public class CutscenePlayer : ModPlayer
    {
        public Vector2[] CreditsScreenPosition = new Vector2[4];
        public bool RegisteredCreditsPositions;

		public bool PlayCredits;

        public override void ModifyScreenPosition()
		{
			for (int i = 0; i < CutsceneLoader.Cutscenes.Count; i++)
			{
				var cutscene = CutsceneLoader.Cutscenes[i];
				cutscene.ModifyScreenPosition();
			}
		}

        public override void PostUpdateMiscEffects()
        {
            if (!RegisteredCreditsPositions)
            {
                RegisterCreditsPositions();
                RegisteredCreditsPositions = true;
            }
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
			if (CutsceneLoader.GetCutscene<Credits>().Visible)
                foreach (var layer in layers)
                    layer.visible = false;
        }

		public override void UpdateLifeRegen()
		{
			if (CutsceneLoader.GetCutscene<Credits>().Visible)
				player.lifeRegen = 0;
		}

        private void RegisterCreditsPositions()
        {
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);

                    if (tile.type == TileID.CorruptGrass || tile.type == TileID.FleshGrass)
                        CreditsScreenPosition[0] = new Vector2(i, j) * 16f;

                    if (tile.type == TileID.Sunplate)
                        CreditsScreenPosition[1] = new Vector2(i, j) * 16f;

                    CreditsScreenPosition[2] = new Vector2(Main.dungeonX, Main.dungeonY) * 16f;

                    if (tile.type == TileID.LihzahrdAltar)
                        CreditsScreenPosition[3] = new Vector2(i, j) * 16f;
                } 
            }
        }
    }
}

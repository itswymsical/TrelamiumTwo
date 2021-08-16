using TrelamiumTwo.Common.Cutscenes;
using TrelamiumTwo.Core.Loaders;

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Worlds;

namespace TrelamiumTwo.Common.Players
{
	public class CutscenePlayer : ModPlayer
    {
        public Vector2[] CreditsScreenPosition = new Vector2[4];
        public bool RegisteredCreditsPositions;

        public bool PlayCredits;

        public override void ModifyScreenPosition()
        {
            foreach (var cutscene in CutsceneLoader.Cutscenes.Where(c => c.Visible))
                cutscene.ModifyScreenPosition();
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
                Player.lifeRegen = 0;
        }

        public override void OnEnterWorld(Player player)
        {
            if (Main.netMode == NetmodeID.Server && !TrelamiumWorld.initialCutscene)
                CutsceneLoader.GetCutscene<WorldOpenup>().Begin();
        }

        private void RegisterCreditsPositions()
        {
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);

                    if (tile.type == TileID.Emerald || tile.type == TileID.Sapphire)
                        CreditsScreenPosition[0] = new Vector2(i, j) * 16f;

                    if (tile.type == TileID.Cactus)
                        CreditsScreenPosition[1] = new Vector2(i, j) * 16f;

                    CreditsScreenPosition[2] = new Vector2(Main.dungeonX, Main.dungeonY) * 16f;

                    if (tile.type == TileID.JunglePlants)
                        CreditsScreenPosition[3] = new Vector2(i, j) * 16f;
                }
            }
        }
    }
}

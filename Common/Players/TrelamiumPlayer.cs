using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TrelamiumTwo.Common.Players
{
    public sealed partial class TrelamiumPlayer : ModPlayer
    {
        public bool floralSpirit;
        public bool mossMonarch;
        public bool pholiotaMinion;
        public bool toadstoolExplode;
        public float shakeEffects = 0;
        #region Shovel PickTile method
        public void ShovelPickTile(int x, int y)
        {
            int digTile = player.HeldItem.GetGlobalItem<Items.GlobalTrelamiumItem>().digPower;
            for (int i = -1; i < 2; i++)
            {
                int posx = x / 16 + i;
                int posy = y / 16 + i;
                Tile t1 = Main.tile[posx, y / 16];
                Tile t2 = Main.tile[x / 16, posy];
                if (Main.tile[posx, y / 16].type != TileID.DemonAltar && Main.tile[x / 16, posy].type != TileID.DemonAltar
                    && Main.tile[posx, y / 16].type != TileID.Trees && Main.tile[x / 16, posy].type != TileID.Trees
                    && Main.tile[posx, y / 16].type != TileID.PalmTree && Main.tile[x / 16, posy].type != TileID.PalmTree
                    && Main.tile[posx, y / 16].type != TileID.MushroomTrees && Main.tile[x / 16, posy].type != TileID.MushroomTrees
                    && Main.tile[posx, y / 16].type != TileID.ShadowOrbs && Main.tile[x / 16, posy].type != TileID.ShadowOrbs
                    && Main.tile[posx, y / 16].type != TileID.Cactus && Main.tile[x / 16, posy].type != TileID.Cactus)
                {
                    player.PickTile(posx, y / 16, digTile);
                    player.PickTile(x / 16, posy, digTile);
                }
            }
        }
        #endregion
        public override void ResetEffects()
        {
            floralSpirit = false;
            mossMonarch = false;
            pholiotaMinion = false;
            toadstoolExplode = false;
            shakeEffects = 0;
        }
        public override void UpdateDead()
        {
            floralSpirit = false;
            mossMonarch = false;
            pholiotaMinion = false;
            toadstoolExplode = false;
            shakeEffects = 0;
        }
        public override void UpdateBiomes()
        {
        }
        public override void UpdateBiomeVisuals()
        {
        }
        public override void ModifyScreenPosition()
        {
            if (shakeEffects > 0)
            {
                Main.screenPosition.X += Main.rand.NextFloat(-shakeEffects, shakeEffects + 1);
                Main.screenPosition.Y += Main.rand.NextFloat(-shakeEffects, shakeEffects + 1);
            }
        }
    }
}
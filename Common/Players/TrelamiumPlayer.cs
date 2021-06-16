using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

using TrelamiumTwo.Content.Items.Fish;
using TrelamiumTwo.Common.Extensions;

namespace TrelamiumTwo.Common.Players
{
    public partial class TrelamiumPlayer : ModPlayer
    {
        public bool dustrollerSkates;

        public bool gluttonAmulet;

        public bool microlith;

        public bool onSand;

        public bool scarabIdol;

        public float shakeEffects = 0;

        public bool theMagnolia;

        public bool toadstoolExplode;


        #region ShovelPickTile() Method
        public void ShovelPickTile(int x, int y)
        {
            int digTile = player.HeldItem.GetGlobalItem<Items.GlobalTrelamiumItem>().digPower;
            for (int i = -1; i < 2; i++)
            {
                int posx = x / 16 + i;
                int posy = y / 16 + i;
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
            dustrollerSkates = false;

            gluttonAmulet = false;

            microlith = false;

            onSand = false;

            scarabIdol = false;

            shakeEffects = 0;

            theMagnolia = false;

            toadstoolExplode = false;
        }
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (player.ZoneForest() && Main.rand.NextBool(1050 / power))
            {
                caughtType = ModContent.ItemType<Fleurer>();
            }
            if (player.ZoneForest() && Main.rand.NextBool(300 / power))
            {
                caughtType = ModContent.ItemType<Barkfish>();
            }
            if (player.ZoneForest() && Main.rand.NextBool(300 / power))
            {
                caughtType = ModContent.ItemType<ShreemCarp>();
            }
            if (player.ZoneDesert && Main.rand.NextBool(1050 / power))
            {
                caughtType = ModContent.ItemType<UraeusEel>();
            }
            if (player.ZoneDesert && Main.rand.NextBool(300 / power))
            {
                caughtType = ModContent.ItemType<Scaracod>();
            }
            if (player.ZoneDesert && Main.rand.NextBool(300 / power))
            {
                caughtType = ModContent.ItemType<Sunfish>();
            }
        }
        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if (gluttonAmulet)
                healValue = (int)(healValue * 1.33f);          
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
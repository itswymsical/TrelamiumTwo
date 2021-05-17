using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TrelamiumTwo.Common.Worlds;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Common.Players
{
    public sealed partial class TrelamiumPlayer : ModPlayer
    {
        #region A-G
        public bool desertKB;
        public bool dustrollerSkates;
        public bool floralSpirit;
        public bool frostbarkBonus;
        public bool gleamingNectar;
        #endregion
        #region H-P
        public bool kindledSetBonus;
        public bool legionAccessory;
        public bool magicGuantlet;
        public bool mossMonarch;
        public bool onSand;
        public bool pholiotaMinion;
        #endregion
        #region Q-T
        public float shakeEffects = 0;
        public bool solarAura;
        public bool toadstoolExplode;
        #endregion
        #region U-Z
        public bool ZoneDruidsGarden;
        #endregion

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
            #region A-G
            desertKB = false;
            dustrollerSkates = false;
            floralSpirit = false;
            frostbarkBonus = false;
            gleamingNectar = false;
            #endregion
            #region H-P
            kindledSetBonus = false;
            legionAccessory = false;
            magicGuantlet = false;
            mossMonarch = false;
            onSand = false;
            pholiotaMinion = false;
            #endregion
            #region Q-T
            shakeEffects = 0;
            solarAura = false;
            toadstoolExplode = false;
            #endregion
            #region U-Z

            #endregion
        }
        public override void UpdateDead()
        {
            #region A-G
            desertKB = false;
            dustrollerSkates = false;
            floralSpirit = false;
            frostbarkBonus = false;
            gleamingNectar = false;
            #endregion
            #region H-P
            kindledSetBonus = false;
            legionAccessory = false;
            magicGuantlet = false;
            mossMonarch = false;
            onSand = false;
            pholiotaMinion = false;
            #endregion
            #region Q-T
            shakeEffects = 0;
            solarAura = false;
            toadstoolExplode = false;
            #endregion
            #region U-Z
            #endregion
        }
        public override void UpdateBadLifeRegen()
        {
            if (gleamingNectar)
            {
                player.lifeRegen += 4;
                player.manaRegen += 4;
            }
            if (solarAura)
            {
                if (player.lavaWet)
                    player.statDefense %= 10;
            }
        }
    
        public override void UpdateBiomes() 
        {
            ZoneDruidsGarden = TrelamiumWorld.DruidsGardenTiles > 180;
        }

        public override void UpdateBiomeVisuals() 
            => player.ManageSpecialBiomeVisuals("Blizzard", 
            NPC.AnyNPCs(ModContent.NPCType<Content.NPCs.Glacier.Glacier>()), player.Center);
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
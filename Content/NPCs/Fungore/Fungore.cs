using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using TrelamiumTwo.Content.Dusts;
using TrelamiumTwo.Content.Items;
using static Terraria.ModLoader.ModContent;
using TrelamiumTwo.Content.NPCs.Fungore;
using TrelamiumTwo.Common.Extensions;

namespace TrelamiumTwo.Content.NPCs.Fungore
{
    [AutoloadBossHead]
    public class Fungore : ModNPC
    {
        #region AIState
        private enum AIState
        {
            Idle = 0,
            Jump = 1,
            Stomp = 2
        }
        private AIState State
        {
            get => (AIState)npc.ai[0];
            set => npc.ai[0] = (int)value;
        }
        public float AttackTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float JumpTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float StopTimer
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        private int jumpTimer;
        private int jumpRegular;
        private int Timer2;
        #endregion
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fungore");
            Main.npcFrameCount[npc.type] = 7;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 1700;
            npc.defense = 10;
            npc.damage = 16;

            npc.noTileCollide = false;
            npc.boss = true;
            npc.noGravity = false;
            npc.lavaImmune = true;

            npc.width = 90;
            npc.height = 92;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.DD2_OgreHurt;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.sellPrice(gold: 1);
            //bossBag = ItemType<FungoreBag>();
        }
        public override void NPCLoot()
        {
            /*
            int choice = Main.rand.Next(2);
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (choice == 0)
                {
                    Item.NewItem(npc.getRect(), ItemType<Items.Fungore.MycelialWarhammer>());
                }
                else if (choice == 1)
                {
                    Item.NewItem(npc.getRect(), ItemType<Items.Fungore.ToadstoolClusterclot>());
                }
                Item.NewItem(npc.getRect(), ItemType<Items.Fungore.Fungocybin>());
            }
            if (!PrimordialSandsWorld.downedFungore)
            {
                PrimordialSandsWorld.downedFungore = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }*/
        }
        public override void FindFrame(int frameHeight)
        {

            npc.spriteDirection = npc.direction;
            if (++npc.frameCounter > 7)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[npc.type]);
            }

        }
        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            Player target = Main.player[npc.target];
            npc.TargetClosest(true);
            npc.velocity.X = MathHelper.SmoothStep(npc.position.X, target.position.X, 0.1f);

            #region Tile Collision

            if (npc.velocity.Y == 0)
            {
                JumpTimer++;
            }
            else
            {
                JumpTimer = 0;
            }
            // If have been on ground for at least 1.5 seonds, and are hitting wall or there is a hole
            if (JumpTimer >= 90 && (HoleBelow() || (npc.collideX && npc.position.X == npc.oldPosition.X)))
            {
                npc.netUpdate = true;
                npc.velocity.Y = Main.rand.Next(-8, -6);
            }

            if (npc.velocity.Y >= 0f)
            {
                Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY, 1, false, 1);
                //SlopedCollision();
            }
            #endregion

        }
        private bool HoleBelow()
        {
            int tileWidth = (int)Math.Round(npc.width / 16f);
            int tileX = (int)(npc.Center.X / 16f) - tileWidth;
            if (npc.velocity.X > 0f)
            {
                tileX += tileWidth;
            }
            int tileY = (int)((npc.position.Y + npc.height) / 16f);
            for (int y = tileY; y < tileY + 2; y++)
            {
                for (int x = tileX; x < tileX + tileWidth; x++)
                {
                    if (Main.tile[x, y].active())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
   
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}

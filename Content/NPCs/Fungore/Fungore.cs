using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using TrelamiumTwo.Content.Dusts;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using TrelamiumTwo.Utilities;
using TrelamiumTwo.Common.Worlds;
using TrelamiumTwo.Utilities.Extensions;

namespace TrelamiumTwo.Content.NPCs.Fungore
{
    [AutoloadBossHead]
    public class Fungore : ModNPC
    {
        private enum AIState
        {
            Idle,
            Punch,
            Slam
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fungore");
            Main.npcFrameCount[npc.type] = 12;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 1200;
            npc.defense = 10;
            npc.damage = 16;

            npc.noTileCollide = false;
            npc.boss = true;
            npc.noGravity = false;
            npc.lavaImmune = true;

            npc.width = 88;
            npc.height = 100;

            npc.aiStyle = -1;
            aiType = -1;

            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.DD2_OgreHurt;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.sellPrice(gold: 1);
            bossBag = ItemType<Items.Fungore.FungoreBag>();
        }
        int textTimer;
        public override void FindFrame(int frameHeight)
        {
            npc.frame.Width = 100;
            npc.spriteDirection = npc.direction;
            npc.TargetClosest();
            if (State == AIState.Idle)
            {
                if (++npc.frameCounter >= 8)
                {
                    npc.frameCounter = 0;
                    npc.frame.X = 1 * npc.frame.Width;
                    npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * 8);
                }
            }
            if (State == AIState.Punch)
            {
                if (++npc.frameCounter >= 12)
                {
                    npc.frameCounter = 0;
                    npc.frame.X = 0 * npc.frame.Width;
                    npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * 12);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return npc.DrawNPCCentered(spriteBatch, drawColor);
        }
        public override void AI()
        {
            textTimer++;
            AttackTimer++;
            Player target = Main.player[npc.target];
            npc.spriteDirection = npc.direction;
            if (State == AIState.Idle)
            {
                npc.GenericFighterAI();
                if (AttackTimer >= 180)
                {
                    State = AIState.Punch;
                    AttackTimer = 0;
                }
            }

            if(State == AIState.Punch)
            {
                npc.velocity = new Vector2(0);
                if (AttackTimer >= 120)
                {
                    State = AIState.Idle;
                    AttackTimer = 0;
                }
            }
        }
        public override void NPCLoot()
        {

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
                    //Item.NewItem(npc.getRect(), ItemType<Items.Fungore.ToadstoolClusterclot>());
                }
            }
            if (!TrelamiumWorld.downedFungore)
            {
                TrelamiumWorld.downedFungore = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}

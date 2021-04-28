#region Using Directives
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using TrelamiumTwo.Content.Dusts;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
#endregion

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
        int textTimer;
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
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            string header = "-- Fungore --";
            string subheader = "-- The mutated fungus symbiote --";
            spriteBatch.DrawString(Main.fontDeathText, header, new Vector2((float)(Main.screenWidth / 2 + Main.rand.NextFloat(0f, 2f)) - Main.fontDeathText.MeasureString(header).X / 2f, (float)(Main.screenHeight / 10f + Main.rand.NextFloat(0f, 2f))), new Color(Main.rand.Next(100, 255), Main.rand.Next(100, 255), Main.rand.Next(100, 255), 255), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(Main.fontMouseText, subheader, new Vector2((float)(Main.screenWidth / 2 + Main.rand.NextFloat(0f, 2f)) - Main.fontMouseText.MeasureString(subheader).X / 2f, (float)(Main.screenHeight / 10f + Main.rand.NextFloat(58f, 60f))), new Color(Main.rand.Next(100, 255), Main.rand.Next(100, 255), Main.rand.Next(100, 255), 255), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }
        public override void AI()
        {
            textTimer++;
            npc.spriteDirection = npc.direction;
            Player player = Main.player[npc.target];
            if (npc.ai[0] == 0f)
            {
                TileCollision();
                npc.TargetClosest(true);
                Vector2 speed1 = Vector2.Normalize(player.Center - npc.Center) * 2.5f;
                npc.velocity.X = speed1.X;
                Dust.NewDustDirect(npc.Center + new Vector2(0f, 40f), 0, 0, DustType<MushroomDust>(), 0f, 0f, 0, default, 0.5f);
                npc.ai[1]++;
                if (npc.ai[1] >= 8f * 60f)
                {
                    if (player.WithinRange(npc.Center, 6f * 16f)) // 6 tiles                
                        npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                }

                npc.ai[2]++;
                if (npc.ai[2] >= 160f)
                {
                    npc.TargetClosest(true);
                    Vector2 speed2 = (player.Center - npc.Center).SafeNormalize(Vector2.UnitX) * 7.33f;
                    float angle = npc.direction == 1 ? -10f : 10f; // Avoids tMod funnies!!!
                    speed2 = speed2.RotatedBy(MathHelper.ToRadians(angle));
                    int damageScale = 5;
                    if (Main.expertMode)
                    {
                        damageScale = 10;
                    }
                    npc.ai[2] = 0f;
                }
                npc.ai[3]++;
                if (npc.ai[3] >= 80f)
                {
                    Dust.NewDust(npc.Center + new Vector2(0f, 20f), 0, 0, DustType<MushroomDust>(), 0f, 0f, 0, default, 0.75f);
                    npc.ai[3] = 0f;
                }
            }
            else if (npc.ai[0] == 1f)
            {
                //PunchAttack();
                npc.ai[2]++;
                if (npc.ai[2] >= 120f)
                {
                    npc.ai[0] = 0f;
                }
            }
        }
        private bool HoleBelow()
        {
            // Width of npc in tiles
            int tileWidth = (int)Math.Round(npc.width / 16f);
            int tileX = (int)(npc.Center.X / 16f) - tileWidth;
            if (npc.velocity.X > 0f) // If moving to the right
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
        private void TileCollision()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (npc.velocity.Y == 0f)
            {
                jumpTimer++;
                jumpRegular++;
            }
            else
            {
                jumpTimer = 0;
                jumpRegular = 0;
            }
            // If have been on ground for at least 1.5 seonds, and are hitting wall or there is a hole
            if (jumpTimer >= 40 && (HoleBelow() || (npc.collideX && npc.position.X == npc.oldPosition.X)))
            {
                // Jump
                npc.velocity.Y = Main.rand.NextFloat(-10f, -8f);
                npc.netUpdate = true;
            }
            if (jumpRegular >= 140 || (npc.collideX && npc.position.X == npc.oldPosition.X))
            {
                // Jump
                npc.velocity.Y = Main.rand.NextFloat(-10f, -8f);
                npc.netUpdate = true;
            }
            if (npc.velocity.Y >= 0f)
            {
                Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY, 1, false, 1);
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(jumpTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            jumpTimer = reader.ReadInt32();
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}

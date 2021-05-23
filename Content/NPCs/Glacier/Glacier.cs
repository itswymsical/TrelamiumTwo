using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.Glacier
{
    public sealed class Glacier : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacier");

            NPCID.Sets.TrailCacheLength[npc.type] = 6;
            NPCID.Sets.TrailingMode[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noTileCollide = true;

            npc.defense = 16;
            npc.lifeMax = 26000;

            npc.width = 152;
            npc.height = 226;

            npc.aiStyle = -1;
            aiType = -1;

            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(gold: 5);

            npc.HitSound = SoundID.NPCHit18;
            npc.DeathSound = SoundID.NPCDeath13;
            musicPriority = MusicPriority.BossMedium;
        }

        public override void FindFrame(int frameHeight) => npc.spriteDirection = npc.direction;

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = 40;
        }

        public float MoveTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }

        public float AttackTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }

        private Player player;

        private Vector2 offset = new Vector2(Main.rand.Next(4, 6) * (Main.rand.NextBool() ? 1 : -1), Main.rand.Next(4, 6) * (Main.rand.NextBool() ? 1 : -1)) * 64f;

        public override void SendExtraAI(BinaryWriter writer) => writer.WriteVector2(offset);

        public override void ReceiveExtraAI(BinaryReader reader) => offset = reader.ReadVector2();

        public override void AI()
        {
            player = Main.player[npc.target];

            npc.TargetClosest();

            Movement();

            /*
             * TODO: Naka - Attacks
             * 
             * First Attack -
             * 
             * Second Attack -
             * 
             * (I dont know, help :baked:)
             */
        }

        private void Movement()
        {
            var position = player.Center + offset;

            MoveTimer++;

            if (MoveTimer > 200f)
            {
                offset = new Vector2(Main.rand.Next(4, 6) * (Main.rand.NextBool() ? 1 : -1), Main.rand.Next(4, 6) * (Main.rand.NextBool() ? 1 : -1)) * 64f;

                MoveTimer = 0f;
                npc.netUpdate = true;
            }

            /*
            * NOTES (From Naka)
            * 
            * Maybe a alpha fade in/out when Glacier starts to move would be cool?
            * 
            * We would only need to clamp the alpha between 0 and 255
            * 
            * npc.alpha = (int)MathHelper.Clamp(npc.alpha, 0, 255);
            * 
            * And somehow, make it fade out whenever movement starts, and fade in whenever it stops
            */

            float speed = Vector2.Distance(npc.Center, position);
            speed = MathHelper.Clamp(speed, -18f, 18f);

            Move(position, speed);

            npc.velocity.Y += (float)Math.Sin(MoveTimer * 0.05f);
            npc.rotation = Utils.AngleLerp(npc.rotation, npc.velocity.X * 0.01f, 0.2f);
        }

        private void Move(Vector2 position, float speed)
        {
            Vector2 direction = npc.DirectionTo(position);

            Vector2 velocity = direction * speed;

            npc.velocity = Vector2.SmoothStep(npc.velocity, velocity, 0.2f);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;

            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
                npc.DropBossBags();
        }

        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            npc.DrawNPCTrailCentered(spriteBatch, npc.GetAlpha(lightColor), 0.8f, 0.15f);

            return npc.DrawNPCCentered(spriteBatch, npc.GetAlpha(lightColor));
        }*/

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;

            return null;
        }
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Content.NPCs.Boss.Cumulor
{
    public class Cumulor : ModNPC
    {
        private enum States
        {
            Flying
        }
        private States State
        {
            get => (States)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
        }
        private Player target;

        private int frameY;
        private int frameX;
        private float alpha;
        private float alphaTimer;
        public override string Texture => Assets.NPCs.Cumulor + "Cumulor";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cumulor");
        }
        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;

            NPC.width = NPC.height = 190;

            NPC.lifeMax = 16000;
            NPC.defense = 26;
            NPC.damage = 0;

            NPC.knockBackResist = 0f;

            NPC.aiStyle = aiType = -1;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(gold: 1);
        }
        public override void AI() => HandleAll();
        

        public override void FindFrame(int frameHeight) => NPC.spriteDirection = NPC.direction;
        
        private void HandleAll()
        {
            HandleStates();
            HandleAttacks();
        }
        private void HandleAttacks()
        {
            //const int minAttackCooldown = 180;

        }
        private void HandleStates()
        {
            NPC.TargetClosest();

            target = Main.player[NPC.target];

            switch (State)
            {
                case States.Flying:
                    Movement();
                    break;
            }
        }
        private void Move(Vector2 position, float speed)
        {
            Vector2 direction = NPC.DirectionTo(position);

            Vector2 velocity = direction * speed;

            NPC.velocity = Vector2.SmoothStep(NPC.velocity, velocity, 0.2f);
        }
        private void Movement()
        {
            int dist = 200;
            if (target.direction == 1)
                dist = -dist;

            else if (target.direction == 0)
                dist = 200;
            
            NPC.noTileCollide = true;
            var position = new Vector2(target.Center.X - dist, target.Center.Y);
            float speed = Vector2.Distance(NPC.Center, position);
            speed = MathHelper.Clamp(speed, -20f, 20f);
            Move(position, speed);
            NPC.rotation = Utils.AngleLerp(NPC.rotation, NPC.velocity.X * 0.01f, 0.2f);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor) => HandleScreenText(spriteBatch);
        private void HandleScreenText(SpriteBatch spriteBatch)
        {
            var position = new Vector2(Main.screenWidth / 2f, 200f);

            Color color = Color.White * alpha;

            alphaTimer++;

            if (alphaTimer < 180)
            {
                alpha += 0.025f;
            }
            else
            {
                alpha -= 0.025f;
            }

            Helper.DrawText(spriteBatch, position, "- Cumulor, Exiled Prince -", color);
        }
    }
}
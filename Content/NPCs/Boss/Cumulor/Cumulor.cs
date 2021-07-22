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
            get => (States)npc.ai[0];
            set => npc.ai[0] = (int)value;
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
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;

            npc.width = npc.height = 190;

            npc.lifeMax = 16000;
            npc.defense = 26;
            npc.damage = 0;

            npc.knockBackResist = 0f;

            npc.aiStyle = aiType = -1;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(gold: 1);
        }
        public override void AI() => HandleAll();
        

        public override void FindFrame(int frameHeight) => npc.spriteDirection = npc.direction;
        
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
            npc.TargetClosest();

            target = Main.player[npc.target];

            switch (State)
            {
                case States.Flying:
                    Movement();
                    break;
            }
        }
        private void Move(Vector2 position, float speed)
        {
            Vector2 direction = npc.DirectionTo(position);

            Vector2 velocity = direction * speed;

            npc.velocity = Vector2.SmoothStep(npc.velocity, velocity, 0.2f);
        }
        private void Movement()
        {
            int dist = 200;
            if (target.direction == 1)
                dist = -dist;

            else if (target.direction == 0)
                dist = 200;
            
            npc.noTileCollide = true;
            var position = new Vector2(target.Center.X - dist, target.Center.Y);
            float speed = Vector2.Distance(npc.Center, position);
            speed = MathHelper.Clamp(speed, -20f, 20f);
            Move(position, speed);
            npc.rotation = Utils.AngleLerp(npc.rotation, npc.velocity.X * 0.01f, 0.2f);
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
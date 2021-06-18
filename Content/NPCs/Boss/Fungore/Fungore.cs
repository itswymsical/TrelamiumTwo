using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;

using Terraria.ModLoader;
using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Content.NPCs.Boss.Fungore
{
    [AutoloadBossHead]
    public class Fungore : ModNPC
    {
        private enum States
        {
            Walking,
            Punching,
            Jumping
        }

        private States State
        {
            get => (States)npc.ai[0];
            set => npc.ai[0] = (int)value;
        }

        private float AttackCooldown
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }

        private float TimeGrounded
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }

        private Player player;

        private int frameY;
        private int frameX;

        private int punchDirection;

        private Vector2 scale = Vector2.One;

        private float alpha;
        private float alphaTimer;

        public override string Texture => Assets.NPCs.Fungore + "Fungore";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fungore");

            Main.npcFrameCount[npc.type] = 16;
        }

        public override void SetDefaults()
        {
            npc.boss = true;
            npc.lavaImmune = true;

            npc.width = 88;
            npc.height = 90;

            drawOffsetY = 10;

            npc.lifeMax = 2560;
            npc.defense = 14;
            npc.damage = 18;

            npc.knockBackResist = 0f;

            npc.aiStyle = -1;
            aiType = -1;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Fungore");
            musicPriority = MusicPriority.BossMedium;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = State == States.Punching ? punchDirection : npc.direction;

            npc.frame.Width = 138;
            npc.frame.Height = 134;
            npc.frameCounter++;

            int frameRate = 5;

            // Make the actual punch last slightly longer.
            if (State == States.Punching && (frameY == 5 || frameY == 6))
            {
                frameRate = 15;
            }
            if (State == States.Jumping && (frameY == 2 || frameY == 6))
            {
                frameRate = 35;
            }

            if (npc.frameCounter > frameRate)
            {
                frameY++;

                npc.frameCounter = 0;
            }

            frameX = State == States.Walking ? 0 : State == States.Punching ? 1 : State == States.Jumping ? 2 : 3;

            if (State == States.Walking && frameY > 7)
            {
                frameY = 0;
            }
            npc.frame.Y = frameY * frameHeight;
            npc.frame.X = frameX * npc.frame.Width;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage += npc.damage / 4;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
            Item.NewItem(npc.Hitbox, ItemID.Mushroom, Main.rand.Next(2, 7));

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData);
            }

            // Set downed flag to true here.
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                // Normal loot here.
            }
        }

        public override void AI() => HandleAll();

        private void HandleAll()
        {
            HandleStates();
            HandleAttacks();
            HandleDespawn();
            HandleCollision(60, 6f);
            HandleScale();
        }

        private void HandleStates()
        {
            npc.TargetClosest();

            player = Main.player[npc.target];

            switch (State)
            {
                case States.Walking:
                Walk();
                break;

                case States.Punching:
                Punch();
                break;

                case States.Jumping:
                Jump();
                break;
            }
        }

        private void HandleAttacks()
        {
            const int minAttackCooldown = 180;

            AttackCooldown++;

            // Made a attack cooldown to avoid stuff as immediately doing certain attack out of nowhere, etc...
            if (AttackCooldown > minAttackCooldown)
            {
                const float minPunchDistance = 8f * 16f;

                // Punch if the player is close enough to Fungore.
                if (player.Distance(npc.Center) < minPunchDistance)
                {
                    punchDirection = Math.Sign(player.position.X - npc.position.X);

                    frameY = 0; // Make sure to reset the frame. Will cause weird looks if you dont.

                    State = States.Punching;
                    AttackCooldown = 0;
                }
                if (AttackCooldown > minAttackCooldown)
                {
                    if (Main.rand.Next(16) == 0)
                    {
                        frameY = 0; // Make sure to reset the frame. Will cause weird looks if you dont.

                        State = States.Jumping;
                        AttackCooldown = 0;
                    }
                }
            }
        }

        private void HandleDespawn() { }

        private void HandleCollision(int maxTime, float jumpHeight)
        {
            if (npc.velocity.Y == 0f)
            {
                TimeGrounded++;
            }
            else
            {
                TimeGrounded = 0;
            }

            if (TimeGrounded > maxTime && npc.collideX && npc.position.X == npc.oldPosition.X)
            {
                npc.velocity.Y = -jumpHeight;
            }

            Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY);
        }

        private void Walk()
        {
            const float maxSpeed = 2.5f;

            if (npc.velocity.X < -maxSpeed || npc.velocity.X > maxSpeed)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity *= 0.8f;
                }
            }
            else
            {
                if (npc.velocity.X < maxSpeed && npc.direction == 1)
                {
                    npc.velocity.X += 0.07f;
                }

                if (npc.velocity.X > -maxSpeed && npc.direction == -1)
                {
                    npc.velocity.X -= 0.07f;
                }

                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -maxSpeed, maxSpeed);
            }
        }

        private void Punch()
        {
            if (frameY == 4 || frameY == 5)
            {
                npc.velocity.X += punchDirection * 1.25f;
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -6f, 6f);
            }
            else
            {
                npc.velocity.X *= 0.95f;
            }

            if (npc.velocity.X != 0f)
            {
                var scaleMult = Math.Abs(npc.velocity.X) * 0.05f;

                scale.X += scaleMult;

                if (scale.X > 1.25f)
                {
                    scale.X -= scaleMult;
                }
            }

            // Go back to walking after finishing punching or if it collides with a side tile.
            if (frameY > 10 || npc.collideX)
            {
                frameY = 0; // Make sure to reset the frame. Will cause weird looks if you dont.
                State = States.Walking; 
            }
        }

        private void Jump()
        {
            npc.velocity.X = 0;   
            if (frameY > 2 && frameY < 4)
            {
                npc.velocity.Y = Main.rand.NextFloat(-9f, -8f);
                npc.TargetClosest();
                npc.netUpdate = true;
                if (npc.velocity.Y >= 0f)
                {
                    Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY, 1, false, 1);
                }
            }
            if (frameY >= 8 && (npc.collideY || npc.collideX))
            {
                Walk();
                Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().ScreenShakeIntensity = 4f;
                frameY = 0;
                State = States.Walking;
            }

            if (frameY > 11 && (npc.collideY || npc.collideX))
            {
                frameY = 0; 
                State = States.Walking;
            }
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.DrawNPCCenteredWithTexture(Main.npcTexture[npc.type], spriteBatch, drawColor);
            //npc.DrawNPCTrailCenteredWithTexture(Main.npcTexture[npc.type], spriteBatch, drawColor, default, default, 1);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor) => HandleScreenText(spriteBatch);

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;

            return null;
        }
        private void HandleScale()
        {
            Vector2 targetScale = Vector2.One;

            if (scale.Y != targetScale.Y)
            {
                scale.Y = MathHelper.Lerp(scale.Y, targetScale.Y, 0.2f);
            }

            if (scale.X != targetScale.X)
            {
                scale.X = MathHelper.Lerp(scale.X, targetScale.X, 0.2f);
            }
        }

        private void HandleScreenText(SpriteBatch spriteBatch)
        {
            var position = new Vector2(Main.screenWidth / 2f, 200f);
            var position2 = new Vector2(Main.screenWidth / 2f, 250f);
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

            Helper.DrawText(spriteBatch, position, "- Fungore -", color);
            Helper.DrawText(spriteBatch, position2, "The Numbskull Fungus Brute", color, default, .45f);
        }
    }
}
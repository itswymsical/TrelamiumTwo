using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using Terraria;
using Terraria.ID;

using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Boss.Fungore;
using TrelamiumTwo.Content.Projectiles.Melee;
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
            Jumping,
            SuperJumping
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
        private int AITimer;
        private bool flag = false;
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

            npc.width = npc.height = 88;

            drawOffsetY = 8;

            npc.lifeMax = 1840;
            npc.defense = 5;
            npc.damage = 15;

            npc.knockBackResist = 0f;

            npc.aiStyle = -1;
            aiType = -1;

            npc.HitSound = SoundID.DD2_OgreHurt;
            npc.DeathSound = SoundID.DD2_SkeletonDeath;
            bossBag = ModContent.ItemType<FungoreBag>();
            npc.value = Item.buyPrice(gold: 1);
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/FungalFracas");
            //musicPriority = MusicPriority.BossMedium;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = State == States.Punching ? punchDirection : npc.direction;

            npc.frame.Width = 138;
            npc.frame.Height = 134;
            npc.frameCounter++;

            int frameRate = 4;
            // Make the actual punch last slightly longer.
            if (State == States.Punching && (frameY == 5 || frameY == 6))
            {
                frameRate = 15;
            }
            if (State == States.Jumping && frameY == 6)
            {
                frameRate = 35;
            }
            if (State == States.SuperJumping && frameY == 2)
            {
                frameRate = 35;
            }
            if (State == States.SuperJumping && (frameY == 6 || frameY == 7 || frameY == 8))
            {
                frameRate = 25;
            }
            if (State == States.SuperJumping && frameY == 9)
            {
                frameRate = 30;
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
            float damage = npc.damage;
            npc.damage += (int)(damage * .15f);
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
            int choice = Main.rand.Next(2);
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (choice == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Boss.Fungore.MycelialWarhammer>());
                }
                else if (choice == 1)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<ToadstoolClusterclot>());
                }
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

                case States.SuperJumping:
                SuperJump();
                break;
            }
        }
        private void CheckPlatform(Player player) // credits to Spirit Mod :sex:
        {
            bool onplatform = true;
            for (int i = (int)npc.position.X; i < npc.position.X + npc.width; i += npc.height / 2)
            {
                Tile tile = Framing.GetTileSafely(new Point((int)npc.position.X / 16, (int)(npc.position.Y + npc.height + 8) / 16));
                if (!TileID.Sets.Platforms[tile.type])
                    onplatform = false;
            }
            if (onplatform && (npc.Center.Y < player.position.Y - 20))
                npc.noTileCollide = true;
            else
                npc.noTileCollide = false;
        }
        private void HandleAttacks()
        {
            const int minAttackCooldown = 180;
            CheckPlatform(player);
            AttackCooldown++;
            // Made a attack cooldown to avoid stuff as immediately doing certain attack out of nowhere, etc...
            if (AttackCooldown > minAttackCooldown)
            {
                const float minPunchDistance = 120;
                // Punch if the player is close enough to Fungore.
                if (player.Distance(npc.Center) < minPunchDistance && (Main.rand.Next(20) == 0) || (Main.rand.Next(52) == 0))
                {
                    punchDirection = Math.Sign(player.position.X - npc.position.X);

                    frameY = 0; // Make sure to reset the frame. Will cause weird looks if you dont.

                    State = States.Punching;
                    AttackCooldown = 0;
                }
                if (player.Distance(npc.Center) > 220f && (Main.rand.Next(40) == 0) || (Main.rand.Next(70) == 0))
                {
                    frameY = 0;

                    State = States.SuperJumping;
                    AttackCooldown = 0;
                }
            

                if (Main.rand.Next(48) == 0)
                {
                    frameY = 0;

                    State = States.Jumping;
                    AttackCooldown = 0;
                }
            }
        }
        private void HandleDespawn()
        {
            if (!player.active || player.dead)
            {
                flag = true;
                State = States.SuperJumping;
                if (++AITimer == 220)
                {
                    npc.active = false;
                    npc.TargetClosest(false);
                }
            }
        }
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
            const float maxSpeed = 2.7f;

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
            Dust.NewDust(npc.oldPosition, npc.width, npc.height, ModContent.DustType<Dusts.Mushroom>(), npc.oldVelocity.X, npc.oldVelocity.Y, 0, default, 1f);
        }
        private void Leap()
        {
            const float leapVelocity = 8.5f;

            if (npc.velocity.X < -leapVelocity || npc.velocity.X > leapVelocity)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity *= 3f;
                }
            }
            else
            {
                if (!(frameY >= 6 && State == States.SuperJumping))
                {
                    if (npc.velocity.X < leapVelocity && npc.direction == 1)
                    {
                        npc.velocity.X += 2f;
                    }

                    if (npc.velocity.X > -leapVelocity && npc.direction == -1)
                    {
                        npc.velocity.X -= 2f;
                    }
                }
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -leapVelocity, leapVelocity);
            }
        }
        private void Punch()
        {
            if (frameY == 3)
                Main.PlaySound(SoundID.DD2_OgreAttack, npc.position);

            if (frameY == 4 || frameY == 5)
            {
                npc.velocity.X += punchDirection * 1.75f;
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

                if (scale.X > 1.75f)
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
            Walk();
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
            if (frameY == 4)
                Main.PlaySound(SoundID.DD2_OgreAttack, npc.position);              
         
            if (frameY >= 8 && (npc.collideY || npc.collideX))
            {
                Main.PlaySound(SoundID.DD2_OgreGroundPound, npc.position);
                Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().ScreenShakeIntensity = 3f;

                Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y + 40), new Vector2(0), ModContent.ProjectileType<Projectiles.FungoreSmoke>(), npc.damage / 2, 16f, Main.myPlayer);
                for (int i = 0; i < 6; ++i)
                {
                    var index = Projectile.NewProjectile(npc.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 12f, ModContent.ProjectileType<Mushroom>(), (int)(npc.damage * 0.25f), 0.5f);
                    Main.projectile[index].hostile = true;
                }

                frameY = 0;
                State = States.Walking;
            }
            if (frameY > 15 && (npc.collideY || npc.collideX))
            {
                Main.PlaySound(SoundID.DD2_OgreGroundPound, npc.position);
                frameY = 0; 
                State = States.Walking;
            }
        }
        private void SuperJump()
        {
            if (frameY < 4)
            {
                npc.velocity.X = 0;
            }
            else
            {
                Leap();
            }
            if (frameY > 2 && frameY < 4)
            {
                npc.velocity.Y = Main.rand.NextFloat(-13f, -12f);
                npc.TargetClosest();
                npc.netUpdate = true;
                if (npc.velocity.Y >= 0f)
                {
                    Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY, 1, false, 1);
                }
                if (flag)
                {
                    npc.velocity.Y = Main.rand.NextFloat(-20f, -20f);
                }
            }

            if (frameY == 4)
                Main.PlaySound(SoundID.DD2_OgreAttack, npc.position);

            if (frameY >= 8 && (npc.collideY || npc.collideX))
            {
                Main.PlaySound(SoundID.DD2_OgreGroundPound, npc.position);
                Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().ScreenShakeIntensity = 5f;

                Projectile.NewProjectile(npc.position, new Vector2(0), ModContent.ProjectileType<Projectiles.FungoreSlam>(), npc.damage / 2, 16f, Main.myPlayer);
                for (int i = 0; i < 12; ++i)
                {
                    var index = Projectile.NewProjectile(npc.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 12f, ModContent.ProjectileType<Mushroom>(), (int)(npc.damage * 0.25f), 0.5f);
                    Main.projectile[index].hostile = true;
                }

                for (int i = 0; i < 2; ++i)
                {
                    NPC.NewNPC((int)npc.position.X + Main.rand.Next(-i * -20, i * 20), (int)npc.oldPosition.Y, ModContent.NPCType<Enemies.Forest.MushroomSlime>(), 0, i);
                }
                
                frameY = 0;
                State = States.Walking;
            }
            if (frameY > 15 && (npc.collideY || npc.collideX))
            {
                Main.PlaySound(SoundID.DD2_OgreGroundPound, npc.position);
                frameY = 0;
                State = States.Walking;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) => npc.DrawNPCCenteredWithTexture(Main.npcTexture[npc.type], spriteBatch, drawColor);        
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
                scale.Y = MathHelper.Lerp(scale.Y, targetScale.Y, 0.33f);
            }

            if (scale.X != targetScale.X)
            {
                scale.X = MathHelper.Lerp(scale.X, targetScale.X, 0.33f);
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
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/Fungore/FungoreGore" + i));
                }
            }
            
            for (int i = 0; i < 28; i++)
                Dust.NewDust(npc.Center, npc.width, npc.height, ModContent.DustType<Dusts.Mushroom>(), hitDirection, -1f);
        }
    }
}
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.BloodShaman
{
    public class BloodlightShaman : ModNPC
    {
        public override string BossHeadTexture => TrelamiumTwo.PLACEHOLDER_TEXTURE;
        #region AIState
        private enum AIState
        {
            Idle = 0,
            Jump = 1,
            Fly = 2
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
        private float FlyTimer
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        private bool bloodRain;
        private bool bloodShot;
        private bool locust;
        #endregion

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodlight Shaman");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 32; npc.height = 52;

            npc.npcSlots = 8f;
            npc.damage = 27;
            npc.defense = 10;
            npc.life = 2700;
            npc.boss = true;

            for (int a = 0; a < npc.buffImmune.Length; a++)
                npc.buffImmune[a] = true;

            npc.value = Item.buyPrice(gold: 2);
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCHit11;
            npc.knockBackResist = 0f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GruesomeVisage");
        }
        public override void AI()
        {
            npc.lifeRegen++;
            Player target = Main.player[npc.target];

            if (State == AIState.Idle)
            {
                npc.TargetClosest(true);
                npc.velocity.X = MathHelper.SmoothStep(npc.position.X, target.position.X, 0.1f);

                if (++AttackTimer >= 500)
                {
                    npc.netUpdate = true;
                    AttackType();
                    AttackTimer = 0f;
                    State = AIState.Jump;
                }
            }
            else if (State == AIState.Jump)
            {
                npc.TargetClosest(false);
            }

            #region Tile Collision

            if (npc.velocity.Y == 0)
            {
                JumpTimer++;
            }
            else
            {
                JumpTimer = 0;
            }

            // If have been on ground for at least 1 seond(s), and are hitting wall or there is a hole
            if (JumpTimer >= 60 && (HoleBelow() || (npc.collideX && npc.position.X == npc.oldPosition.X)))
            {
                npc.netUpdate = true;
                npc.velocity.Y = Main.rand.Next(-8, -6);
            }

            if (npc.velocity.Y >= 0f)
            {
                Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY, 1, false, 1);
            }

            #endregion
        }
        private bool HoleBelow()
        {
            // Width of npc in tiles
            int tileWidth = (int)Math.Round(npc.width / 16f);

            int tileX = (int)(npc.Center.X / 16f) - tileWidth;
            int tileY = (int)((npc.position.Y + npc.height) / 16f);

            if (npc.velocity.X > 0) // If moving right
            {
                tileX += tileWidth;
            }

            for (int y = tileY; y < tileY + 2; y++)
            {
                for (int x = tileX; x < tileX + tileWidth; x++)
                {
                    if (Main.tile[x, y].active())
                    {
                        return (false);
                    }
                }
            }
            return (true);
        }
        private void AttackType()
        {
            Player player = Main.player[npc.target];
            bool[] attackTypes = new bool[]
            {
                bloodRain,
                bloodShot,
                locust
            };

            for (int i = 0; i < attackTypes.Length; i++)
            {
                attackTypes[i] = false;
            }
            attackTypes[Main.rand.Next(attackTypes.Length)] = true;
            if (Main.rand.Next(attackTypes)) // Just some placeholder assets
            {
                if (bloodRain)
                {
                    for (int b = 0; b < 10; b++)
                    {
                        Projectile.NewProjectile(
                            new Vector2(npc.position.X - Main.rand.Next(25, 50), npc.position.Y - 60),
                            new Vector2(0, 2.5f), ProjectileID.IchorSplash, npc.damage, default, Main.myPlayer);
                    }
                }
                if (locust)
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Bee, 0);

                if (bloodShot)
                    Projectile.NewProjectile(npc.position, npc.velocity, ProjectileID.DeathLaser, npc.damage, default, Main.myPlayer);
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
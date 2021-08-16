using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Summon
{
    public class Funguy : ModProjectile
    {
        public override string Texture => Assets.Projectiles.Summon + "Funguy";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Funguy");

            Main.projFrames[Projectile.type] = 9;
            Main.projPet[Projectile.type] = true;

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            ProjectileID.Sets.Homing[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.minion = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60;

            Projectile.width = Projectile.height = 36;

            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;

            Projectile.aiStyle = -1;
            aiType = -1;
        }

        private NPC Target => Main.npc[(int)Projectile.ai[1]];

        private const float AttackRange = 32f * 16f;

        private const float PlayerRange = 64f * 16f;

        private bool attacking;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.player[Projectile.owner];

            if (player.position.Y < Projectile.position.Y - 200f && Main.rand.NextBool(10))
            {
                Projectile.velocity.Y = -Main.rand.NextFloat(8f, 12f);

                Projectile.netUpdate = true;
            }

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (attacking && Main.rand.NextBool(2))
            {
                target.velocity.Y = -Main.rand.NextFloat(8f, 12f);

                target.netUpdate = true;
                Projectile.netUpdate = true;
            }
        }

        public override void SendExtraAI(BinaryWriter writer) => writer.Write(attacking);

        public override void ReceiveExtraAI(BinaryReader reader) => attacking = reader.ReadBoolean();

        public override bool MinionContactDamage() => true;
        public override bool? CanCutTiles() => false;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<Buffs.Minions.Funguy>());
            }

            if (player.HasBuff(ModContent.BuffType<Buffs.Minions.Funguy>()))
            {
                Projectile.timeLeft = 5;
            }

            if (attacking)
            {
                Attack();
            }
            else
            {
                Idle(player);

                TargetClosest();
            }

            Projectile.velocity.Y += 0.3f;

            if (player.Distance(Projectile.Center) > PlayerRange)
            {
                Projectile.position = player.position;
                Projectile.netUpdate = true;
            }

            RightClickTarget(player);

            Animate();

            Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);
        }

        private void Idle(Player player)
        {
            Vector2 position = player.Center - new Vector2(36f * (Projectile.minionPos + 1f) * player.direction, 0f);

            Move(position, 3f); 
        }

        private void Attack()
        {
            Move(Target.Center, 6f);

            if (!Target.active || Target.Distance(Projectile.Center) > AttackRange)
            {
                attacking = false;
                Projectile.netUpdate = true;
            }
        }

        private void RightClickTarget(Player player)
        {
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];

                if (npc.CanBeChasedBy() && npc.Distance(Projectile.Center) < AttackRange)
                {
                    Projectile.ai[1] = npc.whoAmI;

                    attacking = true;
                    Projectile.netUpdate = true;
                }
            }
        }

        private void TargetClosest()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.CanBeChasedBy() && npc.Distance(Projectile.Center) < AttackRange)
                {
                    Projectile.ai[1] = npc.whoAmI;

                    attacking = true;
                    Projectile.netUpdate = true;
                }
            }
        }

        private void Animate()
        {
            Projectile.spriteDirection = Projectile.direction;

            if (Projectile.velocity.Y > 1f)
            {
                Projectile.frame = 8;
                return;
            }

            if (Projectile.velocity.X != 0f)
            {
                Projectile.frameCounter++;
            }

            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }

            int maxFrame = !attacking ? 3 : 7;
            int minFrame = !attacking ? 0 : 4;

            if (Projectile.frame > maxFrame)
            {
                Projectile.frame = minFrame;
            }
        }

        private void Move(Vector2 position, float maxSpeed)
        {
            int direction = Math.Sign(position.X - Projectile.position.X);

            Projectile.velocity.X += direction * 0.2f;
            Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -maxSpeed, maxSpeed);
        }
    }
}

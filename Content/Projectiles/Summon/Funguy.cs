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

            Main.projFrames[projectile.type] = 9;
            Main.projPet[projectile.type] = true;

            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.friendly = true;
            projectile.minion = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 60;

            projectile.width = projectile.height = 36;

            projectile.minionSlots = 1f;
            projectile.penetrate = -1;

            projectile.aiStyle = -1;
            aiType = -1;
        }

        private NPC Target => Main.npc[(int)projectile.ai[1]];

        private const float AttackRange = 32f * 16f;

        private const float PlayerRange = 64f * 16f;

        private bool attacking;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.player[projectile.owner];

            if (player.position.Y < projectile.position.Y - 200f && Main.rand.NextBool(10))
            {
                projectile.velocity.Y = -Main.rand.NextFloat(8f, 12f);

                projectile.netUpdate = true;
            }

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (attacking && Main.rand.NextBool(2))
            {
                target.velocity.Y = -Main.rand.NextFloat(8f, 12f);

                target.netUpdate = true;
                projectile.netUpdate = true;
            }
        }

        public override void SendExtraAI(BinaryWriter writer) => writer.Write(attacking);

        public override void ReceiveExtraAI(BinaryReader reader) => attacking = reader.ReadBoolean();

        public override bool MinionContactDamage() => true;
        public override bool? CanCutTiles() => false;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<Buffs.Minions.Funguy>());
            }

            if (player.HasBuff(ModContent.BuffType<Buffs.Minions.Funguy>()))
            {
                projectile.timeLeft = 5;
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

            projectile.velocity.Y += 0.3f;

            if (player.Distance(projectile.Center) > PlayerRange)
            {
                projectile.position = player.position;
                projectile.netUpdate = true;
            }

            RightClickTarget(player);

            Animate();

            Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY);
        }

        private void Idle(Player player)
        {
            Vector2 position = player.Center - new Vector2(36f * (projectile.minionPos + 1f) * player.direction, 0f);

            Move(position, 3f); 
        }

        private void Attack()
        {
            Move(Target.Center, 6f);

            if (!Target.active || Target.Distance(projectile.Center) > AttackRange)
            {
                attacking = false;
                projectile.netUpdate = true;
            }
        }

        private void RightClickTarget(Player player)
        {
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];

                if (npc.CanBeChasedBy() && npc.Distance(projectile.Center) < AttackRange)
                {
                    projectile.ai[1] = npc.whoAmI;

                    attacking = true;
                    projectile.netUpdate = true;
                }
            }
        }

        private void TargetClosest()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.CanBeChasedBy() && npc.Distance(projectile.Center) < AttackRange)
                {
                    projectile.ai[1] = npc.whoAmI;

                    attacking = true;
                    projectile.netUpdate = true;
                }
            }
        }

        private void Animate()
        {
            projectile.spriteDirection = projectile.direction;

            if (projectile.velocity.Y > 1f)
            {
                projectile.frame = 8;
                return;
            }

            if (projectile.velocity.X != 0f)
            {
                projectile.frameCounter++;
            }

            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }

            int maxFrame = !attacking ? 3 : 7;
            int minFrame = !attacking ? 0 : 4;

            if (projectile.frame > maxFrame)
            {
                projectile.frame = minFrame;
            }
        }

        private void Move(Vector2 position, float maxSpeed)
        {
            int direction = Math.Sign(position.X - projectile.position.X);

            projectile.velocity.X += direction * 0.2f;
            projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -maxSpeed, maxSpeed);
        }
    }
}

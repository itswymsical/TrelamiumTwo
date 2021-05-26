using TrelamiumTwo.Content.Projectiles.Magic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Summon
{
	public class BloomBulb : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloom Bulb");

			Main.projPet[projectile.type] = true;
			Main.projFrames[projectile.type] = 2;

			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.minion = true;
			projectile.friendly = true;
			projectile.tileCollide = false;

			projectile.width = projectile.height = 20;

			projectile.minionSlots = 1f;
			projectile.penetrate = -1;

			projectile.aiStyle = -1;
			aiType = -1;
		}

		public enum States
		{
			Idle,
			Attack
		}

		public States State 
		{
			get => (States)projectile.ai[0];
			set => projectile.ai[0] = (int)value;
		}

		private NPC Target => Main.npc[(int)projectile.ai[1]];

		public override void AI()
		{
			Player player = Main.player[projectile.owner];

			if (player.dead || !player.active)
				player.ClearBuff(ModContent.BuffType<Buffs.Minion.BloomBulbBuff>()); 

			if (player.HasBuff(ModContent.BuffType<Buffs.Minion.BloomBulbBuff>()))
				projectile.timeLeft = 2;

			float attackRange = 40f * 16f;

			if (State == States.Idle)
			{
				Move(player.Center);

				projectile.rotation = projectile.velocity.X * 0.02f;

				for (int i = 0; i < Main.maxNPCs; i++) 
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy() && npc.Distance(projectile.Center) < attackRange)
					{
						State = States.Attack;
						projectile.ai[1] = npc.whoAmI;
					}
				}
			}
			else if (State == States.Attack)
			{
				Move(Target.Center);

				var angle = Target.Center - projectile.Center;

				projectile.rotation = angle.ToRotation() + MathHelper.PiOver2;

				if (projectile.frame == 1 && projectile.frameCounter == 1)
				{
					Main.PlaySound(SoundID.Item17, projectile.position);

					var speed = Vector2.Normalize(angle) * 10f;

					Projectile proj = Projectile.NewProjectileDirect(projectile.Center, speed, ModContent.ProjectileType<BloomRosePetal>(), projectile.damage, projectile.knockBack, projectile.owner);
					proj.tileCollide = false;
				}

				if (!Target.active || Target.Distance(projectile.Center) > attackRange)
					State = States.Idle;
			}
			
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];

				if (npc.Distance(projectile.Center) < attackRange)
					projectile.ai[1] = npc.whoAmI;				
			}

			projectile.frameCounter++;
			if (projectile.frameCounter >= 30)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
			}

			if (projectile.frame > 1)
				projectile.frame = 0;
		}

		public override bool MinionContactDamage() => true;

		public override bool? CanCutTiles() => false;

		private void Move(Vector2 position)
		{
			int xDirection = Math.Sign(position.X - projectile.Center.X);
			projectile.velocity.X += xDirection * 0.05f;

			int yDirection = Math.Sign(position.Y - projectile.Center.Y);
			projectile.velocity.Y += yDirection * 0.05f;

			projectile.velocity = Vector2.Clamp(projectile.velocity, new Vector2(-4f), new Vector2(4f));
		}
	}
}

using TrelamiumTwo.Content.Projectiles.Magic;
using TrelamiumTwo.Core;

using Microsoft.Xna.Framework;

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace TrelamiumTwo.Content.Projectiles.Summon
{
	public class BloomBulb : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Summon + "BloomBulb";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloom Bulb");

			Main.projPet[Projectile.type] = true;
			Main.projFrames[Projectile.type] = 2;

			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

			ProjectileID.Sets.Homing[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.minion = true;
			Projectile.friendly = true;
			// projectile.tileCollide = false;

			Projectile.width = Projectile.height = 20;

			Projectile.minionSlots = 1f;
			Projectile.penetrate = -1;

			Projectile.aiStyle = -1;
			aiType = -1;
		}

		public enum States
		{
			Idle,
			Attack
		}

		public States State 
		{
			get => (States)Projectile.ai[0];
			set => Projectile.ai[0] = (int)value;
		}

		private NPC Target => Main.npc[(int)Projectile.ai[1]];
        public override bool CanDamage() => false;
        
        public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (player.dead || !player.active)
				player.ClearBuff(ModContent.BuffType<Buffs.Minions.BloomBulb>()); 

			if (player.HasBuff(ModContent.BuffType<Buffs.Minions.BloomBulb>()))
				Projectile.timeLeft = 2;

			float attackRange = 40f * 16f;

			if (State == States.Idle)
			{
				Move(player.Center);

				Projectile.rotation = Projectile.velocity.X * 0.02f;

				for (int i = 0; i < Main.maxNPCs; i++) 
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy() && npc.Distance(Projectile.Center) < attackRange)
					{
						State = States.Attack;
						Projectile.ai[1] = npc.whoAmI;
					}
				}
			}
			else if (State == States.Attack)
			{
				Move(Target.Center);

				var angle = Target.Center - Projectile.Center;

				Projectile.rotation = angle.ToRotation() + MathHelper.PiOver2;

				if (Projectile.frame == 1 && Projectile.frameCounter == 1)
				{
					SoundEngine.PlaySound(SoundID.Item17, Projectile.position);

					var speed = Vector2.Normalize(angle) * 10f;

					Projectile proj = Projectile.NewProjectileDirect(Projectile.Center, speed, ModContent.ProjectileType<BloomRosePetal>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
					// proj.tileCollide = false;
				}

				if (!Target.active || Target.Distance(Projectile.Center) > attackRange)
					State = States.Idle;
			}
			
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];

				if (npc.Distance(Projectile.Center) < attackRange)
					Projectile.ai[1] = npc.whoAmI;				
			}

			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 30)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}

			if (Projectile.frame > 1)
				Projectile.frame = 0;
		}

		public override bool MinionContactDamage() => true;

		public override bool? CanCutTiles() => false;

		private void Move(Vector2 position)
		{
			int xDirection = Math.Sign(position.X - Projectile.Center.X);
			Projectile.velocity.X += xDirection * 0.05f;

			int yDirection = Math.Sign(position.Y - Projectile.Center.Y);
			Projectile.velocity.Y += yDirection * 0.05f;

			Projectile.velocity = Vector2.Clamp(Projectile.velocity, new Vector2(-4f), new Vector2(4f));
		}
	}
}

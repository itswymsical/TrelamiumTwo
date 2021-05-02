using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Common.Extensions;

namespace TrelamiumTwo.Content.Projectiles.Melee // Eldrazi Code imported from EH
{
	public sealed class MycelialWarhammerProjectile : ModProjectile
	{
		private enum AIState
		{
			Spawning,
			Swinging = 2
		}
		AIState State
		{
			get => (AIState)projectile.ai[0];
			set => projectile.ai[0] = (int)value;
		}

		private bool IsMaxCharge;
		private readonly float MaxChargeTime = 45f;

		private float RotationStart => MathHelper.PiOver2 + (projectile.direction == -1 ? MathHelper.Pi : 0);
		private float RotationOffset => projectile.direction == 1 ? 0 : MathHelper.PiOver2;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 54;
			projectile.height = 54;

			projectile.penetrate = -1;

			projectile.melee = true;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.netImportant = true;
			projectile.ownerHitCheck = true;
			projectile.manualDirectionChange = true;

			IsMaxCharge = false;
		}

		public override bool PreAI()
		{
			Player owner = Main.player[projectile.owner];
			if (!owner.active || owner.dead)
			{
				projectile.Kill();
			}

			if (State == AIState.Swinging)
			{
				projectile.ai[1] -= 4;

				if (projectile.ai[1] <= 0)
				{
					projectile.Kill();
				}
			}
			else
			{
				if (++projectile.ai[1] >= MaxChargeTime)
				{
					IsMaxCharge = true;
					projectile.ai[1] = MaxChargeTime;
					Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().shakeEffects = .025f;
				}

				if (Main.myPlayer == projectile.owner && !owner.channel && projectile.ai[1] >= (MaxChargeTime / 2))
				{
					State = AIState.Swinging;
					projectile.netUpdate = true;
				}
			}

			SetProjectilePosition(owner);

			SetOwnerAnimation(owner);

			return (false);
		}

		public override bool CanDamage()
			=> State != AIState.Spawning;

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			if (IsMaxCharge && Main.myPlayer == projectile.owner)
			{
				for (int i = 0; i < 2; ++i)
				{
					Projectile.NewProjectile(projectile.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 4f,
					ModContent.ProjectileType<MushroomProjectile>(), (int)(projectile.damage * 0.5f), 0.5f, projectile.owner);
				}
			}

			Core.Utils.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 0, 60);
		}
        public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			Core.Utils.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 0, 50);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (State == AIState.Swinging)
			{
				this.DrawProjectileTrailCentered(spriteBatch, lightColor, 0.4f, 0.15f);
			}

			return this.DrawProjectileCentered(spriteBatch, lightColor);
		}

		private void SetProjectilePosition(Player owner)
		{
			projectile.spriteDirection = projectile.direction;

			Vector2 rotatedPoint = owner.RotatedRelativePoint(owner.MountedCenter);

			projectile.rotation = RotationStart - (MathHelper.Pi / MaxChargeTime * projectile.ai[1]) * projectile.direction;
			projectile.Center = rotatedPoint + (projectile.rotation - MathHelper.PiOver4 - RotationOffset).ToRotationVector2() * 64;
		}

		private void SetOwnerAnimation(Player owner)
		{
			owner.itemTime = owner.itemAnimation = 10;

			owner.heldProj = projectile.whoAmI;

			float currentAnimationFraction = projectile.ai[1] / MaxChargeTime;

			if (currentAnimationFraction < 0.4f)
			{
				owner.bodyFrame.Y = owner.bodyFrame.Height * 3;
			}
			else if (currentAnimationFraction < 0.75f)
			{
				owner.bodyFrame.Y = owner.bodyFrame.Height * 2;
			}
			else
			{
				owner.bodyFrame.Y = owner.bodyFrame.Height;
			}
		}
	}
}
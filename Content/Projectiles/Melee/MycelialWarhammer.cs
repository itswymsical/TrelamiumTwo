using Terraria;
using Terraria.ID;

using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public class MycelialWarhammer : ModProjectile
	{
		public override string Texture => Assets.Items.Fungore + "MycelialWarhammer";
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
		private readonly float MaxChargeTime = 35f;
		private float RotationStart => MathHelper.PiOver2 + (projectile.direction == -1 ? MathHelper.Pi : 0);
		private float RotationOffset => projectile.direction == 1 ? 0 : MathHelper.PiOver2;
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 74;
			projectile.penetrate = -1;

			projectile.melee =
				projectile.friendly =
				projectile.netImportant =
				projectile.ownerHitCheck =
				projectile.manualDirectionChange = true;

			projectile.tileCollide = IsMaxCharge = false;
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
				}

				if (Main.myPlayer == projectile.owner && !owner.channel && projectile.ai[1] >= (MaxChargeTime / 2))
				{
					State = AIState.Swinging;
					projectile.netUpdate = true;
				}
			}

			SetProjectilePosition(owner);

			SetOwnerAnimation(owner);

			return false;
		}

		public override bool CanDamage() => State != AIState.Spawning;

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);

			Helper.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 0, 60);
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			if (IsMaxCharge && Main.myPlayer == projectile.owner)
			{
				for (int i = 0; i < 3; ++i)
				{
					Projectile.NewProjectile(projectile.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 8f, ModContent.ProjectileType<Mushroom>(), (int)(projectile.damage * 0.5f), 0.5f, projectile.owner);
				}
			}
			Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().ScreenShakeIntensityHammer = .8f;
			Helper.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 0, 50);
		}

		private void SetProjectilePosition(Player owner)
		{
			projectile.spriteDirection = projectile.direction;

			Vector2 rotatedPoint = owner.RotatedRelativePoint(owner.MountedCenter);

			projectile.rotation = RotationStart - (MathHelper.Pi / MaxChargeTime * projectile.ai[1]) * projectile.direction;
			projectile.Center = rotatedPoint + (projectile.rotation - MathHelper.PiOver4 - RotationOffset).ToRotationVector2() * 60;
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
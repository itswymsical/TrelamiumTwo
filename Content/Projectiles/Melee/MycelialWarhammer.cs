using Terraria;
using Terraria.ID;

using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Core;
using Terraria.Audio;

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
			get => (AIState)Projectile.ai[0];
			set => Projectile.ai[0] = (int)value;
		}
		private bool IsMaxCharge;
		private readonly float MaxChargeTime = 35f;
		private float RotationStart => MathHelper.PiOver2 + (Projectile.direction == -1 ? MathHelper.Pi : 0);
		private float RotationOffset => Projectile.direction == 1 ? 0 : MathHelper.PiOver2;
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 74;
			Projectile.penetrate = -1;

			Projectile.DamageType = DamageClass.Melee;
				Projectile.friendly =
				Projectile.netImportant =
				Projectile.ownerHitCheck =
				Projectile.manualDirectionChange = true;

			Projectile.tileCollide = IsMaxCharge = false;
		}
		private Player player;
		public override bool PreAI()
		{
			Player owner = Main.player[Projectile.owner];
			if (!owner.active || owner.dead)
			{
				Projectile.Kill();
			}

			if (State == AIState.Swinging)
			{
				Projectile.ai[1] -= 4;

				if (Projectile.ai[1] <= 0)
				{
					Projectile.Kill();
				}
			}
			else
			{
				if (++Projectile.ai[1] >= MaxChargeTime)
				{
					IsMaxCharge = true;
					Projectile.ai[1] = MaxChargeTime;
				}

				if (Main.myPlayer == Projectile.owner && !owner.channel && Projectile.ai[1] >= (MaxChargeTime / 2))
				{
					State = AIState.Swinging;
					Projectile.netUpdate = true;
				}
			}

			SetProjectilePosition(owner);

			SetOwnerAnimation(owner);

			return false;
		}

		public override bool CanDamage() => State != AIState.Spawning;

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);

			Helper.SpawnDustCloud(Projectile.position, Projectile.width, Projectile.height, 0, 60);
		}
		public override void Kill(int timeLeft)
		{
			player = Main.player[Projectile.owner];
			SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			if (IsMaxCharge && Main.myPlayer == Projectile.owner)
			{
				for (int i = 0; i < 3; ++i)
				{
					Projectile.NewProjectile(Projectile.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 8f, ModContent.ProjectileType<Mushroom>(), (int)(Projectile.damage * 0.5f), 0.5f, Projectile.owner);
				}
			}
			player.GetModPlayer<Common.Players.TrelamiumPlayer>().ScreenShakeIntensity = .7f;
			Helper.SpawnDustCloud(Projectile.position, Projectile.width, Projectile.height, 0, 50);
		}

		private void SetProjectilePosition(Player owner)
		{
			Projectile.spriteDirection = Projectile.direction;

			Vector2 rotatedPoint = owner.RotatedRelativePoint(owner.MountedCenter);

			Projectile.rotation = RotationStart - (MathHelper.Pi / MaxChargeTime * Projectile.ai[1]) * Projectile.direction;
			Projectile.Center = rotatedPoint + (Projectile.rotation - MathHelper.PiOver4 - RotationOffset).ToRotationVector2() * 60;
		}

		private void SetOwnerAnimation(Player owner)
		{
			owner.itemTime = owner.itemAnimation = 10;

			owner.heldProj = Projectile.whoAmI;

			float currentAnimationFraction = Projectile.ai[1] / MaxChargeTime;

			if (currentAnimationFraction < 0.4f)
				owner.bodyFrame.Y = owner.bodyFrame.Height * 3;
			
			else if (currentAnimationFraction < 0.75f)
				owner.bodyFrame.Y = owner.bodyFrame.Height * 2;
			
			else
				owner.bodyFrame.Y = owner.bodyFrame.Height;
			
		}
	}
}
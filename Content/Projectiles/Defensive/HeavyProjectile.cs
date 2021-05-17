using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Common.Extensions;

namespace TrelamiumTwo.Content.Projectiles.Defensive // Eldrazi Code imported from EH
{
	public sealed class AntlionClubProjectile : ModProjectile
	{
        public override string Texture => TrelamiumTwo.HeaviesAssets + "AntlionClub";
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
		private readonly float MaxChargeTime = 40f;

		private float RotationStart => MathHelper.PiOver2 + (projectile.direction == -1 ? MathHelper.Pi : 0);
		private float RotationOffset => projectile.direction == 1 ? 0 : MathHelper.PiOver2;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 44;
			projectile.height = 44;

			projectile.penetrate = -1;

			projectile.melee = true;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.netImportant = true;
			projectile.ownerHitCheck = true;
			projectile.manualDirectionChange = true;
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
					projectile.ai[1] = MaxChargeTime;
					Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().shakeEffects = .015f;
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
			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 32, 60);
		}
        public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 32, 50);
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
			projectile.Center = rotatedPoint + (projectile.rotation - MathHelper.PiOver4 - RotationOffset).ToRotationVector2() * 48;
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
	public sealed class CrustaceanClobbererProjectile : ModProjectile
	{
		public override string Texture => TrelamiumTwo.HeaviesAssets + "CrustaceanClobberer";
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

		//private bool IsMaxCharge;
		private readonly float MaxChargeTime = 60f;

		private float RotationStart => MathHelper.PiOver2 + (projectile.direction == -1 ? MathHelper.Pi : 0);
		private float RotationOffset => projectile.direction == 1 ? 0 : MathHelper.PiOver2;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 74;
			projectile.height = 84;

			projectile.penetrate = -1;

			projectile.melee = true;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.netImportant = true;
			projectile.ownerHitCheck = true;
			projectile.manualDirectionChange = true;

			//IsMaxCharge = false;
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
				projectile.ai[1] -= 5;
				if (projectile.ai[1] <= 0)
				{
					projectile.Kill();
				}
			}
			else
			{
				if (++projectile.ai[1] >= MaxChargeTime)
				{
					//IsMaxCharge = true;
					projectile.ai[1] = MaxChargeTime;
					Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().shakeEffects = .05f;
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
			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 151, 60);
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 151, 50);
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
	public sealed class HeavensGaloreProjectile : ModProjectile
	{
		public override string Texture => TrelamiumTwo.HeaviesAssets + "HeavensGalore";
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
		private readonly float MaxChargeTime = 40f;

		private float RotationStart => MathHelper.PiOver2 + (projectile.direction == -1 ? MathHelper.Pi : 0);
		private float RotationOffset => projectile.direction == 1 ? 0 : MathHelper.PiOver2;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 58;
			projectile.height = 58;

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
				if (IsMaxCharge && Main.myPlayer == projectile.owner)
				{
					for (int i = 0; i < 2; ++i)
					{
						Projectile.NewProjectile(projectile.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 4f,
							ProjectileID.DD2SquireSonicBoom, (int)(projectile.damage * 0.5f), 0.5f, projectile.owner);
					}
				}
			}
			else
			{
				if (++projectile.ai[1] >= MaxChargeTime)
				{
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

			return (false);
		}

		public override bool CanDamage()
			=> State != AIState.Spawning;

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 32, 60);
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 32, 50);
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
			projectile.Center = rotatedPoint + (projectile.rotation - MathHelper.PiOver4 - RotationOffset).ToRotationVector2() * 48;
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
	public sealed class TheLaceratorProjectile : ModProjectile
	{
		public override string Texture => TrelamiumTwo.HeaviesAssets + "TheLacerator";

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
		private readonly float MaxChargeTime = 60f;

		private float RotationStart => MathHelper.PiOver2 + (projectile.direction == -1 ? MathHelper.Pi : 0);
		private float RotationOffset => projectile.direction == 1 ? 0 : MathHelper.PiOver2;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 74;

			projectile.penetrate = -1;

			projectile.melee = projectile.friendly = projectile.netImportant = projectile.ownerHitCheck = projectile.manualDirectionChange = true;

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
					Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().shakeEffects = .05f;
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
			target.AddBuff(BuffID.Bleeding, 240);
			if (IsMaxCharge && Main.myPlayer == projectile.owner)
			{
				for (int i = 0; i < 2; ++i)
				{
					/*Projectile.NewProjectile(projectile.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 4f,
						ProjectileID.BloodWater, (int)(projectile.damage * 0.5f), 0.5f, projectile.owner);*/
				}
			}

			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 5, 60);
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 5, 50);
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
			projectile.Center = rotatedPoint + (projectile.rotation - MathHelper.PiOver4 - RotationOffset).ToRotationVector2() * 56;
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
	public sealed class MycelialWarhammerProjectile : ModProjectile
	{
		public override string Texture => TrelamiumTwo.HeaviesAssets + "MycelialWarhammer";
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

			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 0, 60);
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			if (IsMaxCharge && Main.myPlayer == projectile.owner)
			{
				for (int i = 0; i < 3; ++i)
				{
					Projectile.NewProjectile(projectile.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 8f,
					ModContent.ProjectileType<MushroomProjectile>(), (int)(projectile.damage * 0.5f), 0.5f, projectile.owner);
				}
			}
			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 0, 50);
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
			projectile.Center = rotatedPoint + (projectile.rotation - MathHelper.PiOver4 - RotationOffset).ToRotationVector2() * 48;
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
	public sealed class PrimordialEarthProjectile : ModProjectile
	{
		public override string Texture => TrelamiumTwo.HeaviesAssets + "PrimordialEarth";
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
		private readonly float MaxChargeTime = 65f;

		private float RotationStart => MathHelper.PiOver2 + (projectile.direction == -1 ? MathHelper.Pi : 0);
		private float RotationOffset => projectile.direction == 1 ? 0 : MathHelper.PiOver2;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 60;

			projectile.penetrate = -1;

			projectile.melee = projectile.friendly = projectile.netImportant = projectile.ownerHitCheck = projectile.manualDirectionChange = true;

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
						ProjectileID.DirtBall, (int)(projectile.damage * 0.5f), 0.5f, projectile.owner);
				}
			}

			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 0, 60);
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, -1, -1);
			Utilities.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 0, 50);
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
			projectile.Center = rotatedPoint + (projectile.rotation - MathHelper.PiOver4 - RotationOffset).ToRotationVector2() * 48;
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

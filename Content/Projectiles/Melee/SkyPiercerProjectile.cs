using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public class SkyPiercerProjectile : ModProjectile
	{
		public float MovementFactor
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}
		public override void SetStaticDefaults() => DisplayName.SetDefault("Sky Piercer");

		public override void SetDefaults()
		{
			projectile.width = projectile.height = 10;

			projectile.aiStyle = 19;
			projectile.scale = 1.1f;
			projectile.penetrate = -1;

			projectile.hide = projectile.friendly = projectile.melee = projectile.ownerHitCheck = true;

			projectile.tileCollide = false;

			projectile.width = projectile.height = 10;

		}

		int Timer = 0;
		public override void AI()
		{
			Player projOwner = Main.player[projectile.owner];
			Vector2 ownerCenter = projOwner.RotatedRelativePoint(projOwner.Center, true);
			projectile.direction = projOwner.direction;
			projOwner.heldProj = projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			projectile.position = ownerCenter - projectile.Size / 2;

			if (!projOwner.frozen)
			{
				if (MovementFactor == 0f)
				{
					MovementFactor = 3f;
					projectile.netUpdate = true;
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
				{
					MovementFactor -= 3f;
				}
				else
				{
					MovementFactor += 2f;
				}
			}

			projectile.position += projectile.velocity * MovementFactor;
			if (projOwner.itemAnimation == 0)
			{
				projectile.Kill();
			}

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);

			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= MathHelper.ToRadians(90f);
			}

			Timer++;
			if (Timer >= 7)
            {
				Player player = Main.player[projectile.owner];
				if (player.direction == -1)
				{
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-10, 3) * .25f, Main.rand.Next(-10, 5) * .25f, ModContent.ProjectileType<LightningProjectile>(), (int)(projectile.damage * .5f), 0, projectile.owner);
					Main.PlaySound(SoundID.Item93, -1, -1);
				}
				else if(player.direction == 1)
				{
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-3, 10) * .25f, Main.rand.Next(-5, 10) * .25f, ModContent.ProjectileType<LightningProjectile>(), (int)(projectile.damage * .5f), 0, projectile.owner);
					Main.PlaySound(SoundID.Item93, -1, -1);
				}

            }
		}
    }
}


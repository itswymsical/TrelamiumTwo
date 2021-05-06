using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public class DustiliteJavelinProjectile : ModProjectile
	{
		public float MovementFactor
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}
		public override void SetStaticDefaults() 
			=> DisplayName.SetDefault("Dustilite Javelin");

		public override void SetDefaults()
		{
			projectile.width = 76;
			projectile.height = 86;

			projectile.aiStyle = 19;
			projectile.scale = 1.1f;
			projectile.penetrate = -1;

			projectile.hide = 
				projectile.friendly = 
				projectile.melee = 
				projectile.ownerHitCheck = true;

			projectile.tileCollide = false;

			projectile.width = projectile.height = 32;

		}
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
		}
    }
}


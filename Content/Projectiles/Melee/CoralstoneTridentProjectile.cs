using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public class CoralstoneTridentProjectile : ModProjectile
	{
		public float MovementFactor
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public override void SetDefaults() 
		{
			projectile.width = projectile.height = 28;
			
			projectile.aiStyle = 19;
			projectile.scale = 1.1f;
			projectile.penetrate = -1;

			projectile.hide = true;
			projectile.melee = true;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
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

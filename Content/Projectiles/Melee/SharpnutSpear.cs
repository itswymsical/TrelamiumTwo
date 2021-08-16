using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public class SharpnutSpear : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Melee + "SharpnutSpear";
		public float MovementFactor
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void SetDefaults() 
		{
			Projectile.width = Projectile.height = 28;
			
			Projectile.aiStyle = 19;
			Projectile.scale = 1.1f;
			Projectile.penetrate = -1;

			Projectile.hide = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ownerHitCheck = true;
		}
		
		public override void AI() 
		{
			Player projOwner = Main.player[Projectile.owner];
			Vector2 ownerCenter = projOwner.RotatedRelativePoint(projOwner.Center, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position = ownerCenter - Projectile.Size / 2;

			if (!projOwner.frozen)
			{
				if (MovementFactor == 0f) 
				{
					MovementFactor = 3f; 
					Projectile.netUpdate = true; 
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

			Projectile.position += Projectile.velocity * MovementFactor;
			if (projOwner.itemAnimation == 0) 
			{
				Projectile.Kill();
			}
			
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);

			if (Projectile.spriteDirection == -1) 
			{
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) 
			=> damage += target.defense / 3;
	}
}

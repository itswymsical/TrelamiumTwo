using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Projectiles
{
	public abstract class StickyProjectile : ModProjectile
	{
		protected NPC Target => Main.npc[(int)Projectile.ai[1]];

		protected bool stickToNPC;
		protected bool stickToTile;

		protected bool stickingToNPC;
		protected bool stickingToTile;

		private Vector2 offset;

		private float oldRotation;

		public override void AI()
		{
			if (stickingToNPC)
			{
				if (Target.active && !Target.dontTakeDamage)
				{
					// projectile.tileCollide = false;

					Projectile.Center = Target.Center - offset;

					Projectile.gfxOffY = Target.gfxOffY;
				}
				else
				{
					Projectile.Kill();
				}
			}

			if (stickingToTile || stickingToNPC)
				Projectile.rotation = oldRotation;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (!stickingToNPC && !stickingToTile && stickToNPC)
			{
				Projectile.ai[1] = target.whoAmI;

				oldRotation = Projectile.rotation;

				offset = target.Center - Projectile.Center + (Projectile.velocity * 0.75f);

				stickingToNPC = true;

				Projectile.netUpdate = true;
			}
			else
			{
				RemoveStackProjectiles();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!stickingToTile && !stickingToNPC && stickToTile)
			{
				oldRotation = Projectile.rotation;

				Projectile.velocity = Vector2.Zero;

				stickingToTile = true;
			}

			return false;
		}

		protected void RemoveStackProjectiles(int max = 3)
		{
			var sticking = new Point[max];
			int index = 0;

			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile currentProjectile = Main.projectile[i];

				if (i != Projectile.whoAmI 
					&& currentProjectile.active 
					&& currentProjectile.owner == Main.myPlayer
					&& currentProjectile.type == Projectile.type 
					&& currentProjectile.ai[0] == 1f 
					&& currentProjectile.ai[1] == Target.whoAmI
				) 
				{
					sticking[index++] = new Point(i, currentProjectile.timeLeft); 
					
					if (index >= sticking.Length)
						break;				
				}
			}

			if (index >= sticking.Length)
			{
				int oldIndex = 0;

				for (int i = 1; i < sticking.Length; i++)		
					if (sticking[i].Y < sticking[oldIndex].Y)
						oldIndex = i;							
				
				Main.projectile[sticking[oldIndex].X].Kill();
			}
		}
	}
}

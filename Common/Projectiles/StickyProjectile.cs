using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Projectiles
{
	public abstract class StickyProjectile : ModProjectile
	{
		protected NPC Target => Main.npc[(int)projectile.ai[1]];

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
					projectile.tileCollide = false;

					projectile.Center = Target.Center - offset;

					projectile.gfxOffY = Target.gfxOffY;
				}
				else
				{
					projectile.Kill();
				}
			}

			if (stickingToTile || stickingToNPC)
				projectile.rotation = oldRotation;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (!stickingToNPC && !stickingToTile && stickToNPC)
			{
				projectile.ai[1] = target.whoAmI;

				oldRotation = projectile.rotation;

				offset = target.Center - projectile.Center + (projectile.velocity * 0.75f);

				stickingToNPC = true;

				projectile.netUpdate = true;
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
				oldRotation = projectile.rotation;

				projectile.velocity = Vector2.Zero;

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

				if (i != projectile.whoAmI 
					&& currentProjectile.active 
					&& currentProjectile.owner == Main.myPlayer
					&& currentProjectile.type == projectile.type 
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

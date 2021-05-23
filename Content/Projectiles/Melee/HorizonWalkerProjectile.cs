using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public class HorizonWalkerProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
			=> DisplayName.SetDefault("Mirage");
		
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 16;
			projectile.aiStyle = 99;
			projectile.friendly = projectile.melee = true;
			projectile.penetrate = -1;
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 30f;
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 180f;
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 15f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int num2 = 0; num2 < 10; num2++)
			{
				int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, 0f, 0f, 125, default, 0.65f);
				Main.dust[num].velocity *= 3f;
			}
		}
	}
}
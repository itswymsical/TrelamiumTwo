using System;

using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Common.Extensions;

namespace TrelamiumTwo.Content.Projectiles.Summon
{
	internal sealed class FloralSpiritGuardian : ModProjectile
	{
		private float FloatTimer
		{
			get => projectile.localAI[0];
			set => projectile.localAI[0] = value;
		}

		private bool JustSpawned => projectile.localAI[0] == 0;

		public override void SetDefaults()
		{
			projectile.width = projectile.height = 50;

			projectile.minion = true;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.netImportant = true;
		}

		public override bool PreAI()
		{
			Player owner = Main.player[projectile.owner];
			Common.Players.TrelamiumPlayer modPlayer = owner.GetModPlayer<Common.Players.TrelamiumPlayer>();

			if (JustSpawned)
			{
				for (int i = 0; i < 40; ++i)
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 78, 0, 0, 100);
					dust.velocity *= 1.5f;
				}
				for (int i = 0; i < 15; ++i)
				{
					Dust.NewDust(projectile.position, projectile.width, projectile.height, 78, 0, 0, 100);
				}
			}

			if (!owner.active)
			{
				projectile.active = false;
				return (false);
			}

			if (owner.dead)
			{
				modPlayer.floralSpirit = false;
			}
			if (modPlayer.floralSpirit)
			{
				projectile.timeLeft = 2;
			}

			FollowOwner(owner);

			if (++projectile.ai[1] >= 150)
			{
				int target = -1;
				float distance = 600;

				for (int i = 0; i < Main.maxNPCs; i++)
				{
					if (Main.npc[i].CanBeChasedBy(projectile))
					{
						float currentDistance = projectile.Distance(Main.npc[i].Center);

						if (currentDistance < distance)
						{
							target = i;
							distance = currentDistance;
						}
					}
				}

				if (target != -1)
				{
					if (Main.myPlayer == projectile.owner)
					{
						Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<FloralSpiritOrb>(),
							25, 1f, projectile.owner, projectile.whoAmI, target);
					}
					projectile.ai[1] = 0;
				}
			}

			return (false);
		}

		public override bool CanDamage() => false;

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];
			Vector2 origin = texture.Size() / 2;
			SpriteEffects effects = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			float scaleModifier = Math.Abs((float)Math.Cos(FloatTimer / 30)) * 1.2f;

			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, lightColor * 0.7f, projectile.rotation, origin, scaleModifier, effects, 0f);

			return this.DrawProjectileCentered(spriteBatch, lightColor);
		}

		private void FollowOwner(Player owner)
		{
			Vector2 targetPosition = owner.Center;

			FloatTimer++;

			targetPosition.X -= (5 + owner.width / 2) * owner.direction;
			targetPosition.Y -= 25f + (float)Math.Sin(FloatTimer / 30) * 8f;

			projectile.Center = Vector2.Lerp(projectile.Center, targetPosition, 0.2f);
			projectile.velocity *= 0.8f;
			projectile.direction = projectile.spriteDirection = owner.direction;
		}
	}
}

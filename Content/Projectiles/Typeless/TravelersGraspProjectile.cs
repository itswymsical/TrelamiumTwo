using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrelamiumTwo.Content.Projectiles.Typeless
{
	public class TravelersGraspProjectile : ModProjectile
	{
		private readonly float hangDistance = 20f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Traveler's Grasp");
		}
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.GemHookAmethyst);

			projectile.penetrate = -1;

			projectile.friendly = true;
		}

		public override bool PreAI()
		{
			if (projectile.aiStyle != 7)
			{
				projectile.aiStyle = 7;
			}

			Lighting.AddLight(projectile.Center, new Vector3(0.5f, 0.2f, 0f) * 0.2f);
			return (true);
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
			=> projectile.aiStyle = 0;

		public override bool? CanHitNPC(NPC target)
			=> projectile.ai[0] != 1f;

		public override bool? SingleGrappleHook(Player player)
			=> true;

		public override bool? CanUseGrapple(Player player)
			=> player.ownedProjectileCounts[projectile.type] < 1;

		public override float GrappleRange()
			=> 400f;

		public override void NumGrappleHooks(Player player, ref int numHooks)
			=> numHooks = 1;

		public override void GrappleRetreatSpeed(Player player, ref float speed)
			=> speed = 10f;

		public override void GrapplePullSpeed(Player player, ref float speed)
			=> speed = 6f;

		public override void GrappleTargetPoint(Player player, ref float grappleX, ref float grappleY)
		{
			Vector2 dirToPlayer = projectile.DirectionTo(player.Center);
			grappleX += dirToPlayer.X * hangDistance;
			grappleY += dirToPlayer.Y * hangDistance;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Player owner = Main.player[projectile.owner];

			Vector2 mountedCenter = owner.MountedCenter;
			Texture2D chainTexture = ModContent.GetTexture(Texture + "_Chain");
			Rectangle chainFrame = chainTexture.Bounds;

			var drawPosition = projectile.Center;
			var remainingVectorToPlayer = mountedCenter - drawPosition;

			float rotation = remainingVectorToPlayer.ToRotation();

			bool drawChain = true;
			while (drawChain)
			{
				float length = remainingVectorToPlayer.Length();

				if (float.IsNaN(length))
				{
					break;
				}

				if (length <= chainTexture.Width + 4)
				{
					drawChain = false;
					chainFrame.Width = (int)length;
				}

				drawPosition += remainingVectorToPlayer * chainTexture.Width / length;
				remainingVectorToPlayer = mountedCenter - drawPosition;

				Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
				spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, chainFrame, color, rotation, chainFrame.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			}

			return true;
		}
	}
}

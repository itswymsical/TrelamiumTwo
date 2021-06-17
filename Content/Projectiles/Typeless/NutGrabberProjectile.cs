using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Typeless
{
	public class NutGrabberProjectile : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Typeless + "NutGrabberProjectile";

		private readonly float hangDistance = 20f;
		public override void SetDefaults()
			=> projectile.CloneDefaults(ProjectileID.GemHookAmethyst);

		public override bool? SingleGrappleHook(Player player)
			=> true;

		public override bool? CanUseGrapple(Player player)
			=> player.ownedProjectileCounts[projectile.type] < 1;

		public override float GrappleRange()
			=> 250f;

		public override void NumGrappleHooks(Player player, ref int numHooks)
			=> numHooks = 1;

		public override void GrappleRetreatSpeed(Player player, ref float speed)
			=> speed = 12.5f;

		public override void GrapplePullSpeed(Player player, ref float speed)
			=> speed = 7.5f;

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
			Texture2D[] chainTextures = new Texture2D[] {
				ModContent.GetTexture(Texture + "_Chain1"),
				ModContent.GetTexture(Texture + "_Chain2"),
				ModContent.GetTexture(Texture + "_Chain3")
			};
			Rectangle chainFrame = chainTextures[0].Frame(1, 1, 0, 0);

			var drawPosition = projectile.Center;
			var remainingVectorToPlayer = mountedCenter - drawPosition;

			float rotation = remainingVectorToPlayer.ToRotation();

			bool drawChain = true;
			int chainIncrement = 0;

			while (drawChain)
			{
				int currentChain = chainIncrement % chainTextures.Length;
				float length = remainingVectorToPlayer.Length();

				if (float.IsNaN(length))
				{
					break;
				}

				if (length <= chainTextures[currentChain].Width + 4)
				{
					drawChain = false;
					chainFrame.Width = (int)length;
				}

				drawPosition += remainingVectorToPlayer * chainTextures[currentChain].Width / length;
				remainingVectorToPlayer = mountedCenter - drawPosition;

				Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
				spriteBatch.Draw(chainTextures[currentChain], drawPosition - Main.screenPosition, chainFrame, color, rotation, chainTextures[currentChain].Size() * 0.5f, 1f, SpriteEffects.None, 0f);

				chainIncrement += 2;
			}

			return (true);
		}
	}
}

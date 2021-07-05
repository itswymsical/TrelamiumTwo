using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Content.Dusts;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class BloomRose : ModProjectile
	{
        public override string Texture => Assets.Items.Materials + "BloomRose";

		public override void SetDefaults()
		{
			projectile.magic = true;
			projectile.friendly = true;

			projectile.width = projectile.height = 20;

			projectile.timeLeft = 180;
			projectile.aiStyle = 0;
		}
		public override void AI()
		{
			projectile.ai[0]++;

			if (projectile.ai[0] > 20f)
				projectile.velocity.Y += 0.2f;

			if (Main.rand.NextBool(10))
			{
				Dust.NewDustDirect(projectile.Center, 0, 0, ModContent.DustType<PinkPetal>(), -projectile.velocity.X, -projectile.velocity.Y);

				projectile.netUpdate = true;
			}

			projectile.rotation += projectile.velocity.X / 25f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Dig, projectile.position);
			Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);

			return true;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = mod.GetTexture("Assets/Glow");
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				float scale = projectile.scale * (projectile.oldPos.Length - k) / projectile.oldPos.Length * .45f;
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + Main.projectileTexture[projectile.type].Size() / 2f;
				Color color = projectile.GetAlpha(Color.Pink) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);

				spriteBatch.Draw(texture2D, drawPos, null, color, projectile.rotation, Main.projectileTexture[projectile.type].Size(), scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 16; i++)
			{
				var velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
				Dust.NewDustDirect(projectile.Center, 0, 0, ModContent.DustType<PinkPetal>(), velocity.X, velocity.Y);
			}

			int petalAmount = Main.rand.Next(2, 6);
			for (int i = 0; i < petalAmount; i++)
			{
				float rotation = i / (float)petalAmount * MathHelper.TwoPi;
				var velocity = rotation.ToRotationVector2() * 5f;

				Projectile.NewProjectile(projectile.Center, velocity, ModContent.ProjectileType<BloomRosePetal>(), projectile.damage / 2, projectile.knockBack / 2f, projectile.owner);

				projectile.netUpdate = true;
			}
		}
	}
}
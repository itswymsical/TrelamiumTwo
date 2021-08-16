using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Content.Dusts;
using TrelamiumTwo.Core;
using Terraria.Audio;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class BloomRose : ModProjectile
	{
        public override string Texture => Assets.Items.Materials + "BloomRose";
        public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;

			Projectile.width = Projectile.height = 20;

			Projectile.timeLeft = 180;
			Projectile.aiStyle = 0;
		}
		public override void AI()
		{
			Projectile.ai[0]++;

			if (Projectile.ai[0] > 20f)
				Projectile.velocity.Y += 0.2f;

			if (Main.rand.NextBool(10))
			{
				Dust.NewDustDirect(Projectile.Center, 0, 0, ModContent.DustType<PinkPetal>(), -Projectile.velocity.X, -Projectile.velocity.Y);

				Projectile.netUpdate = true;
			}

			Projectile.rotation += Projectile.velocity.X / 25f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

			return true;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = Mod.Assets.Request<Texture2D>("Assets/Glow").Value;
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				float scale = Projectile.scale * (Projectile.oldPos.Length - k) / Projectile.oldPos.Length * .45f;
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + Main.projectileTexture[Projectile.type].Size() / 2f;
				Color color = Projectile.GetAlpha(Color.Pink) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);

				spriteBatch.Draw(texture2D, drawPos, null, color, Projectile.rotation, Main.projectileTexture[Projectile.type].Size(), scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 16; i++)
			{
				var velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
				Dust.NewDustDirect(Projectile.Center, 0, 0, ModContent.DustType<PinkPetal>(), velocity.X, velocity.Y);
			}

			int petalAmount = Main.rand.Next(2, 6);
			for (int i = 0; i < petalAmount; i++)
			{
				float rotation = i / (float)petalAmount * MathHelper.TwoPi;
				var velocity = rotation.ToRotationVector2() * 5f;

				Projectile.NewProjectile(Projectile.Center, velocity, ModContent.ProjectileType<BloomRosePetal>(), Projectile.damage / 3, Projectile.knockBack / 2f, Projectile.owner);

				Projectile.netUpdate = true;
			}
		}
	}
}
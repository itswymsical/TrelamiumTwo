using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Core;
using Terraria.Audio;

namespace TrelamiumTwo.Content.Projectiles.Typeless
{
	public class FallingLeaf : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Typeless + "FallingLeaf";
        public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 10;

			Projectile.friendly = true;
		}

		public override bool PreAI()
		{
			if (Projectile.localAI[0] == 0)
			{
				Projectile.localAI[0] = 1;
				SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
				Helper.SpawnDustCloud(Projectile.position, Projectile.height, Projectile.height, ModContent.DustType<Dusts.Wood>());
			}

			Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y + 0.05f, 0, 2f);

			Projectile.spriteDirection = Projectile.direction;
			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
			}

			Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver4;
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.PiOver2;
			}

			return (false);
		}

		public override bool CanDamage() => false;

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
			Helper.SpawnDustCloud(Projectile.position, Projectile.height, Projectile.height, ModContent.DustType<Dusts.Wood>());

			if (Main.myPlayer == Projectile.owner)
			{
				Item.NewItem(Projectile.getRect(), ModContent.ItemType<Items.Materials.Leaf>());
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => Projectile.DrawProjectileCentered(spriteBatch, lightColor);
	}
}

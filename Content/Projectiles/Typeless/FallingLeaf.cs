using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Typeless
{
	public class FallingLeaf : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Typeless + "FallingLeaf";
        public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 10;

			projectile.friendly = true;
		}

		public override bool PreAI()
		{
			if (projectile.localAI[0] == 0)
			{
				projectile.localAI[0] = 1;
				Main.PlaySound(SoundID.Grass, projectile.position);
				Helper.SpawnDustCloud(projectile.position, projectile.height, projectile.height, ModContent.DustType<Dusts.Wood>());
			}

			projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y + 0.05f, 0, 2f);

			projectile.spriteDirection = projectile.direction;
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % Main.projFrames[projectile.type];
			}

			projectile.rotation = projectile.velocity.ToRotation() - MathHelper.PiOver4;
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= MathHelper.PiOver2;
			}

			return (false);
		}

		public override bool CanDamage() => false;

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Grass, projectile.position);
			Helper.SpawnDustCloud(projectile.position, projectile.height, projectile.height, ModContent.DustType<Dusts.Wood>());

			if (Main.myPlayer == projectile.owner)
			{
				Item.NewItem(projectile.getRect(), ModContent.ItemType<Items.Materials.Leaf>());
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => projectile.DrawProjectileCentered(spriteBatch, lightColor);
	}
}

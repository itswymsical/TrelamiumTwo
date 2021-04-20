using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrelamiumTwo.Content.Projectiles
{
	public sealed class LanternLight : ModProjectile
	{
		public override string Texture => "TrelamiumTwo/Assets/Glow";

		public override void SetDefaults()
		{
			projectile.width = 38;
			projectile.height = 38;

			projectile.tileCollide = false;
		}
	}
}

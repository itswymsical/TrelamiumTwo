using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Tools
{
	public class NutGrabber : ModItem
	{
		public override string Texture => Assets.Items.Tools + "NutGrabber";
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.AmethystHook);

			item.damage = 10;
			item.knockBack = 0.1f;

			item.shootSpeed = 12f;
			item.shoot = ModContent.ProjectileType<Projectiles.Typeless.NutGrabberProjectile>();
		}
	}
}

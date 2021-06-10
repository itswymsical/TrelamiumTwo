using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Tools.Hooks
{
	public class ElysianGrasp : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.AmethystHook);

			item.damage = 10;
			item.knockBack = 0.1f;

			item.shootSpeed = 12f;
			item.shoot = ModContent.ProjectileType<Projectiles.Typeless.ElysianGraspProjectile>();
		}
	}
}

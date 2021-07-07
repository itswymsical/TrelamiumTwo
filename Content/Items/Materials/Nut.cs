using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class Nut : ModItem
	{
		public override string Texture => Assets.Items.Materials + "Nut";
		public override void SetDefaults()
		{
			item.width = item.height = 28;
			item.maxStack = 999;
			item.rare = ItemRarityID.White;
			
			item.damage = 2;

			item.useTime = item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.EatingUsing;
			
			item.ranged = true;
			item.material = true;
			item.consumable = true;
			item.value = Item.sellPrice(copper: 1);
			item.shootSpeed = 4f;
			item.ammo = item.type;
			item.shoot = ModContent.ProjectileType<Projectiles.Ranged.NutRocketProjectile>();
		}
		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			foreach (Projectile projectile in Main.projectile)
			{
				if (projectile.Hitbox.Intersects(item.Hitbox) && projectile.type == ModContent.ProjectileType<Projectiles.Typeless.NutGrabberProjectile>())
				{
					item.position = projectile.Center;
					item.Center = projectile.position;
				}
			}
		}
        public override bool ConsumeItem(Player player)
		{	  
			item.healLife = 10;
			item.potion = true;
			item.shoot = ProjectileID.None;

			return true;
		}
	}
}

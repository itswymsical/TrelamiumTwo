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
			Item.width = Item.height = 28;
			Item.maxStack = 999;
			Item.rare = ItemRarityID.White;
			
			Item.damage = 2;

			Item.useTime = Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.EatFood;
			
			Item.DamageType = DamageClass.Ranged;
			Item.material = true;
			Item.consumable = true;
			Item.value = Item.sellPrice(copper: 1);
			Item.shootSpeed = 4f;
			Item.ammo = Item.type;
			Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.NutRocketProjectile>();
		}
		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			foreach (Projectile projectile in Main.projectile)
			{
				if (projectile.Hitbox.Intersects(Item.Hitbox) && projectile.type == ModContent.ProjectileType<Projectiles.Typeless.NutGrabberProjectile>())
				{
					Item.position = projectile.Center;
					Item.Center = projectile.position;
				}
			}
		}
        public override bool ConsumeItem(Player player)
		{	  
			Item.healLife = 10;
			Item.potion = true;
			Item.shoot = ProjectileID.None;

			return true;
		}
	}
}


using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.Items.Weapons.Ranged
{
	public class Prevalence : ModItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Prevalence");

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 62;
			item.rare = ItemRarityID.LightRed;
			item.value = Item.sellPrice(gold: 2);

			item.damage = 19;
			item.knockBack = 2f;

			item.useTime = 10;
			item.useAnimation = 30;
			item.reuseDelay = 14;

			item.useStyle = ItemUseStyleID.HoldingOut;

			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = false;

			item.shootSpeed = 8f;
			item.useAmmo = AmmoID.Arrow;
			item.shoot = ProjectileID.WoodenArrowFriendly;

			item.UseSound = SoundID.Item5;
		}
		public override Vector2? HoldoutOffset() => new Vector2(-7, 0);
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = ProjectileID.FireArrow;
			}
			return true;
		}
	}
}

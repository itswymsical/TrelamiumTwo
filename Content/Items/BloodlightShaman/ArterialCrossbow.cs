
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.Items.BloodlightShaman
{
	public class ArterialCrossbow : TrelamiumItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Arterial Crossbow");

		public override void SetDefaults()
		{
			item.rare = ItemRarityID.LightRed;
			item.value = Item.sellPrice(gold: 2);

			item.damage = 45;
			item.knockBack = 2f;

			item.useTime = 22;
			item.useAnimation = 22;

			item.useStyle = ItemUseStyleID.HoldingOut;

			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;

			item.shootSpeed = 9.75f;
			item.useAmmo = AmmoID.Arrow;
			item.shoot = ProjectileID.WoodenArrowFriendly;

			item.UseSound = SoundID.Item5;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly) // Make this 'imbue' arrows with the 'bleeding' debuff, there should be a spiraling dust effect as well
			{
				if (Main.rand.NextBool(5))
				{
					Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<Projectiles.Ranged.ArterialClotProjectile>(), damage, knockBack);
				}
			}
			return true;
        }
    }
}

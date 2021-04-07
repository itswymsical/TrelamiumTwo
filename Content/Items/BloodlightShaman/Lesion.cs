using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.Items.BloodlightShaman
{
	public class Lesion : TrelamiumItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Lesion");

		public override void SetDefaults()
		{
			item.rare = ItemRarityID.LightRed;
			item.value = Item.sellPrice(gold: 2);

			item.damage = 135;
			item.knockBack = 2f;

			item.useTime = 38;
			item.useAnimation = 38;

			item.useStyle = ItemUseStyleID.HoldingOut;

			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;

			item.shootSpeed = 11f;
			item.useAmmo = AmmoID.Arrow;
			item.shoot = ProjectileID.WoodenArrowFriendly;

			item.UseSound = SoundID.Item36;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 32f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			int numberProjectiles = 4 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(12));
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, 
					ModContent.ProjectileType<Projectiles.Ranged.LesionProjectile>(), damage / numberProjectiles, knockBack, player.whoAmI);
			}
			return false;
		}
	}
}

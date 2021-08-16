using TrelamiumTwo.Core;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.BloomRose
{
	public class RoseWrath : ModItem
	{
		public override string Texture => Assets.Weapons.BloomRose + "RoseWrath";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rose Wrath");
			Tooltip.SetDefault("Shoots a Bloom Rose, that releases petals when it hits a tile or enemy.");

			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			// item.autoReuse = false;

			Item.damage = 9;
			Item.knockBack = 1f;
			Item.mana = 4;

			Item.width = Item.height = 38;

			Item.useTime = Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Shoot;

			Item.shoot = ModContent.ProjectileType<Projectiles.Magic.BloomRose>();
			Item.shootSpeed = 8f;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 1, copper: 20);

			Item.UseSound = SoundID.Item43;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			var muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;

			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
				position += muzzleOffset;

			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 2).AddIngredient(ModContent.ItemType<Materials.Leaf>(), 4).AddIngredient(ItemID.Wood, 16).AddTile(TileID.Anvils).AddTile(TileID.WorkBenches).Register();
		}
	}
}

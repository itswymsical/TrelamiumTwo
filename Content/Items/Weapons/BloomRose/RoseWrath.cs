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

			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = false;

			item.damage = 9;
			item.knockBack = 1f;
			item.mana = 4;

			item.width = item.height = 38;

			item.useTime = item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.shoot = ModContent.ProjectileType<Projectiles.Magic.BloomRose>();
			item.shootSpeed = 8f;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 1, copper: 20);

			item.UseSound = SoundID.Item43;
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
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 2);
			recipe.AddIngredient(ModContent.ItemType<Materials.Leaf>(), 4);
			recipe.AddIngredient(ItemID.Wood, 16);
			recipe.AddTile(TileID.Anvils);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

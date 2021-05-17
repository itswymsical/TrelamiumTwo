using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.Ranged
{
    public class DustiliteBow : TrelamiumItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dustilite Bow");
			Tooltip.SetDefault("Converts Wooden Arrows into Dustilite Arrows");
		}

		public override void SetDefaults()
		{
			item.rare = ItemRarityID.Green;
			item.value = Item.sellPrice(silver: 49);

			item.damage = 13;
			item.knockBack = 2f;

			item.useTime = item.useAnimation = 32;
			
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = false;

			item.shootSpeed = 8f;
			item.useAmmo = AmmoID.Arrow;
			item.shoot = ProjectileID.WoodenArrowFriendly;

			item.UseSound = SoundID.Item5;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (type == ProjectileID.WoodenArrowFriendly)
				type = ModContent.ProjectileType<Projectiles.Ranged.DustiliteArrow>();
			
			return true;
        }
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.DustiliteCrystal>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

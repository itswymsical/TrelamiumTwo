using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Sandcrawler
{
	public class SandcrawlerBow : ModItem
	{
		public override string Texture => Assets.Weapons.Sandcrawler + "SandcrawlerBow";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandcrawler Bow");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 60;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 5);

			item.damage = 7;
			item.knockBack = 3.25f;

			item.useTime = item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;
			
			item.ranged = 
				item.noMelee = true;

			item.useTurn = 
				item.autoReuse = false;
			
			item.shootSpeed = 7f;
			item.useAmmo = AmmoID.Arrow;
			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.UseSound = SoundID.Item5;
		}
		
		public override Vector2? HoldoutOffset() => new Vector2(-2.5f, 0f);
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
				position += muzzleOffset;		
			return true;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AntlionMandible, 2);
			recipe.AddIngredient(ModContent.ItemType<Materials.AntlionChitin>(), 7);
			recipe.AddIngredient(ModContent.ItemType<Materials.SandcrawlerShell>(), 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

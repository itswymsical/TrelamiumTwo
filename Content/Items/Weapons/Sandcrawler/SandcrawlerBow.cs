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
			Item.width = 32;
			Item.height = 60;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 5);

			Item.damage = 7;
			Item.knockBack = 3.25f;

			Item.useTime = Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Shoot;
			
			Item.DamageType = 
				// item.noMelee = true;

			Item.useTurn = 
				// item.autoReuse = false;
			
			Item.shootSpeed = 7f;
			Item.useAmmo = AmmoID.Arrow;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.UseSound = SoundID.Item5;
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
			CreateRecipe(1).AddIngredient(ItemID.AntlionMandible, 2).AddIngredient(ModContent.ItemType<Materials.AntlionChitin>(), 7).AddIngredient(ModContent.ItemType<Materials.SandcrawlerShell>(), 2).AddTile(TileID.WorkBenches).Register();
		}
	}
}

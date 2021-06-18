using TrelamiumTwo.Content.Projectiles.Summon;
using TrelamiumTwo.Core;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.BloomRose
{
	public class AromaticBouquet : ModItem
	{
		public override string Texture => Assets.Items.BloomRose + "AromaticBouquet";

		public override void SetStaticDefaults() => DisplayName.SetDefault("Aromatic Bouquet");

		public override void SetDefaults()
		{
			item.summon = true;
			item.noMelee = true;
			item.autoReuse = false;

			item.damage = 8;
			item.knockBack = 1f;
			item.mana = 10;

			item.width = 28;
			item.height = 24;

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;

			item.shoot = ModContent.ProjectileType<BloomBulb>();

			item.buffType = ModContent.BuffType<Buffs.Minions.BloomBulb>();

			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 1, copper: 80);

			item.UseSound = SoundID.Item44;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(item.buffType, 2);
			position = Main.MouseWorld;
			return true;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

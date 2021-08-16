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
		public override string Texture => Assets.Weapons.BloomRose + "AromaticBouquet";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Summons a flower bulb that spews petals at enemies");

		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			// item.autoReuse = false;

			Item.damage = 8;
			Item.knockBack = 1f;
			Item.mana = 10;

			Item.width = 28;
			Item.height = 24;

			Item.useTime = Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.shoot = ModContent.ProjectileType<BloomBulb>();

			Item.buffType = ModContent.BuffType<Buffs.Minions.BloomBulb>();

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 1, copper: 80);

			Item.UseSound = SoundID.Item44;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 3).AddIngredient(ModContent.ItemType<Materials.Leaf>(), 4).AddIngredient(ItemID.Wood, 18).AddTile(TileID.Anvils).Register();
		}
	}
}

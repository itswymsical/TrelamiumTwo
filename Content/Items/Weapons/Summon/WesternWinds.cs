using TrelamiumTwo.Content.Projectiles.Summon;
using TrelamiumTwo.Core;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.Summon
{
	public class WesternWinds : ModItem
	{
		public override string Texture => Assets.Weapons.Summon + "WesternWinds";

		public override void SetStaticDefaults() => Tooltip.SetDefault("Summons a tumbleweed that gains size and damage based on travelled distance");

		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			// item.autoReuse = false;

			Item.damage = 10;
			Item.knockBack = 1f;
			Item.mana = 10;

			Item.width = 28;
			Item.height = 24;

			Item.useTime = Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.shoot = ModContent.ProjectileType<Tumbleweed>();

			Item.buffType = ModContent.BuffType<Buffs.Minions.Tumbleweed>();

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
			CreateRecipe(1).AddIngredient(ItemID.PalmWood, 12).AddIngredient(ModContent.ItemType<Materials.AridFiber>(), 6).AddIngredient(ItemID.FallenStar, 2).AddTile(TileID.Anvils).Register();
		}
	}
}

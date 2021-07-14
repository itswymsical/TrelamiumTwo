using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.Sandcrawler
{
	[AutoloadEquip(EquipType.Head)]
	public class SandcrawlerMask : ModItem
	{
		public override string Texture => Assets.Armor.Sandcrawler + "SandcrawlerMask";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Increases critical strike chance by 2");
        public override void SetDefaults()
		{
			item.defense = 2;

			item.width = 22;
			item.height = 20;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 1, copper: 65);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<SandcrawlerChestplate>() && legs.type == ModContent.ItemType<SandcrawlerLeggings>();
        public override void UpdateEquip(Player player) => player.AddAllCrit(2);		
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "You deal contact damage based on your movement speed";
			player.GetModPlayer<ArmorSetPlayer>().sandcrawlerSet = true;
			if (player.velocity.X >= 0f)
				player.thorns = player.velocity.X / 8f;

			if (player.velocity.X <= 0f)
				player.thorns = -player.velocity.X / 8f;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<HardenedCarapace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AntlionChitin>(), 3);
			recipe.AddIngredient(ItemID.AntlionMandible, 2);
			recipe.AddIngredient(ModContent.ItemType<SandcrawlerShell>(), 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
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
			Item.defense = 2;

			Item.width = 22;
			Item.height = 20;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 1, copper: 65);
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
			CreateRecipe(1).AddIngredient(ModContent.ItemType<HardenedCarapace>(), 1).AddIngredient(ModContent.ItemType<AntlionChitin>(), 3).AddIngredient(ItemID.AntlionMandible, 2).AddIngredient(ModContent.ItemType<SandcrawlerShell>(), 3).AddTile(TileID.Anvils).Register();
		}
	}
}
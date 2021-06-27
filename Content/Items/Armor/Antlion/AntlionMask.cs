using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.Antlion
{
	[AutoloadEquip(EquipType.Head)]
	public class AntlionMask : ModItem
	{
		public override string Texture => Assets.Items.Antlion + "AntlionMask";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Increases critical strike chance by 2");
        public override void SetDefaults()
		{
			item.defense = 2;

			item.width = 22;
			item.height = 20;

			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 1, copper: 65);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<AntlionBreastplate>() && legs.type == ModContent.ItemType<AntlionLeggings>();
        public override void UpdateEquip(Player player) => player.AddAllCrit(2);
		
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Antlion enemies become passive until you get close or damage them.";
			player.GetModPlayer<ArmorSetPlayer>().AntlionSet = true;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<HardenedCarapace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AntlionChitin>(), 3);
			recipe.AddIngredient(ItemID.AntlionMandible, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
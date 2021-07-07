using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Fungore
{
	public class Fungocybin : ModItem
	{
		public override string Texture => Assets.Items.Fungore + "Fungocybin";
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons the pea-brained fungus giant");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 12;
		}

		public override void SetDefaults() {
			item.width = 
				item.height = 
				item.maxStack = 20;

			item.value = Item.sellPrice(0);
			item.rare = ItemRarityID.White;

			item.useAnimation = item.useTime = 30;

			item.useStyle = ItemUseStyleID.HoldingUp;
			item.consumable = true;
		}

		public override bool CanUseItem(Player player) => !NPC.AnyNPCs(ModContent.NPCType<NPCs.Boss.Fungore.Fungore>());

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.Boss.Fungore.Fungore>());
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Mushroom, 8);
			recipe.AddIngredient(ItemID.Chain, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
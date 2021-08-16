using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;
using Terraria.Audio;

namespace TrelamiumTwo.Content.Items.Fungore
{
	public class Fungocybin : ModItem
	{
		public override string Texture => Assets.Items.Fungore + "Fungocybin";
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons the pea-brained fungus giant");
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 12;
		}

		public override void SetDefaults() {
			Item.width = 
				Item.height = 
				Item.maxStack = 20;

			Item.value = Item.sellPrice(0);
			Item.rare = ItemRarityID.White;

			Item.useAnimation = Item.useTime = 30;

			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}

		public override bool CanUseItem(Player player) => !NPC.AnyNPCs(ModContent.NPCType<NPCs.Boss.Fungore.Fungore>());

		public override bool? UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.Boss.Fungore.Fungore>());
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Mushroom, 8).AddIngredient(ItemID.Chain, 2).AddTile(TileID.Anvils).Register();
		}
	}
}
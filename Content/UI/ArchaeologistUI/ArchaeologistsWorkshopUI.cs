using System.Linq;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using Microsoft.Xna.Framework;

using TrelamiumTwo.Core.Abstracts;
using TrelamiumTwo.Content.Items.Materials;

namespace TrelamiumTwo.Content.UI.ArchaeologistUI
{
	// TODO: Developer - Might need a little rework/cleanup when it comes to the registering of recipes for this workshop UI.

	public sealed class ArchaeologistsWorkshopUI : SmartUIState
	{
		private readonly WorkshopItemSlotWrapper[] WorkshopItems = new WorkshopItemSlotWrapper[5];
		private WorkshopItemSlotWrapper ResultItemSlot 
			=> WorkshopItems[4] ?? new WorkshopItemSlotWrapper();
		private Item[] Items 
			=> WorkshopItems.Select(x => x.Item).ToArray();
		public override int InsertionIndex(List<GameInterfaceLayer> layers)
			=> layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

		public override void OnInitialize()
		{
			for (int i = 0; i < WorkshopItems.Length; ++i)
			{
				WorkshopItems[i] = new WorkshopItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
				{
					Left = { Pixels = Main.screenWidth / 2 - 200 + 50 * i },
					Top = { Pixels = Main.screenHeight / 2 - 100 }
				};

				this.Append(WorkshopItems[i]);
			}

			ResultItemSlot.Left.Set(Main.screenWidth / 2 + 100, 0f);
			ResultItemSlot.ValidItemFunc = item => item.IsAir;
		}

		public override void OnActivate()
			=> this.Visible = true;
		public override void OnDeactivate()
		{
			this.Visible = false;

			foreach (Item item in Items)
			{
				if(!item.IsAir)
				{
					Main.LocalPlayer.QuickSpawnClonedItem(item, item.stack);
					item.TurnToAir();
				}
			}
		}

		public override void Update(GameTime gameTime)
		{
			// KEEP THIS LINE IF YOU REMOVE IT EVERYTHING FUCKS UP.
			base.Update(gameTime);

			Player player = Main.LocalPlayer;

			foreach (Item item in Items)
			{
				if (item.stack > 1)
				{
					Item mouse = Main.mouseItem;
					player.QuickSpawnClonedItem(mouse, mouse.stack);
					mouse.SetDefaults(item.type);
					mouse.stack = item.stack - 1;
					player.inventory[58] = mouse;
					item.stack = 1;
				}
			}

			// EXAMPLE:          First Slot         Second Slot       Third Slot        Fourth Slot  Result
			AddWorkshopRecipe(ItemID.StoneBlock, ItemID.DirtBlock, ItemID.ClayBlock, ItemID.Wood, ItemID.CopperShortsword);
			AddWorkshopRecipe(ItemType<VultureGizzard>(), ItemID.Leather, ItemType<AridFiber>(), ItemType<SandwormTooth>(), ItemID.TerraBlade);
			AddWorkshopRecipe(ItemType<RattlesnakeTail>(), ItemType<DesolateHusk>(), ItemID.AntlionMandible, ItemID.None, ItemID.TerraBlade);
			AddWorkshopRecipe(ItemType<CrackedScarabHorn>(), ItemType<HardenedCarapace>(), ItemType<DesolateHusk>(), ItemType<SandwormTooth>(), ItemType<AFunny>());
			AddWorkshopRecipe(ItemType<SeveredSpiderLegs>(), ItemID.AntlionMandible, ItemType<SandwormTooth>(), ItemID.None, ItemID.TerraBlade);
			AddWorkshopRecipe(ItemType<HeliacalFeather>(), ItemType<HardenedCarapace>(), ItemID.Silk, ItemID.Leather, ItemID.TerraBlade);
		}
		/// <summary>
		/// Adds a recipe for the Archeologist's Workshop.
		/// </summary>
		/// <param name="slot1">The item type of the item that goes in the first slot.</param>
		/// <param name="slot2">The item type of the item that goes in the second slot.</param>
		/// <param name="slot3">The item type of the item that goes in the third lot.</param>
		/// <param name="slot4">The item type of the item that goes in the fourth slsot.</param>
		/// <param name="result">The item type of the result item.</param>
		public void AddWorkshopRecipe(int slot1, int slot2, int slot3, int slot4, int result)
		{
			var currentItems = Items;

			if (currentItems[0].type == slot1 && currentItems[1].type == slot2 && currentItems[2].type == slot3 && currentItems[3].type == slot4)
			{
				if(!currentItems[4].IsAir)
				{
					Main.LocalPlayer.QuickSpawnClonedItem(currentItems[4], currentItems[4].stack);
				}
				currentItems[4].SetDefaults(result);
				
				for (int i = 0; i < currentItems.Count() - 1; i++)
				{
					currentItems[i].TurnToAir();
				}
				Main.PlaySound(TrelamiumTwo.Instance.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/WorkshopCraft").WithVolume(1.25f));
			}
		}
	}
}

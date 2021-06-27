using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Viking
{
	public class Claymore : ModItem
	{
		public override string Texture => Assets.Items.Viking + "Claymore";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Attacks have a chance to inflict frostburn");
        public override void SetDefaults()
		{
			item.width = item.height = 58;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 2);

			item.damage = 11;
			item.knockBack = 2.5f;

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;

			item.melee = true;
			item.autoReuse = true;

			item.UseSound = SoundID.Item18;
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			if (Main.rand.Next(6) == 0)
				target.AddBuff(BuffID.Frostburn, 60);
        }
        public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BorealWood, 10);
			recipe.AddIngredient(ItemID.IceBlock, 4);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Viking
{
	public class Claymore : ModItem
	{
		public override string Texture => Assets.Weapons.Viking + "Claymore";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Attacks have a chance to inflict frostburn");
        public override void SetDefaults()
		{
			Item.width = Item.height = 58;
			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(silver: 2);

			Item.damage = 9;
			Item.knockBack = 3.5f;

			Item.useTime = Item.useAnimation = 28;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.DamageType = DamageClass.Melee;
			Item.autoReuse = true;

			Item.UseSound = SoundID.Item18;
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			if (Main.rand.Next(6) == 0)
				target.AddBuff(BuffID.Frostburn, 60);
        }
        public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.BorealWood, 18).AddRecipeGroup(RecipeGroupID.IronBar, 8).AddIngredient(ItemID.IceBlock, 3).AddTile(TileID.Anvils).Register();
		}
	}
}

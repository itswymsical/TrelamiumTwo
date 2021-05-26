using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class OrnamentalShovel : ShovelItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ornamental Shovel");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 6;
            item.useTime = 18;
            item.useAnimation = 18;
            item.rare = ItemRarityID.Blue;
            DiggingPower(60);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;

            item.value = Item.sellPrice(silver: 5);
            item.useTurn = true;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 8);
            recipe.AddIngredient(ItemID.Ruby, 2);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddIngredient(ItemID.Amethyst, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumBar, 8);
            recipe.AddIngredient(ItemID.Ruby, 2);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddIngredient(ItemID.Amethyst, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
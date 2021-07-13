using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Items;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Brass
{
    public class BrassPickaxe : ModItem
    {
        public override string Texture => Assets.Weapons.Brass + "BrassPickaxe";
        public override void SetDefaults()
        {
            item.pick = 45;

            item.melee = true;
            item.damage = 4;
            item.useTime = item.useAnimation = 20;
            item.width = item.height = 36;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(silver: 6);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.BrassChunk>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
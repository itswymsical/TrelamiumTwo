using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Items;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Brass
{
    public class BrassShovel : ShovelItem
    {
        public override string Texture => Assets.Weapons.Brass + "BrassShovel";
        public override void SetDefaults()
        {
            DiggingPower(40);

            item.melee = true;
            item.damage = 4;
            item.useTime = item.useAnimation = 18;
            item.width = item.height = 36;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(silver: 6);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.BrassChunk>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
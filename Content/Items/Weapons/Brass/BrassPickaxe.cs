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
            Item.pick = 45;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 4;
            Item.useTime = Item.useAnimation = 20;
            Item.width = Item.height = 36;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(silver: 6);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.BrassChunk>(), 12).AddTile(TileID.Anvils).Register();
        }
    }
}
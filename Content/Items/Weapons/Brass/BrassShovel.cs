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

            Item.DamageType = DamageClass.Melee;
            Item.damage = 4;
            Item.useTime = Item.useAnimation = 18;
            Item.width = Item.height = 36;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(silver: 6);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.BrassChunk>(), 10).AddTile(TileID.Anvils).Register();
        }
    }
}
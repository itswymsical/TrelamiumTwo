using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Materials;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class DustiliteClaymore : TrelamiumItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Dustilite Claymore");

        public override void SafeSetDefaults()
        {
            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 28;
            item.useTime = 28;
            item.damage = 15;
            item.crit = 2;

            item.knockBack = 2.5f;

            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;

            item.value = Item.buyPrice(silver: 80);

            Item.staff[item.type] = true;
            // A chasm is forming in my leg and is pussing, cannot finish now :/
        }
        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<DustiliteCrystal>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
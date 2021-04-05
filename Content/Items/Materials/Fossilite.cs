using Terraria;

namespace Trelamium2.Content.Items.Materials
{
    public class Fossilite : TrelamiumItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Fossilite");

        public override void SetDefaults() => item.value = Item.buyPrice(silver: 10);
    }
}
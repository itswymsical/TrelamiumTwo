using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Dusts;

namespace TrelamiumTwo.Content.Items.Tools.Luminescent
{
    public class LuminescentPickaxe : TrelamiumItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Luminescent Pickaxe");

        public override void SafeSetDefaults()
        {
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.useTime = 20;
            item.useAnimation = 20;
            item.damage = 10;

            item.knockBack = 2f;
            item.pick = 50;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<LuminescentDust>());
        }
    }
}

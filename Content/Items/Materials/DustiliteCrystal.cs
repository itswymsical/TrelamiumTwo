#region Using directives

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Extensions;

#endregion

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class DustiliteCrystal : ModItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 2);

			item.material = true;
		}
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
			spriteBatch.End();
			spriteBatch.Begin(default, BlendState.Additive, default, default, default, Filters.Scene["TrelamiumTwo:DustifiedCrystalShine"].GetShader().Shader);
			Filters.Scene["TrelamiumTwo:DustifiedCrystalShine"].GetShader().ApplyTime((float)Main.time * 0.02f);
			return true;
		}
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
			spriteBatch.End();
			spriteBatch.Begin(default, BlendState.Additive, default, default, default, Filters.Scene["TrelamiumTwo:DustifiedCrystalShine"].GetShader().Shader);
			Filters.Scene["TrelamiumTwo:DustifiedCrystalShine"].GetShader().ApplyTime((float)Main.time * 0.02f);
			return true;
		}
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
			spriteBatch.End();
			spriteBatch.Begin(default, default, default, default, default, default);
		}
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
			spriteBatch.End();
			spriteBatch.Begin(default, default, default, default, default, default);
		}
    }
}

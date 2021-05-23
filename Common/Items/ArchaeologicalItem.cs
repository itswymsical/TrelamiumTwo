using System.Collections.Generic;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Items
{
	public abstract class ArchaeologicalItem : ModItem
	{
		public sealed override void SetDefaults()
		{
			item.width = item.height = 20;

			SafeSetDefaults();
		}

		protected abstract void SafeSetDefaults();

		public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			SafeModifyTooltips(tooltips);

			tooltips.Add(new TooltipLine(mod, "TrelamiumTwo:ArchaeologicalItem", "Archaeological Item")
			{
				overrideColor = Color.SandyBrown
			});
		}

		protected virtual void SafeModifyTooltips(List<TooltipLine> tooltips) { }
	}
}

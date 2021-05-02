using System.Collections.Generic;
using Terraria.UI;

namespace TrelamiumTwo.Core.Abstracts
{
	public abstract class Cutscene
	{
		public abstract int InsertionIndex(List<GameInterfaceLayer> layers);

		public virtual bool Visible { get; set; } = false;

		public virtual void Draw() { }

		public virtual void ModifyScreenPosition() { }
	}
}

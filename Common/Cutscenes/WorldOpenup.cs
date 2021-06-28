using System.Collections.Generic;
using Terraria.UI;

using TrelamiumTwo.Core.Mechanics;

namespace TrelamiumTwo.Common.Cutscenes
{
	public class WorldOpenup : Cutscene
	{
		public override int InsertionIndex(List<GameInterfaceLayer> layers) => layers.FindIndex(l => l.Name.Equals("Vanilla: Mouse Text"));

		public override void Draw()
		{
			// TODO - Cutscene
		}
	}
}

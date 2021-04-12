#region Using directives

using Terraria.World.Generation;

#endregion

namespace TrelamiumTwo.Core
{
	public static class TrelamiumConditions
	{
		public class IsEmpty : GenCondition
		{
			protected override bool CheckValidity(int x, int y)
				=> !_tiles[x, y].active();
		}

		public class OffsetIsSolid : Conditions.IsSolid
		{
			private readonly int offX, offY;

			public OffsetIsSolid(int x, int y)
			{
				offX = x;
				offY = y;
			}

			protected override bool CheckValidity(int x, int y)
				=> base.CheckValidity(x + offX, y + offY);
		}

		public class OffsetIsEmpty : IsEmpty
		{
			private readonly int offX, offY;

			public OffsetIsEmpty(int x, int y)
			{
				offX = x;
				offY = y;
			}

			protected override bool CheckValidity(int x, int y)
				=> base.CheckValidity(x + offX, y + offY);
		}
	}
}

using TrelamiumTwo.Content.UI;
using TrelamiumTwo.Core.Loaders;
using Microsoft.Xna.Framework;
using Terraria;
using TrelamiumTwo.Core.Abstracts.Interface;

namespace TrelamiumTwo.Core.ModLoadables
{
	public class TrelamiumDetours : ILoadable
	{
		public float Priority => 1f;

		public bool LoadOnDedServer => false;

		public void Load() => Main.OnTick += TrelamiumTwo.Update;

		public void Unload() => Main.OnTick -= TrelamiumTwo.Update;
	}
}

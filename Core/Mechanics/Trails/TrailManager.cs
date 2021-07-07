using System.Collections.Generic;

using Terraria;
using Terraria.ID;

using TrelamiumTwo.Core.Abstraction.Interfaces;

namespace TrelamiumTwo.Core.Mechanics.Trails
{
    public class TrailManager : ILoadable
    {
        public float Priority => 1f;

        public bool LoadOnDedServer => true;

        public static TrailManager Instance { get; private set; }

        public List<Trail> Trails;

        public void Load()
        {
            Trails = new List<Trail>();

            Instance = this;
        }

        public void Unload()
        {
            Trails?.Clear();

            Instance = null;
        }

        public void DrawTrails()
        {
            for (int i = 0; i < Trails.Count; i++)
            {
                Trails[i].Draw();
            }
        }

        public void UpdateTrails()
        {
            for (int i = 0; i < Trails.Count; i++)
            {
                Trails[i].Update();
            }
        }

        public Trail CreateTrail(Trail trail)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return trail;
            }

            Trails.Add(trail);

            return trail;
        }
    }
}

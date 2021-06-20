using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;
using Terraria;

using TrelamiumTwo.Core.Mechanics.Trails;

public class TestTrail : Trail
{
    public TestTrail(Entity entity) => Entity = entity;

    public override void SetVertices()
    {
        if (!Entity.active)
        {
            return;
        }
    }

    public override void Update()
    {
        if (!Entity.active)
        {
            Kill();
        }
    }
}
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;
using Terraria;

using TrelamiumTwo.Core.Mechanics.Trails;

public class TestTrail : Trail
{
    public TestTrail(Entity entity)
    {
        Entity = entity;
        Color = Microsoft.Xna.Framework.Color.Pink;
        Width = 13;
    }
    public override void SetShaders() => SetBasicShader();
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
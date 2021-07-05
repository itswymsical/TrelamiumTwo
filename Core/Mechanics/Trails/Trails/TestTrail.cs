using Microsoft.Xna.Framework;

using Terraria;

using TrelamiumTwo.Core.Mechanics.Trails;

public class TestTrail : Trail
{
    public TestTrail(Entity entity, Color color, int width = 12, int size = 20)
    {
        Entity = entity;
        Color = Color.Pink;
        Color = color;
        Width = width;
        Size = size;
    }
    public override void SetShaders() => SetBasicShader();

    public override void Update()
    {
        if (!Entity.active)
        {
            Kill();
        }
    }
}
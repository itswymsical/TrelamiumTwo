using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using Terraria;

namespace TrelamiumTwo.Core.Mechanics.Trails
{
    public partial class Trail : IDisposable
    {
        public GraphicsDevice Device;

        public BasicEffect BasicEffect { get; protected set; }

        public Effect Effect;

        public Entity Entity;

        public Color Color { get; protected set; } = new Color(255, 255, 255);

        public float Alpha;

        public float Width;
        public int Size { get; protected set; }

        public int PointCount;

        public int CurrentIndex;

        public VertexPositionColorTexture[] Vertices;

        public List<Vector2> Points = new List<Vector2>();

        public Trail()
        {
            Device = Main.graphics.GraphicsDevice;
            BasicEffect = new BasicEffect(Device) { VertexColorEnabled = true };
            SetDefaults();

            Vertices = new VertexPositionColorTexture[Size];
        }

        public void Draw()
        {
            Vertices = new VertexPositionColorTexture[PointCount];
            CurrentIndex = 0;

            SetShaders();

            if (PointCount >= 1)
            {
                foreach (var pass in Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    Device.DrawUserPrimitives(PrimitiveType.TriangleStrip, Vertices, 0, PointCount / 3);
                }
            }
        }

        public virtual void Kill() => TrailManager.Instance.Trails.Remove(this);

        public virtual void SetShaders() => SetBasicShader();

        public virtual void SetDefaults() { }

        public virtual void Update() { }

        public void Dispose() => Effect?.Dispose();
    }
}

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

        public Effect Effect;

        public Entity Entity;

        public Color Color;

        public float Alpha;

        public float Width;

        public int Size;

        public int PointCount;

        public int CurrentIndex;

        public VertexPositionColorTexture[] Vertices;

        public List<Vector2> Points  = new List<Vector2>();

        public Trail()
        {
            Device = Main.graphics.GraphicsDevice;

            SetDefaults();

            Vertices = new VertexPositionColorTexture[Size];
        }

        public void Draw()
        {
            Vertices = new VertexPositionColorTexture[PointCount];
            CurrentIndex = 0;

            SetShaders();
            SetVertices();

            if (PointCount >= 1)
            {
                foreach (var pass in Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    Device.DrawUserPrimitives(PrimitiveType.TriangleList, Vertices, 0, PointCount / 3);
                }
            }
        }

        public virtual void Kill() => TrailManager.Instance.Trails.Remove(this);

        public virtual void SetShaders() => SetBasicShader();

        public virtual void SetVertices() { }

        public virtual void SetDefaults() { }

        public virtual void Update() { }

        public void Dispose() => Effect?.Dispose();
    }
}

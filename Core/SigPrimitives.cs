using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TrelamiumTwo.Core
{
    public class SigPrimitives : IDisposable
    {
        public bool PrimitivesDisposed { get; private set; }
        private readonly GraphicsDevice device;
        private readonly VertexBuffer vertexBuffer;
        private readonly IndexBuffer indexBuffer;

        public SigPrimitives(GraphicsDevice device, int maxV, int maxI)
        {
            this.device = device;
            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionColorTexture), maxV, BufferUsage.None);
            indexBuffer = new IndexBuffer(device, IndexElementSize.SixteenBits, maxI, BufferUsage.None);
        }
        public void RenderPrimitives(Effect effect)
        {
            device.Indices = indexBuffer;
            device.SetVertexBuffer(vertexBuffer);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.LineStrip, default, default, vertexBuffer.VertexCount, default, indexBuffer.IndexCount / 2);
            }
            // TODO: learn C#
        }
        public void Dispose()
        {
            PrimitivesDisposed = true;

            vertexBuffer?.Dispose();
            indexBuffer?.Dispose();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.UI.Chat;

namespace TrelamiumTwo.Helpers
{
    internal static partial class Helper
    {
        public static Vector2 GetMiddleBetween(this Vector2 start, Vector2 end) => (start + end) / 2;
        public static Vector2 TurnRight(this Vector2 vector) => new Vector2(-vector.Y, vector.X);
        public static Vector2 TurnLeft(this Vector2 vector) => new Vector2(vector.Y, -vector.X);
        public static Vector2 ToVector2(this Vector3 vector) => new Vector2(vector.X, vector.Y);
        public static Vector3 ToVector3(this Vector2 vector) => new Vector3(vector.X, vector.Y, 0f);
        public static Vector2 ToDrawPosition(this Vector2 vector) => vector - Main.screenPosition;
        public static Matrix DefaultEffectMatrix
        {
            get
            {
                var device = Main.instance.GraphicsDevice;

                float width = device.Viewport.Width > 0 ? 2f / device.Viewport.Width : 0;
                float height = device.Viewport.Height > 0 ? -2f / device.Viewport.Height : 0;

                return new Matrix
                {
                    M11 = width,
                    M22 = height,
                    M33 = 1,
                    M44 = 1,
                    M41 = -1 - width / 2f,
                    M42 = 1 - height / 2f
                };
            }
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color? color = null, int lineWidth = 1)
        {
            Vector2 dist = end - start;

            float rotation = dist.ToRotation();

            Color lineColor = color ?? Color.White;

            var destRect = new Rectangle((int)start.X, (int)start.Y, (int)dist.Length(), lineWidth);

            spriteBatch.Draw(Main.magicPixel, destRect, null, lineColor, rotation, default, SpriteEffects.None, 0);
        }
        public static void DrawTriangle(Vector2[] vertices, Color? color = null)
        {
            GraphicsDevice device = Main.graphics.GraphicsDevice;

            var basicEffect = Main.dedServ ? null : new BasicEffect(device)
            {
                VertexColorEnabled = true,
                View = DefaultEffectMatrix
            };

            if (basicEffect is null)
            {
                return;
            }

            VertexPositionColor[] points = new VertexPositionColor[3];

            for (int i = 0; i < 3; i++)
            {
                points[i] = new VertexPositionColor(vertices[i].ToVector3(), color ?? Color.White);
            }

            device.SetVertexBuffer(null);

            var vertexBuffer = new VertexBuffer(device, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData(points);

            device.SetVertexBuffer(vertexBuffer);

            foreach (var pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
            }   
        }
        public static void DrawTriangle(Texture2D texture, Vector2[] vertices, Vector2[] texCoords)
        {
            GraphicsDevice device = Main.graphics.GraphicsDevice;

            var basicEffect = Main.dedServ ? null : new BasicEffect(device)
            {
                TextureEnabled = true,
                Texture = texture,
                View = DefaultEffectMatrix
            };

            if (basicEffect is null)
            {
                return;
            }

            VertexPositionTexture[] points = new VertexPositionTexture[3];

            for (int i = 0; i < 3; i++)
            {
                points[i] = new VertexPositionTexture(vertices[i].ToVector3(), texCoords[i]);
            }

            device.SetVertexBuffer(null);

            var vertexBuffer = new VertexBuffer(device, typeof(VertexPositionTexture), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData(points);

            device.SetVertexBuffer(vertexBuffer);

            foreach (var pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
            }
        }
        public static void DrawText(SpriteBatch spriteBatch, Vector2 position, string text, Color? color = null, Color? shadowColor = null, float scale = 1f)
        {
            var font = Main.fontDeathText;

            var textSize = font.MeasureString(text) * scale;
            var textPosition = position - textSize / 2f;

            Color drawTextColor = color ?? Color.White;
            Color shadowTextColor = shadowColor ?? Color.Black;

            shadowTextColor.A = drawTextColor.A;

            ChatManager.DrawColorCodedStringShadow(spriteBatch, font, text, textPosition, shadowTextColor, 0f, default, new Vector2(scale));
            ChatManager.DrawColorCodedString(spriteBatch, font, text, textPosition, drawTextColor, 0f, default, new Vector2(scale));
        }
        public static void DrawText(SpriteBatch spriteBatch, Color color, string text, Vector2 position, float scale = 1f)
        {
            var font = Main.fontDeathText;

            var textSize = font.MeasureString(text) * scale;
            var textPosition = position - textSize / 2f;

            Color shadowColor = Color.Black;
            shadowColor.A = color.A;

            ChatManager.DrawColorCodedStringShadow(spriteBatch, font, text, textPosition, shadowColor, 0f, default, Vector2.One * scale);
            ChatManager.DrawColorCodedString(spriteBatch, font, text, textPosition, color, 0f, default, Vector2.One * scale);
        }
        public static void DrawTextCollumn(SpriteBatch spriteBatch, Color color, string text, ref Vector2 position, float scale = 1f, float spacement = 5f)
        {
            var font = Main.fontDeathText;

            var textSize = font.MeasureString(text) * scale;
            var textPosition = position - textSize / 2f;
            position.Y += textSize.Y + spacement;

            // Maybe we could use DrawText here?

            Color shadowColor = Color.Black;
            shadowColor.A = color.A;

            ChatManager.DrawColorCodedStringShadow(spriteBatch, font, text, textPosition, shadowColor, 0f, default, Vector2.One * scale);
            ChatManager.DrawColorCodedString(spriteBatch, font, text, textPosition, color, 0f, default, Vector2.One * scale);
        }
    }
}

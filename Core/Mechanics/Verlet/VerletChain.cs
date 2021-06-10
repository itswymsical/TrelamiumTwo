using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Core.Mechanics.Verlet
{
    public class VerletChain
    {
        public List<VerletPoint> Points;
        public List<VerletStick> Sticks;

        public const float FRICTION = 0.999f;
        public const float GRAVITY = 0.3f;

        public VerletChain(List<VerletPoint> points, List<VerletStick> sticks)
        {
            Points = points;
            Sticks = sticks;
        }

        public void Kill() => VerletManager.Instance.Chains.Remove(this);

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawSticks(spriteBatch);
            DrawPoints(spriteBatch);
        }

        public void Update()
        {
            UpdatePoints();
            UpdateSticks();
        }

        private void UpdatePoints()
        {
            if (Main.gameInactive)
            {
                return;
            }

            for (int i = 0; i < Points.Count; i++)
            {
                VerletPoint point = Points[i];

                Vector2 distance = point.Position - point.OldPosition;
                Vector2 velocity = distance * FRICTION;

                point.OldPosition = point.Position;

                if (!point.Pinned)
                {
                    point.Velocity = velocity;

                    point.Position += point.Velocity;
                    point.Position.Y += GRAVITY;
                }
            }
        }

        private void UpdateSticks()
        {
            for (int i = 0; i < Sticks.Count; i++)
            {
                VerletStick stick = Sticks[i];

                Vector2 distance = stick.End.Position - stick.Start.Position;

                float length = (float)Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                float difference = stick.Length - length;

                float percent = difference / length / 2;

                Vector2 offset = distance * percent;

                if (!stick.Start.Pinned)
                {
                    stick.Start.Position -= offset;
                }

                if (!stick.End.Pinned)
                {
                    stick.End.Position += offset;
                }
            }
        }

        private void DrawPoints(SpriteBatch spriteBatch)
        {
            Texture2D texture = ModContent.GetTexture("TestMod/Assets/Point");

            for (int i = 0; i < Points.Count; i++)
            {
                var point = Points[i];

                spriteBatch.Draw(texture, point.Position.ToDrawPosition() - texture.Size() / 2f, Color.White);
            }
        }

        private void DrawSticks(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Sticks.Count; i++)
            {
                var stick = Sticks[i];

                spriteBatch.DrawLine(stick.Start.Position.ToDrawPosition(), stick.End.Position.ToDrawPosition(), null, 2);
            }
        }
    }
}

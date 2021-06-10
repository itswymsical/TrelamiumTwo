using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using TrelamiumTwo.Core.Abstraction.Interfaces;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Core.Mechanics.Particles
{
    public class ParticleManager : ILoadable
    {
        public float Priority => 1f;

        public bool LoadOnDedServer => true;

        public static ParticleManager Instance { get; private set; }

        public int MaxParticles { get; private set; } = 200;

        public static List<Particle> Particles;

        public void Load()
        {
            Instance = this;

            Particles = new List<Particle>();
        }

        public void Unload()
        {
            Particles?.Clear();

            Instance = null;
        }

        public void SpawnParticle(Particle particle)
        {
            if (Particles.Count >= MaxParticles)
            {
                return;
            }

            particle.SpawnPosition = Main.screenPosition;

            Particles.Add(particle);
        }

        public void KillParticle(Particle particle) => Particles.Remove(particle);

        public void UpdateParticles()
        {
            if (Main.gameInactive)
            {
                return;
            }

            for (int i = 0; i < Particles.Count; i++)
            {
                Particle particle = Particles[i];

                particle.Position += particle.Velocity;

                particle.Update();
            }
        }

        public void DrawParticles(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particle particle = Particles[i];

                Vector2 drawPosition = particle.Parallax ? particle.Position - Vector2.Lerp(Main.screenPosition, Main.screenPosition - 2 * (particle.SpawnPosition - Main.screenPosition), particle.Scale) : particle.Position.ToDrawPosition();

                Vector2 origin = particle.Texture.Size() / 2f;

                Color color = particle.Color * particle.Alpha;

                Vector2 scale = particle.Scale * Main.GameViewMatrix.Zoom;

                spriteBatch.Draw(particle.Texture, drawPosition, null, color, particle.Rotation, origin, scale, SpriteEffects.None, 0f);

                particle.Draw(spriteBatch);
            }
        }
    }
}

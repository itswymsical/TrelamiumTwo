using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace TrelamiumTwo.Core.Mechanics.Particles
{
    public class Particle
    {
        public Vector2 SpawnPosition;

        public Vector2 Position;

        public Vector2 Velocity = Vector2.Zero;

        public Color Color = Color.White;

        public float Rotation;

        public float Scale = 1f;

        public float Alpha = 1f;

        public Particle() => SetDefaults();

        public virtual Texture2D Texture
        {
            get
            {
                string texturePath = GetType().FullName.Replace(".", "/");

                return ModContent.Request<Texture2D>(texturePath);
            }
        }

        public virtual void Kill() => ParticleManager.Instance.KillParticle(this);

        public virtual bool Parallax => false;

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void SetDefaults() { }
    }
}

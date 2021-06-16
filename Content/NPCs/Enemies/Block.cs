using Microsoft.Xna.Framework;

using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.NPCs.Enemies
{
    public class Block : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Block");
        }

        public override void SetDefaults()
        {
            npc.noTileCollide = false;
            npc.noGravity = true;

            npc.width = npc.height = 200;

            npc.aiStyle = -1;
            aiType = -1;
        }

        private bool firstTick;

        private bool slamming;

        private Vector2 spawnPosition;

        public override void AI()
        {
            if (!firstTick)
            {
                spawnPosition = npc.Center;

                firstTick = true;
            }

            if (!slamming)
            {
                npc.ai[0]++;

                if (npc.position.Y > spawnPosition.Y)
                {
                    npc.velocity.Y -= 0.1f;
                }
                else
                {
                    npc.velocity.Y *= 0.8f;
                }

                if (npc.ai[0] > 300)
                {
                    slamming = true;

                    npc.ai[0] = 0;
                }
            }
            else
            {
                npc.ai[0] += 0.02f;

                npc.velocity.Y += npc.ai[0];
            }
            if (npc.collideY && npc.position.Y == npc.oldPosition.Y)
            {
                Collision.HitTiles(npc.position, npc.velocity, npc.width, npc.height);

                npc.ai[0] = 0;

                slamming = !slamming;

                Main.player[npc.target].GetModPlayer<TrelamiumPlayer>().shakeEffects = Math.Abs(npc.velocity.Y * 2f);
            }
        }
    }
}
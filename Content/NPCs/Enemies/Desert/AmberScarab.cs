using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.Enemies.Desert
{
    public class AmberScarab : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amber Scarab");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 22;

            npc.damage = 14;
            npc.lifeMax = 50;
            npc.defense = 2;
            npc.aiStyle = -1;

            npc.knockBackResist = 0.75f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(copper: 20);
        }
        public override void NPCLoot()
        {
            if (Main.rand.NextBool(2))
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Amber, Main.rand.Next(1, 3));
        }
        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);
            if (npc.ai[0] == 0f)
            {
                npc.TargetClosest(true);
                Vector2 speed1 = Vector2.Normalize(player.Center - npc.Center) * 2.5f;
                npc.velocity.X = speed1.X;
                npc.ai[1]++;
                if (npc.ai[1] >= 8f * 60f)
                {
                    if (player.WithinRange(npc.Center, 6f * 16f))
                        npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                }
            }
            if (npc.velocity.Y >= 0f)
                Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY, 1, false, 1);
        }
        public override void FindFrame(int frameHeight)
        {
            if (++npc.frameCounter > 2)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[npc.type]);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.DesertCave.Chance * 0.305f;
    }
}
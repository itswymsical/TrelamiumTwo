using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Helpers.Extensions;

namespace TrelamiumTwo.Content.NPCs.Enemies.DruidsGarden
{
    public class BlossomSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blossom Slime");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 32;

            npc.damage = 18;
            npc.lifeMax = 45;
            npc.defense = 2;
            npc.aiStyle = 1;

            npc.knockBackResist = 0.75f;

            animationType = NPCID.BlueSlime;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(copper: 20);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                for (int i = 0; i < damage / npc.lifeMax * 100f; ++i)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 4, hitDirection, -1f, npc.alpha, Microsoft.Xna.Framework.Color.Green, npc.scale);
                    Dust.NewDust(npc.position, npc.width, npc.height, 4, hitDirection, -1f, npc.alpha, Microsoft.Xna.Framework.Color.Pink, npc.scale);
                }
                return;
            }
            for (int i = 0; i < 50; ++i)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 4, hitDirection, -2f, npc.alpha, Microsoft.Xna.Framework.Color.Green, npc.scale);
                Dust.NewDust(npc.position, npc.width, npc.height, 4, hitDirection, -2f, npc.alpha, Microsoft.Xna.Framework.Color.Pink, npc.scale);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (player.ZoneNoEvent())
            {
                return spawnInfo.player.GetModPlayer<TrelamiumPlayer>().ZoneDruidsGarden ? 1.0f : 0f;
            }
            return 0f;
        }
        public override void AI() 
            => npc.direction = -npc.spriteDirection;
    }
}
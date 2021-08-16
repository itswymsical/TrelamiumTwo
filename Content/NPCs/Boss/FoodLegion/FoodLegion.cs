using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;

using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Boss.Fungore;
using TrelamiumTwo.Content.Items.Consumable.Food;
using TrelamiumTwo.Content.Projectiles.Melee;
using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Content.NPCs.Boss.FoodLegion
{
    public class FoodLegion : ModNPC
    {
        private enum States
        {
            Walking,
            Punching,
            Jumping,
            SuperJumping
        }

        private States State
        {
            get => (States)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
        }

        private float AttackCooldown
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        private Player player;

        private int frameY;
        private int frameX;
        private int punchDirection;

        public override string Texture => Assets.NPCs.FoodLegion + "FoodLegion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Food Legion");

            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.lavaImmune = true;

            NPC.width = NPC.height = 94;

            NPC.lifeMax = 5000;
            NPC.defense = 16;
            NPC.damage = 22;

            NPC.knockBackResist = 0f;

            NPC.aiStyle = -1;
            aiType = -1;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            //bossBag = ModContent.ItemType<FungoreBag>();
            NPC.value = Item.buyPrice(gold: 1);
        }
        public override void AI()
        {
            base.AI();
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = State == States.Punching ? punchDirection : NPC.direction;

            NPC.frame.Width = 94;
            NPC.frame.Height = 96;
            NPC.frameCounter++;

            int frameRate = 4;

            if (NPC.frameCounter > frameRate)
            {
                frameY++;

                NPC.frameCounter = 0;
            }

            frameX = State == States.Walking ? 0 : State == States.Punching ? 1 : State == States.Jumping ? 2 : 3;

            if (State == States.Walking && frameY > 7)
            {
                frameY = 0;
            }
            NPC.frame.Y = frameY * frameHeight;
            NPC.frame.X = frameX * NPC.frame.Width;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f * bossLifeScale);
            float damage = NPC.damage;
            NPC.damage += (int)(damage * .15f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
            int foodChoice = Main.rand.Next(5);

            if (foodChoice == 0)
                Item.NewItem(NPC.Hitbox, ModContent.ItemType<Apple>(), Main.rand.Next(4));
            if (foodChoice == 1)
                Item.NewItem(NPC.Hitbox, ModContent.ItemType<Banana>(), Main.rand.Next(4));
            if (foodChoice == 2)
                Item.NewItem(NPC.Hitbox, ModContent.ItemType<Onion>(), Main.rand.Next(3));
            if (foodChoice == 3)
                Item.NewItem(NPC.Hitbox, ModContent.ItemType<Elderberry>(), Main.rand.Next(3));
            if (foodChoice == 4)
                Item.NewItem(NPC.Hitbox, ModContent.ItemType<Carrot>(), Main.rand.Next(3));

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData);
            }
        }
    }
}
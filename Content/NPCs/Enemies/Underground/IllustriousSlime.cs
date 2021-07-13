using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.NPCs.Enemies.Underground
{
    public class IllustriousSlime : ModNPC
    {
        public override string Texture => Assets.NPCs.Underground + "IllustriousSlime";
        public override void SetStaticDefaults() => Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueSlime];
        
        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 40;

            npc.damage = 17;
            npc.lifeMax = 68;
            npc.defense = 9;
            npc.aiStyle = 1;

            npc.alpha = 100;
            aiType = NPCID.BlueSlime;
            npc.knockBackResist = 0.45f;

            animationType = NPCID.BlueSlime;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(silver: 2, copper: 80);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return base.PreDraw(spriteBatch, drawColor);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            int alpha = (int)npc.ai[1] / 4;
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (npc.spriteDirection == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            
            Color color = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
            Color whiteAlpha = new Color(255, 0, 0, alpha / 2);

            Texture2D text = Main.npcTexture[npc.type];
            Texture2D textGlow = mod.GetTexture(Texture + "_Glow");

            Vector2 drawOrigin = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
            Vector2 rot = npc.rotation.ToRotationVector2();

            Rectangle rectangle = new Rectangle(0, 0, text.Width, text.Height);
            Vector2 origin2 = rectangle.Size() / 2f;

            int i = 1;
            int num = 1;
            float value3 = 0.5f;
            float num2 = 0f;
            int num3 = num;

            while ((i > 0 && num3 < 0) || (i < 0 && num3 > 0))
            {
                npc.GetAlpha(color);
                if (!(npc.oldPos[num3] == Vector2.Zero))
                {
                    float num161 = num3 / num;
                    if (num161 < 0.5f)
                    {
                        color = Color.Lerp(color, whiteAlpha, Utils.InverseLerp(0f, 0.5f, num161, false));
                        goto IL_65CC;
                    }
                    color = Color.Lerp(whiteAlpha, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), Utils.InverseLerp(0.5f, 1f, num161, false));
                    goto IL_65CC;
                }
                else
                {
                    goto IL_65CC;
                }
            IL_65B4:
                num3 += i;
                continue;
            IL_65CC:

                float i2 = 0 - num3;

                if (i < 0)
                    i2 = num - num3;

                color *= i2 / NPCID.Sets.TrailCacheLength[npc.type] * 1.5f;

                Vector2 value4 = npc.oldPos[num3];
                float num163 = npc.rotation;
                SpriteEffects effects = spriteEffects;

                Main.spriteBatch.Draw(text, value4 + npc.Size / 2f - Main.screenPosition + new Vector2(0f, npc.gfxOffY), 
                    new Rectangle?(rectangle), color, num163 + npc.rotation * num2 * (num3 - 1) * -spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, MathHelper.Lerp(npc.scale, value3, num3 / 15f), effects, 0f);
                goto IL_65B4;
            }

            for (int i3 = 0; i3 < 4; i3++)
            {
                spriteBatch.Draw(textGlow, drawOrigin + rot.RotatedBy(1.57079637f * i3, default) * 4f, new Rectangle?(rectangle), whiteAlpha, npc.rotation, origin2, npc.scale, spriteEffects, 0f);
            }
            
            spriteBatch.Draw(text, drawOrigin, new Rectangle?(rectangle), npc.GetAlpha(color), npc.rotation, origin2, npc.scale, spriteEffects, 0f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.Underground.Chance * 0.26f;
        public override void NPCLoot() => Item.NewItem(npc.getRect(), ModContent.ItemType<IllustriousShard>(), Main.rand.Next(3));  
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
                for (int i = 1; i <= 60; i++)
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.t_Slime, hitDirection, hitDirection, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
        }
    }

    public class IllustriousShard : ModItem
    {
        public override string Texture => Assets.PlaceholderTexture;
        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = ItemRarityID.Expert;
            item.value = Item.sellPrice(silver: 9);
        }
    }
}
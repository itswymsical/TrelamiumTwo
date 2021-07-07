using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Common.Globals
{
    public class GlobalTrelamiumNPC : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Antlion)
                if (Main.rand.Next(2) == 0)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Content.Items.Materials.AntlionChitin>(), Main.rand.Next(2));

            if (npc.type == NPCID.WalkingAntlion)
                if (Main.rand.Next(2) == 0)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Content.Items.Materials.AntlionChitin>(), Main.rand.Next(2));
                else
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Content.Items.Materials.HardenedCarapace>(), Main.rand.Next(2));

            if (npc.type == NPCID.FlyingAntlion)
                if (Main.rand.Next(2) == 0)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Content.Items.Materials.AntlionChitin>(), Main.rand.Next(2));

            if (npc.type == NPCID.Tumbleweed)
                Item.NewItem(npc.getRect(), ModContent.ItemType<Content.Items.Materials.AridFiber>(), Main.rand.Next(3));

            if (npc.type == NPCID.Zombie)
                Item.NewItem(npc.getRect(), ItemID.Leather, Main.rand.Next(2));

            if (npc.type == NPCID.ArmedZombie)
                Item.NewItem(npc.getRect(), ItemID.Leather, Main.rand.Next(2));
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
          if (projectile.type == ModContent.ProjectileType<Content.Projectiles.Typeless.NutGrabberProjectile>() && npc.life <= 0)
            {
                Item.NewItem(npc.position, ModContent.ItemType<Content.Items.Materials.Nut>(), 2);
            }
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(BuffID.Bleeding))
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 14;
                if (damage < 1)
                {
                    damage = 1;
                }
            }
            if (Main.LocalPlayer.GetModPlayer<Players.BuffPlayer>().solarAura)
            {
                if (npc.HasBuff(BuffID.OnFire))
                {
                    if (npc.lifeRegen > 0)
                        npc.lifeRegen = 0;
                    
                    npc.lifeRegen -= 7;
                    if (damage < 2)
                        damage = 2;
                }
                if (npc.HasBuff(BuffID.CursedInferno))
                {
                    if (npc.lifeRegen > 0)
                        npc.lifeRegen = 0;

                    npc.lifeRegen -= 8;
                    if (damage < 3)
                        damage = 3;
                }
                if (npc.HasBuff(BuffID.ShadowFlame))
                {
                    if (npc.lifeRegen > 0)
                        npc.lifeRegen = 0;

                    npc.lifeRegen -= 8;
                    if (damage < 6)
                        damage = 6;
                }
            }
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(BuffID.Bleeding))
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height - 4, 5, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 1f);
                    Main.dust[dust].scale = 1.3f;
                    Main.dust[dust].noGravity = true;
                }
                Lighting.AddLight(npc.position, 0f, 0.25f, 0f);
            }
        }
    }
}
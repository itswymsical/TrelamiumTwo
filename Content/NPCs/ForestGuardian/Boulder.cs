﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.ForestGuardian
{
    public class Boulder : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boulder");
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 10;
            npc.damage = 10;
            npc.defense = int.MaxValue;

            npc.width = 36;
            npc.height = 34;

            npc.aiStyle = -1;
            npc.knockBackResist = 0f;

            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            npc.netUpdate = true;

            npc.HitSound = SoundID.Item50;
            npc.DeathSound = SoundID.DD2_WitherBeastCrystalImpact;
        }
        float charge = 0f;
        public override bool CheckActive()
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override bool PreAI()
        {
            Player target = Main.player[npc.target];
            if (!target.active || target.dead)
            {
                npc.TargetClosest(false);
                if (!target.active || target.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 150)
                    {
                        npc.timeLeft = 150;
                    }
                }
            }
            else if (npc.timeLeft > 1800)
                npc.timeLeft = 1800;
            
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
                npc.TargetClosest();
            
            npc.ai[3] += 1;
            if (Main.rand.Next(2) == 0)
            {
                npc.ai[3] += 2;
            }
            if (npc.ai[2] <= 100)
            {
                npc.ai[2]++;
            }
            double deg = npc.ai[1];
            double rad = deg * (Math.PI / 360);
            double dist = npc.ai[2];
            NPC parent = Main.npc[(int)npc.ai[0]];
            npc.position.X = parent.Center.X - (int)(Math.Cos(rad) * dist) - npc.width / 4;
            npc.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * dist) - npc.height / 4;
            if (charge <= 5f)
            {
                charge += 0.02f;
            }
            if (parent.life <= 0 || !parent.active)
            {
                npc.active = false;
                npc.life = 0;
                npc.checkDead();
                npc.HitEffect();
            }
            npc.rotation += .01f;
            npc.ai[1] += charge;
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int loop = 0; loop < 25; loop++)
                {
                    int DustIndex = Dust.NewDust(npc.Center, 0, 0, 0, 0f, 0f, 0, default, 1.25f);
                    Dust dust = Main.dust[DustIndex];
                    dust.velocity *= 1.6f;
                    Dust dust2 = Main.dust[DustIndex];
                    dust2.velocity.Y -= dust2.velocity.Y - 1f;
                    Main.dust[DustIndex].position = Vector2.Lerp(Main.dust[DustIndex].position, npc.Center, 1.25f);
                }
                Projectile.NewProjectile(npc.position, new Vector2(0, 1.5f), 
                    ModContent.ProjectileType<Projectiles.Hostile.BoulderProjectile>(), 20, 2f, Main.myPlayer, 0f, 0f);
            }
        }
    }
}
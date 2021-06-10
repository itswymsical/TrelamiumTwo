using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace TrelamiumTwo.Common.Items
{
    public abstract class ShovelItem : ModItem
    {
        public bool Autosize;
        public override bool CloneNewInstances => false;    
        public void DiggingPower(int digPower)
        {
            item.GetGlobalItem<GlobalTrelamiumItem>().Shovel = true;
            item.GetGlobalItem<GlobalTrelamiumItem>().digPower = digPower;
            item.GetGlobalItem<GlobalTrelamiumItem>().radius = 6;
            return;
        }
        public void DigTile(Player player, int rangeinBlocks)
        {
            if (player.Distance(Main.MouseWorld) < 16 * rangeinBlocks)
            {
                player.GetModPlayer<Players.TrelamiumPlayer>().ShovelPickTile((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y);
            }
        }
        public override bool UseItem(Player player)
        {
            DigTile(player, 5);
            return true;
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return rand.Next(new int[] { PrefixID.Agile, PrefixID.Quick, PrefixID.Light, PrefixID.Slow, PrefixID.Sluggish, PrefixID.Lazy, PrefixID.Large });
        }

        public override void SetDefaults() 
            => SafeSetDefaults();        

        public virtual void SafeSetDefaults() { }
    }
}
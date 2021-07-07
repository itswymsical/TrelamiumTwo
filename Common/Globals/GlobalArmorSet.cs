using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Globals
{
    public class ArmorGlobalItem : GlobalItem
    {
        //Remove any IsArmorSet and UpdateArmorSet hooks from the associated armor pieces, and add this class somewhere in your mod. replace the vanilla item types with your own ones

        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ItemID.CactusHelmet && body.type == ItemID.CactusBreastplate && legs.type == ItemID.CactusLeggings)
            {
                return "T2:Cactus";
            }

            return base.IsArmorSet(head, body, legs);
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            if (set == "T2:Cactus")
            {
                player.thorns += 0.1f;
                player.setBonus = "Enemies take 10% contact damage";
            }
        }
    }
}
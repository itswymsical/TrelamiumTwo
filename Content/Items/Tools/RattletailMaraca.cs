using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Tools
{
	public class RattletailMaraca : ArchaeologicalItem 
	{
		private int cooldown = 0;
		private const int MaxCooldown = 3200;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Transforms the player into a Rattletail snake." +
				"\nClicking automatically defaults to spitting venom.");
		}
		protected override void SafeSetDefaults()
		{
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 70);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingUp;

			item.noMelee = true;

			item.buffTime = 900;
			item.buffType = ModContent.BuffType<Buffs.SlitheringGrace>();
			item.accessory = true;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Maraka");
		}
		// TODO: Make the Rattletail Maraca transform the player into a snake that can only run fast. Clicking will fire a venom spit projectile.
		public override bool CanUseItem(Player player)
			=> cooldown <= 0;

		public override bool UseItem(Player player)
		{
			cooldown = MaxCooldown;
			return true;
		}

		public override void UpdateInventory(Player player)
		{
			if (cooldown > 0)
			{
				cooldown--;
			}
		}
	}
    public class RattlesnakeHead : EquipTexture
    {
        public override bool DrawHead()
        {
            return false;
        }
    }

    public class RattlesnakeBody : EquipTexture
    {
        public override bool DrawBody()
        {
            return false;
        }
    }

    public class RattlesnakeLeg : EquipTexture
    {
        public override bool DrawLegs()
        {
            return false;
        }
    }
}

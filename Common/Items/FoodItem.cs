using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Utilities;

namespace TrelamiumTwo.Common.Items
{
    public abstract class FoodItem : ModItem
    {
        public bool Autosize;
        public override bool CloneNewInstances => false;
        public override void SetDefaults()
        {
            if (Autosize)
                item.Autosize();

            SafeSetDefaults();
        }

        public virtual void SafeSetDefaults() { }
    }
}
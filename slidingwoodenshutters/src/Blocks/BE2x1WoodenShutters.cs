using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

namespace slidingwoodenshutters.Blocks
{
    internal class BE2x1WoodenShutters : BlockEntity
    {
        public void Toggle(IPlayer byPlayer)
        {
            MarkDirty();

            string side = Block.Variant["side"];
            string status = Block.Variant["status"];
            string state = Block.Variant["state"];
            string wood = Block.Variant["wood"];
            string updatedStatus = (status == "closed") ? "opened" : "closed";
            string newBlockCode = $"slidingwoodenshutters:2x1woodenshutters-{side}-{updatedStatus}-{state}-{wood}";

            Block newBlock = Api.World.BlockAccessor.GetBlock(newBlockCode);
            Api.World.BlockAccessor.SetBlock(0, Pos);
            AssetLocation son = new AssetLocation("game:sounds/block/door");
            Api.World.PlaySoundAt(son, Pos.X + 0.5f, Pos.InternalY + 0.5f, Pos.Z + 0.5f, byPlayer, EnumSoundType.Sound, 1);
            if (newBlock != null)
            {
                Api.World.BlockAccessor.SetBlock(newBlock.BlockId, Pos);
            }
        }
    }
}

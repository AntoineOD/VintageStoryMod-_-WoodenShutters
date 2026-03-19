using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.MathTools;
using Vintagestory.API.Common;

namespace slidingwoodenshutters.Blocks
{
    internal class _1x1BlockWoodenShutters : Block
    {
        public override void OnBlockPlaced(IWorldAccessor world, BlockPos blockPos, ItemStack byItemStack = null)
        {
            base.OnBlockPlaced(world, blockPos, byItemStack);
        }


        public override void OnBlockBroken(IWorldAccessor world, BlockPos pos, IPlayer byPlayer, float dropQuantityMultiplier = 1)
        {
            base.OnBlockBroken(world, pos, byPlayer, dropQuantityMultiplier);
        }

        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            var be = world.BlockAccessor.GetBlockEntity(blockSel.Position) as BE1x1WoodenShutters;
            be.Toggle(byPlayer);
            world.BlockAccessor.MarkBlockEntityDirty(blockSel.Position);
            return true;
        }
        public override int GetRetention(BlockPos pos, BlockFacing facing, EnumRetentionType type)
        {
            string status;
            if (!Variant.TryGetValue("status", out status))
            {
                return base.GetRetention(pos, facing, type);
            }
            return status == "closed" ? 3 : 0;
        }
    }
}

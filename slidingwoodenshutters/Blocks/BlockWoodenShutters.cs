//Here are the imports for this script. Most of these will add automatically.
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
namespace slidingwoodenshutters.Blocks
{

    internal class BlockWoodenShutters : Block
    {


        public override void OnBlockPlaced(IWorldAccessor world, BlockPos blockPos, ItemStack byItemStack = null)
        {
            api.Logger.Event("Shutter Block Placed!");
            base.OnBlockPlaced(world, blockPos, byItemStack);          
        }


        public override void OnBlockBroken(IWorldAccessor world, BlockPos pos, IPlayer byPlayer, float dropQuantityMultiplier = 1)
        {
            api.Logger.Event("Shutter Block Broken!");
            base.OnBlockBroken(world, pos, byPlayer, dropQuantityMultiplier);
        }

        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            api.Logger.Event("Shutter Block Is toggled");
            var be = world.BlockAccessor.GetBlockEntity(blockSel.Position) as BEWoodenShutters;
            be.Toggle();
            world.BlockAccessor.MarkBlockEntityDirty(blockSel.Position);
            return true;
        }

       public override Cuboidf[] GetCollisionBoxes(IBlockAccessor blockAccessor, BlockPos pos)
        {
            Block block = blockAccessor.GetBlock(pos);
            string side = block.Variant?["side"] ?? "north";  // Default to north if missing
            string half = block.Variant?["state"] ?? "left";
            Cuboidf box = new Cuboidf(0, 0, 0, 1, 1, 0.125f);

            switch (side)
            {
                case "south":
                    box = box.RotatedCopy(0, 180, 0, new Vec3d(0.5f, 0, 0.5f));
                    break;
                case "east":
                    box = box.RotatedCopy(0, 270, 0, new Vec3d(0.5f, 0, 0.5f));
                    break;
                case "west":
                    box = box.RotatedCopy(0, 90, 0, new Vec3d(0.5f, 0, 0.5f));
                    break;
            }

            return new[] { box };
        }

        public override Cuboidf[] GetSelectionBoxes(IBlockAccessor blockAccessor, BlockPos pos)
        {
            return GetCollisionBoxes(blockAccessor, pos);
        }

        public override bool CanPlaceBlock(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel, ref string failureCode)
        {
            string side = this.Variant?["side"] ?? "north";
            BlockPos pos = blockSel.Position;
            BlockPos adjacentPos;
            switch (side)
            {
                case "north":
                    adjacentPos = pos.EastCopy();
                    break;
                case "south":
                    adjacentPos = pos.WestCopy();
                    break;
                case "east":
                    adjacentPos = pos.SouthCopy();
                    break;
                case "west":
                    adjacentPos = pos.NorthCopy();
                    break;
                default:
                    adjacentPos = pos;
                    break;
            }
            Block adjacentBlock = world.BlockAccessor.GetBlock(adjacentPos);
            if (!adjacentBlock.IsReplacableBy(this))
            {
                api.Logger.Event("Can't place block here!");
                return false;
            }
            else
            {
                return base.CanPlaceBlock(world, byPlayer, blockSel, ref failureCode);
            }
        }
    }
}
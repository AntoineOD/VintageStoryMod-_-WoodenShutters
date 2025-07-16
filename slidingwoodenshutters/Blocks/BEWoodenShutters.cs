using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Client;
using Newtonsoft.Json.Converters;

namespace slidingwoodenshutters.Blocks
{
    public class BEWoodenShutters : BlockEntity
    {

        public void Toggle()
        {
            MarkDirty();

            string state = Block.Variant["state"];
            string side = Block.Variant["side"];
            string wood = Block.Variant["wood"];

            string updatedState = (state == "right") ? "left" : "right";
            string newBlockCode = $"slidingwoodenshutters:woodenshutters-{side}-{updatedState}-{wood}";


            Block newBlock = Api.World.BlockAccessor.GetBlock(newBlockCode);

            BlockPos orientationSwitch = Pos;
            //Remove block with air.
            Api.World.BlockAccessor.SetBlock(0, Pos);
            if (state == "left")
            {
                switch (side)
                {
                    case "south":
                        orientationSwitch = orientationSwitch.Offset(BlockFacing.WEST);
                        break;
                    case "east":
                        orientationSwitch = orientationSwitch.Offset(BlockFacing.SOUTH);
                        break;
                    case "west":
                        orientationSwitch = orientationSwitch.Offset(BlockFacing.NORTH);
                        break;
                    case "north":
                        orientationSwitch = orientationSwitch.Offset(BlockFacing.EAST);
                        break;
                }
            }
            else
            {
                switch (side)
                {
                    case "south":
                        orientationSwitch = orientationSwitch.Offset(BlockFacing.EAST);
                        break;
                    case "east":
                        orientationSwitch = orientationSwitch.Offset(BlockFacing.NORTH);
                        break;
                    case "west":
                        orientationSwitch = orientationSwitch.Offset(BlockFacing.SOUTH);
                        break;
                    case "north":
                        orientationSwitch = orientationSwitch.Offset(BlockFacing.WEST);
                        break;
                }
            }
            if (newBlock != null)
            {
                Api.World.BlockAccessor.SetBlock(newBlock.BlockId, orientationSwitch);
            }
        }
    }
}

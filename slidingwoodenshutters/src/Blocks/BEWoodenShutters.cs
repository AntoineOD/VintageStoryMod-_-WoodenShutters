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
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace slidingwoodenshutters.Blocks
{
    public class BEWoodenShutters : BlockEntity
    {

        public void Toggle(IPlayer byPlayer)
        {
            MarkDirty();

            string state = Block.Variant["state"];
            string side = Block.Variant["side"];
            string wood = Block.Variant["wood"];

            string updatedState = (state == "right") ? "left" : "right";
            string newBlockCode = $"slidingwoodenshutters:woodenshutters-{side}-{updatedState}-{wood}";

            Block newBlock = Api.World.BlockAccessor.GetBlock(newBlockCode);
            BlockPos orientationSwitch = Pos.Copy();

            if (state == "left")
            {
                switch (side)
                {
                    case "south": orientationSwitch = orientationSwitch.Offset(BlockFacing.WEST); break;
                    case "east": orientationSwitch = orientationSwitch.Offset(BlockFacing.SOUTH); break;
                    case "west": orientationSwitch = orientationSwitch.Offset(BlockFacing.NORTH); break;
                    case "north": orientationSwitch = orientationSwitch.Offset(BlockFacing.EAST); break;
                }
            }
            else
            {
                switch (side)
                {
                    case "south": orientationSwitch = orientationSwitch.Offset(BlockFacing.EAST); break;
                    case "east": orientationSwitch = orientationSwitch.Offset(BlockFacing.NORTH); break;
                    case "west": orientationSwitch = orientationSwitch.Offset(BlockFacing.SOUTH); break;
                    case "north": orientationSwitch = orientationSwitch.Offset(BlockFacing.WEST); break;
                }
            }

            // Check if newBlock is valid and destination is empty
            if (newBlock != null)
            {
                Block blockAtTarget = Api.World.BlockAccessor.GetBlock(orientationSwitch);

                // Only proceed if destination is air (Id = 0)
                if (blockAtTarget.Id == 0)
                {
                    AssetLocation sound = new AssetLocation("game:sounds/block/door");
                    Api.World.BlockAccessor.SetBlock(0, Pos);
                    Api.World.PlaySoundAt(sound, Pos.X + 0.5f, Pos.InternalY + 0.5f, Pos.Z + 0.5f, byPlayer, EnumSoundType.Sound, 1);
                    Api.World.BlockAccessor.SetBlock(newBlock.BlockId, orientationSwitch);
                }
                else
                {
                    IServerPlayer serverPlayer = byPlayer as IServerPlayer;
                    if (serverPlayer != null)
                    {
                        serverPlayer.SendIngameError("Cant Interact!", "Something is blocking the shutter from sliding!");
                    }
                }
            }
        }
    }
}

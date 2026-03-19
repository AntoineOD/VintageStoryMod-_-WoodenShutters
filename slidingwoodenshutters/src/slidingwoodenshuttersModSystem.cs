using slidingwoodenshutters.Blocks;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace slidingwoodenshutters
{
    public class slidingwoodenshuttersModSystem : ModSystem
    {

        // Called on server and client
        // Useful for registering block/entity classes on both sides
        public override void Start(ICoreAPI api)
        {
            api.RegisterBlockClass(Mod.Info.ModID + ".slidingwoodenshutters", typeof(BlockWoodenShutters));
            api.RegisterBlockEntityClass("BEWoodenShutters", typeof(BEWoodenShutters));
            api.RegisterBlockClass(Mod.Info.ModID + ".1x1woodenshutters", typeof(_1x1BlockWoodenShutters));
            api.RegisterBlockEntityClass("BE1x1WoodenShutters", typeof(BE1x1WoodenShutters));
            api.RegisterBlockClass(Mod.Info.ModID + ".2x1woodenshutters", typeof(_2x1BlockWoodenShutters));
            api.RegisterBlockEntityClass("BE2x1WoodenShutters", typeof(BE2x1WoodenShutters));
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            Mod.Logger.Notification("Hello from template mod server side: " + Lang.Get("slidingwoodenshutters:hello"));
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            Mod.Logger.Notification("Hello from template mod client side: " + Lang.Get("slidingwoodenshutters:hello"));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResoniteModLoader;
using HarmonyLib;
using FrooxEngine;

namespace DisableDoubleTap
{
    public class Main : ResoniteMod
    {
        public override string Name => "DisableDoubleTap";
        public override string Author => "kokoa";
        public override string Version => "1.0.0";
        public override string Link => "";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("com.kokoa.DisableDoubleTap");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(FrooxEngine.InputNode), "MultiTap", new Type[] { typeof(IEnumerable<IInputNode<bool>>), typeof(int), typeof(float) })]
        class Patch
        {
            static bool Prefix(ref MultiTapInput __result,
                IEnumerable<IInputNode<bool>> nodes,
                int tapCount = 2,
                float interval = 0.25f)
            {
                __result = new MultiTapInput(nodes, false, 3, interval);
                return false;
            }
        }
    }
}

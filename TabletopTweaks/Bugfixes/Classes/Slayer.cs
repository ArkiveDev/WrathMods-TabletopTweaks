﻿using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using TabletopTweaks.Config;

namespace TabletopTweaks.Bugfixes.Classes {
    class Slayer {
        [HarmonyPatch(typeof(ResourcesLibrary), "InitializeLibrary")]
        static class ResourcesLibrary_InitializeLibrary_Patch {
            static bool Initialized;

            static void Postfix() {
                if (Initialized) return;
                Initialized = true;
                if (Settings.Fixes.Slayer.DisableAllFixes) { return; }
                Main.LogHeader("Patching Slayer Resources");
                PatchBaseClass();
                Main.LogHeader("Slayer Resource Patch Complete");
            }
            static void PatchBaseClass() {
                if (Settings.Fixes.Slayer.Base.DisableAllFixes) { return; }
                PatchSlayerStudiedTarget();
            }
            static void PatchSlayerStudiedTarget() {
                if (!Settings.Fixes.Slayer.Base.Fixes["StudiedTarget"]) { return; }
                BlueprintBuff SlayerStudiedTargetBuff = Resources.GetBlueprint<BlueprintBuff>("45548967b714e254aa83f23354f174b0");
                SlayerStudiedTargetBuff.GetComponent<ContextRankConfig>().m_Progression = ContextRankProgression.OnePlusDivStep;
                Main.LogPatch("Patched", SlayerStudiedTargetBuff);
            }
        }
    }
}

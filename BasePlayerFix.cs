using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Core.Libraries.Covalence;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("BasePlayer Fix", "marlboroblue", "1.0.0")]
    [Description("Prevents NullReferenceException crashes in BasePlayer")]
    public class BasePlayerFix : RustPlugin
    {
        private void Init()
        {
            Puts("BasePlayerFix plugin loaded!");
        }

        private object OnPlayerUpdate(BasePlayer player)
        {
            try
            {
                if (player == null || !player.IsValid())
                {
                    LogError("Null or invalid player detected in OnPlayerUpdate");
                    return true; // Prevent original method from running
                }

                // Add additional safety checks here if needed
                if (player.transform == null)
                {
                    LogError($"Player {player.UserIDString} has null transform");
                    return true;
                }

                // Let the original method run if all checks pass
                return null;
            }
            catch (Exception ex)
            {
                LogError($"Exception in OnPlayerUpdate for player {player?.UserIDString}: {ex}");
                return true;
            }
        }

        private void OnPlayerDisconnected(BasePlayer player, string reason)
        {
            if (player == null)
            {
                LogError("Null player disconnected with reason: " + reason);
                return;
            }

            Puts($"Player {player.UserIDString} disconnected: {reason}");
        }

        private void Unloaded()
        {
            Puts("BasePlayerFix plugin unloaded!");
        }

        private void LogError(string message)
        {
            Puts("[ERROR] " + message);
            Interface.Oxide.LogError("[BasePlayerFix] " + message);
        }
    }
}

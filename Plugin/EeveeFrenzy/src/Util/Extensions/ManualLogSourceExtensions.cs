using System.Diagnostics;
using BepInEx.Logging;

namespace EeveeFrenzy.src.Util.Extensions;

public static class ManualLogSourceExtensions {
	[Conditional("DEBUG")]
	public static void LogVerbose(this ManualLogSource logger, object data) {
		logger.LogInfo(data);
	}
}
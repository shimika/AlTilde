using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace AlTilde {
	class WindowUtils {

		private static uint[] filterState = new uint[] {
			0x08000000,	// WS_DISABLED
			0x20000000	// WS_MINIMIZE
		};

		private static List<IntPtr> EnumerateProcessWindowHandles(int processId) {
			var handles = new List<IntPtr>();

			foreach (ProcessThread thread in Process.GetProcessById(processId).Threads) {
				WinAPI.EnumThreadWindows(thread.Id, (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);
			}

			return handles
				.Where(handle => WinAPI.IsWindow(handle) && WinAPI.IsWindowVisible(handle) && WinAPI.GetWindowTextLength(handle) > 0)
				.Where(handle => {
					WinAPI.WINDOWINFO pwi = new WinAPI.WINDOWINFO();
					WinAPI.GetWindowInfo(handle, ref pwi);

					return filterState.Where(state => (pwi.dwStyle & state) > 0).Count() == 0;
				})
				.OrderBy(handle => handle.ToInt64())
				.ToList();
		}

		public static void SwitchProcessWindow() {
			IntPtr foregroundWindowPtr = WinAPI.GetForegroundWindow();
			if (foregroundWindowPtr == null) { return; }

			int processId = 0;
			WinAPI.GetWindowThreadProcessId(foregroundWindowPtr, out processId);

			if (processId != 0) {
				List<IntPtr> handles = EnumerateProcessWindowHandles(processId);
				int index = handles.IndexOf(foregroundWindowPtr);

				if (index >= 0) {
					WinAPI.SetForegroundWindow(handles[(index + 1) % handles.Count]);
				}
			}
		}
	}
}

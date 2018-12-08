using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AlTilde {
	class WindowUtils {
		public static void SwitchProcessWindow() {
			IntPtr foregroundWindowPtr = WinAPI.GetForegroundWindow();
			if (foregroundWindowPtr == null) { return; }

			int processId = 0;
			WinAPI.GetWindowThreadProcessId(foregroundWindowPtr, out processId);

			if (processId != 0) {
				List<IntPtr> handles = WinAPI.EnumerateProcessWindowHandles(processId);
				int index = handles.IndexOf(foregroundWindowPtr);

				if (index >= 0) {
					WinAPI.SetForegroundWindow(handles[(index + 1) % handles.Count]);
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AlTilde {
	class GlobalHotKey {
		static HwndSource HWndSource;

		static int AlTrideHotKey;

		public static void registerHotKey(Window window) {
			WindowInteropHelper wih = new WindowInteropHelper(window);
			HWndSource = HwndSource.FromHwnd(wih.Handle);
			HWndSource.AddHook(MainWindowProc);

			AlTrideHotKey = WinAPI.GlobalAddAtom("AlTrideHotKey");
			WinAPI.RegisterHotKey(wih.Handle, AlTrideHotKey, WinAPI.MOD_ALT, WinAPI.VK_OEM_3);
		}

		private static IntPtr MainWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
			if (msg == WinAPI.WM_HOTKEY) {
				if (wParam.ToString() == AlTrideHotKey.ToString()) {
					WindowUtils.SwitchProcessWindow();
				}
				handled = true;
			}
			return IntPtr.Zero;
		}
	}
}

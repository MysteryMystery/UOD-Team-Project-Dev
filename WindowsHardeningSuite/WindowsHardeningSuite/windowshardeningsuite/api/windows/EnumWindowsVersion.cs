using System;

namespace WindowsHardeningSuite.windowshardeningsuite.api.windows
{
    public enum EnumWindowsVersion
    {
        WINDOWS_95,
        WINDOWS_98,
        WINDOWS_ME,
        WINDOWS_NT_4_0,
        WINDOWS_2000,
        WINDOWS_XP,
        WINDOWS_2003,
        WINDOWS_VISTA,
        WINDOWS_2008,
        WINDOWS_7,
        WINDOWS_2008_R2,
        WINDOWS_8,
        WINDOWS_8_1,
        WINDOWS_10
    }

    public static class WindowsVersionExtension
    {
        /*
         * Think
         * object x {
         *    implicit class windowsPimp(final val x : EnumWindowsVersion){
         *         def method(arg): return = ...
         *     }
         * }
         */
        public static Boolean IsThisMachine(this EnumWindowsVersion windowsVersion)
        {
            return true;
        }
    }
}
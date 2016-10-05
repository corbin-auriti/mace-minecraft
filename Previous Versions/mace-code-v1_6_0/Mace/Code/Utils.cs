/*
 * this code was contributed by Surrogard <surrogard@googlemail.com>
 * thank you!
 * */

using System;
using System.IO;
namespace Mace
{
    public static class Utils
    {
        public static String GetMinecraftSavesDir(String subPath)
        {
            String path;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    path = Environment.GetEnvironmentVariable("APPDATA") + @"\.minecraft\saves\" + subPath;
                    break;
                case PlatformID.Unix:
                    path = Environment.GetEnvironmentVariable("HOME") + "/.minecraft/saves/" + subPath;
                    break;
                default:
                    path = Environment.GetEnvironmentVariable("APPDATA") + @"\.minecraft\saves\" + subPath;
                    break;
            }
            return path;
        }
    }
}


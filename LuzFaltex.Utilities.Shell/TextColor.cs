using System;
using System.Collections.Generic;
using System.Text;

namespace LuzFaltex.Utilities.Shell
{
    public struct TextColor
    {
        public int Code { get; }

        private TextColor(int code)
        {
            Code = code;
        }

        public static implicit operator TextColor(int colorCode)
            => new TextColor(colorCode);

        public static implicit operator string(TextColor color)
            => $"&{color.Code.ToString("x")}";

        public static implicit operator ConsoleColor(TextColor color)
            => (ConsoleColor)color.Code;

        public static implicit operator TextColor(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black:
                    return Black;
                case ConsoleColor.DarkBlue:
                    return DarkBlue;
                case ConsoleColor.DarkGreen:
                    return DarkGreen;
                case ConsoleColor.DarkCyan:
                    return DarkCyan;
                case ConsoleColor.DarkRed:
                    return DarkRed;
                case ConsoleColor.DarkMagenta:
                    return DarkMagenta;
                case ConsoleColor.DarkYellow:
                    return DarkYellow;
                case ConsoleColor.Gray:
                    return Gray;
                case ConsoleColor.DarkGray:
                    return DarkGray;
                case ConsoleColor.Blue:
                    return Blue;
                case ConsoleColor.Green:
                    return Green;
                case ConsoleColor.Cyan:
                    return Cyan;
                case ConsoleColor.Red:
                    return Red;
                case ConsoleColor.Magenta:
                    return Magenta;
                case ConsoleColor.Yellow:
                    return Yellow;
                case ConsoleColor.White:
                default:
                    return White;
            }
        }

        public static TextColor Black = 0x0;
        public static TextColor DarkBlue = 0x1;
        public static TextColor DarkGreen = 0x2;
        public static TextColor DarkCyan = 0x3;
        public static TextColor DarkRed = 0x4;
        public static TextColor DarkMagenta = 0x5;
        public static TextColor DarkYellow = 0x6;
        public static TextColor Gray = 0x7;
        public static TextColor DarkGray = 0x8;
        public static TextColor Blue = 0x9;
        public static TextColor Green = 0xa;
        public static TextColor Cyan = 0xb;
        public static TextColor Red = 0xc;
        public static TextColor Magenta = 0xd;
        public static TextColor Yellow = 0xe;
        public static TextColor White = 0xf;
    }
}

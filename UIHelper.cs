using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace CybersecurityAwarenessBotPart2
{
    // Utility class that handles UI styling, console text effects, and name validation rules
    public static class UIHelper
    {
        // Returns the exact framed ASCII banner matching your original console application design
        public static string GetAsciiBanner()
        {
            return @"    ╔══════════════════════════════════════════════════════════════════╗
    ║   ██████╗██╗   ██╗███████╗███████╗██████╗ ███████╗███████╗██████╗║
    ║  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝██╔══██║
    ║  ██║     ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████╗█████╗  ██████╔╝
    ║  ██║      ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══╝  ██╔══██║
    ║  ╚██████╗  ██║   ██████╔╝███████╗██║  ██║███████║███████║██║  ██║
    ║   ╚═════╝  ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝║
    ║                AWARENESS & SAFETY BOT                               ║
    ╚══════════════════════════════════════════════════════════════════╝
                ═══ Developed by GAIN ═══";
        }

        // Validates user names to ensure they aren't blank, stay within character bounds, and use proper letters
        public static bool IsValidName(string name, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(name))
            {
                errorMessage = "[!] Validation Error: Name cannot be blank.";
                return false;
            }

            string clean = name.Trim();

            if (clean.Length < 2 || clean.Length > 20)
            {
                errorMessage = "[!] Validation Error: Name must be between 2 and 20 characters long.";
                return false;
            }

            if (!Regex.IsMatch(clean, @"^[a-zA-Z\s'-]+$"))
            {
                errorMessage = "[!] Validation Error: Name can only contain letters and spaces.";
                return false;
            }

            return true;
        }

        // Cleanly formats names into Title Case (e.g., turning "john" into "John")
        public static string FormatName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "User";
            string clean = name.Trim();
            return char.ToUpper(clean[0]) + (clean.Length > 1 ? clean.Substring(1).ToLower() : "");
        }
    }
}
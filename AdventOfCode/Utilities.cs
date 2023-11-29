namespace AdventOfCodeNet;

public static class Utilities
{
    public static int GetShortYear(this DateTime dateTime) =>
        dateTime.Year - 2000;
}

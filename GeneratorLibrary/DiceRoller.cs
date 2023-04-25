namespace GeneratorLibrary
{
    public static class DiceRoller
    {
        public static int BasicRoll(int modifiers = 0)
        {
            int rollResult = 0;
            for (int i = 0; i < 3; i++)
                rollResult += Random.Shared.Next(1, 7);

            return rollResult + modifiers;
        }

        public static int RollD6(uint diceNumber = 3, int modifiers = 0)
        {
            int rollResult = 0;
            for (int i = 0; i < diceNumber; i++)
                rollResult += Random.Shared.Next(1, 7);

            return rollResult + modifiers;
        }
    }
}
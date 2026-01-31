using AdventLibrary.Helpers;

namespace AdventLibrary.CustomObjects
{
    public class CardObject
    {
        public CardObject(int value, int suit, bool higherBetter = true)
        {
            HigherBetter = higherBetter;
            Value = CalculateValue(value);
            Suit = new SuitObject(suit);
        }

        public SuitObject? Suit { get; set; }

        public ValueObject? Value { get; set; }

        public bool HigherBetter { get; set; }

        private ValueObject CalculateValue(int value)
        {
            if (HigherBetter)
            {
                return new ValueObject(value);
            }
            return new ValueObject(15 - value);
        }
    }

    public class SuitObject
    {
        public SuitObject(int id)
        {
            SuitNum = id;
            SuitName = PlayingCardHelper.NumToSuit[id];
        }

        public SuitObject(string name)
        {
            SuitNum = PlayingCardHelper.SuitToNum[name];
            SuitName = name;
        }

        public SuitObject(int id, string name)
        {
            SuitNum = id;
            SuitName = name;
        }

        public int SuitNum { get; set; }

        public string SuitName { get; set; }
    }

    public class ValueObject
    {
        public ValueObject(int id)
        {
            ValueNum = id;
            ValueName = PlayingCardHelper.ValueToCharCardName[id]; ;
        }

        public ValueObject(char name)
        {
            ValueNum = PlayingCardHelper.ChardCardNameToValue[name];
            ValueName = name;
        }

        public ValueObject(string name)
        {
            ValueNum = PlayingCardHelper.StringCardNameToValue[name];
            ValueNum = PlayingCardHelper.ValueToCharCardName[ValueNum];
        }

        public ValueObject(int id, char name)
        {
            ValueNum = id;
            ValueName = name;
        }

        public int ValueNum { get; set; }

        public char ValueName { get; set; }
    }
}

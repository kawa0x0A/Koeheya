namespace Koeheya
{
    public static class TableUtility
    {
        public static string HeyaTableDataAttribute(IEnumerable<Data.Heya> heyas, int x, int y)
        {
            string classAttribute = "square";

            if ((heyas == null) || (heyas.Count() == 0))
            {
                return classAttribute;
            }

            var InnerWidthHeyas = heyas.Where(heya => (y >= heya.Y) && (y < (heya.Y + heya.Height)));
            var InnerHeightHeyas = heyas.Where(heya => (x >= heya.X) && (x < (heya.X + heya.Width)));

            if ((InnerWidthHeyas.Count() == 0) && (InnerHeightHeyas.Count() == 0))
            {
                return classAttribute;
            }

            if ((InnerWidthHeyas.Any(heya => ((heya.X <= x) && (x < (heya.X + heya.Width)))) && (InnerHeightHeyas.Any(heya => ((heya.Y <= y) && (y < (heya.Y + heya.Height)))))))
            {
                classAttribute += " heya";
            }

            if (InnerWidthHeyas.Any(heya => (x == heya.X)))
            {
                classAttribute += " left";
            }

            if (InnerWidthHeyas.Any(heya => ((x + 1) == (heya.X + heya.Width))))
            {
                classAttribute += " right";
            }

            if (InnerHeightHeyas.Any(heya => (y == heya.Y)))
            {
                classAttribute += " top";
            }

            if (InnerHeightHeyas.Any(heya => ((y + 1) == (heya.Y + heya.Height))))
            {
                classAttribute += " bottom";
            }

            return classAttribute;
        }
    }
}

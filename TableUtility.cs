namespace Koeheya
{
    public enum HeyaStructEdgeType
    {
        Top,
        Bottom,
        Left,
        Right,
    }

    public enum HeyaStructVertexType
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
    }

    public enum HeyaStructVertexCrossType
    {
        None,
        TopLeftTop,
        TopLeftLeft,
        TopLeftCross,
        TopRightTop,
        TopRightRight,
        TopRightCross,
        BottomLeftBottom,
        BottomLeftLeft,
        BottomLeftCross,
        BottomRightBottom,
        BottomRightRight,
        BottomRightCross,
    }

    public static class TableUtility
    {
        public static bool ExistEdge(IEnumerable<Data.Heya> heyas, int x, int y, HeyaStructEdgeType edgeType)
        {
            var InnerWidthHeyas = heyas.Where(heya => (y >= heya.Y) && (y < (heya.Y + heya.Height)));
            var InnerHeightHeyas = heyas.Where(heya => (x >= heya.X) && (x < (heya.X + heya.Width)));

            switch (edgeType)
            {
                case HeyaStructEdgeType.Top:
                    return InnerHeightHeyas.Any(heya => (y == heya.Y));

                case HeyaStructEdgeType.Bottom:
                    return InnerHeightHeyas.Any(heya => ((y + 1) == (heya.Y + heya.Height)));

                case HeyaStructEdgeType.Left:
                    return InnerWidthHeyas.Any(heya => (x == heya.X));

                case HeyaStructEdgeType.Right:
                    return InnerWidthHeyas.Any(heya => ((x + 1) == (heya.X + heya.Width)));
            }

            return false;
        }

        public static HeyaStructVertexCrossType GetVertexCrossType(IEnumerable<Data.Heya> heyas, int x, int y, HeyaStructVertexType vertexType)
        {
            switch (vertexType)
            {
                case HeyaStructVertexType.TopLeft:
                    if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Top))
                    {
                        if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Left))
                        {
                            return HeyaStructVertexCrossType.TopLeftCross;
                        }
                        else
                        {
                            return HeyaStructVertexCrossType.TopLeftTop;
                        }
                    }
                    else
                    {
                        if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Left))
                        {
                            return HeyaStructVertexCrossType.TopLeftLeft;
                        }
                        else
                        {
                            return HeyaStructVertexCrossType.None;
                        }
                    }

                case HeyaStructVertexType.TopRight:
                    if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Top))
                    {
                        if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Right))
                        {
                            return HeyaStructVertexCrossType.TopRightCross;
                        }
                        else
                        {
                            return HeyaStructVertexCrossType.TopRightTop;
                        }
                    }
                    else
                    {
                        if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Right))
                        {
                            return HeyaStructVertexCrossType.TopRightRight;
                        }
                        else
                        {
                            return HeyaStructVertexCrossType.None;
                        }
                    }

                case HeyaStructVertexType.BottomLeft:
                    if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Bottom))
                    {
                        if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Left))
                        {
                            return HeyaStructVertexCrossType.BottomLeftCross;
                        }
                        else
                        {
                            return HeyaStructVertexCrossType.BottomLeftBottom;
                        }
                    }
                    else
                    {
                        if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Left))
                        {
                            return HeyaStructVertexCrossType.BottomLeftLeft;
                        }
                        else
                        {
                            return HeyaStructVertexCrossType.None;
                        }
                    }

                case HeyaStructVertexType.BottomRight:
                    if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Bottom))
                    {
                        if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Right))
                        {
                            return HeyaStructVertexCrossType.BottomRightCross;
                        }
                        else
                        {
                            return HeyaStructVertexCrossType.BottomRightBottom;
                        }
                    }
                    else
                    {
                        if (ExistEdge(heyas, x, y, HeyaStructEdgeType.Right))
                        {
                            return HeyaStructVertexCrossType.BottomRightRight;
                        }
                        else
                        {
                            return HeyaStructVertexCrossType.None;
                        }
                    }
            }

            return HeyaStructVertexCrossType.None;
        }

        public static string HeyaTableDataAttribute(IEnumerable<Data.Heya> heyas, int x, int y)
        {
            string classAttribute = "box";

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

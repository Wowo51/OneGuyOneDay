namespace DancingLinks
{
    public class ColumnNode : DancingNode
    {
        public int Size;
        public string Name;
        public ColumnNode(string name) : base()
        {
            Size = 0;
            Name = name;
            Column = this;
        }

        public void Cover()
        {
            RemoveLeftRight();
            for (DancingNode row = this.Down; row != this; row = row.Down)
            {
                for (DancingNode node = row.Right; node != row; node = node.Right)
                {
                    node.Up.Down = node.Down;
                    node.Down.Up = node.Up;
                    node.Column.Size--;
                }
            }
        }

        public void Uncover()
        {
            for (DancingNode row = this.Up; row != this; row = row.Up)
            {
                for (DancingNode node = row.Left; node != row; node = node.Left)
                {
                    node.Column.Size++;
                    node.Up.Down = node;
                    node.Down.Up = node;
                }
            }

            ReinsertLeftRight();
        }
    }
}
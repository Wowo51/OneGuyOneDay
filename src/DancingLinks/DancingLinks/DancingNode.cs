namespace DancingLinks
{
    public class DancingNode
    {
        public DancingNode Left;
        public DancingNode Right;
        public DancingNode Up;
        public DancingNode Down;
        public ColumnNode Column = default !;
        public DancingNode()
        {
            this.Left = this;
            this.Right = this;
            this.Up = this;
            this.Down = this;
        }

        public void LinkDown(DancingNode node)
        {
            node.Down = this.Down;
            node.Down.Up = node;
            node.Up = this;
            this.Down = node;
        }

        public void LinkRight(DancingNode node)
        {
            node.Right = this.Right;
            node.Right.Left = node;
            node.Left = this;
            this.Right = node;
        }

        public void RemoveLeftRight()
        {
            this.Left.Right = this.Right;
            this.Right.Left = this.Left;
        }

        public void ReinsertLeftRight()
        {
            this.Left.Right = this;
            this.Right.Left = this;
        }
    }
}
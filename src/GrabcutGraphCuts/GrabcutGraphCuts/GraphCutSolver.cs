namespace GrabcutGraphCuts
{
    public class GraphCutSolver
    {
        public bool[, ] Segment(ImageData image)
        {
            // Use the graph-cut based Grabcut segmentation.
            return GraphCutMinimizer.MinCut(image);
        }
    }
}
namespace WatershedTransformation
{
    public interface IWatershedSegmenter
    {
        int[, ] SegmentImage(int[, ]? image);
    }
}
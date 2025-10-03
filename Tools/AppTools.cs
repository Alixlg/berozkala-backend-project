namespace berozkala_backend.Tools
{
    public static class AppTools
    {
        public static int PageSkipCount(int pageId, int pageCount)
        {
            return pageId == 0 || pageId == 1 ? 0 : (pageId - 1) * pageCount;
        }
    }
}
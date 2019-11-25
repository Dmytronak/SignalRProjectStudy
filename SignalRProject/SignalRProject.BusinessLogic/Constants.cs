namespace SignalRProject.BusinessLogic
{
    public static class Constants
    {
        public static class DefaultImageSizes
        {
            public static readonly int MaxWidthOriginalImage = 1500;
            public static readonly int MaxHeightOriginalImage = 900;
        }

        public static class DefaultIconSizes
        {
            public static readonly int MaxWidthOriginalImage = 70;
            public static readonly int MaxHeightOriginalImage = 70;
        }

        public static class FilePaths
        {
            public static readonly string RoomAvatarImages = @"uploaded-images\rooms\icons";
            public static readonly string UserAvatarImages = @"uploaded-images\users\icons";
        }
    }
}

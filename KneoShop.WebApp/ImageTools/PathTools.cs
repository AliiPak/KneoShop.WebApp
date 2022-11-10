namespace KenoShop.WebApp.ImageTools
{
    public static class PathTools
    {
        public static string ProductImagePath = "/media/images/product/";
        public static string ProductImageUploadPath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/media/images/product/");
        public static string ProductThumbImagePath = "/media/images/product/thumb/";
        public static string ProductThumbImageUploadPath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/media/images/product/thumb/");
        
        
        public static string ProductGalleryImagePath = "/media/images/product-gallery/";
        public static string ProductGalleryImageUploadPath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/media/images/product-gallery/");
        public static string ProductGalleryThumbImagePath = "/media/images/product-gallery/thumb/";
        public static string ProductGalleryThumbImageUploadPath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/media/images/product-gallery/thumb/");

        public static string UserImagePath = "/media/images/user-gallery/";
        public static string UserImageUploadPath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/media/images/user-gallery/");
        public static string UserThumbImagePath = "/media/images/user-gallery/thumb/";
        public static string UserThumbImageUploadPath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/media/images/user-gallery/thumb/");
    }
}

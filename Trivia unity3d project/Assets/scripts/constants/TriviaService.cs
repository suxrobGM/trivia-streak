public static class TriviaService
{
    private static readonly string _hostName = "localhost"; // original: www.loadingworld.com

    public static string GetHostAddress()
    {
        return _hostName;
    }
    public static string GetHttpFolderPath()
    {
        return "trivia_admin/trivia_service/";
    }
    public static string GetHttpMainCategoryMediaPath()
    {
        return "http://" + _hostName + "/trivia_admin/images/1st_level_categories/";
    }
    public static string GetHttpSubCategoryMediaPath()
    {
        return "http://" + _hostName + "/trivia_admin/images/2nd_level_categories/";
    }
    public static string GetHttpGrandSubCategoryMediaPath()
    {
        return "http://" + _hostName + "/trivia_admin/images/3rd_level_categories/";
    }
    public static string GetHttpPrizeMediaPath()
    {
        return "http://" + _hostName + "/trivia_admin/images/prize/";
    }
    public static string GetImageUploadUrl()
    {
        return "http://" + _hostName + "/trivia_admin/trivia_service/upload_file.php";
    }
    public static string GetImageDisplayUrl()
    {
        return "http://" + _hostName + "/trivia_admin/trivia_service/profile_images/";
    }
}

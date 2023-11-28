using System.Security.Cryptography.X509Certificates;

namespace Standard.ToList.Model.Constants
{
    public static class RegexPatterns
    {
        public static string SEARCHER_OPEN_SYMBOLS = "^(?:.*?\\[(?!.*?\\])[^\\]]*|[^[\\r\n]*\\].*?|.*?\\((?!.*?\\))[^)]*|[^(\r\n]*\\).*?)$";
        
        public static string SEARCHER_AUCHAN_MATCHES =  "<div class=\"auc-product-tile__[name|prices]*\"[^>]*>([\\s\\S]*?)<\\/div>|<span class=\"sales\">([\\s\\S]*?)<\\/span>";
        public static string SEARCHER_AUCHAN_NAME = "<a class=\"link\"[^>]*>([\\s\\S]*?)<\\/a>";
        public static string SEARCHER_AUCHAN_PRICE = "<span class=\"value\"[^>]*content=\"(\\d*.\\d*?)\">";
        
        public static string SEARCHER_CONTINENTE_NAME = "<a class=\"[^>]*col-tile--description\"[^>]*>([\\s\\S]*?)<\\/a>";
        public static string SEARCHER_CONTINENTE_PRICE = "<span class=\"value\"[^>]*content=\"(\\d*.\\d*?)\">";
        public static string SEARCHER_CONTINENTE_UNIT = "<span class=\"pwc-m-unit\">\\n*\\s*(\\/\\w*)\n*\\s*<\\/span>";
        public static string SEARCHER_CONTINENTE_BRAND = "<p class=\"[^>]*col-tile--brand\"[^>]*>([\\s\\S]*?)<\\/p>";
    }
}

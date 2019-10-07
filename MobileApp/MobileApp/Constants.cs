namespace MobileApp
{
	public static class Constants
	{
		//DOCKER PORTS - HTTP 8080, HTTPS 8083
		//WEB API PORTS - Visual Studio IIS Express HTTP 12109 HTTPS 44383
		//WEB API PORTS - Debug Build(Mimirium.exe) HTTP 5000 HTTPS 5001
		public static string SERVER_HOST = "http://192.168.100.4:8080";
		public static string COMPANY_BASE_URL = SERVER_HOST + "/api/companies";
		public static string COMPANY_EDIT_URL = COMPANY_BASE_URL + "/{0}";
		public static string COMPANY_SEARCH_URL = COMPANY_BASE_URL + "?name={0}";
	}
}
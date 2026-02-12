using System.Net;
using RestSharp;
namespace KuaKe.Cookie
{
    internal interface CookieInterface
    {
        void AddJsonBody(RestRequest restRequest,object obj);
        RestClientOptions AddCookieContainer(RestClientOptions options);
        void AddCookieHeader(object obj,RestRequest restRequest,RestClientOptions options);
        RestRequest AddParmeter(RestRequest restRequest);
    }
    internal class Cookie:CookieInterface
    {
        private static string _cookie = "";
        private static Uri? _baseUri;
        public Cookie(string baseUri,string cookie)
        {
            _baseUri = new Uri(baseUri);
            _cookie = cookie;
        }
        public RestClientOptions AddCookieContainer(RestClientOptions options)
            {
            options.CookieContainer = new CookieContainer();
            foreach (var part in _cookie.Split(';'))
            {
                var kv = part.Trim();
                if (string.IsNullOrEmpty(kv))
                {
                    continue;
                }
                int i = kv.IndexOf('=');
                if (i <= 0)
                {
                continue;
                }
                var name = kv.Substring(0,i).Trim();
                var value = kv.Substring(i+1).Trim();
                if (_baseUri == null)
                {
                    _baseUri = new Uri("ERROR");
                }
                options.CookieContainer.Add(_baseUri,new System.Net.Cookie(name,value,"/",_baseUri.Host));
            }

                return options;
            }
            public void AddCookieHeader(object obj,RestRequest restRequest,RestClientOptions options)
            {
            AddParmeter(restRequest);
            AddJsonBody(restRequest,obj);
            AddCookieContainer(options);
            }
            public RestRequest AddParmeter(RestRequest restRequest)
            {
                restRequest.AddQueryParameter("pr", "ucpro");
                restRequest.AddQueryParameter("fr", "pc");
                restRequest.AddQueryParameter("uc_param_str", "");
                restRequest.AddQueryParameter("sys", "win32");
                restRequest.AddQueryParameter("ve", "2.5.56");
                restRequest.AddQueryParameter("ut", "");
                restRequest.AddQueryParameter("guid", "");
                return restRequest;
            }
            public void AddJsonBody(RestRequest restRequest,object obj)
            {
                restRequest.AddJsonBody(obj);
            }
    }
}

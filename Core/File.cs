using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using RestSharp;
namespace KuaKe
{
    internal interface KuakeFunction
    {
        public bool MoveFiles(string target_folder_id, string file_ids, IEnumerable<string>? excludeFids = null);
        public string SearchFiles(string keyword, string folder_id = "0", int page = 1, int size = 50, string sort_field = "file_name", string sort_order = "asc");
        public string RenameFile(string file_id, string NewFileName);
        public bool DeleteFiles(string fileIds);
        public string Create_Folder(string folderName, string? floderPath);
        public byte[] StartDownloadFile(string url);
        public string GetFileDownloadUrl(string fid);
        public List<KuakeListClass> GetFileList(string folder_id = "0",
        int page = 1,
        int size = 50,
        string sort_field = "file_name",
        string sort_order = "asc");

    }
    public class KuakeListClass
    {
        public string? Name { get; set; }
        public string? ID { get; set; }
        public string? FlieType { get; set; }
    }
    public class KuaKeFileSerice : KuakeFunction, IDisposable
    {
        private static RestClientOptions ERROR = new RestClientOptions();
        private bool _disposed;
        private const string Url = "https://drive-h.quark.cn";
        private static Uri baseUri = new Uri(Url);
        private static RestClientOptions options = new RestClientOptions(Url)
        {
            CookieContainer = new System.Net.CookieContainer()
        };
        private static RestRequest restRequest = new RestRequest();
        private string _cookie = "";
        private static RestClient restClient = new RestClient();
        private static Cookie.Cookie? cookies;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="__pus">夸克网盘的Cookie中的__pus就OK了</param>
        public KuaKeFileSerice(string __pus)
        {
            _cookie = "__pus=" + __pus;
            cookies = new Cookie.Cookie(Url, _cookie);
            ERROR?.CookieContainer?.Add(new System.Net.Cookie { Name = "ERROR", Value = "ERROR" });
        }
        public bool MoveFiles(string target_folder_id, string file_ids, IEnumerable<string>? excludeFids = null)
        {
            restRequest = new RestRequest("/1/clouddrive/file/move", Method.Post);
            options.CookieContainer = new CookieContainer();
            cookies?.AddCookieHeader(new
            {
                action_type = 1,
                to_pdir_fid = target_folder_id,
                filelist = new[] { file_ids },
                exclude_fids = excludeFids ?? Array.Empty<string>()
            }, restRequest, options);
            using (restClient = new RestClient(options))
            {
                string returnText = restClient.Execute(restRequest).Content ?? "-404";
                if (returnText == "-404")
                {
                    return false;
                }
                int status = int.Parse(JsonDocument.Parse(returnText).RootElement.GetProperty("status").ToString());
                if (status == 200)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }
        public string SearchFiles(string keyword, string folder_id = "0", int page = 1, int size = 50, string sort_field = "file_name", string sort_order = "asc")
        {
            restRequest = new RestRequest("/1/clouddrive/file/search", Method.Get);
            restRequest.AddHeader("Cookie",_cookie);
            cookies?.AddCookieHeader(new
            {
                q = keyword,
                _page = page,
                _size = size,
                _fetch_total = 1,
                _sort = $"{sort_field}:{sort_order},updated_at:desc",
                _is_hl = 1
            }, restRequest, options);
            using (restClient = new RestClient(options))
                return restClient.Execute(restRequest).Content ?? "-404";
        }
        public string RenameFile(string file_id, string NewFileName)
        {
            restRequest = new RestRequest("/1/clouddrive/file/rename", Method.Post);
            restRequest.AddHeader("Cookie", _cookie);
            cookies?.AddCookieHeader(new
            {
                fid = file_id,
                file_name = NewFileName
            }, restRequest, options);
            using (restClient = new RestClient(options))
            {
                return restClient.Execute(restRequest).Content ?? "-404";
            }

        }

        public bool DeleteFiles(string fileIds)
        {
            options = new RestClientOptions(Url)
            {
                CookieContainer = new System.Net.CookieContainer()
            };
            using (restClient = new RestClient(options))
            {
                restRequest = new RestRequest("/1/clouddrive/file/delete", Method.Post);
                restRequest.AddQueryParameter("pr", "ucpro");
                restRequest.AddQueryParameter("fr", "pc");
                restRequest.AddQueryParameter("uc_param_str", "");
                restRequest.AddHeader("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:147.0) Gecko/20100101 Firefox/147.0");
                restRequest.AddHeader("Cookie", _cookie);
                restRequest.AddJsonBody(new
                {
                    action_type = 2,
                    filelist = new[] { fileIds },                    // 必须是 IEnumerable<string> 或 string[]
                    exclude_fids = Array.Empty<string>()   // 空数组（等于 Python 的 []）
                });
                var response = restClient.Execute(restRequest);
                if (JsonDocument.Parse(response.Content ?? "-404").RootElement.GetProperty("status").ToString() == "200")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public string Create_Folder(string folderName, string? floderPath)
        {
            using (restClient = new RestClient(options))
            {
                restRequest = new RestRequest("/1/clouddrive/file", Method.Post);
                restRequest.AddHeader("Cookie", _cookie);
                if (floderPath == null)
                {
                    cookies?.AddCookieHeader(new
                    {
                        dir_init_lock = false,
                        dir_path = "",
                        file_name = folderName,
                        pdir_fid = "0"
                    }, restRequest, options);
                }
                else
                {
                    cookies?.AddCookieHeader(new
                    {
                        dir_init_lock = false,
                        dir_path = "",
                        file_name = folderName,
                        pdir_fid = floderPath
                    }, restRequest, options);
                }

                return restClient.Execute(restRequest).Content ?? "-404";
            }

        }
        public byte[] StartDownloadFile(string url)
        {
            using (restClient = new RestClient())
            {
                restRequest = new RestRequest(url, Method.Get);
                restRequest.AddHeader("UA", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:147.0) Gecko/20100101 Firefox/147.0");
                byte[] bytes = restClient.Execute(restRequest).RawBytes ?? new byte[] { };
                if (bytes.Length != 0)
                {
                    return bytes;
                }
                else
                {
                    throw new Exception("错误");
                }
            }

        }
        public string GetFileDownloadUrl(string fid)
        {

            options.CookieContainer = new CookieContainer();
            options = cookies?.AddCookieContainer(options) ?? ERROR;
            if (options == null)
            {
                throw new Exception("错误");
            }
            else if (options.CookieContainer == null)
            {
                throw new Exception("错误");
            }
            else if (options.CookieContainer.GetCookies(new Uri("ERROR"))[0].Value == "ERROR")
            {
                throw new Exception("错误");
            }
            using (restClient = new RestClient(options))
            {
                restRequest = new RestRequest("/1/clouddrive/file/download", Method.Post);
                cookies?.AddCookieHeader(new { fids = new[] { fid } }, restRequest, options);
                JsonElement json;
                var response = restClient.Execute(restRequest);
                try
                {
                    json = JsonDocument.Parse(response.Content ?? "-404").RootElement;
                }
                catch (Exception ms)
                {
                    throw new Exception("解析JSON失败:" + ms.Message);
                }
                try
                {
                    return json.GetProperty("data")[0].GetProperty("download_url").ToString();
                }
                catch
                {
                    if (json.GetProperty("status").GetInt32() == 400)
                    {
                        throw new Exception("文件过大,超过100MB,无法下载");
                    }
                    throw new Exception("未知错误");
                }
            }


        }
        public List<KuakeListClass> GetFileList(string folder_id = "0", int page = 1, int size = 50, string sort_field = "file_name", string sort_order = "asc")
        {
            List<KuakeListClass> ListKuaKe = new List<KuakeListClass>() { };
            using (restClient = new RestClient(Url))
            {
                restRequest = new RestRequest("/1/clouddrive/file/sort", Method.Get);
                restRequest.AddQueryParameter("pr", "ucpro");
                restRequest.AddQueryParameter("fr", "pc");
                restRequest.AddQueryParameter("uc_param_str", "");
                restRequest.AddHeader("Cookie", _cookie);
                restRequest.AddQueryParameter("pdir_fid", folder_id);
                restRequest.AddQueryParameter("_page", page.ToString());
                restRequest.AddQueryParameter("_size", size.ToString());
                restRequest.AddQueryParameter("_sort", $"{sort_field}:{sort_order}");
                var response = restClient.Execute(restRequest);
                if (response.Content == null)
                {
                    throw new Exception("返回Content错误,Content为null");
                }
                if (JsonDocument.Parse(response.Content).RootElement.GetProperty("status").ToString() == "401")
                {
                    throw new Exception("-404错误");
                }
                if (!string.IsNullOrEmpty(response.Content))
                {
                    int count = JsonDocument.Parse(response.Content).RootElement.GetProperty("data").GetProperty("list").GetArrayLength();
                    for (int i = 0; i < count; i++)
                    {
                        ListKuaKe.Add(new KuakeListClass { Name = JsonDocument.Parse(response.Content).RootElement.GetProperty("data").GetProperty("list")[i].GetProperty("file_name").ToString(), ID = JsonDocument.Parse(response.Content).RootElement.GetProperty("data").GetProperty("list")[i].GetProperty("fid").ToString(), FlieType = JsonDocument.Parse(response.Content).RootElement.GetProperty("data").GetProperty("list")[i].GetProperty("file_type").ToString() });
                    }

                }
                return ListKuaKe;
            }

        }
        public void Dispose()
        {
            _disposed = true;
            restClient.Dispose();
            GC.SuppressFinalize(this);

        }
        ~KuaKeFileSerice()
        {
            if (!_disposed)
            {
                restClient.Dispose();
            }
        }
    }
}
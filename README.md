# QuarkPanDirectLink
一个可直接调用夸克网盘API的C#类库.

`QuarkPanDirectLink` 是一个用于调用夸克网盘的 C# 原生实现类库。它提供了简单的 API 用于编写相关网络工具，如自动备份，客户端等

## 项目特点
- ✅ 不使用curl，原生实现所有功能(暂时只实现了部分,未来实现全部)
## 已经实现的功能有
- ✅ 获取文件下载链接
- ✅ 访问全部文件目录
- ✅ 删除文件夹
- ✅ 搜索文件/文件夹
- ✅ 下载文件功能
- ✅ 文件重命名
- ✅ 创建文件夹
## 支持

本项目支持以下 .NET 版本：

- `.NET 6`、`.NET 7`、`.NET 8`、`.NET 9`、`.NET 10`、以及更高版本的 .NET。
## 使用示例

获取根目录文件列表
```csharp
using KuaKe;
class Program
{
    static void Main()
    {
        var lib = new KuaKeFileSerice("你的__pus");
        var s = lib.GetFileList();
        foreach(var f in s)
        {
            System.Console.WriteLine(f.Name);
        }
    }
}
```
```
输出:
夸克上传文件
蓝色大海的传说S01E01第1集.mkv
来自：分享
test.cs
haha564@haha564PC:~/桌面/test$ 
```
删除文件夹
````csharp
 Console.WriteLine(lib.DeleteFiles("00e13e95fa154dbf919ccd2495b2753a"));//输出:True
````
获取文件下载直链
````csharp
Console.WriteLine(lib.GetFileDownloadUrl("02b70bcab77847a290cf28977c58eb70"));
//输出:https://dl-pc-sz.pds.quark.cn/5d41c66f4fb92d9cfc7d42768e3440dd949e5a4d/5d41c66feb6e764a9a7b4618a69735d13c81d38a?Expires=1770953122&OSSAccessKeyId=LTAI5tJJpWQEfrcKHnd1LqsZ&Signature=e7OkRsC1%2Bxx0Ncf2TyJC7Qz5%2BKU%3D&x-oss-traffic-limit=503316480&response-content-disposition=attachment%3B%20filename%3Dtest.cs%3Bfilename%2A%3Dutf-8%27%27test.cs&callback-var=eyJ4OmF1IjoiMTc3MDk1MzEyMi0wLTIxNjAwLTdkZTMiLCJ4Om9yayI6ImQ2WjE0OXRlTEdIMTc1N1lrVTMyMmptSFpHNkNySjZJOE1ITjVrYXBOIiwieDp1ZCI6IjE2LTItNi0wLTYtTi00LU4tMS0xNi0wLU4tTi1OLU4iLCJ4OnNwIjoiMTAwIiwieDp0b2tlbiI6IjQtM2RiYjVkYzkwZGVlNDhiODE5MDBkZTgxYjU0YzQxNTktMi0xLTIwNDgtMDAwMDIwNzkwOTZiNGNjZWFmMWY2ZmE5ZjNjODAzYzctMC0wLTAtMC0wZDc0ZjBkNzcxZjk0MWZiNGZhNWM5MzMzMmUwMmYzMSIsIng6dHRsIjoiMjE2MDAifQ%3D%3D&abt=2_0_&dfi=169&callback=eyJjYWxsYmFja0JvZHlUeXBlIjoiYXBwbGljYXRpb24vanNvbiIsImNhbGxiYWNrU3RhZ2UiOiJiZWZvcmUtZXhlY3V0ZSIsImNhbGxiYWNrRmFpbHVyZUFjdGlvbiI6Imlnbm9yZSIsImNhbGxiYWNrVXJsIjoiaHR0cHM6Ly9jbG91ZC1hdXRoLmRyaXZlLnF1YXJrLmNuL291dGVyL29zcy9jaGVja3BsYXkiLCJjYWxsYmFja0JvZHkiOiJ7XCJob3N0XCI6JHtodHRwSGVhZGVyLmhvc3R9LFwic2l6ZVwiOiR7c2l6ZX0sXCJyYW5nZVwiOiR7aHR0cEhlYWRlci5yYW5nZX0sXCJyZWZlcmVyXCI6JHtodHRwSGVhZGVyLnJlZmVyZXJ9LFwiY29va2llXCI6JHtodHRwSGVhZGVyLmNvb2tpZX0sXCJtZXRob2RcIjoke2h0dHBIZWFkZXIubWV0aG9kfSxcInVscnBcIjoke2h0dHBIZWFkZXIueC11bHJwfSxcImlwXCI6JHtjbGllbnRJcH0sXCJwb3J0XCI6JHtjbGllbnRQb3J0fSxcIm9ya1wiOiR7eDpvcmt9LFwib2JqZWN0XCI6JHtvYmplY3R9LFwic3BcIjoke3g6c3B9LFwidWRcIjoke3g6dWR9LFwidG9rZW5cIjoke3g6dG9rZW59LFwiYXVcIjoke3g6YXV9LFwidHRsXCI6JHt4OnR0bH0sXCJkdF9zcFwiOiR7eDpkdF9zcH0sXCJoc3BcIjoke3g6aHNwfSxcImNsaWVudF90b2tlblwiOiR7cXVlcnlTdHJpbmcuY2xpZW50X3Rva2VufX0ifQ%3D%3D&ud=16-2-6-0-6-N-4-N-1-16-0-N-N-N-N
````
## 贡献

欢迎提出问题、改进建议或直接提交 Pull Request！

## 联系方式

- [前往我的Github](https://github.com/sunshineJAS09)
- [前往我的B站首页](https://space.bilibili.com/604524574/)

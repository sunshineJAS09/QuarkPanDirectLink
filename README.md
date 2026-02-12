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
## 贡献

欢迎提出问题、改进建议或直接提交 Pull Request！

## 联系方式

- [前往我的Github](https://github.com/sunshineJAS09)
- [前往我的B站首页](https://space.bilibili.com/604524574/)

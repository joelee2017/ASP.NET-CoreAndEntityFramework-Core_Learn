# ASP.NET-CoreAndEntityFramework-Core_Learn
從零開始學ASP.NET Core 與EntityFramework Core_課程練習筆記
作者：梁桐銘- 微軟最有價值專家（Microsoft MVP）
網址：https://www.52abp.com/yoyomooc/aspnet-core-for-beginners-Index

[TOC]

------

##### 一、使用VS2019 創建ASP.NET Core Web 程序

- 在**配置新項目**菜單欄中，鍵入項目的名稱。我將其命名為`StudentManagement`。我們將創建一個ASP.NET Core web應用程序，在這個程序中，我們將創建、讀取、更新、刪除學生。

- 取消選中"為HTTPS 配置"複選框，如上圖所示，關閉身份驗證。

- 空：名稱暗示的"空"模板不包含任何內容。這是`我们將使用的模板`，**並從頭開始手動設置所有內容，以便我們清楚地了解不同部分如何組合在一起**。

------

##### 二、ASP.NET Core Web 項目文件

- **TargetFramework**：顧名思義，此元素是用於指定應用程序的目標框架，即您希望為應用程序提供的APId程序集。為了指定目標框架，我們使用了一個名為Target Framework Moniker(TFM)的東西。
- 正如您在上面的示例中所看到的，我們的應用程序針對TargetFramework的值為net5.0。即是.NET 5.0的Moniker。當我們創建此應用程序時，我們從**新建項目中**下拉列表中選擇了**.NET 5.0**作為目標框架。

------

##### 三、ASP.NET Core 中的Main 方法

- ASP.NET Core應用程序最初作為控制台應用程序啟動，而`Program.cs`文件中的`Main()`方法就是入口。因此，當運行時執行我們的應用程序時，它會查找此`Main()`方法以及執行配置開始的地方。

- `Main()`方法配置ASP.NET Core並啟動它，此時，它成為一個ASP.NET Core Web應用程序。因此，如果你跟踪一下`Main()`方法，它會調用`CreateWebHostBuilder()`方法傳遞命令行參數。 `CreateWebHostBuilder()`方法調用靜態類`WebHost`中的靜態方法`CreateDefaultBuilder()`。

-  `CreateDefaultBuilder()`方法會在服務器上創建一個已經預設置好的默認值。 `CreateDefaultBuilder()`方法執行多項操作來創建服務器。

- `CreateDefaultBuilder()`方法是**用於在服務器上創建程序配置的默認值而存在**。它作為設置服務器的一部分，還使用了`IWebHostBuilder`接口中的`UseStartup()`的擴展方法來配置`Startup`類。

------

##### 四、ASP.NET Core 進程內(InProcess)託管

需要在項目文件中添加`` `元素，其值為`InProcess`

```xml
<AspNetCoreHostingModel> InProcess </AspNetCoreHostingModel >
```

使用InProcess 託管，應用程序託管在IIS 工作進程(w3wp.exe 或iisexpress.exe)中。使用InProcess 託管，只有一個Web 服務器，它是承載我們的應用程序的IIS 服務器。

![7 進程內託管圖示](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/7-1.png)

###### 進程內(InProcess)託管

- 在本章節中，讓我們了解`CreateDefaultBuilder()`方法用於配置和設置Web服務器的功能。ASP.NET Core應用程序可以託管在進程內(InProcess)或進程外(OutOfProcess)中。在本章節中，我們將討論進程內(InProcess)託管，在下一個視頻中，我們將討論進程外(OutOfProcess)託管。
- 當我們選擇使用一個可用的項目模板，創建一個新的ASP.NET Core 項目時，該項目默認為所有的IIS 和IIS Express 的配置都是作為進程內託管(InProcess)。

- 獲取當前InProcess 託管名，請於 Startup.cs 中 Configure 加入

```c#
// 要獲取執行應用程序的進程名稱，請使用
var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

// 替換原句
await context.Response.WriteAsync(processName);
```

- 當我們從Visual Studio 運行項目時，它默認使用IISExpress。

- **IIS Express**是IIS的輕量級自包含版本，針對應用程序開發進行了優化。我們不會將它用於生產。在生產中我們會使用IIS。

###### 進程外(out-of-Process)託管

- 有2 個Web 服務器,內部Web 服務器和外部Web 服務器。
- 內部Web 服務器是Kestrel， 外部Web 服務器可以是IIS，Nginx 或Apache。
- 使用InProcess 託管，只有一個Web 服務器，承載ASP.NET Core 應用程序的IIS。因此，在內部和外部Web 服務器之間,他們的代理和請求不沒有性能的損失。

###### 什麼是Kestrel

Kestrel 是ASP.NET Core 的跨平台Web 服務器。.NET Core 支持的所有平台和版本都支持它。它默認包含在ASP.NET Core 中作為內部服務器。Kestrel 本身可以用作邊緣服務器，即面向互聯網的Web 服務器，它可以直接處理來自客戶端的傳入HTTP 請求。

在Kestrel中，用於託管應用程序的進程是`dotnet.exe`。當我們使用`.NET Core CLI`(命令行界面)運行.NET Core應用程序時，應用程序使用Kestrel作為Web服務器。

.NET Core CLI 是一個用於開發.NET 核心應用程序的跨平台工具。使用CLI 命令我們做：

- 根據指定的模板創建新項目，配置文件或解決方案
- 恢復.Net Core 項目所需的所有依賴項和工具包
- 生成項目及其所有依賴項
- 運行.net Core 項目等等......

我們可以使用.NET Core CLI 做很多事情。

###### 簡單說下CLI

使用.NET Core CLI 運行我們的ASP.NET Core 應用程序。

- 啟動Windows 命令提示符
- 將目錄更改為包含ASP.NET Core項目的文件夾，然後執行`dotnet run`命令
- 專案資料夾\ StudentManagement > `dotnet run`

在.NET Core CLI 生成並運行項目之後，它會顯示用於訪問應用程序的URL。在我的例子中，應用程序可以通過訪問瀏覽器地址在 `http：//localhost：5000 查看内容。`

於Kestrel，用於託管和執行應用程序的進程是`dotnet.exe`。

因此，當我們導航到`http：//localhost：5000`時，我們將看到顯示進程名稱`dotnet`。

------

##### 五、ASP.NET Core 進程外(out-of-process)託管

###### ASP.NET Core 進程內(InProcess)託管

我們先簡單回顧下ASP.NET Core 中,要配置InProcess 的服務器，

需要在項目文件中添加`` `元素，其值为`InProcess`

```xml
<AspNetCoreHostingModel> InProcess </AspNetCoreHostingModel >
```

使用InProcess 託管，應用程序託管在IIS 工作進程(w3wp.exe 或iisexpress.exe)中。使用InProcess 託管，只有一個Web 服務器，它是承載我們的應用程序的IIS 服務器。

![7 進程內託管圖示](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/7-1.png)

###### ASP.NET Core 進程外(out-of-process)託管

- 有2 個Web 服務器,內部Web 服務器和外部Web 服務器。
- 內部Web 服務器是Kestrel， 外部Web 服務器可以是IIS，Nginx 或Apache。在上節課中我們討論了什麼是Kestrel

根據您運行ASP.NET Core 應用程序的方式的不同，可能會,也可能不會使用外部Web 服務器。

Kestrel是嵌入在ASP.NET Core應用程序中的跨平台web服務器。使用`进程外(out-of-Process)托管`, Kestrel可通過以下兩種方式來進行使用：

**Kestrel可以用作面向互聯網的web服務器,直接處理傳入的HTTP請求。** 在此模型中,我們不使用外部web服務器。只使用Kestrel,它作為服務器可以自主面向互聯網,直接處理傳入的HTTP請求。當我們使用. net Core CLI運行ASP.NET Core應用程序時, Kestrel是唯一用於處理和處理傳入HTTP請求的web服務器。

![kestrel直面互聯網](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/7-2.png)

**Kestrel 還可以與反向代理服務器(如IIS、Nginx 或Apache) 結合使用。**

![配合方向代理](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/7-3.png)

有兩種方法可以配置進程外託管 :

- 方法一：將`<AspNetCoreHostingModel>`元素添加到應用程序的項目文件中，其值為`OutOfProcess`

```xml
<AspNetCoreHostingModel> OutOfProcess </AspNetCoreHostingModel >
```

- 方法二：默認為`OutOfProcess`託管。因此，如果我們從項目文件中刪除`<AspNetCoreHostingModel>`元素，默認情況下ASP.NET Core將使用`OutOfProcess`託管。

###### 為什麼我們需要一個反向代理服務器？

因為Kestrel 使用"進程外(out-of-process)託管", 結合反向代理服務器是一個不錯的選擇, 因為它提供了額外的配置和安全性層。它可能會更好地與現有基礎設施集成。它還可用於負載平衡。

當我們使用.NET Core CLI運行ASP.NET Core項目時，默認情況下它會忽略我們在.csproj文件中指定的`托管設置`。因此項目文件中的``AspNetCoreHostingModel`標籤下的值是被忽略了的。無論您指定的值(InProcess或OutOfProcess)如何，它始終都是OutOfProcess託管，都是通過Kestrel託管應用程序,同時處理http請求。

------

##### 六、ASP.NET Core Launchsettings.json 文件

討論在ASP.NET Core項目中`launchsettings.json`文件的重要性。

###### launchsettings.json 文件

- 您將在項目根文件夾的**"Properties"**文件夾中找到此文件。
- 當我們從Visual Studio 或使用.NET Core CLI 運行此ASP.NET Core 項目時，將使用此文件中的設置。
- **此文件僅用於本地開發環境**。我們不需要把它發佈到生產環境的ASP.NET Core程序中。
- 如果您希望您的ASP.NET Core 應用程序在發布和部署應用程序時使用某些獨立的設置，請將它們存儲在appsettings.json 文件中。我們通常將應用程序的配置信息存儲在此文件中，比如數據庫連接字符串。
- 我們還可以使用不同環境的appsettings.json 文件。例如，appsettings.Staging.json 用於臨時環境。在- ASP.NET Core 中，除了appsettings.json 文件外，我們還可以配置源，如環境變量，用戶密鑰，命令行參數甚至創建屬於我們自己的自定義配置源。

| commandName | AspNetCoreHostingModel 的值 |   Internal Web Server(內部服務器) |   External Web Server(外部服務器) |
| ----------- | :-------------------------: | --------------------------------: | --------------------------------: |
| 項目        |      忽略託管設置的值       |     只使用一個Web 服務器- Kestrel |     只使用一個Web 服務器- Kestrel |
| IISExpress  |    進程內託管(InProcess)    | 只使用一個Web 服務器- IIS Express | 只使用一個Web 服務器- IIS Express |
| IISExpress  |  進程外託管(OutOfProcess)   |                           Kestrel |                       IIS Express |
| IIS         |    進程內託管(InProcess)    |         只使用一個Web 服務器- IIS |         只使用一個Web 服務器- IIS |
| IIS         |  進程外託管(OutOfProcess)   |                           Kestrel |                               IIS |

------


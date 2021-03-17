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

##### 七、ASP.NET Core Appsettings.json文件

們將討論ASP.NET Core項目中`appsettings.json`文件的本質。

在`web.config`ASP.NET Core中，應用程序配置設置可以來自以下不同的配置源。

- 文件（appsettings.json，appsettings..json）`Environment`環境不同，託管在對應環境。
- 用戶機密（用戶機密）
- 環境變量
- 命令行參數（命令行參數）

**appsettings.json**文件：我們的項目是通過ASP.NET Core合理的“空”模板創建的，所以我們的項目中已經有一個appsettings.json的文件了。我們可以對文件進行如下修改，補充一個`MyKey`的鍵值對：

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "MyKey": " appsettings.json中Mykey的值"
}
```

###### ASP.NET Core IConfiguration服務

- `IConfiguration` 服務是為了從ASP.NET Core。
- 如果**在多個配置源中**具有**相同名稱**的配置設置，簡單來說就是重名了，則後面的配置源將覆蓋先前的配置源。
- 靜態類`WebHost`的`CreateDefaultBuilder()`方法在應用程序啟動時會自動去調用，按特定的順序重新配置源。
- 要查看配置源的重新順序，請查看以下鏈接上的`ConfigureAppConfiguration()`方法 https://github.com/aspnet/MetaPackages/blob/release/2.2/src/Microsoft.AspNetCore/WebHost.cs

------

##### 七、ASP.NET Core中的中間件Middleware

###### ASP.NET Core 中的中間件是什麼？

在ASP.NET Core 中，中間件(Middleware)是一個可以處理HTTP 請求或響應的軟件管道。ASP.NET Core 中給中間件組件的定位是具有非常特定的用途。例如，我們可能有需要一個中間件組件驗證用戶，另一個中間件來處理錯誤，另一個中間件來提供靜態文件，如JavaScript 文件，CSS 文件，圖片等等。

我們使用這些中間件組件在ASP.NET Core中設置請求處理管道。而正式這管道決定瞭如何處理請求。而請求管道是由`Startup.cs`文件中的`Configure()`方法進行配置，它也是應用程序啟動的一個重要部分。以下是`Configure()`方法中的代碼。

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
}
```

如您所見，由空項目模板生成的`Configure()`方法中的代碼中，一個非常簡單的請求處理管道中，只有兩個中間件。

`UseDeveloperExceptionPage`是一個中間件，第二個中間件是使用`Run()`方法設置的。現在，通過這個非常簡單的請求處理管道，我們所有的應用程序都可以將消息寫入，然後在由瀏覽器顯示出來。

- 在ASP.NET Core中，中間件組件可以同時訪問-**傳入請求和傳出響應**。因此，中間件組件可以處理傳入請求並將該請求,傳遞給管道中的下一個中間件以進行進一步處理。例如，如果您有一個日誌記錄中間件，它可能只是記錄請求的時間，它處理完畢後將請求傳遞給下一個中間件以進行進一步處理。
- **中間件組件可以處理請求,並決定不調用管道中的下一個中間件，從而使管道短路**，官方微軟給了一個英文的名字叫`"terminal middleware "`,翻譯為"終端中間件"。短路通常是被允許的，因為它可以避免一些不必要的工作。例如,如果請求的是像圖像或css文件這樣的靜態文件,則StaticFiles中間件可以處理和服務該請求並使管道中的其餘部分短路。這個意思就是說，在我們的示例中,如果請求是針對靜態文件,則Staticile中間件不會調用MVC中間件,避免一些無謂的操作。
- **中間件組件可以通過傳入的HTTP請求來響應HTTP請求。**例如，管道中的`mvcmiddleware`處理對`URL/students`的請求並返回學生列表信息。隨著我們在本課程中的進展，在我們即將推出的視頻中，我們將演示`mvcmiddleware`在管道中如何進行請求和響應的。
- **中間件組件還可以處理傳出響應**。例如，日誌記錄中間件組件可以記錄響應發送的時間。此外，它還可以通過計算接收請求和響應發送時間之間的差異來計算處理請求所花費的所有時間。
- **中間件組件是按照添加到管道的順序進行執行的**。所以我們要注意以正確的順序添加中間件，否則應用程序可能無法按預期運行，哪怕編譯成功，但是程序還是會出錯。
- **中間件組件應該用NuGet包的形式提供**。由NuGet處理更新，盡量將中間件拆的足夠小，提供每個中間件獨立更新的能力。

根據您的程序要求，您可以向請求處理管道添加盡可能多的中間件組件。例如，如果您正在使用一些靜態HTML 頁面和圖像,開發簡單的Web 應用程序，那麼您的請求處理管道可能只包含"StaticFiles"中間件。這個就是模塊化設計帶來的好處，讓每個人都像玩積木一樣。另一方面，如果您正在開發一個安全的數據驅動設計的Web 應用程序，那麼您可能需要幾個中間件組件，如StaticFiles 中間件，身份驗證中間件，授權中間件，MVC 中間件等。數據驅動設計,可以簡單理解為複雜項目。

------

##### 八、配置ASP.NET Core請求(Request)處理管道

作為應用程序啟動的一部分，我們要在`Configure()`方法中設置**請求處理管道**。

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
         if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //有點類似setting的感覺,在這個middleware會針對request的內容去做初始化
            app.UseRouting();


            //這邊才是真正去設定的地方,使用delegate去做設定
            app.UseEndpoints(endpoints =>
            {
                 endpoints.MapGet("/", async context =>
                {
                    //避免亂碼
                    context.Response.ContentType = "text/plain;charset=utf-8";

                    await context.Response.WriteAsync("this is a 第一 hello world ");

                    // 第二次調用
                    await context.Response.WriteAsync("this is a second hello world ");
                });                             


                endpoints.MapGet("/test", async context =>
                {
                    await context.Response.WriteAsync("this is a second test hello world ");
                });
            });
    }

}
```

UseDeveloperExceptionPage中間件：顧名思義，如果存在異常並且環境是`Development`，此中間件會被調用，顯示**開發異常頁面**。我們將在後面的視頻中討論這個**DeveloperExceptionPage中間件**和**環境變量的使用**。

Routing 內容補充：

https://ithelp.ithome.com.tw/articles/10242210

https://blog.darkthread.net/blog/aspnetcore-middleware-lab/

Middleware 處理 Request 的原理，Request 會通過一連串 Middleware 組成的 Pipeline，每個 Middleware 在經手 Request 時可以決定接手處理傳回 Response 或呼叫 next() 交給下一個 Middleware 處理。而 next() 執行完下一個 Middleware 邏輯後，主導權又回到上層 Middleware。(如下圖) 換言之，註冊的先後順序很重要，Middleware 1 可優先決定挑選哪些 Request 留下來處理，吃剩的再交給 Middleware 2 發落；而 Middleware 3、Middleware 2 執行完要傳 Response 給使用者之前，Middleware 1 也有權利再跑一段程式對 Response 內容修改加料。

![img](https://blog.darkthread.net/Posts/files/Fig2_637131016820965167.png)

- 請記住，ASP.NET Core中的中間件可以訪問傳入請求和傳出響應
- 請求先到達`Middleware1`，它記錄**(MW1：傳入請求)**，因此我們首先看到此消息。
- 然後`Middleware1`調用`next()`。`next()`會調用管道中的`Middleware2`。
- `Middleware2`記錄**(MW2：傳入請求)**。
- 然後`Middleware2`會調用`next()`再調用`Middleware3`.
- `Middleware3`處理請求並生成響應。因此，我們看到的下一條消息是`(MW3：處理請求全生成響應)`
- 此時管道開始逆轉。
- 此時控制權將，交回到`Middleware2`，並將`Middleware3`生成的響應傳遞給它。`Middleware2`記錄**(MW2：傳出響應)**，這是我們接下來看到的。
- 最後,`Middleware2`將控制權交給`Midleware1`。
- `Middleware1` 記錄(MW1: 傳出響應), 這是我們最後看到的。

###### 請求處理管道的中3 個非常重要的知識點：

- 所有的`請求`都會在每個中間件組件調用`next()方法`之前觸發。`請求`按照圖中箭頭的所示方向，依次穿過所有管道。
- 當中間件處理請求並產生響應時，請求處理流程在管道中開始反向傳遞。
- 所有的`響應`都會在每個中間件組件`調用next()方法`之前觸發。`響應`按照圖中箭頭的所示方向，依次穿過所有管道。

------

##### 九、ASP.NET Core 中的靜態文件

我們將學習如何使ASP.NET Core 應用程序，支持靜態文件，如HTML，圖像，CSS 和JavaScript 文件。

## 靜態文件

- 默認情況下，ASP.NET Core 應用程序不會提供靜態文件。
- 靜態文件的默認目錄是`wwwroot`，此目錄必須位於項目文件夾的根目錄中。

將圖片複製並粘貼到wwwroot文件夾中。我們假設文件的名稱是banner.jpg。為了能夠從瀏覽器訪問此文件，路徑為:`http://{{serverName}}/banner.jpg`在我們的示例中，我們在本地計算機上運行，因此URL將如下所示。您的計算機上的端口號可能不同。`http://localhost:13380/banner.jpg`。

從我的電腦，然後導航到上面的Url的時候，我們仍然是通過`Run()`方法的中間件，返迴響應的結果，。我沒有看到圖片`banner.jpg`。這是因為，目前我們的應用程序請求處理管道，沒有可以提供靜態文件的所需中間件。我們需要使用的中間件`UseStaticFiles()`。

修改`Configure()`方法中的代碼，將`UseStaticFiles()`中間件添加到我們的應用程序的請求處理管道中，如下所示。

```csharp
 app.UseStaticFiles();
```

在`wwwroot`文件夾中沒有像vs提供的默認模板一樣把圖片、CSS和JavaScript文件進行分類，我們建議將不同的文件類型進行文件夾區分，參考下圖文件夾層次結構：

###### 提供wwwroot 文件夾之外的靜態文件

默認情況下，`UseStaticFiles()`中間件僅提供wwwroot文件夾中的靜態文件。如果您願意，我們還可以在`wwwroot`文件夾之外提供靜態文件。

###### 提供默認文檔

大多數Web程序都有一個默認文檔，它是用戶訪問程序地址時顯示的文檔內容。例如，您有一個名為`default.html`的文件，並且您希望在用戶訪問應用程序的根URL時提供它，即`http://localhost:13380`

此時，我們來訪問這個地址看看，我看到我使用`Run()`方法註冊的中間件產生的回調。但是我沒有看到默認文檔`default.html`的內容。為了能夠提供默認頁面，我們必須在應用程序的請求處理管道中插入`UseDefaultFiles()中间件`。

```csharp
//添加默認文件中間件
app.UseDefaultFiles();
//添加靜態文件中間件
app.UseStaticFiles();
```

> 請注意：必須在`UseStaticFiles`之前,註冊`UseDefaultFiles`來提供默認文件。`UseDefaultFiles`是一個URL重寫器，實際上並沒有提供文件。它只是將`URL`重寫定位到默認文檔，然後還是由靜態文件中間件提供。地址欄中顯示的URL仍然是根節點的URL，而不是重寫的URL。

以下是`UseDefaultFiles`中間件默認會去查找的地址信息

```
- index.htm 的默認文件
- index.html
- default.htm
- default.html
```

###### UseFileServer 中間件

`UseFileServer`結合了`UseStaticFiles，UseDefaultFiles和UseDirectoryBrowser`中間件的功能。**DirectoryBrowser**中間件，支持目錄瀏覽，並允許用戶查看指定目錄中的文件。我們可以用**UseFileServer中間件**替換**UseStaticFiles和UseDefaultFiles**中間件。

```csharp
//使用UseFileServer而不是UseDefaultFiles和UseStaticFiles
FileServerOptions fileServerOptions = new FileServerOptions();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("52abp.html");
app.UseFileServer(fileServerOptions);
```

------

##### 十、ASP.NET Core 開發人員異常頁面

###### UseDeveloperExceptionPage 中間件

**我們談談在Startup類的Configure()方法中以下代碼**:

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseFileServer();

    app.Run(async (context) =>
    {
 		throw new Exception("您的請求在管道中發生了一些異常，請檢查。");
        await context.Response.WriteAsync("Hello World!");
    });
}
```

`UseFileServer`中間件結合了`UseDefaultFiles`和`UseStaticFiles`中間件的功能。在我們之前的系列視頻中，我們在wwwroot文件夾中包含了一個名為`default.html`的默認html文檔。

因此，對應用程序根URL的請求即`http：//localhost：49119`由`UseFileServer`處理中間件和管道從那裡反轉。因此，在我們`Run()`方法註冊的請求管道中的下一個中間件也無法執行，因此我們不會看到此中間件拋出的異常。

現在，如果我們向`http：//localhost：49119/abc.html`發出請求，我們會看到異常。因為，在這種情況下，`UseFileServer`中間件找不到名為`abc.html`的文件。它會繼續去調用管道中的下一個中間件，在我們的例子中是我們使用`Run()`方法註冊的中間件。此中間件拋出異常，我們按預期看到異常詳細信息。

如果您對傳統的ASP.NET有任何經驗，那麼您必須非常熟悉此頁面。這類似於傳統的ASP.NET中的**黃色死亡屏幕**。

此`Developer Exception`頁麵包含異常詳細信息:

- 堆棧跟踪，包括導致異常的文件名和行號
- Query String, Cookies 和HTTP headers

目前，在異常頁面的"Query "選項卡上，我們看到"無QueryString 數據"。如果請求URL 中有任何查詢字符串參數，如下所示，您將在"Query "選項卡下看到它們。

```
http://localhost:48118/abc.html?country=person&state=islocked
```

###### 自定義UseDeveloperExceptionPage 中間件

與ASP.NET Core中的大多數其他中間件組件一樣，我們也可以自定義`UseDeveloperExceptionPage`中間件。每當您想要自定義中間件組件時，請始終記住您可能擁有相應的`OPTIONS对象`。那麼，要自定義`UseDeveloperExceptionPage`中間件，

```csharp
DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
{
    SourceCodeLineCount = 10
};
app.UseDeveloperExceptionPage(developerExceptionPageOptions);
```

`SourceCodeLineCount`屬性指定在導致異常的代碼行之前和之後要包含的代碼行數。

------

##### 十一、ASP.NET Core 中的環境變量

**軟件開發環境**在大多數軟件開發組織中，我們通常具有以下開發環境。

- 開發環境--Development
- 演示(模擬、臨時)環境--Staging
- 生產環境-- Production

**為什麼我們需要不同的開發環境，如開發，演示，生產等等環境。**

**開發環境：**我們的軟件開發人員通常將此環境用於我們的日常開發工作。我們希望在開發環境中加載非縮小的JavaScript 和CSS 文件，以便於調試。類似地，如果存在未處理的異常，我們需要開發人員異常頁面，以便我們可以理解異常的根本原因並在需要時進行修復。

**演示環境：**許多組織或者公司嘗試使其演示環境盡可能與實際生產環境保持一致。此環境的主要原因是識別任何與部署相關的問題。此外，如果您正在開發B2B(企業對企業)應用程序，您可能正在與其他服務提供商系統連接。許多組織通常設置其臨時環境以與服務提供商進行交互，以進行完整的端到端測試。我們通常不會在演示環境中進行故障排除和調試，同時為了獲得更好的性能，我們需要加載縮小的JavaScript 和CSS 文件。如果存在未處理的異常，則顯示用戶友好的錯誤頁面而不是開發人員異常頁面。用戶友好的錯誤頁面不包含任何技術細節。它包含如下通用消息 :"出現問題，請使用下面的聯繫方式發送電子郵件，聊天或致電我們的應用程序支持"

**生產環境：**我們用於日常業務的實際環境。應配置生產環境以獲得最大的安全性和性能。因此，加載縮小的JavaScript 和CSS 文件以提高性能。為了更好的安全性，請顯示用戶友好錯誤頁面而不是開發人員異常頁面。Developer Exception 頁面上的技術細節對最終用戶沒有意義，惡意用戶可以使用它們進入您的應用程序。

###### IHostingEnvironment 服務的中常用的方法介紹:

使用IHostingEnvironment 服務的以下方法來標識運行應用程序的環境。

- IsDevelopment()
- IsStaging()
- IsProduction()

如果您擁有UAT(用戶驗收測試)或QA(質量保證)環境等自定義環境，該怎麼辦？

> 開發環境(development)、集成環境(integration)、測試環境(testing)、QA 驗證，模擬環境(staging)、生產環境(production)。

那麼，ASP.NET Core 也支持這些自定義環境。例如，要檢查環境是否為UAT，請使用IsEnvironment()方法，如下所示。 `env.IsEnvironment("UAT")`

```csharp
//如果環境是Development serve Developer Exception Page
if(env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//否则提供友好錯誤頁面聯係信息
else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT")){
    app.UseExceptionHandler("/Error");
}
```

Tag Helpers 是ASP.NET Core 中的新功能。在一個Razor 視圖裡面，也可以在.CSHTML 頁面中進行使用，稱為環境標記助手。

此環境標記幫助程序支持根據`ASPNETCORE_ENVIRONMENT`變量的值呈現不同的內容。在我們學習本課程並為我們的應用程序創建模型，視圖和控制器時，我們將詳細討論Tag Helpers，包括環境標記助手(Environment Tag Helper)。

------

##### 十二、詳解ASP.NET Core MVC 的設計模式

在本節課中我們要討論的內容：

- 什麼是MVC？
- 它是如何工作的？

###### 什麼是MVC

![ASP.NET Core MVC 概觀| Microsoft Docs](https://docs.microsoft.com/zh-tw/aspnet/core/mvc/overview/_static/mvc.png?view=aspnetcore-5.0)

MVC由三個基本部分組成-模型(Model)，視圖(View)和控制器(Controller)。它是用於實現應用程序的**用戶界面層**的架構設計模式。一個典型的實際應用程序通常具有以下層：

- 用戶展現層
- 業務邏輯處理層
- 數據訪問讀取層MVC 設計模式通常用於實現應用程序的用戶界面層。

從Web 瀏覽器我們發出請求，URL 地址如下所示

![15請求完整圖片](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/15-3.png)

- 當我們的請求到達服務器時，作為MVC 設計模式下的Controller，會接收請求並且處理它。
- Controller 會創建模型(Model)，該模型是一個類文件，會進行數據的展示。
- 在Molde 中，除了數據本身，Model 還包含從底層數據源(如數據庫)查詢數據後的邏輯信息。
- 除了創建Model 之外，控制器還選擇View 並將Model 對像傳遞給該View。
- 視圖僅負責呈現Modle 的數據。
- 視圖會生成所需的HTML 以顯示模型數據，即Controller 提供給它的學生數據。
- 然後，此HTML 通過網絡發送，最終呈現在發出請求的用戶面前。

###### Model (模型)

因此，在當前案例中Model，是由Student 類和管理學生數據的StudentRepository 類組成，如下所示。

```csharp
public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }
    }

 public interface IStudentRepository
    {
        Student GetStudent(int id);
        void Save(Student student);
    }

public class StudentRepository : IStudentRepository
    {
        public Student GetStudent(int id)
        {
           //邏輯實現 查詢學生詳情信息
            throw new NotImplementedException();
        }

        public void Save(Student student)
        {
           //邏輯實現保存學生信息
            throw new NotImplementedException();
        }
    }
```

我們使用**Student**類來保存學生數據，而**StudentRepository\**類則負責查詢並保存學生信息到數據庫中。如果要概括model的話，它就是MVC中用於\**包含一組數據的類和管理該數據的邏輯信息。** 表示數據的類是Student類，管理數據的類是StudentRepository類。

如果您想知道我們為什麼使用`IStudentRepository`接口。我們不能只使用沒有接口的**StudentRepository**類。

但是其實我們是可以的，但是我們使用接口的原因，是因為接口，允許我們使用依賴注入，而依賴注入則可以幫助我們創建**低耦合且易於測試的系統**。我們將在即將發布的視頻中詳細討論**依賴注入**。

###### View -視圖

MVC中的View應該只包含顯示Controller提供給它的Model數據的邏輯。您可以將視圖視為HTML模板。假設在我們的示例中，我們希望在HTML表中顯示**Student**數據。

這種情況下的視圖會和**Student**對像一起提供。**Student**對像是將學生數據傳遞給視圖的模型。視圖的唯一作用是將學生數據顯示在HTML表中。這是視圖中的代碼。

```html
@model StudentManagement.Model.Student

<!DOCTYPE html>
<html>
  <head>
    <title>学生页面详情</title>
  </head>
  <body>
    <table>
      <tr>
        <td>Id</td>
        <td>@model.Id</td>
      </tr>
      <tr>
        <td>名字</td>
        <td>@model.Name</td>
      </tr>
      <tr>
        <td>主修科目</td>
        <td>@model.Major</td>
      </tr>
    </table>
  </body>
</html>
```

在MVC中，View僅負責呈現模型數據。視圖中不應該有復雜的邏輯。視圖中的邏輯必須非常少而且要小，並且它也必須僅用於呈現數據。如果到達表示邏輯過於復雜的點，請考慮使用**ViewMode** l或**View Component**。**View Components**是此版本MVC中的新增功能。我們可以在以後的課程中討論它。

###### Controller 控制器

當來自瀏覽器的請求到達我們的應用程序時，作為MVC 中的控制器，它處理傳入的http 請求並響應用戶的操作。

在這種情況下，用戶已向URL發出請求(/student/details/1)，因此該請求被映射到**StudentController**中的**Details**方法，並向其傳遞**Student**的ID，在本例中為1.此映射為由我們的web應用程序中定義的路由規則完成。

```csharp
 public class StudentController:Controller
    {
        private IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public IActionResult Details(int id)
        {
            Student model = _studentRepository.GetStudent(id);
            return View(model);
        }
    }
```

如您所見，從**Details**方法中的代碼，控制器將生成模型，在這種情況下，Model是**Student**對象。要從基礎數據(如數據庫)源檢索**Student**數據，控制器使用**StudentRepository**類。

一旦控制器使用所需數據構造了**Student**模型對象，它就會將該**Student**模型對像傳遞給視圖。然後，視圖生成所需的HTML，以顯示Controller提供給它的**Student**數據。然後，此HTML通過網絡發送給發出請求的用戶。

###### 小結

**MVC 是用於實現應用程序的用戶界面層的架構設計模式**

- 模型(Model)：包含一組數據的類和管理該數據的邏輯信息。
- View(視圖)：包含顯示邏輯，用於顯示Controller 提供給它的模型中數據。
- Controller(控制器):處理Http 請求，調用模型，請選擇一個視圖來呈現該模型。

正如您所看到的，在MVC 設計模式中，我們可以清楚地分離各個關注點，讓他們各司其職。每個組件都有一個非常具體的任務要做。

------

##### 十三、在ASP.NET Core 中安裝MVC

###### 兩個步驟學會在ASP.NET Core 配置MVC

步驟1：在Startup.cs 文件中的Startup 類的**ConfigureServices()**方法中,見下方代碼。這行代碼將所需的MVC 服務添加到ASP.NET Core 中的依賴注入容器中。

```csharp
 services.AddControllersWithViews();
```

步驟2：在Configure()方法中，將**UseMvcWithDefaultRoute()**中間件添加到我們的應用程序的請求處理管道中。修改代碼，如下所示。

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();

	app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
}
```

注意，我們在**UseMvcWithDefaultRoute()**中間件之前放置了**UseStaticFiles()**中間件。此順序很重要，因為如果請求是針對靜態文件(如圖像，CSS或JavaScript文件)，則**UseStaticFiles()**中間件將處理請求並使管道的其餘部分短路。

因此, 如果請求是針對靜態文件, 則不會執行**UseMvcWithDefaultRoute () **中間件, 從而避免不必要的處理。

另一方面,如果請求是MVC請求, **UseStaticFiles ()**中間件將把該請求傳遞給**UseMvcWithDefaultRoute()**中間件,中間件將處理請求並生成響應。

請注意,除了**UseMvcWithDefaultRoute ()**中間件之外,我們還有**UseMvc ()**中間件。現在,讓我們使用**UseMvcWithDefaultRoute()**中間件。

###### 添加HomeController

在項目根文件夾中添加Controllers 文件夾。在"控制器"中添加一個新的控制器。複製並粘貼以下代碼。

```csharp
public class HomeController
{
    public string Index()
    {
        return "Hello from MVC";
    }
}
```

生成解決方案並向應用程序URL發出請求- `http://localhost:49119`。現在，您將看到瀏覽器中顯示的字符串- **"Hello from MVC"**。

------

##### 十四、ASP.NET Core 為什麼有AddMvc 和AddMvcCore 他們是什麼關係？

- AddMvcCore()方法只添加核心MVC 服務。
- AddMvc()方法添加了所有必需的MVC 服務。
- AddMvc()方法在內部調用AddMvcCore()方法，以添加所有核心MVC 服務。
- 因此，如果我們調用AddMvc()方法，則無需再次顯式調用AddMvcCore()方法。

------

##### 十五、ASP.NET Core MVC 中的Model 模型

ASP.NET Core 中的模型類不必位於Models 文件夾中，但將它們保存在名為Models 的文件夾中是一種很好的做法，因為以後更容易找到它們。

------

##### 十六、ASP.NET Core 中的依賴注入介紹

###### 使用ASP.NET Core 依賴注入容器註冊服務：

ASP.NET Core 提供以下3 種方法來使用依賴項注入容器註冊服務。我們使用的方法決定了註冊服務的生命週期。

**AddSingleton()**

AddSingleton()方法創建一個`Singleton`服務。首次請求時會創建`Singleton`服務。然後，所有後續請求都使用相同的實例。因此，通常，每個應用程序只創建一次`Singleton`服務，並且在整個應用程序生命週期中使用該單個實例。

**AddTransient()**

AddTransient() 方法可以稱作：暫時性模式，會創建一個Transient 服務。每次請求時，都會創建一個新的Transient 服務實例。

**AddScoped()**

AddScoped()方法創建一個Scoped 服務。在範圍內的每個請求中創建一個新的Scoped 服務實例。例如，在Web 應用程序中，它為每個http 請求創建1 個實例，但在同一Web 請求中的其他調用中使用相同的實例,在一個客戶端請求中是相同的，但在多個客戶端請求中是不同的。

###### 為什麼我們使用new關鍵字

如果我們在我們的應用程序中的控制器使用了相同的服務，所有控制器中的代碼都必須更改。這不僅無聊而且容易出錯。

簡而言之，使用new 關鍵字創建依賴關係的實例會產生**緊密耦合**，因此您的應用程序將很難更改。通過依賴注入，我們不會有這種緊密耦合。

使用**依賴注入**，即使我們在我們的應用程序中的其他控制器中使用了相同服務，如果我們想用不同的實現交換它，我們只需要在Startup.cs文件中更改代碼。這樣帶來的效果是單元測試也變得更加容易，因為我們可以通過依賴注入輕鬆地交換依賴項。

------

##### 十七、ASP.NET CoreMVC 中的控制器Controller 

Controller

![20 1](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/20-1.png)

- MVC 中的控制器是一個類，它繼承自`Microsoft.AspNetCore.Mvc.Controller`
- 控制器類名稱後綴為**"Controller"**。例如HomeController。

- 當來自瀏覽器的請求到達我們的應用程序時，作為MVC 中的控制器，它會處理傳入的http 請求並響應用戶操作。
- Controller類包含一組公共方法。Controller類中的這些公共方法稱為**操作方法**( action methods)。正是這些控制器的操作方法處理傳入的http請求。

- 假設用戶透過瀏覽器地址訪問服務時，會映射到控制器中的方法。此映射是由我們應用程序中的**路由規則**定義完成。

- 控制器通過依賴的服務，來查詢模型數據。

- 我們將注入的依賴項分配給**readonly**字段。這是一個很好的做法，因為它可以防止在方法中意外地為其分配另一個值。

- 當控制器擁有所需的模型數據，比如我們正在提供服務或RESTful API，它就可以簡單地返回該模型數據。

###### 小結：

- 當來自瀏覽器的請求到達我們的應用程序時，作為MVC 中的控制器，它會處理傳入的http 請求並響應用戶操作。
- 控制器構建模型(Model)
- 如果我們正在構建API, 則將模型數據返回給調用方
- 或者選擇"View 視圖" 並將模型數據傳遞到視圖,然後視圖生成所需的HTML 來顯示數據

------

##### 十八、ASP.NET CoreMVC 中的視圖View

###### MVC 中的視圖

- 用於顯示**Controller**提供給它的**Model**的業務數據。
- 視圖是帶有嵌入Razor 標記的HTML 模板。
- 如果編程語言是C＃，則視圖文件具有.cshtml 擴展名。

- 對於每個**Controller**，我們在"Views"文件夾中有一個單獨的文件夾。

- 每個視圖文件都有一個相同名稱的控制器操作方法。
- **View()\*方法是由基類\*Controller**提供。
- 它按指定的順序在以下3個位置查找此文件：
  1. 首先在"/Views/Home/"文件夾中
  2. 然後在"/Views/Shared/"文件夾中
  3. 最後在"/Pages/Shared/"文件夾中如果找到視圖文件，則視圖生成的HTML 將發送回發出請求的客戶端。如果找不到視圖文件，我們會收到以下錯誤。

------

##### 十九、在ASP.NET CoreMvc 中的自定義視圖發現

ASP.NET Core MVC 中有提供了幾個View()的重載方法。如果我們使用下面提供View()的重載方法，它將查找與Action 方法同名的視圖文件。

- View()
- View(object model)

指定視圖文件路徑

我們可以指定**視圖名稱**或**視圖文件路徑**。如果我們沒有指定視圖文件的路徑，默認情況下，MVC會在"Views/"文件夾中查找當前控制器方法名稱的".cshtml"文件。

- 使用絕對路徑時，必須加上.cshtml擴展名。
- 使用相對路徑時，必須去掉.cshtml擴展名。

###### 絕對路徑：

會項目的根目錄開始搜索，我們可以使用`**/或〜/**`。所以下面3 行代碼做的事情是一樣的。

```csharp
return View("MyViews/Test.cshtml");
return View("/MyViews/Test.cshtml");
return View("~/MyViews/Test.cshtml");
```

###### 相對視圖文件路徑

指定視圖文件路徑時，我們也可以使用相對路徑。如果你要的返回值在文件夾層次結構中超過了2個深度，請使用`../`兩次

```csharp
 return View("../Test/Update");
 return View("../../MyViews/Details");
```

###### 其他view()重載方法

| 重載方法                            |                     描述                     |
| ----------------------------------- | :------------------------------------------: |
| View(object model)                  | 使用此重載方法將模型數據從控制器傳遞到視圖。 |
| View(string viewName, object model) |           傳遞視圖名稱和模型數據。           |

------

##### 二十、將數據傳遞到ASP.NET CoreMVC 中展示

###### 將數據從控制器傳遞到視圖的三種方法

在ASP.NET Core MVC 中，有3 種方法可以將數據從控制器傳遞到視圖:

1. 使用ViewData 弱類型
2. 使用ViewBag 弱類型
3. 使用強類型模型對象。這也稱為**強類型視圖**。

###### ViewData

- ViewData 是弱類型的字典(dictionary )對象。
- 我們使用string 類型的鍵值，來存儲和查詢ViewData 字典中的數據，。
- 可以從ViewData 字典直接訪問數據，而無需將數據轉換為string 類型。
- 如果我們訪問的是任何其他類型的數據，我們需要將其顯式地轉換為我們期望的類型。

- ViewData 在運行時會進行動態解析，因此它不提供編譯時類型檢查，因此我們不會獲得智能提示。
- 由於我們沒有智能感知，因此編寫代碼的速度降低，錯誤拼寫和打錯的可能性也很高。
- 我們只會在運行時才知道這些錯誤。
- 出於這個原因，我們通常不使用ViewData。
- 當我們使用ViewData 時，我們最終會創建一個弱類型的視圖。

------

##### 二十一、ASP.NETCoreMVC中的ViewBag

**ViewBag**是**ViewData**的包裝器。使用**ViewData**，我們使用string類型的鍵名來存儲和查詢數據。而使用**ViewBag**，我們則使用的是動態屬性而不是字符串鍵。

###### ViewData 和ViewBag 對比

- **ViewData**和**ViewBag**兩者都可以從控制器傳遞數據到視圖
- **ViewBag**是**ViewData**的包裝器
- 它們都是一個弱類型的視圖
- 使用**ViewData**，我們使用字符串鍵來存儲和查詢**ViewData**字典中的數據
- 使用**ViewBag**，我們使用動態屬性來存儲和查詢數據。
- 雙方的**ViewData**和**ViewBag**都是在運行時動態解析。
- 雙方的**ViewData**和**ViewBag**不提供編譯時類型檢查，因此我們沒有得到智能提示。
- 由於我們沒有智能提示，因此編寫代碼的速度降低，錯誤拼寫的可能性也很高。
- 我們只會在運行時才會看到這些錯誤。
- 出於這個原因，我們通常不使用**ViewData**或**ViewBag**。
- 將數據從控制器傳遞到視圖的首選方法是使用強類型模型對象。
- 使用強類型模型對象可創建強類型視圖

------

##### 二十二、ASP.NET CoreMVC 中的強類型視圖

###### 強類型視圖優點

與**ViewData**和**ViewBag**不同，強類型視圖提供編譯時類型檢查和智能提示。通過智能提示支持，我們可以提高工作效率，錯誤拼寫的機率幾乎為零。如果我們確實犯了任何錯誤，我們將在編譯時看到錯誤，而不是在運行時才看到他們。

因此，建議始終使用強類型視圖將數據從控制器傳遞到視圖。

------

##### 二十三、ASP.NET CoreMVC 中的ViewModel

###### 為什麼我們需要在ASP.NET Core MVC 中使用ViewModel

在某些情況下，模型對象可能無法包含視圖所需的所有數據。這個時候就需要使用ViewModel 了。它會包含視圖所需的所有數據。

###### ViewModel 示例

ViewModel類可以存在於ASP.NET Core MVC項目的任何位置，但為了管理方便，我們通常將它們放在一個名為ViewModels的文件夾中

此ViewModel 類包含視圖所需的所有數據。通常，我們使用ViewModels 在View 和Controller 之間傳遞數據。因此，ViewModels 也簡稱為數據傳輸對像或DTO。

數據傳輸對象==DTO

------

##### 二十四、ASP.NET CoreMVC 中的佈局顯示

###### 為什麼需要佈局視圖

大多數Web 應用程序網站通常都有以下部分組成

- Header-頭部
- Footer-頁腳
- Menu-導航菜單
- View-具體內容的視圖

![28 1](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/28-1.png)

如果沒有佈局視圖，我們將在Web 程序中的每個視圖中，重複顯示很多HTML 代碼，比如選單欄，導航信息，關於我們，footer 頁腳等等。在每個視圖中都有這個重複的

使用佈局視圖，在所有視圖中保持一致的外觀變得更加容易，因為我們只有一個要修改的佈局視圖文件，如果有任何更改。然後更改後將立即反映在整個應用程序的所有視圖中。

###### ASP.NET Core MVC 中的佈局顯示

- 就像常規視圖一樣，佈局視圖也具有`.cshtml`擴展名的文件
- 您可以將佈局視圖視為ASP.NET Web Form 中的母版頁。
- 由於佈局視圖不特定於控制器，我們通常放在"Views"文件夾的子文件夾"Shared"中。
- 默認情況下，在ASP.NET Core MVC中，佈局視圖文件名為`\_Layout.cshtml`。
- 在ASP.NET Core MVC 中，有一些視圖文件，如佈局的視圖，_ViewStart.cshtml 和_ViewImports.cshtml 等其他.cshtml 文件的文件名以下劃線開頭
- 這些文件名中的前下劃線表示這些文件不是直接面向瀏覽器。
- 也可以在單個應用程序中包含多個佈局視圖文件。比如一個佈局視圖文件服務為管理員用戶，另外一個不同的佈局視圖文件服務於普通用戶。

## 創建佈局創建佈局視圖
右鍵單擊Views文件夾並添加"Shared"文件夾。

右鍵單擊" Shared "文件夾，然後選擇"添加" - "新建項"

在"添加新項"窗口中搜索佈局,一般在web 下的ASP.NET

選擇"Razor 佈局"並單擊"添加"按鈕

名為_Layout.cshtml 的文件將添加到"Shared"文件夾中

_Layout.cshtml 文件中自動生成的HTML視圖

- 右鍵單擊**Views**文件夾並添加"Shared"文件夾。
- 右鍵單擊" **Shared** "文件夾，然後選擇"添加" - "新建項"
- 在"添加新項"窗口中搜索佈局,一般在web 下的ASP.NET
- 選擇"Razor 版面配置"並單擊"添加"按鈕
- 名為_Layout.cshtml 的文件將添加到"Shared"文件夾中
- _Layout.cshtml 文件中自動生成的HTML

請注意，標準**html**，**head**，**title**和**body**元素位於此佈局視圖文件中。由於我們現在將它們放在佈局視圖文件中，因此我們不必在每個視圖中重複所有這些HTML。

###### 使用佈局視圖

要使用佈局視圖(\_Layout.cshtml)渲染視圖，需設置Layout屬性。

```html
@{ 
	Layout ="~/Views/Shared/_Layout.cshtml";
}
```

**我們有一種更好的方法來設置Layout 屬性，而不是在每個視圖中設置它。**

------

##### 二十五、ASP.NET Core MVC 中布局頁面中 Sections

###### 佈局視圖中的 Sections

ASP.NET Core MVC 中的佈局頁面還可以包含一些節點(Section)。節點(Section)可以是選填的也可以是必填的。它提供了一種方法來讓某些頁面元素有組織的放置在一起。

舉個例子
您有一個自定義 JavaScript 文件，項目中的只有一些視圖才需要這些文件。在結束標記之前將 JavaScript文件放在頁面底部是一個好習慣。但是如果所有視圖都需要自定義 JavaScript 文件，那麼我們可以將它放在 Layout 頁面中，如下所示。

```html
<html>
<head>
    <meta name="viewport" content="width=device-width"/>
    <title>@ViewBag.Title</title>
</head>
<body>
    <div>
        @RenderBody()
    </div>

    <script src="~/js/CustomScript.js"></script>
</body>
</html>
```

我們不需要在每個視圖中使用自定義 JavaScript 文件。假設我們只在 Details 視圖中需要它，而在其他視圖中不需要它。我們就可以使用一個節點(Section)。

###### 渲染 Sections

在佈局頁面中，在要渲染節內容的位置調用 RenderSection()方法。在我們的例子中，我們希望 JavaScript 文件包含在結束標記之前。我們把@RenderSection() 放置在結束標記之前。

RenderSection()方法有 2 個參數。第一個參數指定節的名稱。第二個參數參數指定該部分是必需的還是可選的。

```html
<html>
  <head>
    <meta name="viewport" content="width=device-width"/>
    <title>@ViewBag.Title</title>
  </head>
  <body>
    <div>
      @RenderBody()
    </div>

    @RenderSection("Scripts", required: false)
  </body>
</html>
```

如果 required 設置為 true，而內容視圖不包含該部分，則會出現以下錯誤。

```javascript
invalidoperationexception: The layout page "/Views/Shared/_Layout.cshtml" cannot
  find the section "Scripts" in the content page "/Views/Home/Index.cshtml" .;
```

使佈局部分可選
有兩個選項可將佈局部分標記為可選

選項 1：將 RenderSection()方法的必需參數設置為 false

```css
@rendersection ("Scripts", required: false);
```

選項 2：使用 IsSectionDefined()方法

```css
@if (IsSectionDefined("Scripts")) {
  @rendersection ("Scripts", required: false);
}
```

提供節內容
要使用節點，那麼每個視圖都必須包含具有相同名稱的部分。

------

##### 二十六、什麼是_ViewStart.cshtml 文件

###### 設置Layout 屬性

我們使用Layout屬性將視圖與佈局視圖相關聯。沒有_ViewStart.cshtml文件，我們需要在每個視圖中的設置Layout屬性。這違反了DRY (Don't Repeat Yourself)原則，並具有以下缺點:

冗餘代碼。
維護成高。

如果要使用其他佈局文件，還需要更新每個視圖。這工程不僅是繁瑣且耗時，而且還容易出錯，想想你有400 個視圖文件其中200 個要改。

###### ASP.NET Core MVC 中的_ViewStart.cshtml 文件是什麼

它是ASP.NET Core MVC中的一個特殊文件。此文件中的代碼在調用單個視圖中的代碼之前先執行。所以這意味著，我們可以將該公共代碼移動到_ViewStart.cshtml文件中，而不是在每個單獨的視圖中設置Layout屬性。

```css
@{
    Layout = "_Layout";
}
```

通過在`_ViewStart.cshtml`文件中設置Layout屬性，維護我們的應用程序變得更加容易。將來，如果我們想要使用不同的佈局文件，我們只需要在`_ViewStart.cshtml`中的一個位置更改代碼。

###### _ViewStart.cshtml 文件支持分層

我們通常將**ViewStart**文件放在**Views**文件夾中。由於此文件支持分層，我們也可以將它放在**Views**文件夾中的任何子文件夾中。

**Views**文件夾中的所有視圖都將使用**Views**中`ViewStart`文件中指定的佈局頁面，但Home文件夾中的視圖將使用Home文件夾中ViewStart文件中指定的佈局頁面。

我們在`Views`文件夾中放置了一個`ViewStart`文件，在`Home`子文件夾中放置了另一個`ViewStart`文件。

**Home**子文件夾中`ViewStart`文件中指定的佈局頁面,將覆蓋**Views**文件夾中`ViewStart`文件中指定的佈局頁面。

這意味著，**Views**文件夾中的所有視圖都將使用**Views**中`ViewStart`文件中指定的佈局頁面，但Home文件夾中的視圖將使用Home文件夾中ViewStart文件中指定的佈局頁面。

請注意：如果要使用與`_ViewStart.cshtml`中指定的佈局文件不同的佈局文件，可以通過在單個視圖中設置Layout屬性來實現。

如果希望在沒有佈局視圖的情況下渲染視圖，也可以將Layout 屬性設置為null。

###### 邏輯判斷調用佈局視圖

在ASP.NET Core MVC 應用程序中，我們可以有多個佈局視圖。比方說，我們的應用程序中有以下2 個佈局視圖。

```html
 _AdminLayout.cshtml
_NonAdminLayout.cshtml
```

在`_ViewStart.cshtml`中可以通過邏輯判斷登錄用戶角色來選擇對應的佈局視圖

```csharp
@{
    if (User.IsInRole("Admin"))
    {
        Layout = "_AdminLayout";
    }
    else
    {
        Layout = "_NonAdminLayout";
    }
}
```

------

##### 二十七、ASP.NET Core MVC 中的_ViewImports.cshtml 文件

`_ViewImports.cshtml`文件通常放在Views 文件夾中。**它用於包含公共命名空間**，因此我們不必在每個視圖中來引用這些需要的命名空間。

例如, 如果我們在Viewimport 文件中包含以下2 個命名空間, 則這兩個命名空間中的所有類型都可用於"Home" 文件夾中的每個視圖, 而無需再次引入完整的命名空間

```csharp
@using StudentManagement.Models;
@using StudentManagement.ViewModels;
```

注意，@using 指令用於包含公共命名空間。除@using 指令外，_ViewImports.cshtm文件還支持以下指令。

```java
@addTagHelper
@removeTagHelper
@tagHelperPrefix
@model
@inherits
@inject
```

**_ViewStart**文件和**_ViewImports.cshtml**文件均支持分層，除了將它放在Views文件夾中之外，我們還可以在Views文件夾的"Home"子文件夾中放置另一個_ViewImports.cshtml 。

###### _ViewImports.cshtml 文件是分層的

**_ViewStart**文件和**_ViewImports.cshtml**文件均支持分層，除了將它放在Views文件夾中之外，我們還可以在Views文件夾的"Home"子文件夾中放置另一個_ViewImports.cshtml 。

![31 2](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/31-2.png)

在文件Home的文件夾中的`_ViewImports.cshtml`將覆蓋在Shared文件夾中的`_ViewImports.cshtml`文件指定的設置。

請注意：如果在視圖中指定了設置，該設置將覆蓋文件夾中父`_ViewImports.cshtml`文件中的匹配設置。

------

##### 二十八、ASP.NET Core MVC 中的屬性路由

使用屬性路由，我們使用`Route()`屬性來定義路由。我們可以在`Controller`或Controller的操作方法上應用`Route`屬性。

使用屬性路由時, 路由屬性需要在實際使用它們的操作方法上方設置。

屬性路由比傳統路由提供了更大的靈活性。

屬性路由支持層次結構。

需要記住的一個非常重要的一點是,如果操作方法上的`路由模板以/或 ~/開頭`,則控制器路由模板不會與操作方法路由模板組合在一起。

屬性路由中自定義路由，屬性路由通過將標記括在方括號([])中來支持標記替換。標記[controller]和[action]將替換為定義路徑的控制器名稱和操作名稱的值。

重命名控制器或動作名稱，我們就不必更改路徑模板。

而屬性路由則用於服務REST API 的控制器。當然,這個只是規範和建議，如果你的應用程序需要有更多的路由靈活性，我們也可以將常規路由與屬性路由混合使用。

------

##### 二十九、在 ASP.NET Core 中安装和使用 Bootstrap

###### 客戶端包管理工具-LibMan

如果你已經有一些開發經驗了，可能知道有很多工具與Visual Studio一起安裝Bootstrap和jQuery等客戶端軟件包。例如，我們可以使用像Bower，NPM，WebPack等工具。但是，我們不會使用任何這些工具。我們將改為使用**Library Manager (簡稱LibMan )**。Library Manager是一個輕量級的客戶端庫管理工具。它可以從文件系統或CDN(Content Delivery Network-內容分發網絡)下載客戶端庫和框架。

為了能夠使用LibMan，你應該有Visual Studio 2017 版本15.8 或更高版本

如果您使用的Visual Studio 2019，可以忽略。

###### 使用LibMan 安裝Bootstrap

- 右鍵單擊**wwwroot**中的"項目名稱"，然後選擇添加>用戶端程式庫。
- 在打開的"用戶端程式庫"視窗中
- 保留默認提供程序 `cdnjs`
- 在"庫"文本框中，鍵入"twitter-bootstrap"。選擇匹配的條目後，它會嘗試安裝最新版本。當然，您可以手動鍵入所需的版本。它也有智能提示支持。我們將安裝最新版本的Bootstrap 4.6.0。
- 您可以包含"所有庫文件"或"選擇特定文件" -在"目標位置"文本框中，指定要將庫文件複製到的文件夾。默認情況下，重新收集靜態文件僅從WWWRoot 文件夾提供。
- 單擊"安裝"按鈕

###### libman.json 文件

libman.json 是庫管理器清單文件。

請注意，在清單文件中，我們有一個剛剛安裝的Bootstrap 客戶端庫的列表。我們也可以直接編輯清單文件來安裝客戶端軟件包，而不是使用LibMan 提供的圖形界面。

###### 清理和還原客戶端庫

要清理庫文件，請右鍵單擊libman.json 文件，然後從上下文菜單中選擇"清理客戶端庫"。此操作將從目標文件夾中的所有庫文件刪除。但是，不會刪除libman.json 中的已存在內容。

###### 網站中特定的CSS

我們將所有特定於站點的CSS 放在單獨的css 文件中。將名稱為site.css 的樣式表添加到"css"文件夾中。我們已經在"wwwroot"文件夾中創建了這個"css"文件夾。

複製並粘貼以下代碼到site.css 文件中

```css
.btn {
  width: 75px;
}
```

###### 在ASP.NET Core 應用程序中使用Bootstrap

要在ASP.NET Core 應用程序中開始使用Bootstrap，我們需要在佈局文件_Layout.cshtml 中包含對Boostrap.css 文件的引用。當然還要引用我們的自定義樣式表site.css

------

##### 三十、ASP.NET Core 中的Taghelper

###### 什麼是Tag Helpers

Taghelper是服務器端組件。它們在服務器上處理，以在`Razor`文件中創建和渲染`HTML`元素。如果您對以前版本的ASP.NET MVC有任何經驗，那麼您可能熟悉`HTMLTaghelper`。Tag Helpers類似於HTML Taghelper。ASP.NET Core有許多內置的Tag Helper用於常見任務，例如生成鏈接，創建表單，加載數據等。

###### 導入內置Tag Helpers

要在整個應用程序中的所有視圖使用內置Taghelper，需要在`_ViewImports.cshtml`文件導入Taghelper。要導入Taghelper，我們使用`@addTagHelper`指令。

```razor
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, AuthoringTagHelpers
```

通配符`\*`表示我們要導入所有Tag Helpers,而Microsoft.AspNetCore.Mvc.TagHelpers是包含內置Taghelper的組件

###### A 標籤的Tag Helper

A標籤的Tag Helper可通過添加新屬性來增強標準的HTML`(<a></a>)`標籤。比如：

```razor
asp-controller
asp-action
asp-route-{value}
```

所呈現的A標籤的`Href`屬性值由這些`asp`屬性的值決定。

------

##### 三十一、ASP.NET Core Image 標記助手(TagHelper)

###### 如何禁用瀏覽器緩存

在本章節中，我們將通過一個示例討論ASP.NET Core 中的Image Taghelper。

###### 瀏覽器緩存

當我們訪問網頁時，大多數現代瀏覽器會緩存該網頁的圖像。當我們再次訪問該頁面時，瀏覽器不再從Web 服務器再次下載相同的圖像，而是從緩存中提供圖像。在大多數情況下，這不是問題，因為圖像不經常改變。但是這對於開發人員來說，相當的不友好。。。

###### 禁用瀏覽器緩存

由於某種原因，如果您不希望瀏覽器使用它的緩存，您可以禁用它。例如，要在Google Chrome 中禁用緩存

- 使用F12 鍵，啟動Browser Developer Tools
- 單擊"Network"選項卡
- 選中"Disable cache"複選框

![37 1](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/37-1.png)

> 禁用瀏覽器緩存的一個明顯問題是，每次訪問該頁面時都必須從服務器下載圖片。

###### ASP.NET Core 中的Image Taghelper

從性能角度來看，只有在服務器上更改了圖片才能下載圖片。如果圖像未更改，請使用瀏覽器緩存中的圖像。這意味著我們將擁有兩全其美的優勢。

Image Tag Helper可以幫助我們實現這一效果。要使用Image Taghelper，請包含`asp-append-version`屬性並將其設置為true。

```html
<img src="~/images/noimage.jpg" asp-append-version="true"/>
```

Copy

Image TagHelper增強了`<img>`標籤，為靜態圖像文件提供**緩存破壞行為**。將圖像的內容，生成唯一的散列值並將其附加到圖片的URL。此唯一字符串會提示瀏覽器從服務器重新加載圖片，而不是從瀏覽器緩存重新加載。

```html
<img
  class="card-img-top"
  src="/images/noimage.jpg?v=IqNLbsazJ7ijEbbyzWPke-xWxkOFaVcgzpQ4SsQKBqY"
/>
```

Copy

每次服務器上的圖像更改時，都會計算並緩存新的哈希值。如果圖像未更改，則不會重新計算哈希值。使用此唯一哈希值，瀏覽器會跟踪服務器上的圖像內容是否已更改。

###### 哈希緩存行為

Image Taghelper使用`Sha512`哈希計算利用Web服務器上的緩存支持來存儲給定文件

Image Taghelper使用Web服務器上的緩存服務來存儲文件已計算的`Sha512`哈希值。如果多次請求文件，則不重新計算哈希值。只有當磁盤上的的文件發生更改時，將會重新計算生成新的哈希值，緩存才會失效。

------

##### 三十二、ASP.NET Core 環境(environment) Taghelper

###### ASP.NET核心環境標記幫助器

Environment tag helper支持根據應用程序環境呈現不同的內容。使用`ASPNETCORE_ENVIRONMENT`變量設置應用程序環境名稱。

如果應用程序環境是"Development"，則此示例加載非縮小的bootstrap css 文件。

```xml
<environment include="Development">
    <link href="~/lib/bootstrap/css/bootstrap.css" rel="stylesheet"/>
</environment>
```

如果應用程序環境是"Staging"或者"Production"，則此示例從CDN(內容傳送網絡)加載縮小的bootstrap css 文件。

```xml
<environment include="Staging,Production">
    <link rel="stylesheet"
            href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"
            integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
            crossorigin="anonymous">
</environment>
```

" **include** "屬性接受將單個環境環境名稱以逗號分隔的形式生成列表。在`<environment>`tag helper上，我們還有" **exclude** "屬性,當託管環境與exclude屬性值中列出的環境名稱不匹配時，將呈現標籤中的內容。

如果應用程序環境不是"Development"，則此示例從CDN(內容分發網絡)加載縮小的bootstrap css 文件。

```xml
<environment exclude="Development">
    <link rel="stylesheet"
            href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"
            integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
            crossorigin="anonymous">
</environment>
```

`<link>`元素上的" **integrity** "屬性用於檢查**子資源完整性**。 **Subresource Integrity** (簡稱SRI)是一種安全功能，允許瀏覽器檢查被檢索的文件是否被惡意更改。當瀏覽器下載文件時，它會重新計算哈希並將其與"完整性"屬性哈希值進行比較。如果哈希值匹配，則瀏覽器允許下載文件，否則將被阻止。

###### 如果CDN 失敗怎麼辦？

如果CDN 出現故障或由於某種原因，我們的應用程序無法訪問CDN，我們希望我們的應用程序從我們自己的應用程序Web 服務器加載縮小的bootstrap 文件(bootstrap.min.css)。

```xml
<environment include="Development">
    <link href="~/lib/bootstrap/css/bootstrap.css" rel="stylesheet"/>
</environment>

<environment exclude="Development">
    <link rel="stylesheet"
            integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
            crossorigin="anonymous"
            href="https://sstackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"
            asp-fallback-href="~/lib/bootstrap/css/bootstrap.min.css"
            asp-fallback-test-class="sr-only" asp-fallback-test-property="position"
            asp-fallback-test-value="absolute"
            asp-suppress-fallback-integrity="true"/>
</environment>
```



如果應用程序環境是" **Development** "，則從我們的應用程序Web服務器加載非縮小的bootstrap的css文件(bootstrap.css)。如果應用程序環境不是" **Development** "，則從CDN加載縮小的bootstrap css文件(bootstrap.min.css)。

使用`asp-fallback-href`屬性指定回退源。這意味著，如果CDN關閉，我們的應用程序將回退並從我們自己的應用程序Web服務器加載縮小的bootstrap文件(bootstrap.min.css)。

以下3 個屬性及其相關值用於檢查CDN 是否已關閉:

```css
asp-fallback-test-class="sr-only"
asp-fallback-test-property="position"
asp-fallback-test-value="absolute"
```

當然，這裡面是會有一些涉及計算哈希並將其與文件的完整性屬性哈希值進行比較。對於大多數應用程序，CDN失效的時候回退的都是到他們自己的服務器。通過將`asp-suppress-fallback-integrity`屬性設置為true，當然您也可以選擇關閉從本地服務器下載的文件完整性檢查。

------

##### 三十三、ASP.NET Core 中的模型綁定

###### 什麼是模型綁定

- 模型綁定是將HTTP 請求中的數據映射到控制器操作方法上對應的參數。
- 操作方法中的參數可以是簡單類型，如整數，字符串等，也可以是複雜類型，如Customer，Employee，Order 等。
- 模型綁定很棒，有了它我們節約大量的時間，因為沒有它我們必須編寫大量自定義代碼來將請求數據映射到操作方法參數，這不僅無聊乏味而且容易出錯。

###### HTTP 請求數據

要將請求數據綁定到**控制器操作方法上對應的參數**，模型綁定將按以下指定的順序在以下位置查找來自HTTP請求中的數據。

- Form values (表單中值)
- Route values(路由中的值)
- Query strings(查詢字符串)

------

##### 三十四、ASP.NET Core 中的模型驗證

###### ModelState.IsValid 屬性驗證

- 提交表單時，將執行以下`Create()`操作方法

- 在創建學生表單視圖中表單模型是Student 類

- 提交表單時，模型綁定將Post 請求的表單值映射到Student 類的相應屬性

- 在**Name**屬性上添加了**Required**屬性，它會判斷Name中的值，如果該Name中的值為空，或者屬性不存在，則會驗證失敗

- 使用`ModelState.IsValid`屬性會檢查驗證是否失敗或成功

- 如果驗證失敗，我們返回相同的視圖，以便用戶可以提供所需的數據並重新提交表單。
```csharp
     [HttpPost]
          public IActionResult Create(Student student)
          {
              if (ModelState.IsValid)
              {
  
                  Student newStudent = _studentRepository.Add(student);
       return RedirectToAction("Details", new { id = newStudent.Id });
              }
              return View();
          }
```

###### 在視圖中顯示模型驗證錯誤

要顯示驗證錯誤，請使用`asp-validation-for`和`asp-validation-summary`TagHelper。`asp-validation-for`TagHelper用於顯示模型類的單個屬性的驗證消息。`asp-validation-summary`TagHelper用於顯示驗證錯誤的摘要信息。

要顯示與`Student`類的`Name`屬性關聯的驗證錯誤，請在`<span>`元素上使用`asp-validation-for`TagHelper。

```html
<div class="form-group row">
  <label asp-for="Name" class="col-sm-2 col-form-label"></label>
  <div class="col-sm-10">
    <input asp-for="Name" class="form-control" placeholder="请输入名字"/>
    <span asp-validation-for="Name"></span>
  </div>
</div>
```

要顯示所有驗證錯誤的摘要，請在`<div>`元素上使用`asp-validation-summary`

```html
<div asp-validation-summary="All"></div>
```

###### ASP.NET Core 內置模型驗證屬性

以下是ASP.NET Core 中內置的一些驗證屬性

| 屬性              | 作用                                                    |
| ----------------- | ------------------------------------------------------- |
| Required          | 指定該字段是必填的的                                    |
| Range             | 指定允許的最小值和最大值                                |
| MinLength         | 使用MinLength 指定字符串的最小長度                      |
| MaxLength         | 使用MinLength 指定字符串的最大長度                      |
| Compare           | 比較模型的2 個屬性。例如，比較Email 和ConfirmEmail 屬性 |
| RegularExpression | 正則表達式驗證提供的值是否與正則表達式指定的模式匹配    |

###### 自定義模型驗證錯誤的顏色

如果我們要更改視圖中模型驗證錯誤的文字的顏色，請在具有`asp-validation-for`和`asp-validation-summary`Taghelper的`<span>`>和`< div >`元素上使用**Bootstrap **中`text-danger`類

------

##### AddSingleton vs AddScoped vs AddTransient 三者的差異性

###### 註冊服務

ASP.NET Core 提供 3 種方法來註冊服務到依賴注入容器中。而我們使用的方法決定了註冊服務的生命週期。

- AddSingleton() : 顧名思義，AddSingleton()方法創建一個 Singleton 服務。首次請求時會創建 Singleton 服務。然後，所有後續的請求中都會使用相同的實例。因此，通常，每個應用程序只創建一次 Singleton 服務，並且在整個應用程序生命週期中使用該單個實例。

- AddTransient() :此方法創建一個 Transient 服務。每次請求時，都會創建一個新的 Transient 服務實例。

- AddScoped() - 此方法創建一個 Scoped 服務。在範圍內的每個請求中創建一個新的 Scoped 服務實例。例如，在 Web 應用程序中，它為每個 http 請求創建 1 個實例，但在同一 Web 請求中的其他服務在調用這個請求的時候，都會使用相同的實例。注意，它在一個客戶端請求中是相同的，但在多個客戶端請求中是不同的。


在 ASP.NET Core 中，這些服務都是在Startup.cs文件的ConfigureServices()方法中註冊。

###### Scoped(作用域)服務與 Transient(暫時性) 服務與 Singleton(單例)服務以下是 Scoped 服務和 Transient 服務之間的主要區別。

使用作用域服務，我們在給定的 http 請求範圍內獲得相同的實例，但跨不同的 http 請求 獲得新實例.

對於瞬時服務，每次請求實例時都會提供一個新實例，無論它是否在同一 http 的範圍內請求或跨越不同的 http 請求

使用 Singleton 單例服務，只有一個實例。首次請求服務時，將創建一個實例，並且整個應用程序中的所有 http 請求都使用該實例。

| 服務類型          | 同一個Http請求的範圍內 | 橫跨多個不同Http請求 |
| ----------------- | ---------------------- | -------------------- |
| Scoped Service    | 同一個實體             | 新實體               |
| Transient Service | 新實體                 | 新實體               |
| Singleton Service | 同一個實體             | 同一個實體           |

------

##### 三十五、什麼是EF Core,什麼是ORM,為什麼要使用ORM

###### 什麼是EF Core

EF Core 是ORM(對象關係映射器)。

EF Core 是輕量級，可擴展和開源的軟件。與.NET Core 一樣，EF Core 也是跨平台的。它適用於Windows，Mac OS 和Linux。EF Core 是微軟的官方數據訪問平台。

###### 什麼是ORM

ORM的全稱是Object-Relational Mapper,翻譯為中文是**對象關係映射**，它使開發人員能夠使用業務對象去處理數據庫。作為開發人員，我們使用應用程序業務對象，ORM生成底層數據庫可以理解的SQL。簡而言之，ORM降低了開發人員編寫的代碼量，如果不使用ORM通常需要很多訪問數據庫的代碼或者SQL。

###### 為什麼要使用ORM

為了方便我們理解，使用一個例子來解釋ORM。如果我們正在開發一個學生管理系統，我們會有像`Student ，Department、Course`這樣的類在我們的應用程序代碼中。這些類稱為**領域模型**或者說是**實體類**，**模型類**，可見中國語言博大精深。。。

如果沒有像EF Core 這樣的ORM，我們必須編寫大量的代碼來訪問數據庫,來存儲和檢索底層數據庫中的學生和部門數據。

例如，要**查詢**，**添加**，**更新**或**刪除**底層數據庫表中的數據，我們必須在應用程序中編寫代碼，以生成底層數據庫可以理解的sql語句。此外，當數據從數據庫被我們讀取，需要顯示到我們的應用程序時，我們再次編寫自定義代碼以將數據庫數據映射到我們的模型類，如`Student ，Department、Course`等。這是我們幾乎在每個應用程序中都需要做的非常常見的任務。

而有了像EF Core 這樣的ORM 可以為我們完成這些所有的瑣事,為我們節省了大量時間。它就是幫助我們在應用程序代碼和數據庫之間的粘合劑。它消除了我們通常在沒有ORM 的情況下需要編寫大量的代碼來訪問數據庫的需要。

###### EF Core Code First 模式

EF Core 支持Code First 方法和Database First 方法(也被稱作DB First)。但是，如果你是使用Database First 方法，目前EF Core 對他的支持非常有限。

使用`Code First`方法，我們需要首先創建我們應用程序所需要的`領域類`，如Student Major、Order等，以及從Entity Framework DbContext派生的特殊類。基於這些`領域類`和DBContext類，EF Core會為我們創建數據庫和相關表。開箱即用，EF Core使用它的默認約定來創建數據庫和數據庫表。而且如果有需要，我們還可以更改這些默認約定。

###### EF Core Database First 模式

有時我們可能有一個現有數據庫。當我們已經有數據庫和數據庫表時，我們使用DB First的方式來進行開發。使用DB First ，EF Core基於現有數據庫的模型來創建DBContext和`領域類`。

###### EF Core 所支持的數據庫

EF Core支持許多**關係數據庫**甚至**非關係數據庫**。EF Core可以通過使用稱為數據庫提供程序的插件庫來實現此目的。這些數據庫提供程序以NuGet包的形式提供。

EF Core 目前支持的數據庫以及這些數據庫提供程序的插件列表:

https://docs.microsoft.com/zh-cn/ef/core/providers/

數據庫提供程序，通常位於EF Core 及其支持的數據庫之間。數據庫提供程序包含特定於其支持的數據庫的功能。所有數據庫通用的功能都在EF Core 組件中。而對於一些特定於數據庫的功能，例如，Microsoft SQLServer 特定功能需要與EF Core 的SQLServer 提供程序一起使用。

當我們在即將發布的內容中的ASP.NET Core 應用程序中包含對SQLServer 的支持時，我們將討論有關此提供程序模型的更多信息。

------

##### 單層Web 應用和多層Web 應用的區別

###### 單層Web 應用程序

如果它是一個小項目，您可以在一個項目中擁有**界面層**，**業務邏輯層**和**數據訪問層**。因此，如果您使用ASP.NET Core 2.1或更高版本創建了一個Web應用程序項目，那麼在該Web應用程序項目中，您已經安裝了Entity Framework Core。

這個包稱為**metapackage** ,目前還沒有標準的中文翻譯，我個人喜歡叫它**綜合功能包**。**綜合功能包**沒有自己的內容，但是有依賴項列表(其他包)。您可以在解決方案資源管理器中找到此**綜合功能包**。展開**綜合功能包**時，您可以找到所有依賴項。在依賴項中，您將找到已安裝的Entity Framework Core nuget軟件包。

所以我想說的是，使用ASP.NET Core Version 2.1或更高版本創建的ASP.NET Core Web應用程序項目中已經安裝了Entity Framework Core ，它是作為**綜合功能包**的一部分。

##### 多層Web 應用程序--三層架構

在中小型應用程序中，我們通常至少有以下3層,屬於**多層SOA架構**中比較簡單的一種，很多人稱它為**三層架構**:

- 界面層
- 業務邏輯層
- 數據訪問層

這些層的實現都為單獨的項目。Entity Framework Core通常在數據訪問層項目中,因為它是必需的。數據訪問層項目是一個類庫項目，通常不會引用**綜合功能包**。所以這意味著，沒有為數據訪問層項目安裝Entity Framework Core。

##### 多層Web 應用程序-- 領域驅動設計架構

在大型應用程序中,有很多的架構體系，眾多開發極客們，也視圖找到一種銀彈，而目前我個人所推崇的設計思想是：**領域驅動設計**簡稱：DDD。而在這種思想下進行代碼庫分層可以降低代碼複雜性並提高代碼的可重用性。目前我著力於推廣的開發框架52ABP也是基於領域驅動設計思想而來。

**領域驅動設計(DDD)中有四個基本層：**

- 展現層(Presentation)：向用戶提供一個接口(UI)，使用應用層來和用戶(UI)進行交互。
- 應用層(Application)：應用層是展現層和領域層能夠實現交互的中間者，協調業務對象去執行特定的應用任務。
- 領域層(Domain)：包括業務對象和業務規則，這是應用程序的核心層。
- 基礎設施層(Infrastructure)：提供通用技術來支持更高的層。例如基礎設施層的倉儲(Repository)可通過ORM 來實現數據庫交互，或者提供發送郵件的支持。

同樣的基礎設施層也會是一個類庫項目，通常不會引用**綜合功能包**。所以這意味著，沒有為數據訪問層項目安裝Entity Framework Core。

| 表現層             | 多頁MVC、WebApi                                              |
| :----------------- | ------------------------------------------------------------ |
| 應用層             | 針對用戶場景、用例設計應用層服務，隔離底層細節               |
| 領域層             | 專注於維護業務邏輯(編寫業務程式和處理流程時，盡量在純粹的內存環境中進行考量，更利於引入設計模式，不會被底層儲存細節打斷思路) |
| 持久化層(基礎設施) | 負責數據查詢和持久化                                         |

###### 安裝Entity Framework Core

要安裝Entity Framework Core 並能夠將SQLServer 用作應用程序的數據庫，需要安裝以下nuget 包。

| NUGET 包                                 | 作用                                   |
| ---------------------------------------- | -------------------------------------- |
| Microsoft.EntityFrameworkCore.SqlServer  | 此nuget包包含SQLServer特定的功能       |
| Microsoft.EntityFrameworkCore.Relational | 此nuget包包含所有關係數據庫通用的功能  |
| Microsoft.EntityFrameworkCore            | 此nuget包包含通用實體frameowrk核心功能 |

![Entity Framework Core .png](https://git.imweb.io/werltm/picturebed/raw/master/yoyomooc/aspnet/46-1.png)

- Microsoft.EntityFrameworkCore.SqlServer 依賴於Microsoft.EntityFrameworkCore.Relational 包。
- Microsoft.EntityFrameworkCore.Relational 包依賴於
- Microsoft.EntityFrameworkCore 包依賴於其他幾個包。

當我們安裝`Microsoft.EntityFrameworkCore.SqlServer`包時，它還會自動安裝所有其他相關的`nuget`包。

------

##### 三十六、Entity Framework Core 中的DbContext

Entity Framework Core 中的DbContext 類的重要性。

它是EF Core 中非常重要的類之一，DbContext 的作用是在我們的應用程序代碼中用於與底層數據庫交互的類。正是這個類在管理數據庫連接，用於它查詢和保存數據庫中的數據。

###### 在我們的應用程序中使用DbContext

- 我們創建一個派生自DbContext 的類。
- DbContext 類位於Microsoft.EntityFrameworkCore 命名空間中。

```csharp
  public class AppDbContext : DbContext
  { }
```

###### Entity Framework Core 中的DbContextOptions

- 為了使DbContext類能夠執行任何有用的工作，它需要一個**DbContextOptions**類的實例。
- 下述**DbContextOptions**實例會承載應用中的配置信息，如**連接字符串，數據庫提供商**使用等
- 要傳遞**DbContextOptions**實例，我們使用構造函數，如下例所示。
- 我們要使用**DbContext**中的第二個重載方法，該重載方法是指定我們使用的配置信息。所以需要繼承**base**將配置信息傳遞進去。
- 有關**DbContextOptions**類的更多信息，會在我們學習ASP.NET Core中的數據庫連接字符串時，進行學習.

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
```

###### Entity Framework Core DbSet

下面代碼中的DbContext類包括一個`DbSet <TEntity>`模型，而在裡面會包含一個實體屬性。

- 在我們的應用程序中，我們只有一個實體類- Student。
- 所以在我們的AppDbContext類中，只有一個`DbSet<Student>`屬性。
- 我們將使用此DbSet 屬性Students 來查詢和保存Student 類的實例。
- 針對`DbSet<TEntity>`的LINQ查詢將被轉換為針對底層數據庫的查詢。我們將在後面的章節中看到這一點。

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
}
```

為了能夠連接到數據庫，我們需要數據庫連接字符串。在下一個視頻中，我們將討論在何處定義連接字符串並在Entity Framework Core 中使用它。

------

##### 三十七、在Entity Framework Core中使用SQLServer

使用Entity Framework Core時，我們需要配置的重要事項之一就是配置我們計劃使用的數據庫提供程序。Entity Framework Core支持各種的數據庫，包括非關係數據庫。以下MSDN鏈接具有所有受支持的數據庫的列表。

https://docs.microsoft.com/zh-cn/ef/core/providers/

- 我們希望使用Entity Framework Core配置和使用Microsoft SQLServer。
- 我們通常在Startup.cs文件的ConfigureServices（）方法中指定此配置。

###### AddDbContext（）和AddDbContextPool（）方法之間的區別

- 我們可以使用** AddDbContext（）**或**AddDbContextPool（）**方法向ASP.NET Core依賴注入容器中註冊我們的應用程序特定的DbContext類。
- AddDbContext（）和AddDbContextPool（）方法的區別在於，AddDbContextPool（）方法提供了數據庫連接池（DbContextPool）。
- 使用數據庫連接池，如果可用，則提供數據庫連接池中的實例，而不是創建新實例。
- 數據庫連接池在概念上起作用ADO.NET中連接池的工作方式。
- 從性能角度來看，AddDbContextPool（）方法結束AddDbContext（）方法。
- 因此，如果您使用的是ASP.NET Core 2.0或更高版本，則推薦使用AddDbContextPool（）方法而不是AddDbContext（）方法。

###### UseSqlServer（）擴展方法

UseSqlServer（）擴展方法用於配置我們的應用程序特定的DbContext類，以使用Microsoft SQLServer作為數據庫。要連接到數據庫，我們需要將數據庫連接串聯作為參數，添加到UseSqlServer（）擴展方法中。

##### ASP.NET Core中的數據庫連接字符串

我們不需要在應用程序代碼中對連接串行進行硬編碼，或者將其存儲在`appsettings.json`配置文件中。

```csharp
{
  "ConnectionStrings": {
    "StudentDBConnection": "server=(localdb)\\MSSQLLocalDB;database=StudentDB;Trusted_Connection=true"
  }
}
```

在傳統的ASP.NET中，我們將應用程序配置存儲在XML格式的web.config文件中。在ASP.NET Core中，需要採用不同的配置源。這個配置源就是appsettings.json文件，它是JSON格式。

而要從appsettings.json文件中重新連接字符串，我們使用`IConfiguration`服務中的`GetConnectionString()`方法。

我們使用的SQLServer localdb與Visual Studio一起自動安裝。如果要使用完整的SQLServer而不是localdb，只需將appsettings.json配置文件中的連接更改為指向SQLServer實例即可。

```json
//本地的DB
"server=(localdb)\\MSSQLLocalDB;database=StudentDB;Trusted_Connection=true"

//完整的SQL連接字串
"Server=localhost; Database=EmployeeDB; Trusted_Connection=True;"
```

數據庫連接字符串中的內容之間有什麼區別：

- Trusted_Connection=True;
- Integrated Security=SSPI;
- Integrated Security=true;

以上3個設置都代表一個相同的內容，使用集成Windows身份驗證連接到SQLServer而不是使用SQLServer身份驗證。

------

##### 三十八、ASP.NET Core 中的**Repository**模式

在這個章節中，我們將討論

- 什麼是**Repository**模式
- 倉儲模式的好處

###### 什麼是**Repository**模式

**Repository**模式是數據訪問層的抽象呈現。它隱藏了底層數據源保存或查詢數據的詳細信息。有關如何保存和查詢數據的詳細信息都在對應的**Repository**中。例如，您可能擁有一個**Repository**，用於存儲和查詢內存中集合的數據。您可能有另一個**Repository**，用於存儲和查詢如SQLServer，MySQL 等數據庫中的數據。

使用**Repository**模式，我們可以建立基於類的**Repository**，可以使它完成基於類的增、刪、改、查操作，而無需去寫大量的代碼。

###### **Repository**模式中的接口

**我們使用Interface介面來指定的Repository模式**,比如:

- **Repository**支持哪些操作(即具體實現的方法)。
- 每個操作所需的數據，即需要傳遞給方法的參數和該方法返回的數據信息。
- **Interface**介面包含它可以執行的操作，但不實現它如何操作，只實現它可以執行的操作
- 而具體實現的細節則位於實現**Interface**介面的對應**Repository**類中

###### 使用**Repository**模式的好處

- 代碼更清晰，更易於重用和維護。
- 它使我們能夠創建鬆散耦合的系統。例如，如果我們希望我們的應用程序與oracle數據庫工作而不是使用SQLServer工作，那麼我們只需要實現一個**OracleRepository**，再使用依賴注入系統來註冊**OracleRepository**，那麼就可以輕鬆實現一個基於Oracle數據庫的讀取和保存的**Repository**服務。
- 在單元測試項目中，很容易用模擬的實現來替換真實的**Repository**以進行測試。
- **Repository**模式不是特定說服務於數據庫模式，而是我們實際開發中，經常與數據庫打交道而已，所以經常使用數據庫**Repository**模式，除此以外在一些公司裡面進行大量單元測試的時候也會用到內存**Repository**模式。

------

##### 三十九、統一處理ASP.NET Core 中的404 錯誤異常信息

處理不成功的http 狀態代碼組件的作用是處理ASP.NET Core 中的狀態代碼頁。

- UseStatusCodePages 最不實用
- UseStatusCodePagesWithRedirects
- UseStatusCodePagesWithReExecute

###### 404 錯誤的類型

在ASP.NET Core 中，有兩種類型的404 錯誤可能發生:

類型1：找不到指定ID 的資源信息。

類型2：請求的URL 地址和路由不匹配。

###### UseStatusCodePagesWithRedirects 中間件

在生產應用程序中，我們希望攔截這些不成功的http狀態代碼並返回自定義錯誤視圖。為此，我們可以使用`UseStatusCodePagesWithRedirects`中間件或`UseStatusCodePagesWithReExecute`中間件。

Startup 中的 Configure

```csharp
以下兩種都可
app.UseStatusCodePagesWithRedirects("/Error/{0}");
app.UseStatusCodePagesWithReExecute("/Error/{0}");
```

###### 添加ErrorController

```csharp
public class ErrorController : Controller
{
   //如果狀態代碼為404，則路徑將變為Error/404
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        switch (statusCode)
        {
            case 404:
                ViewBag.ErrorMessage = "抱歉，你訪問的頁面不存在";
                break;
        }

        return View("NotFound");
    }
}
```

###### 添加NotFound 視圖

```razor
@{
    ViewBag.Title = "頁面不存在";
}

<h1>@ViewBag.ErrorMessage</h1>

<a asp-action="index" asp-controller="home">
    點擊此處返回首頁
</a>
```

------

##### 四十、UseStatusCodePagesWithRedirects 與UseStatusCodePagesWithReExecute 對比

**UseStatusCodePagesWithRedirects** 中間件組件會攔截404 狀態代碼，顧名思義，它表示發出重定向到指定的錯誤路徑中(在我們的例子中路徑為"/Error/404")。

###### 使用UseStatusCodePagesWithRedirects 請求處理

如果使用UseStatusCodePagesWithRedirects，它向`http://localhost/market/food`發出請求時同樣會觸發404狀態代碼。

- StatusCodePagesWithRedirects 中間件攔截此請求，並將其更改為302，將其指向錯誤路徑(/Error/404)
- 302 狀態代碼表示所請求資源的URL 已被暫時更改，在我們的示例中，它更改為`/Error/404`
- 因此，它會發出另一個GET 請求以滿足重定向的請求。
- 由於發出了重定向，請注意地址欄中的URL也從`/market/food`更改為`/Error/404`
- 請求流會經過http 管道並由MVC 中間件處理，最終返回狀態代碼為200 ,然後導航到NotFound 視圖中(這意味著請求已成功完成)
- 對整個請求流程中的瀏覽器而言，沒有404 錯誤信息。
- 如果您仔細觀察此請求和響應流，我們在實際發生錯誤時返回成功狀態代碼為200，這在語義上不正確的。

###### 使用UseStatusCodePagesWithReExecute 請求處理

如果應用程序使用UseStatusCodePagesWithReExecute("/Error/{0}")。它向`http://localhost/market/food`發出請求時同樣會觸發404狀態代碼。

- UseStatusCodePagesWithReExecute 中間件攔截404 狀態代碼並重新執行將其指向URL 的管道即我們的(/Error/404)中。
- 整個請求流經Http 管道並由MVC 中間件處理，該中間件返回NotFound 視圖HTML 的狀態代碼依然是200。
- 當響應流出到客戶端時，它會通過UseStatusCodePagesWithReExecute 中間件，該中間件會使用HTML 響應，將200 狀態代碼替換為原始的404 狀態代碼。
- 這是一個聰明的中間件。顧名思義，它重新執行管道應該正確的(404)狀態代碼。它只返回自定義視圖(NotFound)
- 因為它只是重新執行管道而不發出重定向請求，所以我們還在地址欄中保留原始`http://localhost/market/food`。它不會從`/market/food`更改為`/Error/404`。

```c#
 var statusCodeResult =
                HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
```

- statusCodeResult.OriginalPath,可以獲取我們的URL 請求信息，
- statusCodeResult.OriginalQueryString,會獲取我們的查詢字符串的搜索信息

------

##### 四十一、ASP.NET Core 中的全局異常處理

###### ASP.NET Core 中的UseDeveloperExceptionPage 中間件

開發環境中將DeveloperExceptionPage 中間件配置到HTTP 請求處理管道中，因此，如果我們在開發環境中運行應用程序，那麼如果存在未處理的異常，我們會看到以下開發人員異常頁面。

![image-20210316113057392](C:\Users\0900086664\AppData\Roaming\Typora\typora-user-images\image-20210316113057392.png)

DeveloperExceptionPage 中間件只能在開發環境中使用。例如，在像Production 這樣的非開發環境中使用此頁面存在安全風險，因為它包含可供攻擊者使用的詳細異常信息。而且此異常頁面對最終用戶也沒有任何意義。

###### ASP.NET Core 中非開發環境異常信息

本地開發機器上模擬在生產環境中運行應用程序將launchSettings.json 中的ASPNETCORE_ENVIRONMENT 變量設置為Production。

```
"ASPNETCORE_ENVIRONMENT": "Production"
```

默認情況下，如果在生產等非開發環境中存在未處理的異常，我們會看到以下默認頁面。

![image-20210316113533367](C:\Users\0900086664\AppData\Roaming\Typora\typora-user-images\image-20210316113533367.png)



請注意，上圖中除了顯示有個http 500 錯誤之外，我們沒有看到任何其他信息。錯誤500 表示服務器上出現錯誤，服務器不知道如何處理。

此默認頁面對最終用戶不是很有用。我們希望處理異常並將用戶重定向到自定義錯誤視圖，這更有用且更有意義。

###### ASP.NET Core 中的異常處理

步驟1：對於非開發環境，使用UseExceptionHandler()方法將異常處理中間件添加到請求處理管道。我們需要打開Startup 類的Configure()方法。異常處理中間件會去ErrorController,請參考以下代碼：

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
    }
}
```

步驟2：修改ErrorController 代碼，它檢索異常詳細信息並返回自定義錯誤視圖。在生產應用程序中，我們不會在錯誤視圖上顯示異常詳細信息。我們可以將它們記錄到數據庫表，文件，事件查看器等，以便開發人員可以查看它們並在需要時提供代碼修復。

```
public class ErrorController : Controller
{
    [AllowAnonymous]
    [Route("Error")]
    public IActionResult Error()
    {
        //獲取異常細節
        var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
        ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
        ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

        return View("Error");
    }
}
```

**請注意**：IExceptionHandlerPathFeature位於Microsoft.AspNetCore.Diagnostics命名空間中。

步驟3：實現錯誤視圖

```html
<h3>
  程式請求時發生了一個內部錯誤，我們會反饋給團隊，我們正在努力解決這個問題。
</h3>
<h5>請通過 ltm@ddxc.org 與我們取得聯繫</h5>
<hr/>
<h3>錯誤詳情:</h3>
<div class="alert alert-danger">
  <h5>異常路徑：</h5>
  <hr/>
  <p>@ViewBag.ExceptionPath</p>
</div>

<div class="alert alert-danger">
  <h5>異常訊息：</h5>
  <hr/>
  <p>@ViewBag.ExceptionMessage</p>
</div>

<div class="alert alert-danger">
  <h5>異常堆棧跟踪：</h5>
  <hr/>
  <p>@ViewBag.StackTrace</p>
</div>
```

------

##### 四十二、ASP.NET Core 中的日誌記錄

###### ASP.NET Core 內置日誌記錄提供程序

- Console
- Debug
- EventSource
- EventLog
- TraceSource
- AzureAppServicesFile
- AzureAppServicesBlob
- ApplicationInsights

###### ASP.NET Core 的第三方日誌記錄提供程序

- NLog
- Log4net
- elmah
- Serilog
- Sentry
- Gelf
- JSNLog
- KissLog.net
- Loggr
- Stackdriver

###### ASP.NET Core 中默認的日誌記錄提供程序

在 Program.cs 文件中的 Program 類中的 Main()方法是我們 ASP.NET Core 應程序的入口。這個方法調用 CreateDefaultBuilder()方法執行幾個任務：

- 設置Web服務器
- 從各種配置源加載主機和應用程序配置信息
- 配置日誌記錄

由於 ASP.NET Core 是開源的，我們可以在他們的官方 github 頁面上看到完整的源代碼。以下是 CreateDefaultBuilder()方法的源代碼：

源代碼路徑地址：

https://github.com/aspnet/AspNetCore/blob/v2.2.2/src/DefaultBuilder/src/WebHost.cs

```csharp
.ConfigureLogging((hostingContext, logging) =>
{
    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventSourceLogger();
})
```

作為配置日誌記錄的一部分，CreateDefaultBuilder()方法默認添加以下 3 個日誌記錄提供程序。這就是我們運行 ASP.NET Core 項目時，我們可以在 Visual Studio 的控制台和調試窗口上都顯示了日誌信息。

- Console
- Debug
- EventSource

在應用程序配置文件 appsettings.json 中可以找到 CreateDefaultBuilder()方法對應的Logging節點 。

以下是電腦上 appsettings.json 文件中的 Logging 部分。

```json
"Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  }
```

###### appsettings.json 文件

LogLevel 用於控制記錄或顯示的日誌數據量。

![image-20210316145120946](C:\Users\0900086664\AppData\Roaming\Typora\typora-user-images\image-20210316145120946.png)

當然在實際開發中不建議您像這樣關閉，建議根據自己的需求，靈活調整。

------

##### 四十三、在 ASP.NET Core 中記錄異常信息

ASP.NET Core 提供的 ILogger 接口記錄我們自己的消息(Info)，警告(Warnings)和異常(exceptions)信息。

當用戶使用我們的應用程序時，如果有異常需要我們在某處記錄異常。然後，開發人員可以查看異常日誌，並在必要時提供修復。所以就需要記錄異常信息,以了解在使用應用程序時生產服務器上發生了什麼異常。

public class ErrorController : Controller
    {
        private ILogger<ErrorController> logger;


```c#
    ///<summary>
    ///注入ASP.NET  Core ILogger服務。
    ///將控制器類型指定为泛型参數。
    ///這有助于我们進行確定哪個類或控制器產生了異常，然后記錄它
    ///</summary>
    ///<param name="logger"></param>
    public ErrorController(ILogger<ErrorController> logger)
    {
        this.logger = logger;
    }

    [AllowAnonymous]
    [Route("Error")]
    public IActionResult Error()
    {
        //獲取異常詳情信息
        var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        //LogError() 方法將異常記錄作為日誌中的錯誤類別記錄
        logger.LogError($"路徑 {exceptionHandlerPathFeature.Path} " +
            $"產生了一个錯誤{exceptionHandlerPathFeature.Error}");
        return View("Error");
    }

    // 測試/market/food/3?name=apple
    //如果狀態代碼為404，則路徑將變為Error/404
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {

        var statusCodeResult =
            HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        switch (statusCode)
        {
            case 404:
                ViewBag.ErrorMessage = "抱歉，你訪問的頁面不存在";
                //LogWarning() 方法將異常記錄作為日誌中的警告類別記錄
                logger.LogWarning($"發生了一個404錯誤. 路徑 = " +
            $"{statusCodeResult.OriginalPath} 以及查詢字符串 = " +
            $"{statusCodeResult.OriginalQueryString}");
                break;
        }
        return View("NotFound");
    }
}
```
###### 在 ASP.NET Core 中記錄異常信息

兩個簡單的步驟來記錄我們自己定義的消息(Info)，警告(Warnings)和異常(exceptions)信息。

在需要日誌記錄功能的地方注入 ILogger 實例

可以指定 注入 ILogger 的類或控制器的類型作為 ILogger 泛型參數的參數。我們這樣做是因為,可以將類或控制器的完整名稱作為日誌類別包含在日誌輸出中。

日誌類別用於對日誌消息進行分組。

打開appsettings.json文件，在日誌級別下面添加以下代碼：

```json
 "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Information"
    }
  },
```

當前的日誌中，顯示了Microsoft 級別的日誌信息，因為我們通過在appsettings.json文件中對日誌級別進行了配置，通過配置的"Microsoft": "Information"顯示了Microsoft的信息，我們也可以通過配置篩選過濾將"Microsoft": "Information"更改為"Microsoft": "Warning"。

可以獲取到所有的完整錯誤日誌信息，從我們的ErrorController中，幫我們攔截到了具體到哪個類文件，以及哪行代碼出錯，以及報錯的信息。

------

##### 四十四、ASP.NET Core 中使用Nlog 記錄信息到文件中

ASP.NET Core 支持以下幾個第三方日誌記錄提供程序

NLog Serilog elmah Sentry JSNLog

###### 在ASP.NET Core 中使用NLog

**步驟1**：安裝`NLog.Web.AspNetCore`nuget包

安裝NLog 包後，您將看到.csproj 文件中的PackageReferenc 包含如下信息：

```xml
     <PackageReference Include="NLog.Web.AspNetCore" Version="4.11.0" />
```

**步驟2**：在項目的根目錄中創建nlog.config文件

創建nlog.config 文件。以下是包含了記錄日誌信息的最低配置信息內容：

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
     xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <!-- 要寫入的目標內容 -->
  <targets>
    <!-- 將日誌寫入文件的具體位置  -->
    <target name="allfile" xsi:type="File"
            fileName="d:\DemoLogs\nlog-all-${shortdate}.log"/>
  </targets>

  <!-- 將日誌程序名稱映射到目標的規則 -->
  <rules>
    <!--記錄所有日誌，包括Microsoft級别-->
    <logger name="*" minlevel="Trace" writeTo="allfile"/>
  </rules>
</nlog>
```

要了解有關nlog.config 文件的更多信息，請參閱以下github wiki 頁面

https://github.com/NLog/NLog/wiki/Configuration-file

步驟3：啟用複製到文件夾

右鍵單擊nlog.config 文件,在解決方案資源管理器中選擇屬性。在"屬性"窗口中設置:

複製到輸出目錄=如果較新則復制

步驟4： 啟用NLog 來記錄我們的日誌信息

除了使用默認日誌記錄提供程序(即Console，Debug和EventSource)之外，我們還使用擴展方法AddNLog()添加了NLog。此方法位於`NLog.Extensions.Logging`命名空間中。

```csharp
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
         .ConfigureWebHostDefaults(webBuilder =>
         {
             webBuilder.UseStartup<Startup>().UseNLog();
         });
    }
```

add Nlog.config，加上一些常用的設定

- throwConfigExceptions - 設定有錯誤時會拋出exception
- internalLogToConsole - 將Log輸出到Console視窗
- archiveAboveSize - 單一檔案的大小限制，超過的拆分檔案
- archiveNumbering - 拆分的檔名的規則
- archiveFileName - 拆分的檔名
- maxArchiveFiles - 拆分的檔案數量上限

------


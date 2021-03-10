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
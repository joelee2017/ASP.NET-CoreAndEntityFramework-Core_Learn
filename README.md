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


# ASP.NET-CoreAndEntityFramework-Core_Learn
從零開始學ASP.NET Core 與EntityFramework Core_課程練習筆記
作者：梁桐銘- 微軟最有價值專家（Microsoft MVP）
網址：https://www.52abp.com/yoyomooc/aspnet-core-for-beginners-Index



------

##### 一、使用VS2019 創建ASP.NET Core Web 程序

在**配置新項目**菜單欄中，鍵入項目的名稱。我將其命名為`StudentManagement`。我們將創建一個ASP.NET Core web應用程序，在這個程序中，我們將創建、讀取、更新、刪除學生。

取消選中"為HTTPS 配置"複選框，如上圖所示，關閉身份驗證。

空：名稱暗示的"空"模板不包含任何內容。這是`我们將使用的模板`，**並從頭開始手動設置所有內容，以便我們清楚地了解不同部分如何組合在一起**。

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


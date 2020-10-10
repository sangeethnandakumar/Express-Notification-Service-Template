# Express-Notification-Service-Template

## To Run As A Windows Service
Install these nuGet libraries
```nuget
Microsoft.Extensions.Hosting
Microsoft.Extensions.Hosting.WindowsServices
```

Add UseWindowsService(); on program.cs
```csharp
Host.CreateDefaultBuilder(args)
     .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
     .ConfigureServices((hostContext, services) =>
     {
         services.AddHostedService<Worker>();
         services.AddSingleton<IMessagingQueue, MessagingQueue>();
         services.AddSingleton<IConsoleDataProvider, ConsoleDataProvider>();
     }).UseWindowsService();
```

## Powershell To Install Service
Open powershell and execute the command to install service on Windows Machines
```powershell
sc.exe create <NameOfService> binpath= C:\test\service.exe start= auto
```
## Powershell To UnInstall Service
Open powershell and execute the command to install service on Windows Machines
> STOP Service first & execute the command
```powershell
sc.exe delete <NameOfService>
```

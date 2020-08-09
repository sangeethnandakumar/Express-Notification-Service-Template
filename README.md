# Express-Notification-Service-Template

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

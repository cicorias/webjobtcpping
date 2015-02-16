# Quick Setup #


----------

## Azure Web Site Configuration ##


### 1. Enable Applications logs ###
  * Navigate to Azure Management Portal [https://manage.windowsazure.com](https://manage.windowsazure.com) 
  * Under the WebSite Configure Tab / Application Diagnostics
  * Enable File System Logging – Information Level
  * Enable Blob Storage Logging – Error is enough
  * Make sure you enter “Manage blob storage” button

![](images/diagconfig.jpg?raw=true)

### 2.	Deploy the Web Job ###
  * Open the VS Solution file and Build – Debug then Release
  * Using VS 2013, use the context menu on the ‘TcpPing’ project and “Publish as Azure WebJob” or,
  * Upload via the Portal -- Zip the release folder shown below

![](images/jobupload.jpg?raw=true)




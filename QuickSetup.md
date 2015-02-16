# Quick Setup #


----------

The intent of this Job is to be used only for troubleshooting as the logging level by default is Error - this is to make the deployment of the job easy and least configuration.  The job isn't necessarily something to be used normally.  If you need to, you can tone down the logging my modification of the code.

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


#### Zipped Files for upload ####
![](images/zipfiles.jpg?raw=true)

#### Upload Job Dialog ####
![](images/jobupload.jpg?raw=true)



### 3.	Monitor the Job ###
The job can be monitored via:

  * https://<SITENAME>.scm.azurewebsites.net/azurejobs/#/jobs
  * Streaming Logs
  * Blob Storage


**Review the following for more information** [Enable diagnostic logging for Azure Websites](http://azure.microsoft.com/en-us/documentation/articles/web-sites-enable-diagnostic-log/) 


During the job run the logs will be shown normally as errors - this is just a diagnostic job that is very verbose and it uses Error reporting normally. The following images are examples of the logging.
  * The Portal - 
  * Streaming Logs
  * Streaming Logs - showing error
 
#### Normal logging ####
![](images/loggingnormal.jpg?raw=true)

#### Portal Logging ####
![](images/loggingportal.jpg?raw=true)


#### Error showing Socket Exception ####
![](images/logerror.jpg?raw=true)
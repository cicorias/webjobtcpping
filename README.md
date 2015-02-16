# Azure Web Job - TCP Ping #
---

### Links ###
  * [Quick Setup](QuickSetup.md) - shows a quick deployment and some examples of logging via the Application Logs
  * [Deploy Simple TCP Server](SimpleTcpServer.md)

## Overview ##
### Visual Studio 2013 Solution ###
The solution file contains several things:

1. JobRunnerShell - simple wrapper class that handles some of the basic management of the process/job for Azure Web Job Classes/Assemblies
2. TcpPing - an implementation of a simple Azure Web Job - intended to be run continuously - that will do a basic TcpPing (open a socket) every second.
3. SimpleTcpServer - a very basic Tcp Listener service that echos back a simple string (1 line) in reverse.


## Purpose ##

The intent of the solution is to provide a very basic diagnostic tool that can be run continuously in an Azure WebSite deployment that will 'ping' (open a socket) to a server -- this is intended for testing availability of a server using IPv4 addresses (ie. 10.0.1.1) across Virtual Networks (VNET) in Azure.

This can be used against any Server listener service - as it only does a Socket.Open() - of course, the Server should be resilient to these Socket Opens and immediate close.

**NOTE:** **`Make sure you Open the Windows Server firewall if using Windows Server as your 'host' for this.`*

Reporting is done to the Azure Web Jobs dashboard and is also visible via Azure WebSite's Streaming Logs 



- Azure Jobs:   ***https://SITENAME.scm.azurewebsites.net/azurejobs/#/jobs***.



- Streaming Logs: [Enable diagnostic logging for Azure Websites](http://azure.microsoft.com/en-us/documentation/articles/web-sites-enable-diagnostic-log/).

The easiest way is just go the the [Azure Portal](https://portal.azure.com) or use Visual Studio Azure Explorer - which comes with the Azure Tools for Visual Studio.

---

## Deployment ##

### Azure WebJob ###
The Azure WebJob - 'TcpPing' utilizes the NuGet packaging that 'lights up' the "Publish as Azure Webjob" tooling in Visual Studio.  Otherwise, this can be deployed using alternate methods - see [How to Deploy Azure WebJobs to Azure Websites](http://azure.microsoft.com/en-us/documentation/articles/websites-dotnet-deploy-webjobs/)
#### Settings ####
Within the TcpPing project - examine the "app.confg" you will find 'appSettings' and 'connectionStrings' that you should review.  The connectionStrings are dependant upon your Azure Storage Account information which you can retrieve from the [Azure Portal](https://portal.azure.com)

##### AppSettings #####
The following settings are used to open the socket - adjust to your need.

    <appSettings>
      <add key="sqlIp" value="10.3.0.4"/>
      <add key="sqlPort" value="8999"/>
    </appSettings>
    

##### Connection Strings #####
Make sure you put in your 'connectionString' - which comes from the Azure Portal for the Storage Account.

    <connectionStrings>
    <!--WEBJOBS_RESTART_TIME  - please set in porta to seconds like 60.-->
    <!--WEBJOBS_STOPPED   setting to 1 means stopped-->
    <!-- The format of the connection string is "DefaultEndpointsProtocol=https;AccountName=NAME;AccountKey=KEY" -->
    <!-- For local execution, the value can be set either in this config file or through environment variables -->
    <add name="AzureWebJobsDashboard"
     connectionString="DefaultEndpointsProtocol=https;AccountName=<accountName>;AccountKey=<accountKey>" />
    <add name="AzureWebJobsStorage"
     connectionString="DefaultEndpointsProtocol=https;AccountName=<accountName>;AccountKey=<accountKey>" />
    </connectionStrings>


### Simple TCP Server ###
The solution also contains a simple TCP Server that is intended to be installed on within the Virtual Network - for example in an IaaS instance -- that you are attempting to validate connectivity and continuous reporting on.

Again, you should be able 
#### Settings ####
There is only 1 setting in the app.config under appSettings. If this is absent, the Simple TCP Server listens on IPv4 addresses only (actually all the time) and uses port 8999

    <appSettings>
     <add key="serverPort" value="8999"/>
    </appSettings>
    


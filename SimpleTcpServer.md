# Simple TCP Server #
---
## Overview


The Simple TCP Server is a Console application that can also run as a Windows Service.

  * Run from Command line
  * Run as a Windows Service

### Deployment and Optional Service Install ###

Deployment is simple, just copy all the binaries to a directory on the Machine. And either run, or install as a service.

#### Run from a Command Line ####

Just execute the EXE in the directory.  It will use the app.config settings.

#### Install as a Windows Service ####

Make use of the ["InstallUtil.exe"](https://msdn.microsoft.com/en-us/library/50614e95%28v=vs.110%29.aspx) that is located in:

  * **%SystemRoot%\Microsoft.NET\Framework64\v4.0.XXXX**


The service is marked as "**Manual Start**" and uses "**Network Service**" as the default.
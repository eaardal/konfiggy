#Konfiggy

A tiny helper framework for providing environment-based app/web.config configurations

It's still very early in development, but the idea is to provide different options of managing different app settings based on what environment the app is running in or built to run in.

####The examples below are not necessarily implemented yet, they are for giving an idea of what Konfiggy will be able to do eventually.

**Example using global variable in app.config:**

In app.config...
```
  <konfiggy>
    <environmentTag value="Local" />
  </konfiggy>

  <appSettings>
    <add key="Local.MyFile" value="C:\Code\Project\MyFile.xml"/>
    <add key="Dev.MyFile" value="C:\ProgramData\Project\MyFile.xml"/>
    <add key="QA.MyFile" value="C:\ProgramData\Project\MyFile.xml"/>
    <add key="Prod.MyFile" value="C:\ProgramFiles (x86)\App\MyFile.xml"/>
  </appSettings>
``` 
In code...
```
 string myFilePath = Konfiggy.GetAppSetting("MyFile");
```
 Since the environmentTag is set to Local, the key Local.MyFile will be used to retrieve the MyFile value. You would still need to swap the environmentTag before building for dev/qa/prod which still kinda sucks, but at least it's only one place you'd need to swap the value.
 
**Example using environment variables on the system/server/pc:**

In the user or system environment variables add a new variable called "Konfiggy" and give it a value which should be
a environment prefix such as those defined in the app.config (see below). For example "Dev"
This will need to be set up in all environments the app will be running in. On the dev server the value would be "Dev", 
on the QA server the value would be "QA" etc... 

In my opinion this is the most elegant since it will be a one-time setup of the env. variables and then you can forget about them. Also, there is no need to swap anything in the app.config file based on the deploy target. Ever. The downside being that you need admin access to modify the env. variables which, depending on your company's policy, you may not have access to beyond the dev server, if at all.

In app.config...
```
  <appSettings>
    <add key="Local.MyFile" value="C:\Code\Project\MyFile.xml"/>
    <add key="Dev.MyFile" value="C:\ProgramData\Project\MyFile.xml"/>
    <add key="QA.MyFile" value="C:\ProgramData\Project\MyFile.xml"/>
    <add key="Prod.MyFile" value="C:\ProgramFiles (x86)\App\MyFile.xml"/>
  </appSettings>
```  
In code...
```
 string myFilePath = Konfiggy.GetAppSetting("MyFile");
```
Konfiggy will look in the environment variables for the "Konfiggy" variable and get the value. 
It will then look for the value in the app.config and prepend it to the app setting requested.


**Example using visual studio build configuration in build script**  

TBA.
Not quite sure how to get around this yet, but probably some after-build app.config manipulation with powershell/batch/build script need to happen in order to give the correct environment tag based on what build configuration was just used to build the solution.


Konfiggy

A tiny helper framework for providing environment-based app/web.config configurations

It's still very early in development, but the idea is to provide different options of managing different app settings based on what environment the app is running in or built to run in.



Example using global variable in app.config:

In app.config...

  <konfiggy>
    <environmentTag value="Local" />
  </konfiggy>

  <appSettings>
    <add key="Local.MyFile" value="C:\Code\Project\MyFile.xml"/>
    <add key="Dev.MyFile" value="C:\ProgramData\Project\MyFile.xml"/>
    <add key="QA.MyFile" value="C:\ProgramData\Project\MyFile.xml"/>
    <add key="Prod.MyFile" value="C:\ProgramFiles (x86)\App\MyFile.xml"/>
  </appSettings>
  
In code...
 string myFilePath = Konfiggy.GetAppSetting("MyFile");
 Since the environmentTag is set to Local, the key Local.MyFile will be used to retrieve the MyFile value


 
Example using environment variables on the system/server/pc:

In the user or system environment variables add a new variable called "Konfiggy" and give it a value which should be
a environment prefix such as those defined in the app.config (see below). For example "Dev"
This will need to be set up in all environments the app will be running in.

In app.config...

  <appSettings>
    <add key="Local.MyFile" value="C:\Code\Project\MyFile.xml"/>
    <add key="Dev.MyFile" value="C:\ProgramData\Project\MyFile.xml"/>
    <add key="QA.MyFile" value="C:\ProgramData\Project\MyFile.xml"/>
    <add key="Prod.MyFile" value="C:\ProgramFiles (x86)\App\MyFile.xml"/>
  </appSettings>
  
In code...
 string myFilePath = Konfiggy.GetAppSetting("MyFile");
 Konfiggy will look in the environment variables for the "Konfiggy" variable and get the value. 
 It will then look for the value in the app.config and prepend it to the app setting requested.


  

  


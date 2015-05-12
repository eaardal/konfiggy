#Konfiggy

Konfiggy is a tiny helper utility for making life easier when dealing with environment-dependent configuration settings in your app/web.config files.

Konfiggy enables you to write your app/web.configs like this:

```csharp
<appSettings>
    <add key="dev.mySetting" value="dev-specific-value" />
    <add key="qa.mySetting" value="qa-specific-value" />
    <add key="prod.mySetting" value="prod-specific-value" />
</appSettings>
```
You then configure Konfiggy to resolve the `dev`, `qa`, or `prod` tags in some way. The built-in methods include
- Machine Environment Variable
- Conditional Compilation
- In code
- A dictionary of machine names
- A file on disk

And query your appSetting/connectionString like this:
```csharp
IKonfiggy konfiggy = new Konfiggy(); // Or resolve from IoC...
string myValue = konfiggy.GetAppSetting("MySetting");
```

[More info in the Wiki](https://github.com/eaardal/Konfiggy/wiki/Getting-started) 

Accepting all kinds of feedback, ideas and/or help :)

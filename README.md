#Konfiggy

Konfiggy is a tiny helper utility for making life easier when dealing with environment-dependent configuration settings in your app/web.config files.

### NuGet install
`Install-Package Konfiggy`

### Elevator pitch
Konfiggy enables you to write your app/web.configs like this:

```csharp
<appSettings>
    <add key="dev.mySetting" value="dev-specific-value" />
    <add key="qa.mySetting" value="qa-specific-value" />
    <add key="prod.mySetting" value="prod-specific-value" />
</appSettings>
```
You then configure Konfiggy to resolve the `dev`, `qa`, or `prod` tags (you can use whatever tags/names you want) in some way. The built-in methods include
- Machine Environment Variable
- Conditional Compilation
- In code
- A dictionary of machine names
- A file on disk

And query your appSetting/connectionString like this:
```csharp
IKonfiggy konfiggy = new Konfiggy(); // Or resolve from IoC...
string myValue = konfiggy.GetAppSetting("mySetting");
```

#### And also...

With a POCO class like

```csharp
class Config
{
    public string MySetting { get; set; }
    public string MyConnectionString { get; set; }
}
```

And a app.config file like

```xml
<appSettings>
  <add key="dev.mySetting" value="foo"/>
</appSettings>
<connectionStrings>
  <add name="dev.myConnectionString" connectionString="bar"/>
</connectionStrings>
```
You can automatically populate it with config data
```csharp
var config = konfiggy.PopulateConfig<Config>()
                     .WithAppSettings()
                     .WithConnectionStrings()
                     .Populate();
```
Or map the properties manually if the names doesn't match exactly
```csharp
var config = konfiggy.PopulateConfig<Config>()
                     .WithAppSettings(c => c.Map(x => x.MySetting, "someAppSettingKey"))
                     .WithConnectionStrings(c => c.Map(x => x.MyConnectionString, "someConnectionStringName"))
                     .Populate();
```

[More info in the Wiki](https://github.com/eaardal/Konfiggy/wiki/Getting-started) 

Accepting all kinds of feedback, ideas and/or help :)

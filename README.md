# ini.net

Ini.net is Dotnet Framwork library for reading and writing to INI file

## How to install

1. Open your project in Visual Studio.
2. Right click on *References* and select *Manage NuGet packages*.
3. Search for *Tantowi.Ini*, and install

## How to use

1. Read ini file
```
    IniFile ini = IniFile.of("d:\\config.ini");
    string name = ini.Read("setting", "company.name");
```

2. Write ini file
```
    IniFile ini = IniFile.of("d:\\config.ini");
    ini.Write("setting", "company.name", "Github Corporation");
```
3. Get list of section
```
    IniFile ini = IniFile.of("d:\\config.ini");
    string[] sections = ini.GetSections();
```
3. Get list of key in a section
```
    IniFile ini = IniFile.of("d:\\config.ini");
    string[] keys = ini.GetKeys("setting");
```

## Reference

- [Tantowi.Ini class](docs/index.html)






<br><br><br><br><br>
###### Copyright &copy; 2020 Tantowi Mustofa




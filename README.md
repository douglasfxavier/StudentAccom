# StudentAccom

Project developed as an assignment for the module Web Development in the MSc Web Technology at University of Southampton.

### Team:
* Douglas Xavier
* Longqiuyu Huang

2017

## Guidelines
### How to generate the two context databases with enabled migrations

*1. In the Visual Studio go to the menu Tools > NuGet Package Manager > Package Manager Console. Note that you will probably need to install the required packages for the project from that point.*


*2. Run each one of the 3 following statements:*
```
update-database -configuration studentaccomconfiguration

update-database -configuration configuration

update-database -configuration configuration
```

:exclamation: Warning: Do it only if you do not have the databases already.

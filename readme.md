﻿# Kerbal Store

Example ASP.Net Core, MVC, Bootstrap, Anugular project in the popular context of Kerbal Space Program.

## Prerequisites

What things you need to install the software and how to install them

* Visual Studio 2017 (15.6) with .Net Core 2.0 installed
* Follow below for getting the right version of npm/angular-cli installed:
```
npm install -g npm@5.1.0
npm cache clean --force
npm install -g @angular/cli@1.3.2
```

## Installing Packages (bower/npm)

* Run (as administrator) ```npm install``` from the root directory of the project (not the solution directory) **This can take a while**
* Run ```ng build``` from the root directory of the project
* Open project in Visual Studio 2017, right-click on bower.config and "Restore Packages" (you can check the output for Bower/npm in Visual Studio) **This can take a while**
* CTRL+F5 from Visual Studio will make sure site is running in IIS Express (check ports)

## Accessing Site

* Go to ```http://localhost:49968/``` once you've CTRL+F5'd in Visual Studio

## Support

There sure is! 
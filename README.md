# Industry App Classifying & Grading

**IACG** is an experimental B/S system for industry app classifying and grading.

- This is the source codes of my programming assignment of SE2019 courses.

![](docs/images/1.png)

## Features

### Register & Login

- Register (as different roles)

![](docs/images/2.png)

- Login

![](docs/images/3.png)

### User Profile Manage

- Profile

![](docs/images/20.png)

- Manage

![](docs/images/21.png)

- Password

![](docs/images/22.png)

- Personal data

![](docs/images/23.png)

- Delete account

![](docs/images/24.png)

### App Manage & Review

- View apps

![](docs/images/6.png)

- View reviews

![](docs/images/7.png)

- App details

![](docs/images/11.png)

- Edit apps

![](docs/images/12.png)

- Delete apps

![](docs/images/13.png)

- Create apps

![](docs/images/14.png)

- Review apps

![](docs/images/17.png)

- Review details

![](docs/images/18.png)

- Edit reviews

![](docs/images/19.png)

- Delete reviews

![](docs/images/26.png)

## Dependences

- .NET Core 3.0
- ASP.NET Core 3.0
- EntityFramework Core 3.0
- MS SQL Server

## Development

Use Visual Studio Community 2019 (v16.3.10) with "ASP.NET and Web Development" workload.

Open `IACG.sln` in VS and press `F5`. It takes time to restore libraries and front-end packages.

Then visit `https://localhost:5001/` or `http://localhost:5000` to view the website. 

Seed data (all password is 123456):
- Admin user:
  - admin
- Enterprise user:
  - enttest
  - enttest2
- Professional user
  - protest
  - protest2

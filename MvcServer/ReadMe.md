1. 在 MvcMovie 文件夹中创建一个新的 ASP.NET Core MVC 项目

`dotnet new mvc -o MvcServer`

2. 在 Visual Studio Code 中加载 MvcMovie.csproj 项目文件
`code -r MvcMovie`

3. 信任 HTTPS 开发证书
`dotnet dev-certs https --trust`

4. 运行

`dotnet watch run或 dotnet run`

5. openapi

`dotnet tool install -g Microsoft.dotnet-openapi`
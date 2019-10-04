🖼 ImageGenerator
===============

ImageGenerator is a project on ASP.NET Core that generates an image with one color and specified size. Used in [SendColorBot](https://github.com/yet-another-devteam/SendColorBot) to send color pictures to Telegram.

![alt text](https://raw.githubusercontent.com/yet-another-devteam/ImageGenerator/master/Screenshots/1.png "Example of result")
 
To generate an image, you should specify parameters in the link and then make GET request:

**DOMAIN**/ImageGenerator?width=**WIDTH OF IMAGE**&height=**HEIGHT OF IMAGE**&color=**RGB COLOR IN HEX**

## 🛠 Tools that project uses

[ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) — Cross-platform open-source web framework.

[ImageSharp](https://github.com/SixLabors/ImageSharp) — Cross-platform library for image file processing.

## 📝 License
The project is licensed under the [MIT license](https://github.com/yet-another-devteam/ImageGenerator/blob/master/LICENSE).

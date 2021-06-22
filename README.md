# QR Generator
A wrapper for QR Server, which generates QR codes based on content provided

### Share with your friends! - Made with GUI - Scan to head to the release tab.
![share me](https://github.com/BIGDummyHead/QR-Generator/blob/master/share.png)

## Use

Using the QR generator is quite simple and will provide you with the most simple methods for generating your QR Code!

See this example on getting your QR code!

```csharp

using QRGenerator;

//the width/height of this image are set to 500 x 500
QR code = new QR("Hello world!");

//we can manually set the width/height as so
//new QR("Custom width", 150);
//new QR("Custom width", height: 150);
QR customDimensions = new QR("Custom width", width: 150, height: 150);

//we can also make QR codes implicitly 
//note: the width and height are set to 500.
QR implicitCode = "Hey it works!";

```

In this example you will see how to get data on your QR code!

```csharp

using QRGenerator;

//create our QR code
QR code = new QR("Hello world!");

//read as byte[]
//this can also be done async.
//code.AsBytesAsync();
byte[] img = code.AsBytes();

//you can then do whatever you want with this, please see the following link of how I used it.
//https://github.com/BIGDummyHead/QR-Generator/blob/0c0aa2e9a90ef616fe92f36d5c3a69248a0fb385/Example%20Wpf/QR-Maker-Sol/QR-Maker/MainWindow.xaml.cs#L48

//QR also allows you to print it out to a file of your choice.
//code.DownloadAsync("myCode.png", override: true);

//by default 'override' is false.
code.Download("myCode.png", override: true);
```

This will provide a full example that you can copy into your code.

```csharp

using QRGenerator;
using System.Threading.Tasks;

class Program
{
   static async Task Main()
   {
      QR code = new QR("Hello world!");
      await code.DownloadAsync("hello.png");
   }
}

```

Results:

![hello](https://github.com/BIGDummyHead/QR-Generator/blob/master/example.png)

Other Results:

![secret](https://github.com/BIGDummyHead/QR-Generator/blob/master/secretqr.png)


## API Used 

[GO QR Code API](http://goqr.me/)

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenGraphProfileImageGenerator.Manager;
using SixLabors.ImageSharp;

Console.WriteLine("Starting!");

var services = new ServiceCollection();
services.AddSingleton<IOpenGraphSpeakerProfileImageGenerator, OpenGraphSpeakerProfileImageGenerator>();
services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});
services.AddHttpClient();
var serviceProvider = services.BuildServiceProvider();

var speakerProfileGenerator = serviceProvider.GetRequiredService<IOpenGraphSpeakerProfileImageGenerator>();

var imageFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\images\"));
var outputFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\output\"));
var fontFile = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\..\Ubuntu-R.ttf"));

// First Test - Images from Web - ThemeFonts
Console.WriteLine("First Sample (1a)- Images from Web");
Console.WriteLine("First Sample (1a)- Images from Web - ThemeFonts - Expected to Work");
var testImage1A = await speakerProfileGenerator.GenerateSpeakerProfileFromUrlsAsync(
    "https://www.josephguadagno.net/assets/images/authors/Joe_Guadagno_512x512.jpg",
    "https://morespeakers.com/images/favicons/android-icon-192x192.png",
    "Joseph Guadagno", speakerProfileGenerator.ThemeFonts);
if (testImage1A != null)
{
    await testImage1A.SaveAsPngAsync(Path.Combine(outputFolder, "test-speaker1a-profile.png"));
    Console.WriteLine("First Sample (1a)- Images from Web - ThemeFonts - Works");
}
else
{
    Console.WriteLine("First Sample (1a)- Failed to generate image for sample 1a");
}

Console.WriteLine("First Sample (1b)- Images from Web - FontFamily - Expected to Work");
var fontFamily1B = speakerProfileGenerator.GetFontFamilyFromList(["Arial"]);
if (fontFamily1B is null)
{
    Console.WriteLine("Failed to get font family 'Arial'. First Sample (1b)- Images from Web - FontFamily - Doesn't Work");
}
else
{
    var testImage1B = await speakerProfileGenerator.GenerateSpeakerProfileFromUrlsAsync(
        "https://www.josephguadagno.net/assets/images/authors/Joe_Guadagno_512x512.jpg",
        "https://morespeakers.com/images/favicons/android-icon-192x192.png",
        "Joseph Guadagno", fontFamily1B.Value);
    if (testImage1B != null)
    {
        await testImage1B.SaveAsPngAsync(Path.Combine(outputFolder, "test-speaker1b-profile.png"));
        Console.WriteLine("First Sample (1b)- Images from Web - FontFile - Works");
    }
    else
    {
        Console.WriteLine("First Sample (1b)- Failed to generate image for sample 1b");
    }
}

Console.WriteLine("First Sample (1c)- Images from Web - FontFile - Expected to Work");
var testImage1C = await speakerProfileGenerator.GenerateSpeakerProfileFromUrlsAsync(
    "https://www.josephguadagno.net/assets/images/authors/Joe_Guadagno_512x512.jpg",
    "https://morespeakers.com/images/favicons/android-icon-192x192.png",
    "Joseph Guadagno", fontFile);
if (testImage1C != null)
{
    await testImage1C.SaveAsPngAsync(Path.Combine(outputFolder, "test-speaker1c-profile.png"));
    Console.WriteLine("First Sample (1c)- Images from Web - FontFile - Works");
}
else
{
    Console.WriteLine("Failed to generate image for sample 1c");
}

// First Test - Images from Web - ThemeFonts
Console.WriteLine("First Sample (2a)- Images from File");
Console.WriteLine("First Sample (2a)- Images from File - ThemeFonts - Expected to Work");
var testImage2A = await speakerProfileGenerator.GenerateSpeakerProfileFromFilesAsync(
    Path.Combine(imageFolder, "Joe_Guadagno_512x512.jpg"),
    Path.Combine(imageFolder, "logo-192x192.png"),
    "Joseph Guadagno", speakerProfileGenerator.ThemeFonts);
if (testImage2A != null)
{
    await testImage2A.SaveAsPngAsync(Path.Combine(outputFolder, "test-speaker2a-profile.png"));
    Console.WriteLine("First Sample (2a)- Images from File - ThemeFonts - Works");
}
else
{
    Console.WriteLine("Failed to generate image for sample 2a");
}

Console.WriteLine("First Sample (2b)- Images from File - FontFamily - Expected to Work");
var fontFamily2B = speakerProfileGenerator.GetFontFamilyFromList(["Arial"]);
if (fontFamily2B is null)
{
    Console.WriteLine("Failed to get font family 'Arial'. First Sample (2b)- Images from File - FontFamily - Doesn't Work");
}
else
{
    var testImage2B = await speakerProfileGenerator.GenerateSpeakerProfileFromFilesAsync(
        Path.Combine(imageFolder, "Joe_Guadagno_512x512.jpg"),
        Path.Combine(imageFolder, "logo-192x192.png"),
        "Joseph Guadagno", fontFamily2B.Value);
    if (testImage2B != null)
    {
        await testImage2B.SaveAsPngAsync(Path.Combine(outputFolder, "test-speaker2b-profile.png"));
        Console.WriteLine("First Sample (2b)- Images from File - FontFamily - Works");
    }
    else
    {
        Console.WriteLine("Failed to generate image for sample 2b");
    }
}

Console.WriteLine("First Sample (2c)- Images from Web - FontFile - Expected to Work");
var testImage2C = await speakerProfileGenerator.GenerateSpeakerProfileFromFilesAsync(
    Path.Combine(imageFolder, "Joe_Guadagno_512x512.jpg"),
    Path.Combine(imageFolder, "logo-192x192.png"),
    "Joseph Guadagno", fontFile);
if (testImage1C != null)
{
    await testImage2C.SaveAsPngAsync(Path.Combine(outputFolder, "test-speaker2c-profile.png"));
    Console.WriteLine("First Sample (2c)- Images from Web - FontFile - Works");
}
else
{
    Console.WriteLine("Failed to generate image for sample 2c");
}

Console.WriteLine("Done");

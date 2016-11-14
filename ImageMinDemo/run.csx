#r "System.Drawing"
#r "Microsoft.WindowsAzure.Storage"

using ImageResizer;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Blob;

public static void Run(Stream myBlob,string name, Stream minifiedblob, TraceWriter log)
{
    
            //myBlob.CopyTo(minifiedblob); 
            log.Info($"C# Blob trigger function Processing blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            var imageBuilder = ImageBuilder.Current;
            var size = imageDimensionsTable[ImageSize.Small].Key;
           
            Stream outputImage = new MemoryStream();
               imageBuilder.Build(myBlob, minifiedblob,
                   new ResizeSettings(size.Width, size.Height, FitMode.Max, null), false);
}

public enum ImageSize
        {
            ExtraSmall,
            Small,
            Medium
        }
 public static readonly IDictionary<ImageSize, KeyValuePair<Size, string>> imageDimensionsTable = new Dictionary<ImageSize, KeyValuePair<Size, string>>
        {
            {ImageSize.ExtraSmall, new KeyValuePair<Size, string>(new Size(100, 100),"thumbnail")},
            {ImageSize.Small,  new KeyValuePair<Size, string>(new Size(300, 300),"small")},
            {ImageSize.Medium,  new KeyValuePair<Size, string>(new Size(480, 480),"medium")}
        };

using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{

    private static Color[] getColors( Bitmap bmp ) {
        ArrayList palette = new ArrayList();
        for (var w = 0; w < bmp.Width; w++)
        {
            for (var h = 0; h < bmp.Height; h++)
            {
                var color = bmp.GetPixel(w, h);
                if (!palette.Contains(color))
                {
                    palette.Add(color);
                }
            }
        }
        return palette.Cast<Color>().ToArray();
    }

    private static void savePalette(Color[] colors, string filepath) {
        Bitmap bmp = new Bitmap(colors.Length, 1);
        for( var i  = 0; i < colors.Length; i++)
        {
            bmp.SetPixel(i, 0, colors[i]);
        }
        bmp.Save(filepath);
    }
    static void Main(string[] args)
    {
        if (!System.OperatingSystem.IsWindows()) return;
        if (args.Length <= 0) throw new ArgumentException("Must supply image file");
        if (!File.Exists(args[0])) throw new ArgumentException($"File: {args[0]} could not be found");
        FileInfo inputFile = new FileInfo(args[0]);
        var outputFile = $"{inputFile.Directory}/{inputFile.Name}-palette.bmp";
        if (args.Length == 2)
        {
            FileInfo testFileInfo = new FileInfo(args[1]);
            if (!Directory.Exists(testFileInfo.DirectoryName)) throw new ArgumentException($"Output directory {args[1]} does not exist");
            outputFile = testFileInfo.FullName;
        }

        savePalette( getColors(new Bitmap(Image.FromFile(inputFile.FullName))), outputFile );
    }
}
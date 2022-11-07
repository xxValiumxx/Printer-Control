// See https://aka.ms/new-console-template for more information
using System.Text;
using ESC_POS_USB_NET.Printer;
using ESC_POS_USB_NET.Enums;
using QRCoder;
using System.Drawing;

Bitmap pad(Image source)
{
    int width = 576;
    int height = source.Height;

    // Create a compatible bitmap
    Bitmap dest = new Bitmap(width, height, source.PixelFormat);
    Graphics gd = Graphics.FromImage(dest);
    gd.Clear(Color.White);
    gd.DrawImage(source, (width/2 - source.Width/2), 0, source.Width, source.Height);
    return dest;
}


byte[] pdfheader = { 0x1B, 0x1D, 0x68, 0x04 };
string pdfmsg = "This is an example of printing a PDF417 barcode.";
QRCodeGenerator qrGenerator = new QRCodeGenerator();
QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.L);
QRCode qrCode = new QRCode(qrCodeData);
Bitmap qrCodeImage = pad(qrCode.GetGraphic(5));

Console.WriteLine("Hello, World!");
System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
Encoding.RegisterProvider(ppp);
Printer printer = new Printer("HP Receipt", "IBM437");
printer.InitializePrint();
printer.Image(qrCodeImage);

printer.Separator();
printer.AlignCenter();
printer.Append("Code 128");
printer.Code128("123456789");
printer.Separator();
printer.Append("Code39");
printer.Code39("123456789");
printer.Separator();
printer.Append("Ean13");
printer.Ean13("1234567891231");
printer.Separator();
printer.Pdf417(pdfmsg);
printer.Separator();

printer.Append("NORMAL - 48 COLUMNS");
printer.Append("1...5...10...15...20...25...30...35...40...45.48");
printer.Separator();
printer.Append("Text Normal");
printer.BoldMode("Bold Text");
printer.UnderlineMode("Underlined text");
printer.Separator();
printer.ExpandedMode(PrinterModeState.On);
printer.Append("Expanded - 23 COLUMNS");
printer.ExpandedMode(PrinterModeState.On);
printer.Append("1...5...10...15...20..23");
printer.ExpandedMode(PrinterModeState.Off);
printer.Separator();
printer.CondensedMode(PrinterModeState.On);
printer.Append("Condensed - 64 COLUMNS");
printer.Append("1...5...10...15...20...25...30...35...40...45...50...55...60..64");
printer.CondensedMode(PrinterModeState.Off);
printer.Separator();
printer.DoubleWidth2();
printer.Append("Font Width 2");
printer.DoubleWidth3();
printer.Append("Font Width 3");
printer.NormalWidth();
printer.Append("Normal width");
printer.Separator();
printer.AlignRight();
printer.Append("Right aligned text");
printer.AlignCenter();
printer.Append("Center-aligned text");
printer.AlignLeft();
printer.Append("Left aligned text");
printer.Separator();
printer.Font("Font A", Fonts.FontA);
printer.Font("Font B", Fonts.FontB);
printer.Font("Font C", Fonts.FontC);
printer.Font("Font D", Fonts.FontD);
printer.Font("Font E", Fonts.FontE);
printer.Font("Font Special A", Fonts.SpecialFontA);
printer.Font("Font Special B", Fonts.SpecialFontB);
printer.Separator();
printer.InitializePrint();
printer.SetLineHeight(30);
printer.Append("This is first line with line height of 30 dots");
printer.SetLineHeight(24);
printer.Append("This is second line with line height of 24 dots");
printer.SetLineHeight(40);
printer.Append("This is third line with line height of 40 dots");
printer.NewLines(3);
printer.Append("End of Test :)");
printer.Separator();
printer.FullPaperCut();
printer.PrintDocument();
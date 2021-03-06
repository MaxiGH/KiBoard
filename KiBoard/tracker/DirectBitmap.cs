﻿/*
 * copied from https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public class DirectBitmap : IDisposable
{
    public Bitmap Bitmap { get; private set; }
    public Int32[] Bits { get; private set; }
    public bool Disposed { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }

    protected GCHandle BitsHandle { get; private set; }

    public DirectBitmap(int width, int height)
    {
        Width = width;
        Height = height;
        Bits = new Int32[width * height];
        BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
        Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
    }

    public void Dispose()
    {
        if (Disposed) return;
        Disposed = true;
        Bitmap.Dispose();
        BitsHandle.Free();
    }

    public void SetPixel(int x, int y, System.Drawing.Color color)
    {
        if ((y * Width + x) > (512 * 424))
            throw new Exception("=" + y*Width+x);
        if ((y * Width + x) < 0)
            throw new Exception("y=" + y + "w=" + Width + "x=" + x);

        Bits[y * Width + x] = color.ToArgb();
    }
}
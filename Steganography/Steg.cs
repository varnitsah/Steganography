using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steganography
{
    class Steg
    {
        public Bitmap hideText(String text, Bitmap bmp)
        {
            // 1 while filling zeros
            int flag = 0;

            int charIndex = 0;
            int charValue = 0;

            long pixelElementIndex = 0;
            int zeros = 0;
            int R = 0, G = 0, B = 0;

           
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    R = pixel.R - pixel.R % 2;
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;
                    for (int n = 0; n < 3; n++)
                    {
                        if (pixelElementIndex % 8 == 0)
                        { 
                            if (flag == 1 && zeros == 8)
                            {
                                if ((pixelElementIndex - 1) % 3 < 2)
                                {
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }
                                return bmp;
                            }

                            if (charIndex >= text.Length)
                            {

                                flag = 1;
                            }
                            else
                            {
                                // move to the next character and process again
                                charValue = text[charIndex++];
                            }
                        }
                        switch (pixelElementIndex % 3)
                        {
                            case 0:
                                {
                                    if (flag == 0)
                                    {
                                        R += charValue % 2;
                                        charValue /= 2;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    if (flag==0)
                                    {
                                        G += charValue % 2;
                                        charValue /= 2;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (flag == 0)
                                    {
                                        B += charValue % 2;
                                        charValue /= 2;
                                    }
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }
                                break;
                        }
                        pixelElementIndex++;
                        if (flag == 1)
                        {
                            zeros++;
                        }
                    }
                }
            }

            return bmp;
        }

        public String extractText(Bitmap bmp)
        {
            int colorUnitIndex = 0;
            int charValue = 0;
            string extractedText = String.Empty;
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    for (int n = 0; n < 3; n++)
                    {
                        switch (colorUnitIndex % 3)
                        {
                            case 0:
                                charValue = charValue * 2 + pixel.R % 2;
                                break;
                            case 1:
                                charValue = charValue * 2 + pixel.G % 2;
                                break;
                            case 2:
                                charValue = charValue * 2 + pixel.B % 2;
                                break;
                        }
                        colorUnitIndex++;
                        if (colorUnitIndex % 8 == 0)
                        {
                            charValue = reverseBits(charValue);
                            if (charValue == 0) { 
                                return extractedText;
                            }
                            char c = (char)charValue;
                            extractedText += c.ToString();
                        }
                    }
                }
            }

            return extractedText;
        }

        public static int reverseBits(int n)
        {
            int result = 0;
            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;
                n /= 2;
            }
            return result;
        }
    }
}


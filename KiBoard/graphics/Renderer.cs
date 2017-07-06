﻿using System;
using System.Numerics;
using System.Drawing;
using KiBoard.math;

namespace KiBoard.graphics
{
    public class Renderer
    {
        private RenderStack stack;
        private FrameBuffer frame;
        private Matrix3x3 transform;
        private Color clearColor;
        private int renderedPos;

        public Renderer(FrameBuffer frame)
        {
            stack = new RenderStack();
            renderedPos = 0;
            clearColor = Color.Black;
            transform = Matrix3x3.identity();
            this.frame = frame;

            Line line = new Line();
            line.nextPoint(new Vector2(0, 0));
            line.nextPoint(new Vector2(1, 1));
            line.color = Color.FromArgb(255, 0, 0);
            stack.push(line);
            stack.push(new Segment(new Vector2(0, 0), new Vector2(1, 1)));
        }

        public FrameBuffer Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        public RenderStack Stack
        {
            get { return stack; }
            set { stack = value; }
        }

        public Matrix3x3 Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        private Matrix3x3 renderTransform()
        {
            // turn upside down
            return transform.translate(new Vector2(0, -1)).scale(new Vector2(frame.Size.X, -frame.Size.Y));
        }

        Color ClearColor
        {
            get { return clearColor; }
            set { clearColor = value; }
        }

        public void clear()
        {
            frame.graphics().Clear(clearColor);
        }

        public void render()
        {
            Matrix3x3 mat = renderTransform();
            foreach (Drawable i in stack)
            {
                i.draw(frame.graphics(), mat);
            }

            renderedPos = stack.Count;
            frame.graphics().Dispose(); // ?
        }

        public void renderNew()
        {
            Matrix3x3 mat = renderTransform();
            for(int i = renderedPos; i < stack.Count; i++)
            {
                stack[i].draw(frame.graphics(), mat);
                renderedPos = i;
            }

            frame.graphics().Dispose(); // ?
        }

        public void renderToFile(String filepath)
        {
            Bitmap bitmap = new Bitmap((int)frame.Size.X, (int)frame.Size.Y);
            Graphics gfx = Graphics.FromImage(bitmap);

            Matrix3x3 mat = renderTransform();
            foreach (Drawable i in stack)
            {
                i.draw(gfx, mat);
            }

            gfx.Dispose(); // ?

            bitmap.Save(filepath);
        }
    }
}

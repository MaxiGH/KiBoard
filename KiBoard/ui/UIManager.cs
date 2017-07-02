﻿using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace KiBoard.ui
{
    class UIManager
    {
        private List<UIElement> elements;
        private Size windowSize;
        private Graphics gfx;

        public UIManager(UIConfiguration conf, Size winSize, Graphics g)
        {
            elements = conf.createConfiguration();
            windowSize = winSize;
            gfx = g;
        }

        public void render()
        {
            foreach (UIElement element in elements)
            {
                element.render(gfx, windowSize);
            }
        }

        public bool isTouchingElement(Vector2 vec)
        {
            foreach (UIElement element in elements)
            {
                if (element.touches(vec))
                {
                    return true;
                }
            }
            return false;
        }

        public void hideHoveredElements(Vector2 vec)
        {
            foreach (UIElement element in elements)
            {
                element.setVisible(!element.touches(vec));
            }
        }

        public void showAllElements()
        {
            foreach (UIElement element in elements)
            {
                element.setVisible(true);
            }
        }
    }
}

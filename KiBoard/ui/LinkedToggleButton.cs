﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KiBoard.ui
{
    class LinkedToggleButton : ToggleButton
    {
        public List<LinkedToggleButton> Links { get; private set; }

        public LinkedToggleButton(string name, Vector2 pos, Vector2 s, Image btmUc, Image btmC, bool isClicked = false) : base(name, pos, s, btmUc, btmC, isClicked)
        {
        }

        public static void link(List<LinkedToggleButton> btns)
        {
            foreach(LinkedToggleButton btn in btns)
            {
                btn.Links = btns;
            }
        }

        public override void onClick()
        {
            if (!isClicked) { 
                if (Links != null) { 
                    foreach(LinkedToggleButton btn in Links)
                    {
                        btn.IsClicked = false;
                    }
                }
                isClicked = true;
            }
        }
    }
}
